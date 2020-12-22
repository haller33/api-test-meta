using System;
using Meta.Domain.src;

namespace Meta.Domain.Auth
{
    public class Autentication
    {
        static public readonly string TAXAJUROS = AppSettingsProvider.taxaJuros;
        static public readonly bool IsDevelopment = AppSettingsProvider.IsDevelopment;
        static public readonly string Enviropment = AppSettingsProvider.Enviropment;
    }
}
