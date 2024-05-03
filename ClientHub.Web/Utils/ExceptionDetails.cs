namespace ClientHub.Web.Utils;

internal record ExceptionDetails(
            int status,
            string type,
            string title,
            string detail);
