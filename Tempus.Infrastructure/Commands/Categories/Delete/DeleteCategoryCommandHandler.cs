using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;

namespace Tempus.Infrastructure.Commands.Categories.Delete;

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

            var deletedCategoryId = request.Id;

            await _categoryRepository.Delete(deletedCategoryId);
            await _categoryRepository.SaveChanges();

            BaseResponse<Guid> result;

            if(deletedCategoryId == Guid.Empty)
            {
                result = BaseResponse<Guid>.NotFound($"Category with Id: {request.Id} not found");
                return result;
            }

            result = BaseResponse<Guid>.Ok(deletedCategoryId);
            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<Guid>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }
}