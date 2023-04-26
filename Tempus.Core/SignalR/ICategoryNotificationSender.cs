using Tempus.Core.Models.Category;

namespace Tempus.Core.SignalR;

public interface ICategoryNotificationSender
{
    Task SendAddCategoryNotification(BaseCategory category);
    Task SendEditCategoryNotification(string message);
    Task SendEditCategoryNotification(Guid categoryId);
}