using Supabase;
using FleetManagement.Models;

namespace FleetManagement.Services
{
    public class FleetService
    {
        private readonly Client _supabase;

        public FleetService(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            try
            {
                var session = await _supabase.Auth.SignIn(email, password);
                if (session?.User == null) return null;

                var result = await _supabase
                    .From<Driver>()
                    .Filter("email", Postgrest.Constants.Operator.Equals, email)
                    .Single();

                if (result == null) return null;

                // Block inactive drivers
                if (!result.IsActive)
                    return "Inactive";

                return result.Role;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            var response = await _supabase.From<Vehicle>().Get();
            return response.Models;
        }

        public async Task<List<Driver>> GetAllDriversAsync()
        {
            var response = await _supabase.From<Driver>().Get();
            return response.Models;
        }
    }
}