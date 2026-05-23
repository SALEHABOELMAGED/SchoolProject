namespace SchoolProject.Data.Helpers
{
    public class JwtSettings
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? ValidateIssuer { get; set; }
        public string? ValidateAudience { get; set; }
        public string? ValidateLifetime { get; set; }
        public string? ValidateIssuerSigningKey { get; set; }


    }
}
