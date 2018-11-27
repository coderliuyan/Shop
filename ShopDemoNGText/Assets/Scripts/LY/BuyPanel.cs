using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPanel : MonoBehaviour {

    #region 菜单切换栏
    private string shuiguoBtnPath = @"BuyPanel/Jinhuojiemian/shuiguoButton";
    UIButton shuiguoBtn;

    private string yinliaoBtnPath = @"BuyPanel/Jinhuojiemian/yinliaoButton";
    UIButton yinliaoBtn;

    private string shucaiBtnPath = @"BuyPanel/Jinhuojiemian/ShucaiButton";
    UIButton shucaiBtn;

    private string riyongBtnPath = @"BuyPanel/Jinhuojiemian/RiyongButton";
    UIButton riyongBtn;

    private string tiyuBtnPath = @"BuyPanel/Jinhuojiemian/TiyuButton";
    UIButton tiyuBtn;

    private string dianziBtnPath = @"BuyPanel/Jinhuojiemian/DianziButton";
    UIButton dianziBtn;

    private string shechipinBtnPath = @"BuyPanel/Jinhuojiemian/shechipinButton";
    UIButton shechipinBtn;

    //退出按钮
    private string quitBtnPath = @"BuyPanel/Quit_Button";
    UIButton quitBtn;

    //结算按钮
    private string jiesuanBtnPath = @"BuyPanel/Gouwuche/jisuan";
    UIButton jiesuanBtn;

    #endregion

    #region 货物的父节点
    //主scroll view 的父节点
    private string huowuPath = @"BuyPanel/HuowuType_Scroll View/ShuiguoLei";
    Transform huowuUI;

    private string gouwuCheParentPath = @"BuyPanel/Gouwuche/Buy_Scroll View/BuyCar";
    Transform gouwuCheTran;
    #endregion
    #region 二级购买页面

    //二级页面
    private string secondPanelPath = @"BuySencondPanel";
    Transform secondPanelTran;

    //加入购物车的按钮
    private string addGouwucheBtnPath = @"BuySencondPanel/jiemian/Gouwuche";
    UIButton addGouWuCheBtn;

    //二级页面的退出按钮
    private string quickBtnPath = @"BuySencondPanel/Quit_Button";
    UIButton quickBtn;

    //二级页面的加 按钮
    private string addNumberBtnPath = @"BuySencondPanel/Num_Lable_Sum/Button_jia+";
    UIButton addNumBtn;
    //二级页面的减 按钮
    private string reduceNumberBtnPath = @"BuySencondPanel/Num_Lable_Sum/Button_jian-";
    UIButton reduceNumBtn;

    //显示货物数量的label
    private string huowuNumberLabelPath = @"BuySencondPanel/Num_Lable_Sum/Num_Lable";
    UILabel huowuNumberLabel;

    //显示金钱的label 
    private string moneyLabelPath = @"BuySencondPanel/Money_Lable/Lable";
    UILabel moneyLabel;

    #endregion

    #region 临时变量 和显示临时变量的label
    [HideInInspector] public int totalPrice = 0; //总价格
    [HideInInspector] public int huowuNumer = 0; //货物数量
    [HideInInspector] public int goodsPrice = 0; //货物单价
    [HideInInspector] public int currentPrice = 0; //当前价格

    private string totalPriceLabelPath = @"BuyPanel/Gouwuche/heji_lable/heji/ajinbiNum";
    UILabel totalPriceLabel;

#endregion


    // Use this for initialization
    void Start () {
        InitPanelComponent();
	}

    void InitPanelComponent()
    {
        shuiguoBtn = transform.Find(shuiguoBtnPath).GetComponent<UIButton>();
        yinliaoBtn = transform.Find(yinliaoBtnPath).GetComponent<UIButton>();
        shucaiBtn = transform.Find(shucaiBtnPath).GetComponent<UIButton>();
        riyongBtn = transform.Find(riyongBtnPath).GetComponent<UIButton>();
        tiyuBtn = transform.Find(tiyuBtnPath).GetComponent<UIButton>();
        dianziBtn = transform.Find(dianziBtnPath).GetComponent<UIButton>();
        shechipinBtn = transform.Find(shechipinBtnPath).GetComponent<UIButton>();
        huowuUI = transform.Find(huowuPath);
        gouwuCheTran = transform.Find(gouwuCheParentPath);
        totalPriceLabel = transform.Find(totalPriceLabelPath).GetComponent<UILabel>();
        quitBtn = transform.Find(quitBtnPath).GetComponent<UIButton>();
        quitBtn.onClick.Add(new EventDelegate(()=> { ClickQuitButton(); }));
        jiesuanBtn = transform.Find(jiesuanBtnPath).GetComponent<UIButton>();
        jiesuanBtn.onClick.Add(new EventDelegate(()=> { ClickJieSuanButton(); }));


        //二级页面
        secondPanelTran = transform.Find(secondPanelPath);
        addGouWuCheBtn = transform.Find(addGouwucheBtnPath).GetComponent<UIButton>();
        quickBtn = transform.Find(quickBtnPath).GetComponent<UIButton>();
        quickBtn.onClick.Add(new EventDelegate(() => { ShowPanelWithTransform(secondPanelTran, false); }));
        addNumBtn = transform.Find(addNumberBtnPath).GetComponent<UIButton>();
        addNumBtn.onClick.Add(new EventDelegate(()=> { AddNumButton(); } ));
        huowuNumberLabel = transform.Find(huowuNumberLabelPath).GetComponent<UILabel>();
        moneyLabel = transform.Find(moneyLabelPath).GetComponent<UILabel>();

        shuiguoBtn.onClick.Add(new EventDelegate(() => { ChooseTypeButton("水果类"); }));
        shucaiBtn.onClick.Add(new EventDelegate(() => { ChooseTypeButton("蔬菜类"); }));
        yinliaoBtn.onClick.Add(new EventDelegate(() => { ChooseTypeButton("饮料类"); }));
        riyongBtn.onClick.Add(new EventDelegate(() => { ChooseTypeButton("日用品类"); }));
        tiyuBtn.onClick.Add(new EventDelegate(() => { ChooseTypeButton("体育用品类"); }));
        dianziBtn.onClick.Add(new EventDelegate(() => { ChooseTypeButton("电子商品类"); }));
        shechipinBtn.onClick.Add(new EventDelegate(() => { ChooseTypeButton("奢侈品类"); }));

        //默认是水果界面
        ChooseTypeButton("水果类");
    }

    //点击了结算按钮

    void ClickJieSuanButton()
    {

#if TEST
        Player.GoldNum = 1000;
#endif


        //结算 , 钱不够 提示 - > message panel , 钱够 结算 -> 存入shop  stock
        if(totalPrice > Player.GoldNum){
            //钱不够 
            DataManager.Instance.msgText = @"钱不够呀,请减少货物或充值.";
            UIManager.Instance.ShowMessagePanel();
            return;
        }

        //没花钱
        if(totalPrice == 0){
            DataManager.Instance.msgText = @"你啥都没买呀,是不是脑子瓦头了?";
            UIManager.Instance.ShowMessagePanel();
            return;
        }

        //钱够,要把货物买下来 ,减少钱, 货物存入 shop stock . 本地保存,更新UI. 清空购物车,数据清零 ,提示购买成功
        Player.GoldNum -= totalPrice;
        //提取数据进行存储
        for (int i = 0; i < gouwuCheTran.childCount; i++)
        {
            short _Linid = short.Parse(gouwuCheTran.GetChild(i).gameObject.name);
            string numstring = gouwuCheTran.GetChild(i).gameObject.transform.Find("FirstNameNum").GetComponent<UILabel>().text;
            int numInt1 = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(numstring, @"[^0-9]+", ""));
            //SaveGoodsNum(_Linid, numInt1);
            //CangkuUI(_Linid, numInt1);

            if (Player.ShopStock.ContainsKey(_Linid))
            {
                Player.ShopStock[_Linid] += numInt1;
            }
            else
            {
                Player.ShopStock.Add(_Linid,numInt1);
            }
            SelectPanel.selectManager.CahngkuUI(_Linid,numInt1);




        }
        Player.SavePlayerData();

        for (int i = 0; i < gouwuCheTran.transform.childCount; i++)
        {
            Destroy(gouwuCheTran.transform.GetChild(i).gameObject);
        }


        totalPrice = 0;
        totalPriceLabel.text = "0";

        DataManager.Instance.msgText = @"购买成功!";
        UIManager.Instance.ShowMessagePanel();

    }


    //点击了退出按钮
    void ClickQuitButton()
    {
        gameObject.SetActive(false);
    }



    //添加货物数量按钮 
    void AddNumButton()
    {
        huowuNumer += 100;
        currentPrice = goodsPrice * huowuNumer;
        huowuNumberLabel.text = huowuNumer.ToString();
        moneyLabel.text = currentPrice.ToString();
    }


    void ClickAddGouWuChe(int goodsID)
    {
        //把当前的货物添加到 购物面板上
        Debug.Log("点击了 添加购物车按钮");
        string _name = secondPanelTran.Find("Name_Lable").GetComponent<UILabel>().text;
        string _Num = secondPanelTran.Find("Num_Lable_Sum").transform.Find("Num_Lable").GetComponent<UILabel>().text;
        string _price = secondPanelTran.Find("Money_Lable").transform.Find("Lable").GetComponent<UILabel>().text;
        if (int.Parse(_Num) <= 0)
        {
            Debug.Log("购买数量不能为0");
            return;
        }
        GameObject obj = ((GameObject)Instantiate(Resources.Load("UI/Buy_Lable")));
        obj.transform.SetParent(gouwuCheTran.transform);
        obj.transform.Find("FirstName").GetComponent<UILabel>().text = _name;
        obj.transform.Find("FirstNameNum").GetComponent<UILabel>().text = _Num + "个";
        obj.transform.Find("jinbi/ajinbiNum").GetComponent<UILabel>().text = _price;
        // obj.transform.Find("SetJiage").GetComponent<UILabel>().text = goodsSet.ToString();
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.Find("Quit").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { AddQuitClick(obj); }));
        gouwuCheTran.GetComponent<UIGrid>().enabled = true;
        secondPanelTran.gameObject.SetActive(false);
        SumPrice(int.Parse(_price));
        obj.name = goodsID.ToString();


    }


 

    public void AddQuitClick(GameObject _BuyLable)
    {
        string _priceJianText;//购物车内预制体价格。
        int _priceJianInt;
        _priceJianText = _BuyLable.transform.Find("jinbi/ajinbiNum").GetComponent<UILabel>().text;
        _priceJianInt = int.Parse(_priceJianText);
        Debug.Log("-------------- " + _priceJianText);
        JianPrice(_priceJianInt);
        Destroy(_BuyLable);
        gouwuCheTran.GetComponent<UIGrid>().enabled = true;
    }

    /// <summary>
    /// 删除一行后减少的金额
    /// </summary>
    /// <param name="_quitprice"></param>
    public void JianPrice(int _quitprice)
    {
        totalPrice -= _quitprice;
        totalPriceLabel.text = totalPrice.ToString();
    }

    /// <summary>
    /// 合计金额的方法
    /// </summary>
    public void SumPrice(int _price)
    {
        totalPrice += _price;
        totalPriceLabel.text = totalPrice.ToString();
    }


    /// <summary>
    /// 进货界面的选择类型方法
    /// </summary>
    /// <param name="type"></param>
    void ChooseTypeButton(string type)
    {
        byte fruitsNum = 0;
        DestoryChild(huowuUI);
        foreach (LineValue item in DataManager.Instance.goodsData)
        {
            short goodsId = short.Parse(item.lineName);
            string goodsType = item.GetString("type");
            string goodsName = item.GetString("name");
            int goodsBuy = item.GetInt("buy");
            int goodsSet = item.GetInt("sell");
            int goodsLevel = item.GetInt("level");
            if (goodsType == type)
            {
                GameObject obj = ((GameObject)Instantiate(Resources.Load("UI/pingguo")));
                obj.transform.SetParent(huowuUI.transform);
                obj.transform.Find("pingguoLabel").GetComponent<UILabel>().text = goodsName;
                obj.transform.Find("shoujia").GetComponent<UILabel>().text = goodsBuy.ToString();
                obj.transform.Find("SetJiage").GetComponent<UILabel>().text = goodsSet.ToString();
                obj.transform.Find("苹果").GetComponent<UISprite>().spriteName = goodsName;
                obj.transform.Find("苹果").GetComponent<UISprite>().MakePixelPerfect();
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.name = fruitsNum.ToString();
                fruitsNum += 1;
                obj.GetComponent<UIDragScrollView>().scrollView = huowuUI.transform.parent.GetComponent<UIScrollView>();

                //测试
#if TEST
                Player.PlayerLevel = 9;
#endif
                if (goodsLevel <= Player.PlayerLevel)
                {
                    obj.transform.Find("Button").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { BuyGoodsButton(goodsId); }));
                }
                else
                {
                    obj.transform.Find("Button").transform.Find("Label").GetComponent<UILabel>().text = "解锁";
                    //按钮是否有UI上的变化。
                    //关闭按钮功能。
                }
                //obj.transform.Find("Button").GetComponent<BtnOpen>()._GoodsId = goodsId;
                //obj.transform.Find("Button").GetComponent<BtnOpen>()._goodsdata = GoodsData;
            }
        }
        huowuUI.GetComponent<UIGrid>().enabled = true;
    }

    void BuyGoodsButton(short _GoodsId)
    {
        ShowPanelWithTransform(secondPanelTran, true);
        UILabel _goodsName = secondPanelTran.transform.Find("Name_Lable").GetComponent<UILabel>();
        UILabel _goodsType = secondPanelTran.transform.Find("Leixing_Lable").transform.Find("Lable").GetComponent<UILabel>();
        _goodsName.text = DataManager.Instance.goodsData.GetString(_GoodsId, "name");
        _goodsType.text = DataManager.Instance.goodsData.GetString(_GoodsId, "type");
        string goodsPriceString = DataManager.Instance.goodsData.GetString(_GoodsId, "buy");
        goodsPrice = int.Parse(goodsPriceString);
        secondPanelTran.transform.Find("TuDiban/Tubiao").GetComponent<UISprite>().spriteName = _goodsName.text;
        ResetZero();
        //_goodsPrice.text = (goodspriceInt * _num_int).ToString();
        addGouWuCheBtn.onClick.Remove(new EventDelegate(() => { ClickAddGouWuChe(_GoodsId); }));
        addGouWuCheBtn.onClick.Add(new EventDelegate(() => { ClickAddGouWuChe(_GoodsId); }));
    }

    void ShowPanelWithTransform(Transform tran,bool isShow = true){
        tran.gameObject.SetActive(isShow);
    }

    void ResetZero()
    {
        huowuNumer = 0;
        currentPrice = 0;
        huowuNumberLabel.text = huowuNumer.ToString();
        moneyLabel.text =currentPrice.ToString();
    }

    void DestoryChild(Transform _destory)
    {
        for (int i = 0; i < _destory.transform.childCount; i++)
        {
            Destroy(_destory.transform.GetChild(i).gameObject);
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
