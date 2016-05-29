using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Pharmacy.Utils
{
    public static class Utils
    {
        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static string Serialize(object obj)
        {
            using (var stream = File.Open(Path.Combine(Environment.CurrentDirectory, "../../AppData/Serializing.txt"), FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            using (var reader = new StreamReader(stream))
            {
                var serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
                stream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        public static object Deserialize(Type toType)
        {
            using (var stream = File.Open(Path.Combine(Environment.CurrentDirectory, "../../AppData/Serializing.txt"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                var deserializer = new DataContractSerializer(toType);
                return deserializer.ReadObject(stream);
            }
        }
    }
}
