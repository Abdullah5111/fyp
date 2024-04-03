using static System.Net.Mime.MediaTypeNames;
using Notifications.Models;

namespace Notifications.Repository
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task<Notification> GetNotificationById(int id);
        Task<Notification> AddNotification(Notification notification);
        Task<Notification> UpdateNotification(Notification notification);
        Task DeleteNotification(int id);
    }
}
