using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace simpleBlog.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }

        
        private const int WorkFactor = 13;
        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }
        public static void FakeHash()
        {
           BCrypt.Net.BCrypt.HashPassword("", WorkFactor);
        }


        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }


    }


    //how hibenage mapping with database there is lot of ways
    //1) old way using XML
    //2) fluwernt way hibenate
    //3) auto mapper
    //4) code way maping (this is we used here)
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.UserName, x => x.NotNullable(true));
            Property(x => x.Email, x => x.NotNullable(true));
            Property(x => x.PasswordHash, x =>
            {
                x.Column("password_hash");
                x.NotNullable(true);
            });
        }
    }
}