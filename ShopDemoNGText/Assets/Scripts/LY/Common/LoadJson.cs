using System;
using System.IO;
using LitJson;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Common{

	public class IJson{
		
		public IJson(){
			
		}



		//加载data数据
		public static JsonData LoadJsonWithPath(string jsonName){
              string path =
#if UNITY_ANDROID   //安卓  
    "jar:file://" + Application.dataPath + "!/assets/";  
#elif UNITY_IPHONE  //iPhone  
    Application.dataPath + "/Raw/";  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR  //windows平台和web平台  
    "file://" + Application.dataPath + "/StreamingAssets/";  
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

		public static bool WriteJsonToFile(string fileName, object obj){
            string path =
#if UNITY_ANDROID   //安卓  
    "jar:file://" + Application.dataPath + "!/assets/";  
#elif UNITY_IPHONE  //iPhone  
    Application.dataPath + "/Raw/";  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR   //windows平台和web平台  
    "file://" + Application.dataPath + "/StreamingAssets/";  
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
