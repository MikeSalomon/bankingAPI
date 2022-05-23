using BankingSystem.API.Entities;
using System;
using System.Collections.Generic;

namespace BankingSystem.API.Services
{
    public interface IBankRepository
    {
        User GetUserById(Guid userId);
        void CreateUser(User account);
        IEnumerable<Account> GetAllUserAccounts(Guid userId);
        Account GetAccount(int accountNumber);
        Account GetAccountById(Guid accountId);
        //void CreateAccount(Guid userId, Account account);
        void CreateAccount(Account account);
        void DeleteAccount(Account account);
        void DepositFunds(int accountNumber, decimal amount);
        void WithdrawFunds(int accountNumber, decimal amount);
        bool UserExists(string email); 
        bool SaveChanges();
    }
}
