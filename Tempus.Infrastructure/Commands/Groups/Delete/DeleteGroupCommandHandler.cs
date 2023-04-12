using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;

namespace Tempus.Infrastructure.Commands.Groups.Delete;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, BaseResponse<Guid>>
{
    private readonly IGroupRepository _groupRepository;

    public DeleteGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<BaseResponse<Guid>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var group = await _groupRepository.GetById(request.Id);

            if (group == null)
            {
                return BaseResponse<Guid>.NotFound($"Group with id: {request.Id} not found");
            }

            await _groupRepository.Delete(group.Id);
            await _groupRepository.SaveChanges();

            return BaseResponse<Guid>.Ok(group.Id);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}