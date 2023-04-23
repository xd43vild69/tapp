namespace TappManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

    public static class Customers
    {
        public static string GetList => $"{Base}/customers";
        public static string GetRecord(Guid id) => $"{Base}/customers/{id}";
        public static string Delete(Guid id) => $"{Base}/customers/{id}";
        public static string Put(Guid id) => $"{Base}/customers/{id}";
        public static string Create => $"{Base}/customers";
        public static string CreateBatch => $"{Base}/customers/batch";
    }

    public static class Admins
    {
        public static string GetList => $"{Base}/admins";
        public static string GetRecord(Guid id) => $"{Base}/admins/{id}";
        public static string Delete(Guid id) => $"{Base}/admins/{id}";
        public static string Put(Guid id) => $"{Base}/admins/{id}";
        public static string Create => $"{Base}/admins";
        public static string CreateBatch => $"{Base}/admins/batch";
    }

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
