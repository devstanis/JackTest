using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JackTest
{
    static class DataIO
    {
        public static bool SaveData(Lotto _lotto, string fileName)
        {
            var res = true;
            Stream stream = null;
            try
            {
                stream = File.Create(fileName);
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, _lotto);
            }
            catch
            {
                res = false;
            }
            finally
            {
                stream?.Dispose();
            }
            return res;
        }

        public static Lotto LoadData(string filePath)
        {
            Stream stream = null;
            Lotto lotto = null;
            try
            {
                stream = File.OpenRead(filePath);
                BinaryFormatter serializer = new BinaryFormatter();
                lotto = (Lotto)serializer.Deserialize(stream);
            }
            catch { }
            finally
            {
                stream.Dispose();
            }
            return lotto;
        }
    }
}
