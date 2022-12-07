using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Categories.Delete;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse<Guid>>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<BaseResponse<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var deletedCategoryId = await _categoryRepository.Delete(request.Id);

            var result = BaseResponse<Guid>.Ok(deletedCategoryId);
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<Guid>.BadRequest(exception.Message);
            return result;
        }
    }
}