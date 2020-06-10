using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class ByteHandler
{
    #region public methods 
    public static T ByteArrayToObject<T>(byte[] arrBytes )
    {     
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = binForm.Deserialize(memStream);

            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
    public static byte[] ObjectToByteArray(System.Object obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }
    #endregion
}
