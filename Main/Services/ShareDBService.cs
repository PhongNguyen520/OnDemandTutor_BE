using BusinessObjects.Models;
using System.Collections.Concurrent;

namespace API.Services
{
    public class ShareDBService
    {
        private readonly ConcurrentDictionary<string, UserChatVM> _connection = new();

        public ConcurrentDictionary<string, UserChatVM> connection => _connection;
    }
}
