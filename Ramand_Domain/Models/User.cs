﻿namespace Ramand_Domain.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;    
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
