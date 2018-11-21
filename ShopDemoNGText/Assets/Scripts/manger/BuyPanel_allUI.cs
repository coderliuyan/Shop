using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPanel_allUI : MonoBehaviour
{

    UIButton _jinhuo;//进货按钮
    Transform _BuyPanel;//进货界面
    UIButton _FuritType;//进货水果类型按钮
    UIButton _CocoType;//饮料类型按钮
    UIButton _VegatableType;//蔬菜类型按钮
    UIButton _CommodityType;//日用品类型按钮
    UIButton _SportsGoodsType;//体育用品类型按钮
    UIButton _ElectronicType;//电子类型按钮
    UIButton _LuxuryType;//奢侈品类型按钮
    TableValue GoodsData;//货物Xml表的数据
    Transform _huowuType;//货物类型节点
    Transform _BuySencoondPanel;//进货二级界面，即加入购物车界面
    UILabel _goodsPrice;//购物车界面单个货物价格
    string goodsPriceString;//购物车界面单个货物价格
    int goodspriceInt;//购物车界面单个货物价格
    UILabel _goodsNum;//购物车里物品数量
    UIButton _goodsNumAdd;//购物车里物品增加按钮
    UIButton _goodsNumReduce;//购物车里物品减少按钮
    GameObject _gouwuChe;//进货界面购物车板块
    Transform _buyCar;//进货界面购物车板块的预制体节点
    UILabel _jisuan;//结算
    int _intPrice;//结算int
    UIButton _AddBuyCar;//加入购物车按钮
    UIButton _jiesuanButton;//总结算按钮
    UIButton _BuyPanelQuit;//进货界面退出按钮
    UIButton _BuySencondPanelQuit;//进货二级界面退出按钮
    Transform _CangkuUI;//仓库ui存放的界面。
	// Use this for initialization
	void Start () 
    {
        Object GoodsObj = Resources.Load("Xml/DataType_Goods");
        XmlHelper.Instance.LoadFile("DataType_Goods", GoodsObj);
        GoodsData = XmlHelper.Instance.ReadFile("DataType_Goods");
        _jinhuo = transform.parent.Find("SelectPanel/ShangCheng").transform.Find("jinhuoButton").GetComponent<UIButton>();   
        _BuyPanel = transform.Find("BuyPanel");
        _gouwuChe = GameObject.Find("BuyPanel/Gouwuche");
        _buyCar = _gouwuChe.transform.Find("Buy_Scroll View/BuyCar");    
        _jisuan = _BuyPanel.transform.Find("Gouwuche/heji_lable/heji").transform.Find("ajinbiNum").GetComponent<UILabel>();
        _BuySencoondPanel = transform.Find("BuySencondPanel");
        _goodsPrice = _BuySencoondPanel.transform.Find("Money_Lable/Lable").GetComponent<UILabel>();
        _goodsNum = _BuySencoondPanel.transform.Find("Num_Lable_Sum/Num_Lable").GetComponent<UILabel>();  
        _huowuType = _BuyPanel.transform.Find("HuowuType_Scroll View/ShuiguoLei");
        _FuritType = _BuyPanel.transform.Find("Jinhuojiemian/shuiguoButton").GetComponent<UIButton>();
        _VegatableType = _BuyPanel.transform.Find("Jinhuojiemian/ShucaiButton").GetComponent<UIButton>();
        _CocoType=_BuyPanel.transform.Find("Jinhuojiemian/yinliaoButton").GetComponent<UIButton>();
        _CommodityType = _BuyPanel.transform.Find("Jinhuojiemian/RiyongButton").GetComponent<UIButton>();
        _SportsGoodsType = _BuyPanel.transform.Find("Jinhuojiemian/TiyuButton").GetComponent<UIButton>();
        _ElectronicType = _BuyPanel.transform.Find("Jinhuojiemian/DianziButton").GetComponent<UIButton>();
        _LuxuryType = _BuyPanel.transform.Find("Jinhuojiemian/shechipinButton").GetComponent<UIButton>();
        _jinhuo.onClick.Add(new EventDelegate(() => { OpenNewUIButton(_BuyPanel,true);}));
        _FuritType.onClick.Add(new EventDelegate(() => { ChooseTypeButton("水果类");}));
        _VegatableType.onClick.Add(new EventDelegate(() => { ChooseTypeButton("蔬菜类");}));
        _CocoType.onClick.Add(new EventDelegate(() => { ChooseTypeButton("饮料类");}));
        _CommodityType.onClick.Add(new EventDelegate(() => { ChooseTypeButton("日用品类"); }));
        _SportsGoodsType.onClick.Add(new EventDelegate(() => { ChooseTypeButton("体育用品类"); }));
        _ElectronicType.onClick.Add(new EventDelegate(() => { ChooseTypeButton("电子商品类"); }));
        _LuxuryType.onClick.Add(new EventDelegate(() => { ChooseTypeButton("奢侈品类"); }));
        _goodsNumAdd = _BuySencoondPanel.transform.Find("Num_Lable_Sum/Button_jia+").GetComponent<UIButton>();
        _goodsNumReduce = _BuySencoondPanel.transform.Find("Num_Lable_Sum/Button_jian-").GetComponent<UIButton>();
        _jiesuanButton = _BuyPanel.transform.Find("Gouwuche/jisuan").GetComponent<UIButton>();
        _jiesuanButton.onClick.Add(new EventDelegate(() => { JiesuanButton();}));
        _AddBuyCar = _BuySencoondPanel.transform.Find("jiemian/Gouwuche").GetComponent<UIButton>();      
        _goodsNumAdd.onClick.Add(new EventDelegate(() => { BuyCarAddNumButton(); }));
        _goodsNumReduce.onClick.Add(new EventDelegate(() => { BuyCarReduceNumButton();}));
        _BuyPanelQuit = _BuyPanel.transform.Find("Quit_Button").GetComponent<UIButton>();
        _BuySencondPanelQuit = _BuySencoondPanel.transform.Find("Quit_Button").GetComponent<UIButton>();
        _BuyPanelQuit.onClick.Add(new EventDelegate(() => { OpenNewUIButton(_BuyPanel,false); }));
        _BuySencondPanelQuit.onClick.Add(new EventDelegate(() => { OpenNewUIButton(_BuySencoondPanel, false); }));
        _CangkuUI = transform.parent.Find("SelectPanel/ShangCheng/CangkuButton/cangkuUI/cunfangUI");
        
	}
    /// <summary>
    /// 打开新界面
    /// </summary>
    /// <param name="_Newpanel"></param>
    public void OpenNewUIButton(Transform _Newpanel,bool _OpenOrClose)
    {
        _Newpanel.gameObject.SetActive(_OpenOrClose);
    }
    /// <summary>
    /// 进货界面的选择类型方法
    /// </summary>
    /// <param name="type"></param>
    void ChooseTypeButton(string type)
    {
        byte fruitsNum = 0;
        DestoryChild(_huowuType);
        foreach (LineValue item in GoodsData)
        {
          short goodsId = short.Parse(item.lineName);
          string   goodsType = item.GetString("type");
          string  goodsName = item.GetString("name");
          int  goodsBuy = item.GetInt("buy");
          int   goodsSet = item.GetInt("sell");
          int  goodsLevel = item.GetInt("level");
            if (goodsType == type)
            {
                GameObject obj = ((GameObject)Instantiate(Resources.Load("UI/pingguo")));
                obj.transform.SetParent(_huowuType.transform);
                obj.transform.Find("pingguoLabel").GetComponent<UILabel>().text = goodsName;
                obj.transform.Find("shoujia").GetComponent<UILabel>().text = goodsBuy.ToString();
                obj.transform.Find("SetJiage").GetComponent<UILabel>().text = goodsSet.ToString();
                obj.transform.Find("苹果").GetComponent<UISprite>().spriteName = goodsName;
                obj.transform.Find("苹果").GetComponent<UISprite>().MakePixelPerfect();
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.name = fruitsNum.ToString();
                fruitsNum += 1;
                obj.GetComponent<UIDragScrollView>().scrollView = _huowuType.transform.parent.GetComponent<UIScrollView>();             
                if(goodsLevel<=APIData.PlayerLevel)
                {
                 obj.transform.Find("Button").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { BuyGoodsButton(goodsId); })); 
                }
                else
                {
                    obj.transform.Find("Button").transform.Find("Label").GetComponent<UILabel>().text="解锁";
                    //按钮是否有UI上的变化。
                    //关闭按钮功能。
                }
                //obj.transform.Find("Button").GetComponent<BtnOpen>()._GoodsId = goodsId;
                //obj.transform.Find("Button").GetComponent<BtnOpen>()._goodsdata = GoodsData;
            }
        }
        _huowuType.GetComponent<UIGrid>().enabled = true;
    }
    /// <summary>
    /// 删除进货界面某一类所有的商品，以便刷新新的类型
    /// </summary>
    void DestoryChild(Transform _destory)
    {
        for (int i = 0; i < _destory.transform.childCount; i++)
        {
            Destroy(_destory.transform.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// 进货界面购买按钮上添加的方法
    /// </summary>
    /// <param name="_GoodsId"></param>
    void BuyGoodsButton(short _GoodsId)
    {
        OpenNewUIButton(_BuySencoondPanel,true);
      UILabel _goodsName = _BuySencoondPanel.transform.Find("Name_Lable").GetComponent<UILabel>();
      UILabel _goodsType = _BuySencoondPanel.transform.Find("Leixing_Lable").transform.Find("Lable").GetComponent<UILabel>();
        _goodsName.text = GoodsData.GetString(_GoodsId, "name");
        _goodsType.text = GoodsData.GetString(_GoodsId, "type");
        goodsPriceString = GoodsData.GetString(_GoodsId, "buy");
        goodspriceInt = int.Parse(goodsPriceString);
        _BuySencoondPanel.transform.Find("TuDiban/Tubiao").GetComponent<UISprite>().spriteName = _goodsName.text;
        ResetZero();
        _goodsPrice.text = (goodspriceInt * _num_int).ToString();
        _AddBuyCar.onClick.Remove(new EventDelegate(() => { BornBuyLable(_GoodsId); }));
        _AddBuyCar.onClick.Add(new EventDelegate(() => { BornBuyLable(_GoodsId); }));
    }
    /// <summary>
    /// 购物车界面数量增加的按钮方法
    /// </summary>
    int _num_int;//购物车物品数量
    int sum_Price;//购物车总结金额。
    void BuyCarAddNumButton()
    {            
        _num_int += 1000;
        sum_Price = goodspriceInt * _num_int;
        _goodsNum.text = _num_int.ToString();
        _goodsPrice.text = sum_Price.ToString();
    }
    /// <summary>
    /// 购物车界面数量减少的按钮方法
    /// </summary>
    void BuyCarReduceNumButton()
    {
        if (_num_int>0)
        {
            _num_int -= 1000;
            sum_Price = goodspriceInt * _num_int;
            _goodsNum.text = _num_int.ToString();
            _goodsPrice.text = sum_Price.ToString();
        }
    }
    /// <summary>
    /// 再次按购买新的物品数量归零。
    /// </summary>
    void ResetZero()
    {
        _num_int = 0;
        _goodsNum.text = _num_int.ToString();
    }
    /// <summary>
    /// 二级界面加入购物车按钮方法
    /// </summary>
    void BornBuyLable(short _id)
    {    
       string  _name =_BuySencoondPanel.transform.Find("Name_Lable").GetComponent<UILabel>().text;
       string  _Num = _BuySencoondPanel.transform.Find("Num_Lable_Sum").transform.Find("Num_Lable").GetComponent<UILabel>().text;
       string _price = _BuySencoondPanel.transform.Find("Money_Lable").transform.Find("Lable").GetComponent<UILabel>().text;
        if (int.Parse(_Num) <= 0)
        {
            Debug.Log("购买数量不能为0");
            return;
        }
        GameObject obj = ((GameObject)Instantiate(Resources.Load("UI/Buy_Lable")));
        obj.transform.SetParent(_buyCar.transform);
        obj.transform.Find("FirstName").GetComponent<UILabel>().text = _name;
        obj.transform.Find("FirstNameNum").GetComponent<UILabel>().text = _Num + "个";
        obj.transform.Find("jinbi/ajinbiNum").GetComponent<UILabel>().text = _price;
        // obj.transform.Find("SetJiage").GetComponent<UILabel>().text = goodsSet.ToString();
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.Find("Quit").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { AddQuitClick(obj); }));
        _buyCar.GetComponent<UIGrid>().enabled = true;
        _BuySencoondPanel.gameObject.SetActive(false);
        SumPrice(int.Parse(_price));
        obj.name = _id.ToString();
    }
    public void AddQuitClick(GameObject _BuyLable)
    {
        string _priceJianText;//购物车内预制体价格。
        int _priceJianInt;
        _priceJianText = _BuyLable.transform.Find("jinbi/ajinbiNum").GetComponent<UILabel>().text;
        _priceJianInt = int.Parse(_priceJianText);
        JianPrice(_priceJianInt);
        Destroy(_BuyLable);
        _buyCar.GetComponent<UIGrid>().enabled = true;
    }

    /// <summary>
    /// 合计金额的方法
    /// </summary>
    public void SumPrice(int _price)
    {
        _intPrice += _price;
        _jisuan.text = _intPrice.ToString();
    }
    /// <summary>
    /// 删除一行后减少的金额
    /// </summary>
    /// <param name="_quitprice"></param>
    public void JianPrice(int _quitprice)
    {
        _intPrice -= _quitprice;
        _jisuan.text = _intPrice.ToString();
    }
    void CloseAccount()
    {

    }
    /// <summary>
    /// 结算购物车内已添加商品的金额。
    /// </summary>
    void JiesuanButton()
    {      
        DestoryChild(_buyCar);
        OpenNewUIButton(_BuyPanel,false);
        Debug.Log("库存金币现在为："+APIData.GoldNum);
        Debug.Log("BuyCar"+_buyCar.childCount);
        transform.parent.Find("SelectPanel").GetComponent<SelectPanelUI>().ReduceMoneyButton(_intPrice);
        //1.打印提示：库存变化，总金币的变化
        //2.存本地数据。
        for (int i = 0; i <_buyCar.childCount; i++)
        {
           short _Linid =short.Parse(_buyCar.GetChild(i).gameObject.name);
           string  numstring=_buyCar.GetChild(i).gameObject.transform.Find("FirstNameNum").GetComponent<UILabel>().text;
           int numInt1 = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(numstring, @"[^0-9]+", ""));
           SaveGoodsNum(_Linid,numInt1);
           CangkuUI(_Linid,numInt1);
        }
        _intPrice = 0;
        _jisuan.text = _intPrice.ToString();
    }
    void SaveGoodsNum(short id,int num)
    {
        if (!APIData.ShopStock.ContainsKey(id))
        {
            Debug.LogError("这个字典里 么有  " + id);
            return;
        }
        APIData.ShopStock[id]+=num;
        //string _goodsName;
        //_goodsName = GoodsData.GetString(id,"name");
        APIData.SaveLocalDate(id.ToString(), APIData.ShopStock[id]);
        Debug.Log("现在库存id："+id+"库存数量："+num);
    }
    /// <summary>
    /// 仓库UI界面
    /// </summary>

    void CangkuUI(short _id, int _num)
    {
        transform.parent.Find("SelectPanel").GetComponent<SelectPanelUI>().CahngkuUI( _id,  _num);
    }
}
