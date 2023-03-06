namespace VehicleManager.Helpers
{
    public static class Utilities
    {
        private static readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        public static bool IsAdmin()
        {
            return httpContextAccessor?.HttpContext?.Request.Cookies["Role"] == "Admin";
        }
    }
}
