using System;
using Domain.Enums;

namespace Domain.Models.Users
{
    public abstract record User
    {
        public User(string username, string password)
        {
            Id = Guid.NewGuid().ToString();
            Username = username;
            Password = password;
        }

        public User() { }

        public string Id { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public abstract EnumUserRole Role { get; }
    }
}