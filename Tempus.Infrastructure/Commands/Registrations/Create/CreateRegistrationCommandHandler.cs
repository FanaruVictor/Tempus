using System.Text.RegularExpressions;
using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;
using Tempus.Infrastructure.SignalR.Abstractization;

namespace Tempus.Infrastructure.Commands.Registrations.Create;

public class
    CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, BaseResponse<RegistrationOverview>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IClientEventSender _clientEventSender;

    public CreateRegistrationCommandHandler(IRegistrationRepository registrationRepository,
        ICategoryRepository categoryRepository, ICloudinaryService cloudinaryService,
        IClientEventSender clientEventSender)
    {
        _registrationRepository = registrationRepository;
        _categoryRepository = categoryRepository;
        _cloudinaryService = cloudinaryService;
        _clientEventSender = clientEventSender;
    }

    public async Task<BaseResponse<RegistrationOverview>> Handle(CreateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var category = await _categoryRepository.GetById(request.CategoryId);
            if (category == null)
            {
                return BaseResponse<RegistrationOverview>.BadRequest(new List<string>
                    { $"Category with Id: {request.CategoryId} not found" });
            }

            var entity = new Registration
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow.Date,
                LastUpdatedAt = DateTime.UtcNow.Date,
                CategoryId = request.CategoryId,
            };

            var images = ExtractImages(request.Content);

            var cloudinaryImages = await _cloudinaryService.UploadRegistrationImages(images);

            if (cloudinaryImages.Length > 0)
            {
                for (var i = 0; i < images.Count; i++)
                {
                    var image = images[i].Value;
                    var style = ExtractStyle(images[i].Value);
                    entity.Content = entity.Content?.Replace(image, CreateImage(cloudinaryImages[i], style));
                }
            }

            await _registrationRepository.Add(entity);
            await _registrationRepository.SaveChanges();


            var registrationOverview = GenericMapper<Registration, RegistrationOverview>.Map(entity);
            registrationOverview.CategoryColor = _categoryRepository.GetCategoryColor(entity.CategoryId);

            await SendEvent(registrationOverview, category);


            var result = BaseResponse<RegistrationOverview>.Ok(registrationOverview);

            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<RegistrationOverview>.BadRequest(new List<string> { exception.Message });
            return result;
        }
    }

    private MatchCollection ExtractImages(string content)
    {
        var regex = new Regex(@"<img.*?src=""(.*?)"".*?>");
        return regex.Matches(content);
    }

    private string CreateImage(string image, string style)
    {
        return $"<img src=\"{image}\" {style}/>";
    }

    private string ExtractStyle(string image)
    {
        string pattern =
            @"<img\s+[^>]*?style\s*=\s*""(?<style>[^""]*)""\s*[^>]*?width\s*=\s*""(?<width>[^""]*)""[^>]*?>";

        Regex regex = new Regex(pattern);

        Match match = regex.Match(image);

        if (match.Success)
        {
            // Extract the style and width attributes
            string style = match.Groups["style"].Value;
            string width = match.Groups["width"].Value;

            return $"style=\"{style}\" width=\"{width}\"";
        }

        return "";
    }

    private async Task SendEvent(RegistrationOverview registration, Category category)
    {
        var groupCategories = category.GroupCategories;

        foreach (var groupCategory in groupCategories)
        {
            var groupUsers = groupCategory.Group?.GroupUsers;

            if (groupUsers == null || groupUsers.Count == 0)
                continue;

            foreach (var groupUser in groupUsers)
            {
                await _clientEventSender.SendRegistrationCreatedEventAsync(registration, groupUser.UserId.ToString());
            }
        }
    }
}