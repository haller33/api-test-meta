namespace Meta.Domain.src 
{
     public static class AppSettingsProvider
    {
        public static string taxaJuros { get; set; }
        public static bool IsDevelopment { get; set; }
        public static string Enviropment { get; set; }
        public static bool TestInProduction { get; set; }
    }
}