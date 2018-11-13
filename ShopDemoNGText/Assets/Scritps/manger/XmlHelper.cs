using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

/// <summary>
/// xml 读取方法
/// </summary>
public class XmlHelper
{
    private static XmlHelper instance = null;
    public static XmlHelper Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new XmlHelper();
            }
            return instance;
        }      
    }
    private Dictionary<string, TableValue> tableDict = null;
    private XmlHelper()
    {
        tableDict = new Dictionary<string, TableValue>();
    }
    /// <summary>
    /// 加载配置文件 
    /// </summary>
    /// <param name="filename">文件名称</param>
    public void LoadFile(string filename, Object file)
    {
        if (tableDict.ContainsKey(filename))
        {
            Debug.LogWarning( filename+" 配置文件已经加载过");
            return;
        }
        LoadXmlFile(filename, file);
       
    }
    /// <summary>
    /// 读取本地配置文件
    /// </summary>
    /// <param name="filename"></param>
    void LoadXmlFile(string filename,Object file)
    {
        //Object file = Resources.Load(filename);
        if (file == null)
        {
            Debug.LogError(filename + " 配置文件不存在");
            return;
        }
        string data = file.ToString();
        if (string.IsNullOrEmpty(data))
        {
            Debug.LogError(filename + " 配置文件内容为空");
            return;
        }
        ExchangeData(filename, data);
    }
    void ExchangeData(string filename, string data)
    {
        //XDocument xmlDoc = new XDocument();
        XDocument xmlDoc = XDocument.Parse(data);

        TableValue tb = new TableValue();
        foreach (XElement elt in xmlDoc.Element("plist").Elements())
        {
            //Debug.Log(":::" + elt.Name);
            LineValue lv = new LineValue();
            IEnumerable<XElement> newElementEleColl = elt.Elements();
            foreach (XElement element in newElementEleColl)
            {
                lv.AddItem(element.Name.ToString(), element.Value);
                // Debug.Log(":::" + element.Name +","+ element.Value);
            }
            ///删除数据名称前缀 "card_"
            string eleName = elt.Name.ToString().Remove(0, 5);
            tb.AddLine(eleName, lv);

        }
        tableDict.Add(filename, tb);
    }
    /// <summary>
    /// 读取配置信息
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public TableValue ReadFile(string filename)
    {
        if (!tableDict.ContainsKey(filename))
        {
            Debug.LogError(filename + " 配置文件未加载");
            return null;
        }
        return tableDict[filename];
    }

    public void UnLoadFile(string fileName)
    {
        if (!tableDict.ContainsKey(fileName))
        {
            Debug.LogError(fileName + " 配置文件未加载");
            return ;
        }
        tableDict.Remove(fileName);
    }
    /// <summary>
    /// 删除所有配置文件
    /// </summary>
    public void ClearAll()
    {
        tableDict.Clear();
    }
}
public class LineValue
{
    public string lineName;
    private Dictionary<string, string> dataValue = new Dictionary<string, string>();

    /// <summary>
    /// 添加配置表单行信息
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void AddItem(string key, string value)
    {
        if (dataValue.ContainsKey(key))
        {
            Debug.LogError(key + "  已存在");
        }
        else
        {
            dataValue.Add(key, value);
        }
    }
    /// <summary>
    /// 获取string类型的数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
     public string GetString(string key)
    {
         if(!dataValue.ContainsKey(key))
         {
             Debug.LogError("LineValue is not contain key="+key);
             return null;
         }
        return dataValue[key];
    }
    /// <summary>
    /// 获取int类型的数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
     public int GetInt(string key)
     {
         string str = GetString(key);
         if (str == null)
         {
             return 0;
         }
         int num = 0;
         if (int.TryParse(str, out num))
         {
             return num;
         }
         else
         {
             Debug.LogError("Wrong format  Int//" + str);
             return 0;
         }
     }
    /// <summary>
    /// 获取short类型的数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
     public short GetShort(string key)
     {
         string str = GetString(key);
         if (str == null)
         {
             return 0;
         }
         short num = 0;
         if (short.TryParse(str, out num))
         {
             return num;
         }
         else
         {
             Debug.LogError("Wrong format  Short//" + str);
             return 0;
         }
     }
    /// <summary>
    /// 获取byte类型的数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
     public byte GetByte(string key)
     {
         string str = GetString(key);
         if (str == null)
         {
             return 0;
         }
         byte num = 0;
         if (byte.TryParse(str, out num))
         {
             return num;
         }
         else
         {
             Debug.LogError("Wrong format  Byte//" + str);
             return 0;
         }
     }
    /// <summary>
    /// 获取float类型的数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
     public float GetFloat(string key)
     {
         string str = GetString(key);
         if (str == null)
         {
             return 0;
         }
         float num = 0;
         if (float.TryParse(str, out num))
         {
             return num;
         }
         else
         {
             Debug.LogError("Wrong format  Float//" + str);
             return 0;
         }
     }
    public IEnumerator GetEnumerator ()
    {
        foreach (var item in dataValue)
        {
            yield return item.Value;
        }
    }
}
public class TableValue
{
    private Dictionary<string, LineValue> dataValue = new Dictionary<string, LineValue>();

