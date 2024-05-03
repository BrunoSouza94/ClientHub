namespace ClientHub.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "Value can't be null");

    public static Error InvalidCredentials = new(
        "Error.InvalidCredentials",
        "As credenciais fornecidas não são válidas.");

    public static Error InvalidAuthorization = new(
        "Error.InvalidAuthorization",
        "O usuário não possui permissão para acessar a informação solicitada.");
}