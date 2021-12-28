namespace CarRental.API.Models.Requests
{
    public class AddRoleRequest
    {
        public string UserEmail { get; set; }
        public string RoleName { get; set; }
    }
}