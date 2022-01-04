namespace CarRental.Common.Options
{
    public class DefaultUserOptions
    {
        public const string SectionName = "DefaultUsers";

        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }

    }
}
