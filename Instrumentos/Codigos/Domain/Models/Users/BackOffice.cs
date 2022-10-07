using Domain.Enums;

namespace Domain.Models.Users
{
    public record BackOffice : User
    {
        public BackOffice(string username, string password) 
            : base(username, password)
        {
        }
        
        public override EnumUserRole Role => EnumUserRole.Backoffice;
    }
}