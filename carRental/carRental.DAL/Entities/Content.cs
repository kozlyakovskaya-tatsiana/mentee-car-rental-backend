using System;
using System.Collections.Generic;
using System.Text;

namespace carRental.DAL.Entities
{
    class Content
    {
        public string Id { get; set; }
        string contentType { get; set; }
        byte[] content { get; set; }
    }
}
