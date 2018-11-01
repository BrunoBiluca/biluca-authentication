using System;
using Authentication.Domain.Entities.Interfaces;

namespace Authentication.Domain.Entities
{
    public class User : IEntityDate
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string EncryptedPassword { get; set; }
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}