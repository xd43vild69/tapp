namespace TappManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

    public static class Appointments
    {
        public static string GetList => $"{Base}/appointments";
        public static string GetRecord(Guid id) => $"{Base}/appointments/{id}";
        public static string Delete(Guid id) => $"{Base}/appointments/{id}";
        public static string Put(Guid id) => $"{Base}/appointments/{id}";
        public static string Create => $"{Base}/appointments";
        public static string CreateBatch => $"{Base}/appointments/batch";
    }

    public static class Users
    {
        public static string GetList => $"{Base}/users";
        public static string GetRecord(Guid id) => $"{Base}/users/{id}";
        public static string Delete(Guid id) => $"{Base}/users/{id}";
        public static string Put(Guid id) => $"{Base}/users/{id}";
        public static string Create => $"{Base}/users";
        public static string CreateBatch => $"{Base}/users/batch";
    }
}
