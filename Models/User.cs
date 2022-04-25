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
        
        public string FuelType { get; set; }

        public ulong FuelSize { get; set; }
        
        
    }
    public class UserSettings
    {
        public UserSettings(string name,string email, string fuelType, string fuelSize)
        {
            Name = name;
            Email = email;
            FuelType = fuelType;
            FuelSize = fuelSize;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FuelType { get; set; }
        public string FuelSize { get; set; }
    }
}