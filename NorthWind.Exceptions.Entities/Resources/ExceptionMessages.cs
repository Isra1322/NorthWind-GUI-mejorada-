namespace NorthWind.Exceptions.Entities.Resources;

public static class ExceptionMessages
{
    public const string ValidationExceptionTitle = "Error en los datos de entrada.";
    public const string ValidationExceptionDetail = "Se encontraron uno o más errores de validación de datos.";
    public const string UpdateExceptionTitle = "Error de actualización.";
    public const string UpdateExceptionDetail = "Se encontraron uno o más errores de actualización de datos. Consulte al administrador.";
    public const string UnhandledExceptionTitle = "El servicio no se encuentra disponible.";
    public const string UnhandledExceptionDetail = "Consulte al administrador.";
}
