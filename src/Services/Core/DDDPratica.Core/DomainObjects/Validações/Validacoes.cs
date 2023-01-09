using System.Text.RegularExpressions;

namespace DDDPratica.Core.DomainObjects.Validações;

public class Validacoes
{
    public static void ValidarSeIgual(object obj1, object obj2, string mensagem)
    {
        if (!obj1.Equals(obj2))
            throw new DomainException(mensagem); 
    }

    public static void ValidarSeDiferente(object obj1, object obj2, string message)
    {
        if (obj1.Equals(obj2))
            throw new DomainException(message); 
    }

    public static void ValidarCaracteres(string valor, int maximo, string message)
    {
        var length = valor.Trim().Length;

        if (length > maximo)
            throw new DomainException(message); 
    }

    public static void ValidarExpressao(string pattern, string valor, string message)
    {
        var regex = new Regex(pattern);

        if (!regex.IsMatch(valor))
            throw new DomainException(message);
    }

    public static void ValidarCaracteres(string valor, int minimo, int maximo, string message)
    {
        var length = valor.Trim().Length;

        if (length < minimo || length > maximo)
            throw new DomainException(message);
    }

    public static void ValidarSeVazio(string valor, string message)
    {
        if (valor == null || valor.Trim().Length == 0)
            throw new DomainException(message); 
    }

    public static void ValidarSeNulo(object obj, string message)
    {
        if (obj == null)
            throw new DomainException(message);
    }

    public static void ValidarMinimoMaximo(double valor, double minimo, double maximo, string message)
    {
        if (valor < minimo || valor > maximo)
            throw new DomainException(message); 
    }

    public static void ValidarMinimoMaximo(float valor, float minimo, float maximo, string message)
    {
        if (valor < minimo || valor > maximo)
            throw new DomainException(message); 
    }

    public static void ValidarMinimoMaximo(int valor, int minimo, int maximo, string message)
    {
        if (valor < minimo || valor > maximo)
            throw new DomainException(message); 
    }

    public static void ValidarSeMenorIgualMinimo(decimal valor, int minimo, string message)
    {
        if (valor <= minimo)
            throw new DomainException(message);
    }
}