    public void AddLine(string key, LineValue value)
    {
        if (dataValue.ContainsKey(key))
        {
            Debug.LogError(key + "  is a  same key");
        }
        else
        {
            value.lineName = key;
            dataValue.Add(key, value);
        }
    }
    /// <summary>
    /// 获取配置文件数据总数
    /// </summary>
    /// <returns></returns>
    public int GetCountNum()
    {
        return dataValue.Count;
    }
    /// <summary>
    /// 获取某条数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public LineValue GetLineValue(string key)
    {
        if (!dataValue.ContainsKey(key))
        {
            Debug.LogError("TableValue is not contain key=" + key);
            return null;
        }
        return dataValue[key];
    }
    public LineValue GetLineValue(int num)
    {
        return GetLineValue(num.ToString());
    }
    /// <summary>
    /// 获取string 数据
    /// </summary>
    public string GetString(string key,string value)
    {
         if(!dataValue.ContainsKey(key))
         {
             Debug.LogError("TableValue is not contain key=" + key);
             return null;
         }
         return dataValue[key].GetString(value);
    }
    public string GetString(int key, string value)
    {
        return GetString(key.ToString(),value);
    }
    /// <summary>
    /// 获取short 数据
    /// </summary>
    public short GetShort(string key, string value)
    {
        string str = GetString(key, value);
        if (str == null)
        {
            return 0;
        }
        short num = 0;
        if (short.TryParse(str, out num))
        {
            return num;
        }
        else
        {
            Debug.LogError("Wrong format  Short//" + str);
            return 0;
        }
    }
    public short GetShort(int key, string value)
    {
        return GetShort(key.ToString(), value);
    }
    /// <summary>
    /// 获取byte 数据
    /// </summary>
    public byte GetByte(string key, string value)
    {
        string str = GetString(key, value);
        if (str == null)
        {
            return 0;
        }
        byte num = 0;
        if (byte.TryParse(str, out num))
        {
            return num;
        }
        else
        {
            Debug.LogError("Wrong format  Byte//" + str);
            return 0;
        }
    }
    public byte GetByte(int key, string value)
    {
        return GetByte(key.ToString(), value);
    }
    /// <summary>
    /// 获取int 数据
    /// </summary>
    public int GetInt(string key,string value)
    {
        string str = GetString(key, value);
        if(str==null)
        {
            return 0;
        }
        int num = 0;
        if(int.TryParse(str,out num))
        {
            return num;
        }
        else
        {
            Debug.LogError("Wrong format  Int//" + str);
            return 0;
        }
    }
    public int GetInt(int key, string value)
    {
        return GetInt(key.ToString(), value);
    }
    /// <summary>
    /// 获取float 数据
    /// </summary>
    public float GetFloat(string key, string value)
    {
        string str = GetString(key, value);
        if (str == null)
        {
            return 0;
        }
        float num = 0;
        if (float.TryParse(str, out num))
        {
            return num;
        }
        else
        {
            Debug.LogError("Wrong format  Float//" + str);
            return 0;
        }
    }
    public float GetFloat(int key, string value)
    {
        return GetFloat(key.ToString(), value);
    }
    public IEnumerator GetEnumerator()
    {
        foreach (var item in dataValue)
        {
            yield return item.Value;
        }
    }
}