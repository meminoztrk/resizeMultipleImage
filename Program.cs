using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace New_folder
{
    class Program
    {
        public static string ImageResize(Image img, int maxWidth, int maxHeight)
        {
            if (img.Width > maxWidth || img.Height > maxHeight)
            {
                double wratio = (double)img.Width / (double)maxWidth;
                double hratio = (double)img.Height / (double)maxHeight;
                double ratio = Math.Max(wratio, hratio);
                int newWidth = (int)(img.Width / ratio);
                int newHeigth = (int)(img.Height / ratio);
                return newHeigth.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }
        static void Main(string[] args)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/images/";
            DirectoryInfo dir = new DirectoryInfo(path);
            List<FileInfo> file_list = new List<FileInfo>();
            file_list.AddRange(dir.GetFiles("*", SearchOption.AllDirectories));


            Console.Write(file_list);
            string newpath = System.IO.Directory.GetCurrentDirectory() + "/newimages/";
            Console.WriteLine("Images are resizing...!");
            foreach (var item in file_list)
            {
                using (var image = Image.Load(item.OpenRead()))
                {
                    string newSize = ImageResize(image, 600, 600);
                    string[] sizeArray = newSize.Split(',');
                    image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));
                    image.Save(newpath + Path.GetFileNameWithoutExtension(item.Name) + item.Extension);
                }

                Console.Write(path + item.Name + " resized to " + newpath + item.Name + "\n");
            }
            Console.WriteLine("Resizing successful!");
        }
    }
}
