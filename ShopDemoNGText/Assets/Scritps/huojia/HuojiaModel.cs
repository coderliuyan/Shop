using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Model是双向的，中间的。
public class HuojiaModel : MonoBehaviour {

    //public event  UpdateDataEventHandle updateDataEvent;
    //货架编号
    private int huojiaId;
    //货架名称
    private string huojiaName;
    //货架等级
    private int huojiaLevel;
    //货架类型
    private string  huojiaType;
    //购买货架金币
    private int huojiaMoney;
    //货架售货能力
    private int huojiaPower;
    //货架容量
    private int huojiaScale;
    //货架升级时间
    private int huojiaTime;

    //货架所放地板的Id.
    private int floorId;

    //货物Id.
    public short   huowuId;
    //存放货物的List
    private List<int> _CunfanghuowuId;
    //货物的数量
    private int huowuNum;




    TableValue GoodsData;
    public int HuojiaId
    {
        get
        {
            return huojiaId;
        }
        set
        {
            huojiaId = value;
           //updateDataEvent(huojiaId, huojiaName,huojiaLevel, huojiaMoney, huojiaPower, huojiaScale);
        }
    }
    public string HuojiaName
    {
        get
        {
            return huojiaName;
        }
        set
        {
            huojiaName = value;
           // updateDataEvent(huojiaId, huojiaName,huojiaLevel,huojiaMoney, huojiaPower, huojiaScale);
        }
    }
    public int HuojiaLevel
    {
        get
        {
            return huojiaLevel;
        }
        set
        {
            huojiaLevel = value;
           // updateDataEvent(huojiaId, huojiaName, huojiaLevel, huojiaMoney, huojiaPower, huojiaScale);
        }
    }
    public string HuojiaType
    {
        get
        {
            return huojiaType;
        }
        set
        {
           huojiaType = value;
        //  updateDataEvent(huojiaId, huojiaName, huojiaLevel, huojiaMoney, huojiaPower, huojiaScale);
        }
    }
    public int HuojiaMoney
    {
        get
        {
            return huojiaMoney;
        }
        set
        {
            huojiaMoney = value;
          // updateDataEvent(huojiaId, huojiaName, huojiaLevel, huojiaMoney, huojiaPower, huojiaScale);
        }
    }
    public int HuojiaPower
    {
        get
        {
            return huojiaPower;
        }
        set
        {
            huojiaPower = value;
          // updateDataEvent(huojiaId, huojiaName, huojiaLevel, huojiaMoney, huojiaPower, huojiaScale);
        }
    }
    public int HuojiaScale
    {
        get
        {
            return huojiaScale;
        }
        set
        {
            huojiaScale = value;
          // updateDataEvent(huojiaId, huojiaName, huojiaLevel, huojiaMoney, huojiaPower, huojiaScale);
        }
    }
    public int HuojiaTime
    {
        get
        {
            return huojiaTime;
        }
        set
        {
            huojiaTime = value;
            // updateDataEvent(huojiaId, huojiaName, huojiaLevel, huojiaMoney, huojiaPower, huojiaScale);
        }
    }
    public int FloorId
    {
        get
        {
            return floorId;
        }
        set
        {
            floorId = value;
        }
    }

   public void BuHuo()
    {
        huowuId = FindGoodsType(huojiaType);
       {
        Debug.Log("ggghhhhhuowu"+huowuId);
       }
        if (!APIData.ShopStock.ContainsKey(huowuId))
        {
            Debug.LogError("这个字典里 么有  " + huowuId);
            return;
        }
        Debug.Log("存放的获取Id为："+huowuId);
        Debug.Log("货物类型为："+huojiaType);
        //_num = huojiaScale;
        APIData.ShopStock[huowuId] = APIData.ShopStock[huowuId] - huojiaScale;
        APIData.SaveLocalDate(huowuId.ToString(), APIData.ShopStock[huowuId]);
    }
   void Start()
   {
        Object GoodsObj = Resources.Load("Xml/DataType_Goods");
        XmlHelper.Instance.LoadFile("DataType_Goods", GoodsObj);
        GoodsData = XmlHelper.Instance.ReadFile("DataType_Goods");
   }
   public void ChangeGoods()
   {
 
   }
   public void AddLevel()
   {
       huojiaId += 1;
   }
    /// <summary>
    /// 返回跟货架类型相同的货物的id.
    /// </summary>
    /// <param name="huojiaType"></param>
    /// <returns></returns>
   short FindGoodsType(string huojiaType)
   {
       short goodId=0;
       foreach (var item in APIData.ShopStock)
       {
           string Goodstype=string.Empty;
           Goodstype = GoodsData.GetString(item.Key,"type");
           if (Goodstype == huojiaType)
           {
               goodId= item.Key;
               break ;
           }
       }
       return goodId;
   }
}
