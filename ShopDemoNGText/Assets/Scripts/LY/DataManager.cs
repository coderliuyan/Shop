using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager> {


    public TableValue playerXml;
    public TableValue shopXml;
    public TableValue huojiaXml;
    public TableValue customerXml;
    public TableValue goodsData;
    public TableValue shopType;

    //记录解锁的 顾客ID
    public List<int> customerId = new List<int>();

    //记录解锁的货架ID
    public List<int> huojiaId = new List<int>();


    //msg label 中的提示信息
    public string msgText;


    // Use this for initialization
    void Start () {
        ReadXML();

        //给 customer id 和 huojia id 赋值 
        FuzhiID();
	}

    private void ReadXML()
    {
        playerXml = ReadExpXml("DataType_playerLevel");
        shopXml = ReadExpXml("DataType_shopLevel");
        huojiaXml = ReadExpXml("DataType_rackLevel");
        customerXml = ReadExpXml("DataType_cusLevel");
        goodsData = ReadExpXml("DataType_Goods");
        shopType = ReadExpXml("ShopType");
    }

    private void FuzhiID()
    {
        //通过商店的等级 解锁顾客
        int _shopid = 2000;
        if(Player.ShopLevel == 0)
        {
            _shopid += 1;
        }
        else
        {
            _shopid += Player.ShopLevel;
        }
        
        int cusid = shopXml.GetInt(_shopid, "cusId");
        foreach(LineValue a in shopXml)
        {
            int num = int.Parse(a.lineName);
            if(_shopid>= num)
            {
                int id = shopXml.GetInt(num, "cusId");
                customerId.Add(id);
            }
        }

        int maxCustomerIdIndex = customerId.Count - 1;
        int maxIDNumber = customerId[maxCustomerIdIndex];
        //通过顾客id 解锁货架
        foreach (LineValue a in customerXml)
        {
            int num = int.Parse(a.lineName);
            if(maxIDNumber >= num)
            {
                huojiaId.Add(customerXml.GetInt(num, "shelfId"));
            }
        }


        ///
        SelectPanel.selectManager.GetHuojiaUI();

  

    }


    TableValue ReadExpXml(string _DataName)
    {
        Object ShopExpObj = Resources.Load("Xml/" + _DataName);
        XmlHelper.Instance.LoadFile(_DataName, ShopExpObj);
        TableValue ShopExpData = XmlHelper.Instance.ReadFile(_DataName);
        return ShopExpData;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
