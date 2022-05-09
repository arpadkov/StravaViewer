namespace StravaViewer.Client
{
    public class StravaUserCredentials
    {
        public int client_id { get; set; } 
        public string client_secret { get; set; }
        public string refresh_token { get; set; }

        public string ClientId
        {
            get {
                return client_id.ToString();
            }
        }

        public string ClientSecret
        {
            get
            {
                return client_secret;
            }            
        }
        
        public string RefreshToken
        {
            get
            {
                return refresh_token;
            }
        }

    }

}
