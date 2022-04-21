namespace ProjectTestAPI_1.Models
{
    public class User
    {
        public User(ulong id,string name,string email,string password,string phone, string token)
        {
            UserId = id;
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Token = token;
        }
        public ulong UserId { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Token { get; set; }
        
        
        
        
    }
}