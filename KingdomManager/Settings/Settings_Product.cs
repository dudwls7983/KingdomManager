using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace KingdomManager
{
    [Serializable]
    class Settings_Building
    {
        string name;
        public List<Settings_Product> products;

        [NonSerialized]
        public Building building;

        public static Dictionary<string, Settings_Building> buildings = new Dictionary<string, Settings_Building>();

        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public int State { get; set; }

        public Settings_Building(System.Windows.Forms.ListBox list, string _name, Building _building, int _maxLevel)
        {
            if (buildings.ContainsKey(_name))
            {
                State = buildings[_name].State;
                Level = buildings[_name].Level;
                products = buildings[_name].products;
                buildings[_name] = this;
            }
            else
            {
                State = 0;
                Level = 0;
                buildings.Add(_name, this);
                products = new List<Settings_Product>();
            }
            name = _name;
            building = _building;
            MaxLevel = _maxLevel;
            list.Items.Add(_name);
        }

        public static void Save()
        {
            Stream stream = new FileStream("settings_product.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, buildings);
            stream.Close();
        }

        public static void Load()
        {
            Stream stream = new FileStream("settings_product.dat", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();

            if(stream.Length> 0)
                buildings = (Dictionary<string, Settings_Building>)formatter.Deserialize(stream);
            stream.Close();
        }
    }

    [Serializable]
    class Settings_Product
    {
        public int ID { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
