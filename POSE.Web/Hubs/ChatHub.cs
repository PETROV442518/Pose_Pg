namespace PROJECT_POSE.Hubs
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using POSE.Domain;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ChatHub" />
    /// </summary>
    [Authorize]
    public class ChatHub : Hub
    {
        /// <summary>
        /// The Send
        /// </summary>
        /// <param name="message">The message<see cref="string"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync(
                "NewMessage",
                new Message { Name = this.Context.User.Identity.Name, Text = message, });
        }
    }
}
