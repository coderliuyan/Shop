using System;
using System.IO;
using LitJson;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Common{

	public class IJson{
		
		public IJson(){
			
		}



		//加载data数据
		public static JsonData LoadJsonWithPath(string jsonName,bool jsonBo = true){
            string path =
#if UNITY_ANDROID && !UNITY_EDITOR   //安卓  
    "jar:file://" + Application.dataPath + "!/assets/ + jsonName";
#elif UNITY_IPHONE  //iPhone  
    Application.dataPath + "/Raw/" + jsonName;  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR  //windows平台和web平台  
    Application.dataPath + "/StreamingAssets/" + jsonName ;  
#else
        string.Empty;  
#endif
			try{
				if (!File.Exists (path))
					return null;
				StreamReader sr = new StreamReader (path);
				if (sr == null)
					return null;
				String json = sr.ReadToEnd ();
				JsonData data = JsonMapper.ToObject (json);
				sr.Close ();
				sr.Dispose ();
				return data;
			}
			catch(Exception error){
				Debug.LogError (error.Message);
			}
			
			return null;
		}

        //加载data数据
        public static Dictionary<int,int> LoadJsonWithPath(string jsonName)
        {
            string path =
#if UNITY_ANDROID && !UNITY_EDITOR   //安卓  
    "jar:file://" + Application.dataPath + "!/assets/ + jsonName";
#elif UNITY_IPHONE  //iPhone  
    Application.dataPath + "/Raw/" + jsonName;  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR  //windows平台和web平台  
    Application.dataPath + "/StreamingAssets/" + jsonName;
#else
        string.Empty;  
#endif
            try
            {
                if (!File.Exists(path))
                    return null;
                StreamReader sr = new StreamReader(path);
                if (sr == null)
                    return null;
                String json = sr.ReadToEnd();
                JsonData data = JsonMapper.ToObject(json);
                sr.Close();
                sr.Dispose();

                Dictionary<int, int> dicInt = new Dictionary<int, int>();

                foreach(var d in data.Keys)
                {
                    int key = int.Parse(d.ToString());
                    int value = int.Parse(data[d].ToString());
                    dicInt.Add(key, value);
                }

                return dicInt;
            }
            catch (Exception error)
            {
                Debug.LogError(error.Message);
            }

            return null;
        }

        public static Dictionary<int, string> LoadJsonStringWithPath(string jsonName)
        {
            string path =
#if UNITY_ANDROID && !UNITY_EDITOR   //安卓  
    "jar:file://" + Application.dataPath + "!/assets/ + jsonName";
#elif UNITY_IPHONE  //iPhone  
    Application.dataPath + "/Raw/" + jsonName;  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR  //windows平台和web平台  
    Application.dataPath + "/StreamingAssets/" + jsonName;
#else
        string.Empty;  
#endif
            try
            {
                if (!File.Exists(path))
                    return null;
                StreamReader sr = new StreamReader(path);
                if (sr == null)
                    return null;
                String json = sr.ReadToEnd();
                JsonData data = JsonMapper.ToObject(json);
                sr.Close();
                sr.Dispose();

                Dictionary<int, string> dic = new Dictionary<int, string>();

                foreach (var d in data.Keys)
                {
                    int key = int.Parse(d.ToString());
                    string value = data[d].ToString();
                    dic.Add(key, value);
                }

                return dic;
            }
            catch (Exception error)
            {
                Debug.LogError(error.Message);
            }

            return null;
        }




        public static bool WriteJsonToFile(string fileName, Dictionary<int, int> dic)
        {
            Dictionary<string, string> dicStr = new Dictionary<string, string>();

            foreach (int key in dic.Keys)
            {
                string keyStr = key.ToString();
                string valueStr = dic[key].ToString();

                dicStr.Add(keyStr,valueStr);
            }

            return WriteJsonToFile(fileName, dicStr);
        }

        
        public static bool WriteJsonToFile(string fileName, Dictionary<int, string> dic)
        {
            Dictionary<string, string> dicStr = new Dictionary<string, string>();

            foreach (int key in dic.Keys)
            {
                string keyStr = key.ToString();
                string valueStr = dic[key];

                dicStr.Add(keyStr,valueStr);
            }

            return WriteJsonToFile(fileName, dicStr);
        }




        public static bool WriteJsonToFile(string fileName, Dictionary<string, string> obj)
        {
            string path =
#if UNITY_ANDROID && !UNITY_EDITOR  //安卓  
    "jar:file://" + Application.dataPath + "!/assets/" + fileName;
#elif UNITY_IPHONE  //iPhone  
    Application.dataPath + "/Raw/" + fileName;  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR   //windows平台和web平台  
    Application.dataPath + "/StreamingAssets/"+fileName;
#else
        string.Empty;  
#endif
			try{
				//找到当前路径
				FileInfo file = new FileInfo(path);
				//判断有没有文件，有则打开文件，，没有创建后打开文件
				StreamWriter sw = file.CreateText();
				//ToJson接口将你的列表类传进去，，并自动转换为string类型
				string json = JsonMapper.ToJson(obj);
				//将转换好的字符串存进文件，
				sw.WriteLine(json);
				//注意释放资源
				sw.Close();
				sw.Dispose();
			}
			catch(Exception error){
				Debug.LogError (error.Message);
				return false;
			}
			#if UNITY_EDITOR
			AssetDatabase.Refresh();
			#endif

			return true;
		}

	}

}
