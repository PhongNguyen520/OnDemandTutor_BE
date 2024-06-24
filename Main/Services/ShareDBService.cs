using BusinessObjects.Models;
using System.Collections.Concurrent;

namespace API.Services
{
    public class ShareDBService
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connection = new();

        public ConcurrentDictionary<string, UserConnection> connection => _connection;
    }
}
