using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class Banners
    {
        public string Id { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public int Price { get; set; }
    }
}
