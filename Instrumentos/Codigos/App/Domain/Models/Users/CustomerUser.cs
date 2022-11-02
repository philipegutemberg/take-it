using System;
using Domain.Enums;

namespace Domain.Models.Users
{
    public record CustomerUser : GenericUser
    {
        public CustomerUser(string username, string password, string fullName, string email, string phone, string walletAddress)
            : base(username, password, EnumUserRole.Customer)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            WalletAddress = walletAddress;
            InternalAddress = string.Empty;
        }

        public CustomerUser(int id, string code, string username, string password, string fullName, string email, string phone, string walletAddress, string? internalAddress)
            : base(id, code, username, password, EnumUserRole.Customer)
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

        public void AssignInternalAddress(string internalAddress)
        {
            if (!string.IsNullOrEmpty(InternalAddress))
                throw new Exception("Internal address already assigned.");

            InternalAddress = internalAddress;
        }
    }
}