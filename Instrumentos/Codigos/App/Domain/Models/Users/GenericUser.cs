using System;
using Domain.Enums;

namespace Domain.Models.Users
{
    public record GenericUser
    {
        protected GenericUser(string username, string password, EnumUserRole role)
        {
            Id = -1;
            Code = Guid.NewGuid().ToString();
            Username = username;
            Password = password;
            Role = role;
        }

        public GenericUser(int id, string code, string username, string password, EnumUserRole role)
            : this(username, password, role)
        {
            Id = id;
            Code = code;
        }

        public int Id { get; }
        public string Code { get; }
        public string Username { get; }
        public string Password { get; }
        public EnumUserRole Role { get; }
    }
}