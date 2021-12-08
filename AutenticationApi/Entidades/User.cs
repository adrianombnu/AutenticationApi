namespace AutenticationApi.Entidades
{
    public class User : Base
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
