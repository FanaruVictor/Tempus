using System.Text.RegularExpressions;
using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;
using Tempus.Infrastructure.SignalR.Abstractization;

namespace Tempus.Infrastructure.Commands.Registrations.Update;

public class
    UpdateRegistrationCommandHandler : IRequestHandler<UpdateRegistrationCommand, BaseResponse<RegistrationDetails>>
{
    private readonly IRegistrationRepository _registrationRepository;
    private ICloudinaryService _cloudinaryService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IClientEventSender _clientEventSender;

    public UpdateRegistrationCommandHandler(IRegistrationRepository registrationRepository,
        ICloudinaryService cloudinaryService, ICategoryRepository categoryRepository,
        IClientEventSender clientEventSender)
    {
        _registrationRepository = registrationRepository;
        _cloudinaryService = cloudinaryService;
        _categoryRepository = categoryRepository;
        _clientEventSender = clientEventSender;
    }

    public async Task<BaseResponse<RegistrationDetails>> Handle(UpdateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = await _registrationRepository.GetById(request.Id);

            if (entity == null)
            {
                return BaseResponse<RegistrationDetails>.NotFound($"Registration with Id: {request.Id} was not found");
            }


            entity = new Registration
            {
                Id = entity.Id,
                Description = request.Description,
                Content = request.Content,
                CreatedAt = entity.CreatedAt,
                LastUpdatedAt = DateTime.UtcNow.Date,
                CategoryId = entity.CategoryId
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

            _registrationRepository.Update(entity);
            await _registrationRepository.SaveChanges();

            await SendClientEvent(entity, request);

            var detailedRegistration = GenericMapper<Registration, RegistrationDetails>.Map(entity);
            var result = BaseResponse<RegistrationDetails>.Ok(detailedRegistration);
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<RegistrationDetails>.BadRequest(new List<string> { exception.Message });
            return result;
        }
    }

    private MatchCollection ExtractImages(string content)
    {
        var regex = new Regex(@"<img\s+[^>]*?src\s*=\s*""data:image\/\w+;base64,[^""]+""[^>]*?>");
        return regex.Matches(content);
    }

    private string CreateImage(string image, string style)
    {
        return $"<img src=\"{image}\" {style}/>";
    }

    private string ExtractStyle(string image)
    {
        string pattern =
            @"(?=.*style\s*=\s*""(?<style>[^""]*)"")(?=.*width\s*=\s*""(?<width>[^""]*)"")";

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

    private async Task SendClientEvent(Registration registration, UpdateRegistrationCommand request)
    {
        var category = await _categoryRepository.GetById(registration.CategoryId);

        var groupCategories = category.GroupCategories;

        foreach (var groupCategory in groupCategories)
        {
            var groupUsers = groupCategory.Group?.GroupUsers;

            if (groupUsers == null || groupUsers.Count == 0)
                continue;
            
            groupUsers = groupUsers.Where(x => x.UserId != request.UserId).ToList();
            foreach (var groupUser in groupUsers)
            {
                if (groupUser.UserId != request.UserId)
                    await _clientEventSender.SendRegistrationUpdated(registration.Id, groupUser.GroupId,
                        groupUser.UserId.ToString());
            }
        }
    }
}