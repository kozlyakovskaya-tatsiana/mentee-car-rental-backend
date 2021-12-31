using System.Collections.Generic;

namespace CarRental.API.Models.Responses
{
    public class ValidationResponse
    {
        public bool IsValid { get; set; }
        public List<string> ValidationMessages { get; set; }

        public ValidationResponse()
        {
            IsValid = true;
            ValidationMessages = new List<string>();
        }
    }
}
