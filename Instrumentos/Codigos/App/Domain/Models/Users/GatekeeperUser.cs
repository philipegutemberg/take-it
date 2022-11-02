using Domain.Enums;

namespace Domain.Models.Users
{
    public record GatekeeperUser : GenericUser
    {
        public GatekeeperUser(string username, string password)
            : base(username, password, EnumUserRole.Gatekeeper)
        {
        }

        public GatekeeperUser(int id, string code, string username, string password)
            : base(id, code, username, password, EnumUserRole.Gatekeeper)
        {
        }
    }
}