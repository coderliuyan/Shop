using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    public enum BossState{
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
    public static int PlayerLevel;

    /// <summary>
    /// 本地存储数据的玩家等级当前的经验值。
    /// </summary>
    public static int PlayerExp;

    /// <summary>
    /// 本地存储数据的店铺口碑等级
    /// </summary>
    public static int ShopLevel;

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
    public static Dictionary<short, int> ShopStock = new Dictionary<short, int>();



    //一个货架 我理解为一个NPC一样
    //店铺里面的货架位置,货架类型 <位置,类型>
    public static Dictionary<int, int> huojiaPos = new Dictionary<int, int>();

    //货架上面摆放的货物种类 <位置,货物种类>
    public static Dictionary<int, int> huojiaType = new Dictionary<int, int>();

    //货架上摆放的物品所剩的数量<位置,货物量>
    public static Dictionary<int, int> huojiaNumber = new Dictionary<int, int>();

}
