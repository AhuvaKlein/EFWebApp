using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EFWebApp.Data
{

    public class ImageRepository
    {
        private string _connectionString;

        public ImageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveImage(Image image)
        {
            using (var context = new ImageContext(_connectionString))
            {
                context.Images.Add(new Image
                {
                    Name = image.Name,
                    Title = image.Title,
                    DateAdded = DateTime.Now
                });

                context.SaveChanges();
            }
        }

        public IEnumerable<Image> GetImages()
        {
            using (var context = new ImageContext(_connectionString))
            {
                return context.Images.OrderByDescending(i => i.DateAdded).ToList();
            }
        }

        public Image GetImageById(int id)
        {
            using (var context = new ImageContext(_connectionString))
            {
                return context.Images.FirstOrDefault(i => i.Id == id);
            }
        }

        public void IncreaseLike(int id)
        {
            using (var context = new ImageContext(_connectionString))
            {
                context.Database.ExecuteSqlCommand(
                    "UPDATE Images SET Likes= Likes + 1 WHERE Id = @id",
                    new SqlParameter("@id", id));
            }
        }

        public int NumberOfLikes(int id)
        {
            using (var context = new ImageContext(_connectionString))
            {
                Image image = context.Images.FirstOrDefault(i => i.Id == id);
                return image.Likes;
            }
        }

    }
}
