namespace BookStore.Model.DataBase
{
    public class ConnectionSettings
    {
        public string Host { get; private set; }

        public string Port { get; private set; }

        public string User { get; private set; }

        public string Password { get; private set; }

        public string DefaultSchema { get; private set; }

        public string CharSet { get; private set; }

        public ConnectionSettings(string host, string port, string user, string password, string defaultSchema, string charSet)
        {
            Host = host;
            Port = port;
            User = user;
            Password = password;
            DefaultSchema = defaultSchema;
            CharSet = charSet;
        }
    }
}
