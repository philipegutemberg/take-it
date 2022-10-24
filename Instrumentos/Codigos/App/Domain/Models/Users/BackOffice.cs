using Domain.Enums;

namespace Domain.Models.Users
{
    public record BackOffice : User
    {
        public BackOffice(string username, string password)
            : base(username, password)
        {
        }

        public BackOffice(int id, string code, string username, string password)
            : base(id, code, username, password)
        {
        }

        public override EnumUserRole Role => EnumUserRole.Backoffice;
    }
}