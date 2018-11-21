//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using LitJson;
//using System.IO;


//namespace Common{
//	public class JsonP {

//		//保存玩家数据
//		public void SavePlayer(Player p){
//			string filePath = Application.dataPath + @"/Resources/JsonPlayer.json";
//			//找到当前路径
//			FileInfo file = new FileInfo(filePath);
//			//判断有没有文件，有则打开文件，，没有创建后打开文件
//			StreamWriter sw = file.CreateText();
//			//ToJson接口将你的列表类传进去，，并自动转换为string类型
//			string json = JsonMapper.ToJson(p);
//			//将转换好的字符串存进文件，
//			sw.WriteLine(json);
//			sw.Close();
//			sw.Dispose();
//		}

//		//读取玩家数据
//		public void LoadPlayer()
//		{
//			//调试用的  //Debug.Log(1);

//			//TextAsset该类是用来读取配置文件的
//			TextAsset asset = Resources.Load("JsonPlayer") as TextAsset;
//			if (!asset)  //读不到就退出此方法
//				return;

//			string strdata = asset.text;        
//			JsonData jsdata = JsonMapper.ToObject(strdata);
//			//在这里循环输出表示读到了数据，，即此数据可以使用了
//			for (int i = 0; i < jsdata.Count; i++)
//			{
//				Debug.Log(jsdata[i]);
//			}


//			Player.Instance.p_id = jsdata["p_id"].ToString();
//			Player.Instance.p_imagePath = jsdata["p_imagePath"].ToString();
//			Player.Instance.p_nickname = jsdata ["p_nickname"].ToString();
//			Player.Instance.p_description = jsdata ["p_description"].ToString();


//			Player.Instance.energy = int.Parse( jsdata ["engergy"].ToString());
//			Player.Instance.conenergy_rate = int.Parse( jsdata ["conenergy_rate"].ToString());
//			Player.Instance.collect_level = int.Parse( jsdata ["collect_level"].ToString());
//			Player.Instance.hunting_level =int.Parse( jsdata ["hunting_level"].ToString());
//			Player.Instance.logging_level = int.Parse( jsdata ["logging_level"].ToString());
//			Player.Instance.dig_level = int.Parse( jsdata ["dig_level"].ToString());


//			Player.Instance.fish_level = int.Parse( jsdata ["fish_level"].ToString());
//			Player.Instance.mk_china_lv = int.Parse( jsdata ["mk_china_lv"].ToString());
//			Player.Instance.mk_build_lv = int.Parse( jsdata ["mk_build_lv"].ToString());
//			Player.Instance.mk_weapon_lv = int.Parse( jsdata ["mk_weapon_lv"].ToString());


//			Player.Instance.p_head_id = int.Parse( jsdata ["p_head_id"].ToString());
//			Player.Instance.p_foot_id = int.Parse( jsdata ["p_foot_id"].ToString());
//			Player.Instance.p_body_id = int.Parse( jsdata ["p_body_id"].ToString());
//			Player.Instance.p_trousers_id = int.Parse( jsdata ["p_trousers_id"].ToString());
//		}


//	} 
//}
