using System;
namespace BankAccount.Models
{
    public class Action : BaseEntity{
        public int ActionId { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}