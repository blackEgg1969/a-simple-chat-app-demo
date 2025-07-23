using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatWebAPI.Models;

namespace chatWebAPI.DataService
{
    public class SharedDb
    {
        // This class serves as a shared database for user connections in the chat application.
        // It pairs user IDs with their connection information, allowing the application to manage
        // It uses a ConcurrentDictionary to store UserConnection objects, which allows for thread-safe operations
        // when multiple users connect or disconnect simultaneously.
        private readonly ConcurrentDictionary<string, UserConnection> _userConnections = new();

        public ConcurrentDictionary<string, UserConnection> UserConnections => _userConnections;
    }
}