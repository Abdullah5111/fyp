using Microsoft.AspNetCore.Mvc;
using Notifications.Models;
using Notifications.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace Notifications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> Get()
        {
            var notifications = await this._notificationRepository.GetAllNotifications();
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> Get(int id)
        {
            var notification = await this._notificationRepository.GetNotificationById(id);
            if (notification == null)
                return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public async Task<ActionResult<Notification>> Post([FromBody] Notification notification)
        {
            var createdNotification = await this._notificationRepository.AddNotification(notification);
            return CreatedAtAction(nameof(Get), new { id = createdNotification.Id }, createdNotification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Notification notification)
        {
            if (id != notification.Id)
                return BadRequest();

            var updatedNotification = await this._notificationRepository.UpdateNotification(notification);
            if (updatedNotification == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await this._notificationRepository.GetNotificationById(id);
            if (notification == null)
                return NotFound();

            await this._notificationRepository.DeleteNotification(id);

            return NoContent();
        }
    }
}
