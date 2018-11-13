using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIData
{
    /// <summary>
    /// 本地存储数据的玩家等级名称。
    /// </summary>
    public static string PalyerLevelName = "PalyerLevel";
    /// <summary>
    /// 本地存储数据的玩家等级。
    /// </summary>
    public static int PalyerLevelNum;
    /// <summary>
    /// 本地存储数据的玩家等级当前的经验值。
    /// </summary>
    public static string PalyerLevelExpName = "PalyerLevelExpName";
    /// <summary>
    /// 本地存储数据的玩家等级当前的经验值。
    /// </summary>
    public static int PalyerLevelExp;
    /// <summary>
    /// 本地存储数据的店铺口碑等级名称。
    /// </summary>
    public static string ShopLevelName = "ShopLevel";
    /// <summary>
    /// 本地存储数据的店铺口碑等级
    /// </summary>
    public static int ShopLevelNum;
    /// <summary>
    /// 本地存储数据的玩家等级当前的经验值。
    /// </summary>
    public static string ShopLevelExpName = "ShopLevelExpName";
    /// <summary>
    /// 本地存储数据的玩家等级当前的经验值。
    /// </summary>
    public static int ShopLevelExp;

    /// <summary>
    /// 本地存储数据的金币名称。
    /// </summary>
    public static string GoldName="gold";
    /// <summary>
    /// 商店全部资金，金币数量。
    /// </summary>
    public static int GoldNum;
    /// <summary>
    /// 本地存储数据的钻石名称。
    /// </summary>
    public static string DianmondName="dianmond";
    /// <summary>
    /// 商店钻石数量
    /// </summary>
    public static int DiamondNum;
    /// <summary>
    /// 商店库存。id和num
    /// </summary>
    public static Dictionary<short, int> ShopStock=new Dictionary<short,int>();
    /// <summary>
    /// 开始游戏场景
    /// </summary>
    public static string LoginScene = "LoginScene";
    /// <summary>
    /// 存入本地数据  无当前数据则赋值为-99
    /// </summary>
    public static  void SaveLocalDate (string _dateName,int _dateNum)
    {
        //PlayerPrefs.SetInt("ChooseCardType" + i + StaticData.role_now, cardIdListTypeNow[i]);
        //if (PlayerPrefs.HasKey("ChooseCardId0" + StaticData.role_now))
        //    PlayerPrefs.GetInt("ChooseCardId" + i + StaticData.role_now, 0)
        //PlayerPrefs.DeleteKey("KillScore");
        PlayerPrefs.SetInt(_dateName , _dateNum);
        Debug.Log("存入本地数据 "+ _dateName+" @ "+ _dateNum);
    }
    /// <summary>
    /// 获取本地数据,无当前数据则赋值为-99
    /// </summary>
    /// <param name="_dateName"></param>
    /// <returns></returns>
    public static int GetLocalDate (string _dateName)
    {
        if (PlayerPrefs.HasKey(_dateName))
        {
        Debug.Log("获取本地数据 " + _dateName + " @ " + PlayerPrefs.GetInt(_dateName, -99));
            return PlayerPrefs.GetInt(_dateName, -99);
        }
        else
        {
            Debug.LogError("there is no have LocalDate:" + _dateName);
            return -99;
        }
    }
    /// <summary>
    /// 删除本地数据
    /// </summary>
    public static void DeleteLocalDate (string _dateName)
    {
        if (PlayerPrefs.HasKey(_dateName))
        {
            PlayerPrefs.DeleteKey(_dateName);
            Debug.Log("删除本地数据 " + _dateName);
        }
        else
        {
            Debug.LogError("there is no have LocalDate:" + _dateName);
        }
    }

    public static bool HaveLocalData(string _dataName)
    {
        return PlayerPrefs.HasKey(_dataName);
    }
}
