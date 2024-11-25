using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SportsAppAPI.Infrastructure
{
    public class WebSocketHandler
    {
        private readonly ConcurrentDictionary<string, WebSocket> _connectedClients = new ConcurrentDictionary<string, WebSocket>();

        // Handle new WebSocket connections
        public async Task HandleWebSocketAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var socket = await context.WebSockets.AcceptWebSocketAsync();
                var clientId = context.Connection.Id;

                _connectedClients.TryAdd(clientId, socket);
                Console.WriteLine($"Client connected: {clientId}");

                await ReceiveMessagesAsync(socket, clientId);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        // Receive messages from clients
        private async Task ReceiveMessagesAsync(WebSocket socket, string clientId)
        {
            var buffer = new byte[1024 * 4];
            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine($"Client disconnected: {clientId}");
                        _connectedClients.TryRemove(clientId, out _);
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
                    }
                    else
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Console.WriteLine($"Received message from {clientId}: {message}");
                        // Optional: Handle messages from clients if needed
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with client {clientId}: {ex.Message}");
                _connectedClients.TryRemove(clientId, out _);
            }
        }

        // Broadcast messages to all connected WebSocket clients
        public async Task BroadcastMessageAsync(string message)
        {
            var byteArray = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(byteArray);

            foreach (var (clientId, socket) in _connectedClients)
            {
                if (socket.State == WebSocketState.Open)
                {
                    try
                    {
                        await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send message to {clientId}: {ex.Message}");
                        _connectedClients.TryRemove(clientId, out _);
                    }
                }
            }
        }
    }
}
