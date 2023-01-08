using MediatR;
using Tempus.Core.Commons;

namespace Tempus.Infrastructure.Commands.Categories.Delete;

public class DeleteCategoryCommand : IRequest<BaseResponse<Guid>>
{
    public Guid Id { get; init; }
}