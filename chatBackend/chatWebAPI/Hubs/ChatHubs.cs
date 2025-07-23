using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatWebAPI.DataService;
using chatWebAPI.Models;
using Microsoft.AspNetCore.SignalR;

namespace chatWebAPI.Hubs
{
    public class ChatHubs : Hub
    {
        private readonly ILogger<ChatHubs> _logger;

        private readonly SharedDb _sharedDb;

        public ChatHubs(SharedDb sharedDb)
        {
            _sharedDb = sharedDb;
            _logger = new LoggerFactory().CreateLogger<ChatHubs>();
            _logger.LogInformation("ChatHubs initialized with SharedDb.");
        }

        public ChatHubs(ILogger<ChatHubs> logger, SharedDb sharedDb)
        {

            _sharedDb = sharedDb;
            _logger = logger;
            _logger.LogInformation("ChatHubs initialized with SharedDb & ILogger.");
        }

        public async Task JoinChat(UserConnection connection)
        {
            await Clients.All
                .SendAsync("receiveMessage", "admin", $"{connection.UserId} has joined the chat.");
            _logger.LogInformation($"{connection.UserId} has joined the chat.");
        }

        public async Task JoinSepcificChatRoom(UserConnection connection)
        {
            if (connection == null || string.IsNullOrWhiteSpace(connection.GroupName) || string.IsNullOrWhiteSpace(connection.UserId))
            {
                _logger.LogWarning("JoinSpecificChatRoom called with invalid UserConnection: {@UserConnection}", connection);
                await Clients.Caller.SendAsync("ReceiveMessage", "admin", "Invalid chat room or user name.");
                return;
            }
            try
            {
                _logger.LogInformation("This is JoinSpecificChatRoom method, {@UserConnection}",
            connection.UserId);
                await Groups.AddToGroupAsync(Context.ConnectionId, connection.GroupName);
                _sharedDb.UserConnections[Context.ConnectionId] = connection;
                await Clients.Group(connection.GroupName)
                    .SendAsync("ReceiveMessage", "admin", $"{connection.UserId} has joined the chat room {connection.GroupName}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in JoinSpecificChatRoom method");
                await Clients.Caller.SendAsync("ReceiveMessage", "admin", "Server error joining chat room.");
                throw;
            }
        }

        public async Task SendMessage(string msg)
        {
            if (_sharedDb.UserConnections.TryGetValue(Context.ConnectionId, out UserConnection connection))
            {
                await Clients.Group(connection.GroupName)
                    .SendAsync("ReceiveSpecificMessage", connection.UserId, msg);
            }
        }

        // New method to send a message to a specific user
        public async Task SendMessageToUser(string targetUserId, string message)
        {
            // Find the connectionId(s) for the target user
            var targetConnections = _sharedDb.UserConnections
                .Where(kvp => kvp.Value.UserId == targetUserId)
                .Select(kvp => kvp.Key)
                .ToList();

            if (!targetConnections.Any())
            {
                _logger.LogWarning("SendMessageToUser: No connection found for user {TargetUserId}", targetUserId);
                await Clients.Caller.SendAsync("ReceiveMessage", "admin", $"User {targetUserId} is not online.");
                return;
            }

            if (_sharedDb.UserConnections.TryGetValue(Context.ConnectionId, out UserConnection senderConnection))
            {
                foreach (var connectionId in targetConnections)
                {
                    await Clients.Client(connectionId)
                        .SendAsync("ReceiveDirectMessage", senderConnection.UserId, message);
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (_sharedDb.UserConnections.TryGetValue(Context.ConnectionId, out UserConnection connection))
            {
                // Remove the user connection from the shared database
                _sharedDb.UserConnections.Remove(Context.ConnectionId, out _);

                // Optionally notify the group that the user has left
                if (!string.IsNullOrWhiteSpace(connection.GroupName))
                {
                    await Clients.Group(connection.GroupName)
                        .SendAsync("ReceiveMessage", "admin", $"{connection.UserId} has left the chat room {connection.GroupName}.");
                }

                _logger.LogInformation("{UserId} disconnected from group {GroupName}", connection.UserId, connection.GroupName);
            }
            else
            {
                _logger.LogInformation("Connection {ConnectionId} disconnected, but no user info found.", Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}