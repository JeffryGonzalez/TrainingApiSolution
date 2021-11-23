namespace TrainingApi;

public class AuthOptions
{
    public static string Section = "Auth";
    public string? MetadataAddress { get; set; }
    public bool RequireHttpToken { get; set; }
    public string? ValidAudience { get; set; }
    public string? ValidIssuer { get; set; }

}
