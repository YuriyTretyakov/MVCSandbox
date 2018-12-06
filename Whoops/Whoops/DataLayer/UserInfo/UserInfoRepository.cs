using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whoops.DataLayer.UserInfo
{
    public class UserInfoRepository
    {
        private readonly UserInfoContext _context;
        private readonly ILogger<UserInfoRepository> _logger;

        public UserInfoRepository(UserInfoContext context, ILogger<UserInfoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddUserHistory(UserHistory history)
        {
            _context.UserHistory.Add(history);

        }

        public List<UserHistory> GetUserHistory(string userId)
        {
            return _context.UserHistory.Where(h => h.UserId == userId).ToList();
        }

        public void UpdateCustomerFeedback(FeedBack custFeedback, string userId,int historyId)
        {
           var concreteHistoryItem= _context.UserHistory.FirstOrDefault(h => h.HistoryId == historyId && h.UserId == userId);
            concreteHistoryItem.CustomerFeedback = custFeedback;
        }

        public void UpdateOperationistFeedback(FeedBack opsFeedback, string userId, int historyId)
        {
            var concreteHistoryItem = _context.UserHistory.FirstOrDefault(h => h.HistoryId == historyId && h.UserId == userId);
            concreteHistoryItem.OperationistFeedback = opsFeedback;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return ((await _context.SaveChangesAsync()) > 0);
        }
    }
}
