namespace $ext_safeprojectname$.API.Models
{
    public class AppSettings
    {
        public string AppPrefixPath { get; set; }
        public string JwtSecretKey { get; set; }
        public string WebApiUrl { get; set; }
        public string[] AllowedOrigins { get; set;}
    } 
    
}
