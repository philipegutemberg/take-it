using Domain.Enums;

namespace Domain.Models.Users
{
    public record Gatekeeper : BackOffice
    {
        public Gatekeeper(string username, string password)
            : base(username, password)
        {
        }

        public Gatekeeper(int id, string code, string username, string password)
            : base(id, code, username, password)
        {
        }

        public override EnumUserRole Role => EnumUserRole.Gatekeeper;
    }
}