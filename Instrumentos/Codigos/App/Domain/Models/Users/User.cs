using System;
using Domain.Enums;

namespace Domain.Models.Users
{
    public abstract record User
    {
        protected User(string username, string password)
        {
            Id = -1;
            Code = Guid.NewGuid().ToString();
            Username = username;
            Password = password;
        }

        protected User(int id, string code, string username, string password)
            : this(username, password)
        {
            Id = id;
            Code = code;
        }

        public int Id { get; }
        public string Code { get; }
        public string Username { get; }
        public string Password { get; }
        public abstract EnumUserRole Role { get; }
    }
}