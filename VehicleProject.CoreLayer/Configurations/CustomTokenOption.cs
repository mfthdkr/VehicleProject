namespace VehicleProject.CoreLayer.Configurations
{
    public class CustomTokenOption
    {
        // İstek yapılabilecek API'ler
        public List<string> Audience { get; set; }

        // Token dağıtan API
        public string Issuer { get; set; }

        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }

        // imzalama key
        public string SecurityKey { get; set; }
    }
}
