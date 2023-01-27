/*using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using Raylib_cs;


namespace DonutEngine
{
    public static class SaveManager
    {
        
        public static bool Save(string saveName, object saveData)
        {
            BinaryFormatter formatter = GetBinaryFormatter();        

            if(Directory.Exists("/SaveData"))
            {
                Directory.CreateDirectory("/SaveData");
            }

            string path = "/SaveData/" + saveName + ".data";
            FileStream file = File.Create(path);

            formatter.Serialize(file, saveData);
            file.Close();

            return true;
        }

        public static object Load(string path)
        {
            if(!File.Exists(path))
            {
                return null;
            }

            BinaryFormatter formatter = GetBinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);

            try
            {
                object save = formatter = GetBinaryFormatter();
                file.Close();
                return save;
            }
            catch
            {
                return null;
            }
        }

        public static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter;
        }


    }

}*/