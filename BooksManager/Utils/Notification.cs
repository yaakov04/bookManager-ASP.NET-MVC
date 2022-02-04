namespace BooksManager.Utils
{
    public class Notification
    {
        public static string Success(string obj, string action)
        {
            return $"{obj} {action} correctamente";
        }

        public static string Failed(string obj, string action)
        {
            return $"No se pudo {action} {obj}";
        }
    }
}
