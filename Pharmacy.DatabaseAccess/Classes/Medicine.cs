using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Pharmacy.DatabaseAccess.Interfaces;
using Pharmacy.DatabaseAccess.SqlHelpers;

namespace Pharmacy.DatabaseAccess.Classes
{
    [DataContract]
    [KnownType(typeof(Image))]
    public class Medicine : IEntity
    {
        public Guid Id { get { return ((IEntity) this).EntityId; } }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Price { get; set; }

        [DataMember]
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

        [DataMember]
        public byte[] PictureByteArray
        {
            get
            {
                if (Image != null)
                {
                    TypeConverter BitmapConverter =
                         TypeDescriptor.GetConverter(Image.GetType());
                    return (byte[])
                         BitmapConverter.ConvertTo(Image, typeof(byte[]));
                }
                else
                    return null;
            }

            set
            {
                if (value != null)
                    Image = new Bitmap(new MemoryStream(value));
                else
                    Image = null;
            }
        }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public List<Ingredient> Ingredients { get; set; }

        [DataMember]
        public MedicineType Type { get; set; }

        public List<Technology> CreateTechnologies { get; set; }

        [DataMember]
        Guid IEntity.EntityId { get; set; }

        public Medicine()
        {
           
        }

        public Medicine(Guid medicineId)
        {
            ((IEntity)this).EntityId = medicineId;
        }

        public List<Ingredient> GetIngredients()
        {
            var query = SqlQueryGeneration.GetIngredientByMedicineIdId(this.Id);
            var ingredients = new SqlExecuteManager().GetIngredient(query);

            return ingredients;
        }

        public override bool Equals(object obj)
        {
            var destinationObj = obj as Medicine;

            if (destinationObj == null)
                return false;

            return this.Id == destinationObj.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
