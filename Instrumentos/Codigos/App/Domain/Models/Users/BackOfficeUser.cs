using Domain.Enums;

namespace Domain.Models.Users
{
    public record BackOfficeUser : GenericUser
    {
        public BackOfficeUser(string username, string password)
            : base(username, password, EnumUserRole.Backoffice)
        {
        }

        public BackOfficeUser(int id, string code, string username, string password)
            : base(id, code, username, password, EnumUserRole.Backoffice)
        {
        }
    }
}