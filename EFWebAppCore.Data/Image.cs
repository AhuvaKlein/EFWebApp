using System;

namespace EFWebApp.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime DateAdded { get; set; }
        public int Likes { get; set; }
    }
}
