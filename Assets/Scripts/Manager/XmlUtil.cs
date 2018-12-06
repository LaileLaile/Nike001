
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class XmlUtil
{
    #region 序列化
    public static string Serializer<T>(T obj) where T : class
    {
        using (MemoryStream ms = new MemoryStream())
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(ms, obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string xmlString = sr.ReadToEnd();
            sr.Dispose();
            ms.Dispose();
            xmlString = xmlString.Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"GBK\"?>");
            return xmlString;
        }
    }

    public static Stream Serializer<T>(T obj, bool isStream) where T : class
    {
        using (MemoryStream ms = new MemoryStream())
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(ms, obj);
            ms.Position = 0;
            return ms;
        }
    }
    #endregion

    #region 反序列化
    public static T Deserialize<T>(string xmlString) where T : class
    {
        StringReader sr = new StringReader(xmlString);
        XmlSerializer xs = new XmlSerializer(typeof(T));
        T obj = (T)xs.Deserialize(sr);
        sr.Dispose();
        return obj;
    }

    public static T Deserialize<T>(Stream stream) where T : class
    {
        XmlSerializer xs = new XmlSerializer(typeof(T));
        return (T)xs.Deserialize(stream);
    }

    public static T Deserialize<T, T1>(T1 obj)
        where T : class
        where T1 : class
    {
        return Deserialize<T>(Serializer<T1>(obj, true));
    }
    #endregion





 


}