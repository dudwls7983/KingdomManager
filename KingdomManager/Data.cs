using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KingdomManager
{
    class Item
    {
        public string name;
        public string key;

        public static Dictionary<string, Item> list = new Dictionary<string, Item>();

        public Item(string _key, string _name)
        {
            name = _name;
            list.Add(_key, this);
        }
    }

    class Product
    {
        public int id;
        public string name;
        public int time; // seconds
        public List<Tuple<Item, int>> materials;
        public int price;
        public int count;

        public Product(int _id, string _name, int _time, int _price = 0, int _count = 1)
        {
            id = _id;
            name = _name;
            time = _time;
            price = _price;
            count = _count;
            materials = new List<Tuple<Item, int>>();
        }

        public void Add(Item item, int count)
        {
            materials.Add(new Tuple<Item, int>(item, count));
        }
    }

    class Building
    {
        public int id;
        public string name;
        public List<Product> products;

        public Building(int _id, string _name)
        {
            id = _id;
            name = _name;
            products = new List<Product>();
        }

        public void Add(Product product)
        {
            products.Add(product);
        }
    }

    class BitmapListData
    {
        Bitmap originalBitmap;
        int originalWidth;
        int originalHeight;
        int screenWidth;
        int screenHeight;
        public Bitmap resultBitmap;
        public int currentScreenWidth;
        public int currentScreenHeight;

        public BitmapListData(Bitmap bitmap, int width, int height)
        {
            originalBitmap = bitmap;
            originalWidth = bitmap.Width;
            originalHeight = bitmap.Height;
            screenWidth = width;
            screenHeight = height;
            Resize(width, height);
        }

        public void Resize(int currentWidth, int currentHeight)
        {
            if (resultBitmap != null)
                resultBitmap.Dispose();

            float resizeWidth = (float)currentWidth / screenWidth;
            float resizeHeight = (float)currentHeight / screenHeight;

            resultBitmap = ResizeImage(originalBitmap, (int)Math.Round(originalWidth * resizeWidth), (int)Math.Round(originalHeight * resizeHeight));
            currentScreenWidth = currentWidth;
            currentScreenHeight = currentHeight;
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
