using VehicleManager.Models;

namespace VehicleManager.Helpers
{
    public static class JwtHelper
    {
        public static async Task GetAndSaveJwt(HttpClient client, HttpRequest request, HttpResponse response)
        {
            string jwtString = request.Cookies["jwtToken"] ?? string.Empty;

            var tokenRequest = new TokenRequest
            {
                Password = "vehicleManager",
                Username = "vehicleManager",
                JwtString = jwtString
            };

            var jwt = await client.PostAsJsonAsync("api/Auth/Authenticate", tokenRequest);
            jwtString = await jwt.Content.ReadAsStringAsync();

            var cookie = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            };
            response.Cookies.Append("jwtToken", jwtString, cookie);
        }
    }
}
