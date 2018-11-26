using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigDefine;
using Common;
public class Player
{

    public  enum BossState{
        prepar = 0,
        shoping
    }

    public static BossState State{
        set{
            if(value != State)
            {
                StateChange();
            }
            State = value;
        }
        get{
            return State;
        }
    }

    private static void StateChange()
    {
        switch(State){
            case(BossState.prepar):
                {
                    Debug.Log("正在购物中....");
                }
                break;
            case (BossState.shoping):
                {
                    Debug.Log("购物结束.....");
                }
                break;
        }
    }


    /// <summary>
    /// 本地存储数据的玩家等级。
    /// </summary>
    public static int PlayerLevel = 1;

    /// <summary>
    /// 本地存储数据的玩家等级当前的经验值。
    /// </summary>
    public static int PlayerExp;

    /// <summary>
    /// 本地存储数据的店铺口碑等级
    /// </summary>
    public static int ShopLevel = 1;

    /// <summary>
    /// 本地存储数据的玩家等级当前的经验值。
    /// </summary>
    public static int ShopExp;


    /// <summary>
    /// 商店全部资金，金币数量。
    /// </summary>
    public static int GoldNum;

    /// <summary>
    /// 商店钻石数量
    /// </summary>
    public static int DiamondNum;

    /// <summary>
    /// 商店库存。id和num
    /// </summary>
    public static Dictionary<int, int> ShopStock = new Dictionary<int, int>();

    //一个货架 我理解为一个NPC一样
    //店铺里面的货架位置,货架类型 <位置,类型>
    public static Dictionary<int, int> huojiaType = new Dictionary<int, int>();

    //货架上面摆放的货物种类 <位置,货物种类>
    public static Dictionary<int, int> huojiaGoodsType = new Dictionary<int, int>();

    //货架上摆放的物品所剩的数量<位置,货物量>
    public static Dictionary<int, int> huojiaNumber = new Dictionary<int, int>();

    //货架朝向 <位置,朝向> 1 - 4
    public static Dictionary<int, int> huojiaDiretion = new Dictionary<int, int>();

    //货架可销售次数剩余 <位置,sale times>
    public static Dictionary<int, int> huojiaSaleTimes = new Dictionary<int, int>();


    //货架等级 <位置,货架等级>
    public static Dictionary<int, int> huojiaLevel = new Dictionary<int, int>();

    //测试使用 删除本地数据 字典手动去 streaming asset 里删除
    public static void DelPlayerData()
    {
        PlayerPrefs.DeleteAll();
      
    }


    public static void SavePlayerData()
    {
        PlayerPrefs.SetInt(Define.PLAYER_LEVEL,PlayerLevel);
        PlayerPrefs.SetInt(Define.PLAYER_EXP, PlayerExp);
        PlayerPrefs.SetInt(Define.SHOP_LEVEL, ShopLevel);
        PlayerPrefs.SetInt(Define.SHOP_EXP, ShopExp);
        PlayerPrefs.SetInt(Define.GOLD, GoldNum);
        PlayerPrefs.SetInt(Define.DIAMOND, DiamondNum);

        IJson.WriteJsonToFile(Define.SHOP_STOCK,ShopStock);
        IJson.WriteJsonToFile(Define.HUOJIA_TYPE, huojiaType);
        IJson.WriteJsonToFile(Define.GOODS_TYPE_SHOW, huojiaGoodsType);
        IJson.WriteJsonToFile(Define.GOODS_NUMBER_SHOW, huojiaNumber);

        IJson.WriteJsonToFile(Define.HUO_JIA_DIRECTION, huojiaDiretion);
        IJson.WriteJsonToFile(Define.HUO_JIA_SALE_TIMES, huojiaSaleTimes);
        IJson.WriteJsonToFile(Define.HUO_JIA_LEVEL, huojiaLevel);

    }

    public static bool GetPlayerData()
    {
        bool haveData = false;
        if (PlayerPrefs.HasKey(Define.PLAYER_LEVEL))
        {
            PlayerLevel = PlayerPrefs.GetInt(Define.PLAYER_LEVEL);
            haveData = true;
        }

        if (PlayerPrefs.HasKey(Define.PLAYER_EXP))
        {
            PlayerExp = PlayerPrefs.GetInt(Define.PLAYER_EXP);
            haveData = true;
        }
        if (PlayerPrefs.HasKey(Define.SHOP_LEVEL))
        {
            ShopLevel = PlayerPrefs.GetInt(Define.SHOP_LEVEL);
            haveData = true;
        }

        if (PlayerPrefs.HasKey(Define.SHOP_EXP))
        {
            ShopExp = PlayerPrefs.GetInt(Define.SHOP_EXP);
            haveData = true;
        }

        if (PlayerPrefs.HasKey(Define.GOLD))
        {
            GoldNum = PlayerPrefs.GetInt(Define.GOLD);
            haveData = true;
        }

        if (PlayerPrefs.HasKey(Define.DIAMOND))
        {
            DiamondNum = PlayerPrefs.GetInt(Define.DIAMOND);
            haveData = true;
        }

        if (IJson.LoadJsonWithPath(Define.SHOP_STOCK) != null)
        {
            ShopStock = IJson.LoadJsonWithPath(Define.SHOP_STOCK);
            haveData = true;
        }

        if (IJson.LoadJsonWithPath(Define.HUOJIA_TYPE) != null)
        {
            huojiaType = IJson.LoadJsonWithPath(Define.HUOJIA_TYPE);
            haveData = true;
        }

        if (IJson.LoadJsonWithPath(Define.GOODS_TYPE_SHOW) != null)
        {
            huojiaGoodsType = IJson.LoadJsonWithPath(Define.GOODS_TYPE_SHOW);
            haveData = true;
        }


        if (IJson.LoadJsonWithPath(Define.GOODS_NUMBER_SHOW) != null)
        {
            huojiaNumber = IJson.LoadJsonWithPath(Define.GOODS_NUMBER_SHOW);
            haveData = true;
        }

        if (IJson.LoadJsonWithPath(Define.HUO_JIA_DIRECTION) != null)
        {
            huojiaDiretion = IJson.LoadJsonWithPath(Define.HUO_JIA_DIRECTION);
            haveData = true;
        }
        if (IJson.LoadJsonWithPath(Define.HUO_JIA_SALE_TIMES) != null)
        {
            huojiaSaleTimes = IJson.LoadJsonWithPath(Define.HUO_JIA_SALE_TIMES);
            haveData = true;
        }

        if (IJson.LoadJsonWithPath(Define.HUO_JIA_LEVEL) != null)
        {
            huojiaLevel = IJson.LoadJsonWithPath(Define.HUO_JIA_LEVEL);
            haveData = true;
        }

        return haveData;
    }



}
