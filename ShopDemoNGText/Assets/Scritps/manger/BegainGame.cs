using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BegainGame : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
#if UNITY_EDITOR
        if (APIData.HaveLocalData(APIData.GoldName))
        {
            APIData.DeleteLocalDate(APIData.GoldName);
            APIData.DeleteLocalDate(APIData.PalyerLevelName);
            APIData.DeleteLocalDate(APIData.PalyerLevelExpName);
            APIData.DeleteLocalDate(APIData.ShopLevelExpName);
            APIData.DeleteLocalDate(APIData.ShopLevelName);
        }
#endif
    }

    private void OnClick()
    {
        Globe.nextSenceName = "MainSence5";
        SceneManager.LoadScene("loading");
        GetLocalData();
    }
    //string 类型的商品id,int类型的商品num;
    void GetLocalData()
    {
        if(APIData.HaveLocalData(APIData.GoldName)){
            //获取本地存储的金币。
            APIData.GoldNum = APIData.GetLocalDate(APIData.GoldName);
            //获取玩家等级
            APIData.PalyerLevelNum = APIData.GetLocalDate(APIData.PalyerLevelName);
            //获取玩家经验值
            APIData.PalyerLevelExp = APIData.GetLocalDate(APIData.PalyerLevelExpName);
            //获取店铺等级
            APIData.ShopLevelNum = APIData.GetLocalDate(APIData.PalyerLevelName);
            //获取店铺经验值
            APIData.ShopLevelExp = APIData.GetLocalDate(APIData.ShopLevelExpName);
        }else{
            APIData.GoldNum = 5000;
            APIData.PalyerLevelNum = 1;
            APIData.PalyerLevelExp = 0;
            APIData.ShopLevelNum = 1;
            APIData.ShopLevelExp = 0;
        }

       ////获取本地存储的金币。
        //APIData.GoldNum = APIData.GetLocalDate(APIData.GoldName);
        //if (APIData.GoldNum < 0)
        //{
        //    APIData.GoldNum = 5000;
        //}
        ////获取玩家等级
        //APIData.PalyerLevelNum = APIData.GetLocalDate(APIData.PalyerLevelName);
        //if (APIData.PalyerLevelNum < 0)
        //{
        //    APIData.PalyerLevelNum =1;
        //}
        ////获取玩家经验值
        //APIData.PalyerLevelExp = APIData.GetLocalDate(APIData.PalyerLevelExpName);
        //if (APIData.PalyerLevelExp < 0)
        //{
        //    APIData.PalyerLevelExp = 0;
        //}
        ////获取店铺等级
        //APIData.ShopLevelNum = APIData.GetLocalDate(APIData.PalyerLevelName);
        //if (APIData.ShopLevelNum < 0)
        //{
        //    APIData.ShopLevelNum = 1;
        //}
        ////获取店铺经验值
        //APIData.ShopLevelExp = APIData.GetLocalDate(APIData.ShopLevelExpName);
        //if (APIData.ShopLevelExp < 0)
        //{
        //    APIData.ShopLevelExp = 0;
        //}
           //获取本地存储的仓储  
        Object GoodsObj = Resources.Load("Xml/DataType_Goods");
        XmlHelper.Instance.LoadFile("DataType_Goods", GoodsObj);
        TableValue GoodsData = XmlHelper.Instance.ReadFile("DataType_Goods");
        foreach (LineValue item in GoodsData)
        {
           short goodsId = short.Parse(item.lineName);
            if(APIData.HaveLocalData(goodsId.ToString())){
                int _aaa = APIData.GetLocalDate(goodsId.ToString());//获取的本地存储物品的数量
                if (_aaa < 0)
                {
                    _aaa = 0;
                }
                APIData.ShopStock.Add(goodsId, _aaa);
            }
                    
        }
        Debug.Log(APIData.ShopStock.Count);
    }
}
