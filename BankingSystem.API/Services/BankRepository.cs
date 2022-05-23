using BankingSystem.API.DbContexts;
using BankingSystem.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingSystem.API.Services
{
    public class BankRepository : IBankRepository
    {
        private readonly BankContext _context;

        public BankRepository(BankContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void CreateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.Id = Guid.NewGuid();
            user.CreateDate = DateTime.Now;
            _context.Users.Add(user);
        }

        public User GetUserById(Guid userId)
        {
            return _context.Users.Find(userId);
        }

        public void CreateAccount(Account account)
        {
            var user = _context.Users.Find(account.UserId);
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (account == null)
                throw new ArgumentNullException(nameof(account));

            _context.Accounts.Add(account);
        }

        public void DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
        }

        public void DepositFunds(int accountNumber, decimal amount)
        {
            var account = _context.Accounts.Where(x => x.AccountNumber == accountNumber).SingleOrDefault();
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            account.AccountBalance += amount;

            _context.Update(account);
        }

        public Account GetAccountById(Guid accountId)
        {
            return _context.Accounts.Find(accountId);
        }

        public void WithdrawFunds(int accountNumber, decimal amount)
        {
            var account = _context.Accounts.Where(x => x.AccountNumber == accountNumber).SingleOrDefault();
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            account.AccountBalance -= amount;

            _context.Update(account);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Account> GetAllUserAccounts(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentNullException(nameof(userId));

            return _context.Accounts.Where(x => x.UserId == userId).ToList();
        }

        public Account GetAccount(int accountNumber)
        {
            return _context.Accounts.Where(x => x.AccountNumber == accountNumber).FirstOrDefault(); 
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }
    }
}
