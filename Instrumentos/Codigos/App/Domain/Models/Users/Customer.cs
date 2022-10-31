using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Models.Users
{
    public record Customer : User
    {
        public Customer(string username, string password, string fullName, string email, string phone, string walletAddress)
            : base(username, password)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            WalletAddress = walletAddress;
        }

        public Customer(int id, string code, string username, string password, string fullName, string email, string phone, string walletAddress)
            : base(id, code, username, password)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            WalletAddress = walletAddress;
        }

        public string FullName { get; }
        public string Email { get; }
        public string Phone { get; }
        public string WalletAddress { get; }

        public override EnumUserRole Role => EnumUserRole.Customer;
    }
}