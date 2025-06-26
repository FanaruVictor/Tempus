using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities.Group;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Group;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Services.Cloudynary;

namespace Tempus.Infrastructure.Commands.Groups.Create;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, BaseResponse<GroupOverview>>
{
	private readonly IGroupRepository _groupRepository;
	private readonly ICloudinaryService _cloudinaryService;
	private readonly IGroupUserRepository _groupUserRepository;
	private readonly IGroupPhotoRepository _groupPhotoRepository;

	public CreateGroupCommandHandler (IGroupRepository groupRepository, ICloudinaryService cloudinaryService,
		 IGroupUserRepository groupUserRepository, IGroupPhotoRepository groupPhotoRepository)
	{
		_groupRepository = groupRepository;
		_cloudinaryService = cloudinaryService;
		_groupUserRepository = groupUserRepository;
		_groupPhotoRepository = groupPhotoRepository;
	}

	public async Task<BaseResponse<GroupOverview>> Handle (CreateGroupCommand request, CancellationToken cancellationToken)
	{
		try
		{
			cancellationToken.ThrowIfCancellationRequested();

			var group = new Group
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				OwnerId = request.UserId,
				CreatedAt = DateTime.UtcNow
			};

			await _groupRepository.Add(group);

			await AddImage(request, group.Id);

			await AddGroupUser(request, group.Id);

			await _groupRepository.SaveChanges();

			var groupOverview = GenericMapper<Group, GroupOverview>.Map(group);

			return BaseResponse<GroupOverview>.Ok(groupOverview);
		}
		catch (Exception exception)
		{
			return BaseResponse<GroupOverview>.BadRequest(new List<string>
			{
				exception.Message
			});
		}
	}

	private async Task AddGroupUser (CreateGroupCommand request, Guid groupId)
	{
		var groupUsers = new List<GroupUser>
		{
			new GroupUser
			{
				GroupId = groupId,
				UserId = request.UserId
			}
		};

		var members = request.Members
			.Replace("\"", "")
			.Split(',')
			.Select(x => x.Replace("\"", ""));

		members = members.Where(x => x.ToLower() != request.UserId.ToString().ToLower());

		foreach (var member in members)
		{
			groupUsers.Add(new GroupUser
			{
				GroupId = groupId,
				UserId = new Guid(member)
			});
		}

		await _groupUserRepository.AddRange(groupUsers);
	}

	private async Task AddImage (CreateGroupCommand request, Guid groupId)
	{
		if (request.Image == null)
		{
			return;
		}

		var uploadResult = await _cloudinaryService.Upload(request.Image);

		var groupPhoto = new GroupPhoto
		{
			Id = Guid.NewGuid(),
			GroupId = groupId,
			PublicId = uploadResult.PublicId,
			Url = uploadResult.Url.ToString(),
		};

		await _groupPhotoRepository.Add(groupPhoto);
	}
}