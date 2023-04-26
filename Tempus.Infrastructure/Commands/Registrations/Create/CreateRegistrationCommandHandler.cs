﻿using System.Text.RegularExpressions;
using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Registrations;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.Registrations.Create;

public class
    CreateRegistrationCommandHandler : IRequestHandler<CreateRegistrationCommand, BaseResponse<RegistrationDetails>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IRegistrationRepository _registrationRepository;

    public CreateRegistrationCommandHandler(IRegistrationRepository registrationRepository,
        ICategoryRepository categoryRepository, ICloudinaryService cloudinaryService)
    {
        _registrationRepository = registrationRepository;
        _categoryRepository = categoryRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<BaseResponse<RegistrationDetails>> Handle(CreateRegistrationCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var category = await _categoryRepository.GetById(request.CategoryId);
            if(category == null)
            {
                return BaseResponse<RegistrationDetails>.BadRequest(new List<string>
                    {$"Category with Id: {request.CategoryId} not found"});
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

            for(var i = 0; i < images.Count; i++)
            {
                var image = images[i].Value;
                entity.Content = entity.Content?.Replace(image, CreateImage(cloudinaryImages[i]));
            }

            await _registrationRepository.Add(entity);
            await _registrationRepository.SaveChanges();

            var detailedRegistration = GenericMapper<Registration, RegistrationDetails>.Map(entity);
            var result = BaseResponse<RegistrationDetails>.Ok(detailedRegistration);

            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<RegistrationDetails>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }

    private MatchCollection ExtractImages(string content)
    {
        var regex = new Regex(@"<img.*?src=""(.*?)"".*?>");
        return regex.Matches(content);
    }
    
    private string CreateImage(string image)
    {
        return $"<img src=\"{image}\" />";
    }
}