using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsModel : MonoBehaviour {

    public event UpdateDataEventGoods  updateDataGoodsEvent;
	// Use this for initialization
    //物品id
    private int goodsId;
    //物品类型
    private int goodsType;
    //物品名称
    private string goodsName;
    //物品进价
    private int goodsbuyMoney;
    //物品售价
    private int goodssetMoney;
    //物品等级到达可解锁
    private int goodsunlockLevel;
    private int maxKucun;
    private int nowKucun;
    public int GoodsId
    {
        get
        {
            return goodsId;
        }
        set
        {
            goodsId = value;
            updateDataGoodsEvent(goodsId, goodsType, goodsName, goodsbuyMoney, goodssetMoney, goodsunlockLevel, maxKucun, nowKucun);
        }
    }
    public int GoodsType
    {
        get
        {
            return goodsType;
        }
        set
        {
            goodsType = value;
            updateDataGoodsEvent(goodsId, goodsType,goodsName, goodsbuyMoney, goodssetMoney, goodsunlockLevel, maxKucun, nowKucun);
        }
    }
    public string  GoodsName
    {
        get
        {
            return goodsName;
        }
        set
        {
            goodsName= value;
            updateDataGoodsEvent(goodsId, goodsType, goodsName, goodsbuyMoney, goodssetMoney, goodsunlockLevel, maxKucun, nowKucun);
        }
    }
    public int GoodsBuyMoney
    {
        get
        {
            return goodsbuyMoney;
        }
        set
        {
            goodsbuyMoney = value;
            updateDataGoodsEvent(goodsId, goodsType, goodsName, goodsbuyMoney, goodssetMoney, goodsunlockLevel, maxKucun, nowKucun);
        }
    }
    public int GoodsSetMoney
    {
        get
        {
            return goodssetMoney;
        }
        set
        {
            goodssetMoney = value;
            updateDataGoodsEvent(goodsId, goodsType, goodsName, goodsbuyMoney, goodssetMoney, goodsunlockLevel, maxKucun, nowKucun);
        }
    }
    public int GoodsUnlockLevel
    {
        get
        {
            return goodsunlockLevel;
        }
        set
        {
            goodssetMoney = value;
            updateDataGoodsEvent(goodsId, goodsType, goodsName, goodsbuyMoney, goodssetMoney, goodsunlockLevel, maxKucun, nowKucun);
        }
    }
    public int MaxKuCun
    {
        get
        {
            return maxKucun;
        }
        set
        {
            maxKucun = value;
            updateDataGoodsEvent(goodsId, goodsType, goodsName, goodsbuyMoney, goodssetMoney, goodsunlockLevel, maxKucun, nowKucun);
        }
    }
    public int NowKuCun
    {
        get
        {
            return nowKucun;
        }
        set
        {
            nowKucun = value;
            updateDataGoodsEvent(goodsId, goodsType, goodsName, goodsbuyMoney, goodssetMoney, goodsunlockLevel, maxKucun, nowKucun);
        }
    }
}
