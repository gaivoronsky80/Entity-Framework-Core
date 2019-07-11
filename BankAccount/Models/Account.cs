using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccount.Models
{
    public class Account
    {
        public string FirstName{get; set;}
        public int Balance{get; set;}
        public List<Transaction> TransactionsOfUser{get; set;}
        public Transaction Transaction{get; set;}
    }
}