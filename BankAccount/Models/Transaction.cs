using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId{get; set;}
        public float Amount{get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        public int UserId{get; set;}
        public User User{get; set;}
    }
}