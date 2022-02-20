namespace qsLog.Presentetion.Models
{
    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}