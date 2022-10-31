using System;
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
            InternalAddress = string.Empty;
        }

        public Customer(int id, string code, string username, string password, string fullName, string email, string phone, string walletAddress, string? internalAddress)
            : base(id, code, username, password)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            WalletAddress = walletAddress;
            InternalAddress = internalAddress ?? string.Empty;
        }

        public string FullName { get; }
        public string Email { get; }
        public string Phone { get; }
        public string WalletAddress { get; }
        public string InternalAddress { get; set; }

        public override EnumUserRole Role => EnumUserRole.Customer;

        public void AssignInternalAddress(string internalAddress)
        {
            if (!string.IsNullOrEmpty(InternalAddress))
                throw new Exception("Internal address already assigned.");

            InternalAddress = internalAddress;
        }
    }
}