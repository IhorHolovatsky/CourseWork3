using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using Pharmacy.DatabaseAccess.Interfaces;

namespace Pharmacy.DatabaseAccess.Classes
{
    public class Medicine : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Image Image { get; set; }

        public BitmapImage BitmapImage
        {
            get
            {
                //Save Image to stream
                var stream = new MemoryStream();
                Image.Save(stream, ImageFormat.Jpeg);
                
                //Reset stream pointer
                stream.Seek(0, SeekOrigin.Begin);

                //Get image from stream
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();

                return image;
            }
        }

        public string ImageUrl { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public MedicineType Type { get; set; }

        public List<Technology> CreateTechnologies { get; set; }

        Guid IEntity.EntityId { get; set; }

        public Medicine()
        {
           
        }

        public Medicine(Guid medicineId)
        {
            ((IEntity)this).EntityId = medicineId;
        }
    }
}
