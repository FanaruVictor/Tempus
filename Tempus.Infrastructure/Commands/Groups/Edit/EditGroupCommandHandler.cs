using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.Group;

namespace Tempus.Infrastructure.Commands.Groups.Edit;

public class EditGroupCommandHandler : IRequestHandler<EditGroupCommand, BaseResponse<GroupOverview>>
{
    private readonly IGroupRepository _groupRepository;

    public EditGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    
    public async Task<BaseResponse<GroupOverview>> Handle(EditGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetById(request.Id);

        return null;
    }
}