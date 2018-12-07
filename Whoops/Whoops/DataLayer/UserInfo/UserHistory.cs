using System;
using System.ComponentModel.DataAnnotations;

namespace Whoops.DataLayer.UserInfo
{
    public class UserHistory
    {
        public string UserId { get; set; }
        [Key]
        public int HistoryId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ProcedureName { get; set; }
        public double AmountPayed { get; set; }
        public FeedBack CustomerFeedback { get; set; }
        public string OperatedBy { get; set; }
        public FeedBack OperationistFeedback { get; set; }
    }
}