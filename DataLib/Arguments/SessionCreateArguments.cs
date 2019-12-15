namespace DemoBlog.DataLib.Arguments
{
    public class SessionCreateArguments
    {
        public bool Restore { get; set; }
        public string RestoreKey { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
