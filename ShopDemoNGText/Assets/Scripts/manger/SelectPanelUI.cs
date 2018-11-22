using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanelUI : MonoBehaviour {

    UILabel _AllMoneyLable;
    int _AllMoneyInt;
    Transform _BuyPanel;
    UIButton _BuyGoodsButton;
    UIButton _CangchuButton;
    Transform _CangchuUI;
    UIButton _ZhuangxiuButton;
    Transform _ZhuangxiuUI;
    UILabel _PlayerLevel;//获取玩家等级
    int _PlayerLevelInt;
    UILabel _PlayerExp;//玩家经验值
    int _PlayerExpInt;
    UILabel _ShopLevel;//获取店铺等级口碑
    int _ShopLevelInt;
    UILabel _ShopExp;//店铺经验值
    int _ShopExpInt;
    Transform _huojiaUIRoot;//获取货架UI节点
    int CustomerId;//获取对应等级的顾客id。
    int HuojiaId;//获取对应顾客的货架id.
    int _shopid;//店铺等级口碑id。
    int _playerid;//玩家等级id.
    TableValue _PlayerXml;
    TableValue _ShopXml;
    TableValue _HuojiaXml;
    TableValue _CustomerXml;
    TableValue GoodsData;
    Transform _BuyHuojiUIFirst;
    Transform _BuyHuojiaUISencond;
    UIButton _TurnButton;//旋转按钮。
    UIButton _MoveButton;//移动按钮。
    UIButton _WelcomeButton;//迎客按钮。
    Transform _CustomerFirstUi;//解锁顾客的一级界面。
    Transform _CustomerSencondUi;//解锁顾客的二级界面。
    Timer _timer;
    Transform _jiesuanUI;
    Transform _NoOpenPanelUI;
    UIButton _NoOpenPanelQuitButton;
    //不可用按钮
    UIButton _nongchangButton;
    UIButton _jiangbeiButton;
    UIButton _guanguangButton;
    UIButton _renwuButton;
    UIButton _fensiButton;
    UIButton _gongchangButton;
    UIButton _dianpuButton;
    UIButton _shangchengButton;
    UIButton _jieriButton;
    UIButton _koubeiButton;
    UIButton _CustomerButton;
    UIButton _yuangongButton;

    UIButton _ShangChengDaojuButton;//商城的道具按钮
    UIButton _ShangChengHaoyouButton;//商城的好友按钮
     //。。。。。。。

    List<GameObject> _huojialist = new List<GameObject>();
	// Use this for initialization
	void Start () 
    {
        //1 加载龙骨资源
        LongGuManager.GetInstance().LoadLongGuData("LongGuFemale");
        LongGuManager.GetInstance().LoadLongGuData("LongGuMale");
        _jiesuanUI = transform.parent.Find("jiesuanUI");
        _PlayerXml = ReadExpXml("DataType_playerLevel");
        _ShopXml = ReadExpXml("DataType_shopLevel");
        _HuojiaXml = ReadExpXml("DataType_rackLevel");
        _CustomerXml = ReadExpXml("DataType_cusLevel");
        GoodsData = ReadExpXml("DataType_Goods");
        _timer = Timer.createTimer("Timer");
        _NoOpenPanelUI = transform.parent.Find("暂未开放Panel");
        _NoOpenPanelQuitButton = _NoOpenPanelUI.transform.Find("mianbanSprite/QuitButton").GetComponent<UIButton>();
     //不可用按钮
        _nongchangButton = transform.Find("ZhongjianButton/nongchang").GetComponent<UIButton>();
        _jiangbeiButton = transform.Find("ZhongjianButton/jiangbei").GetComponent<UIButton>();
        _guanguangButton = transform.Find("ZhongjianButton/guanguang").GetComponent<UIButton>();
        _renwuButton = transform.Find("ZhongjianButton/renwu").GetComponent<UIButton>();
        _fensiButton = transform.Find("ZhongjianButton/fensi").GetComponent<UIButton>();
        _gongchangButton = transform.Find("ZhongjianButton/gongchang").GetComponent<UIButton>();
        _dianpuButton = transform.Find("ZhongjianButton/dainpu").GetComponent<UIButton>();
        _shangchengButton = transform.Find("ZhongjianButton/shangcheng").GetComponent<UIButton>();
        _jieriButton = transform.Find("ZhongjianButton/jieri").GetComponent<UIButton>();
        _koubeiButton = transform.Find("ZhongjianButton/koubei").GetComponent<UIButton>();
        _CustomerButton = transform.Find("ZhongjianButton/Customer").GetComponent<UIButton>();
        _yuangongButton = transform.Find("ZhongjianButton/yuangong").GetComponent<UIButton>();

        _ShangChengDaojuButton = transform.Find("ShangCheng/daojuButton").GetComponent<UIButton>();
        _ShangChengHaoyouButton = transform.Find("ShangCheng/haoyouButton").GetComponent<UIButton>();


        _nongchangButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI,true);}));
        _jiangbeiButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _guanguangButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _renwuButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _fensiButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _gongchangButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _dianpuButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _shangchengButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _jieriButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _koubeiButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _CustomerButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _yuangongButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));

        _ShangChengDaojuButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));
        _ShangChengHaoyouButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, true); }));

        _NoOpenPanelQuitButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_NoOpenPanelUI, false); }));



        foreach (LineValue item in _CustomerXml)
        {
           // Debug.LogError("************"+item.lineName);
        }
        _AllMoneyLable = transform.Find("ShangLan/Money_Jingbi/Money").GetComponent<UILabel>();
        _AllMoneyInt = int.Parse(_AllMoneyLable.text);
        _BuyPanel = transform.parent.Find("BuyPanel_all");
        _PlayerLevel = transform.Find("ShangLan/PalyerLevelNum_Sprite/PalyerLevelNum").GetComponent<UILabel>();
        _PlayerLevelInt = int.Parse(_PlayerLevel.text);
        _PlayerExp = transform.Find("ShangLan/PalyerLevelExp_Sprite/PalyerLevelExpNum").GetComponent<UILabel>();
        _PlayerExpInt = int.Parse(_PlayerExp.text);
        _ShopLevel = transform.Find("ZhongjianButton/dainpu/ShopLevelNum_Sprite/ShopLevelNum").GetComponent<UILabel>();
        _ShopLevelInt = int.Parse(_ShopLevel.text);
        _ShopExp = transform.Find("ZhongjianButton/dainpu/ShopLevelExpNum_Sprite/ShopLevelExpNum").GetComponent<UILabel>();
        _ShopExpInt = int.Parse(_ShopExp.text);
        _BuyGoodsButton = transform.Find("ShangCheng/jinhuoButton").GetComponent<UIButton>();
        _CangchuButton = transform.Find("ShangCheng/CangkuButton").GetComponent<UIButton>();
        _ZhuangxiuButton = transform.Find("ShangCheng/ZhuangxiuButton").GetComponent<UIButton>();
        _ZhuangxiuUI = transform.Find("ShangCheng/ZhuangxiuButton/Tubiao");
        _CangchuUI = _CangchuButton.transform.Find("Diban/cangkuUI");
        _BuyHuojiUIFirst = transform.parent.Find("BuyHuojia").transform.Find("YujiMianBan");
        _BuyHuojiaUISencond = transform.parent.Find("BuyHuojia").transform.Find("QueDingMianBan");
        _TurnButton = transform.Find("ShangCheng/GameBeginCstomer/XuanzhuanButton").GetComponent<UIButton>();
        _MoveButton=transform.Find("ShangCheng/GameBeginCstomer/MoveButton").GetComponent<UIButton>();
        _WelcomeButton=transform.Find("ShangCheng/GameBeginCstomer/WelcomButton").GetComponent<UIButton>();
        _ZhuangxiuButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_ZhuangxiuUI, true); }));
        _ZhuangxiuButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_CangchuUI,false); }));   
        _BuyGoodsButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_BuyPanel,true);}));
        _CangchuButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_CangchuUI, true); }));
        _CangchuButton.onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_ZhuangxiuUI, false); }));
        _BuyHuojiUIFirst.Find("NoButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_BuyHuojiaUISencond.parent, false); }));
        _BuyHuojiaUISencond.Find("NoButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_BuyHuojiaUISencond.parent, false); }));
        _BuyHuojiUIFirst.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_BuyHuojiUIFirst, false); }));
        _BuyHuojiUIFirst.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_BuyHuojiaUISencond, true); }));
        _TurnButton.onClick.Add(new EventDelegate(() => { TurnButton();}));      
        _WelcomeButton.onClick.Add(new EventDelegate(() => { WelcomeButton();}));
        _CustomerFirstUi = transform.parent.Find("CustomerSpritefirst");
        _CustomerSencondUi = transform.parent.Find("CustomerSpritesencond");
        _CustomerFirstUi.transform.Find("QuedingButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_CustomerSencondUi, true); }));
        _CustomerFirstUi.transform.Find("QuedingButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_CustomerFirstUi, false); }));
        _CustomerSencondUi.transform.Find("QuedingButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_CustomerSencondUi, false); }));
        GetInit();
        GameDataManger.Instance._huojiaUIId.Add(10001);
        GetHuojiaUI();      
      //AddShopExpNum(100);
        BornGetMoneyPenplo();
        JiesuoDimian();
	}
    void Update()
    {
   //     _MoveButton.onClick.Add(new EventDelegate(() => { MoveButton(); }));
    }
    /// <summary>
    /// 结算减少金币方法
    /// </summary>
   public  void ReduceMoneyButton(int _reducemoney)
    {
        _AllMoneyInt -= _reducemoney;
        _AllMoneyLable.text = _AllMoneyInt.ToString();
       // Debug.Log(_AllMoneyInt+"hehhehe"+_reducemoney);
        SaveMoney(APIData.GoldName, _AllMoneyInt);
    }
   public  void AddMonryButtoon(int _addmoney)
    {
        _AllMoneyInt += _addmoney;
        _AllMoneyLable.text = _AllMoneyInt.ToString();
        SaveMoney(APIData.GoldName,_AllMoneyInt);
    }
    /// <summary>
    /// 金币的本地存储数据
    /// </summary>
   void SaveMoney(string _name,int _Num)
   {
       APIData.SaveLocalDate(_name, _Num);
   }
 public  void GetInit()
   {
       _AllMoneyInt=APIData.GoldNum;
       _AllMoneyLable.text =_AllMoneyInt.ToString();
       _PlayerLevelInt = APIData.PlayerLevel;
       _PlayerLevel.text = _PlayerLevelInt.ToString();
       _ShopLevelInt = APIData.ShopLevel;
       _ShopLevel.text = _ShopLevelInt.ToString();  
   }
    /// <summary>
    /// 店铺经验数量增加方法
    /// </summary>
    /// <param name="_addExp"></param>
 
public void AddShopExpNum(int _addExp)
 {
     //读表获取经验值
     //跟当前经验对比是否需要升级口碑等级。
     //口碑等级升级。根据口碑等级表，获取应解锁顾客的id,再根据顾客表，获取到顾客需求类型，再根据顾客需求类型（就是货架类型），在根据货架表获取货架类型的id。 
     _shopid = 2000 + APIData.ShopLevel;
   // _ShopExpInt += _addExp;
     APIData.ShopLevelExp += _addExp;
     _ShopExpInt = APIData.ShopLevelExp;
     _ShopExp.text= _ShopExpInt.ToString();
     Debug.Log("jjjjjjj///" + _ShopExpInt+"++++++++"+_ShopXml.GetInt(_shopid, "Exp")+"shopid"+_shopid);
     if (_ShopExpInt >= _ShopXml.GetInt(_shopid, "Exp"))
     {
         _shopid += 1;
         CustomerId = _ShopXml.GetInt(_shopid, "cusId");
         GameDataManger.Instance._LongguCustomerId.Add(CustomerId);
         JieSuoCustomer(CustomerId);
         HuojiaId = _CustomerXml.GetInt(CustomerId, "shelfId");
         //Debug.Log("ssssssssssssss/////"+_shopid+"Cccccccccccc///////"+CustomerId);
        // Debug.Log("hhhhhhhhhhhhhhh////////"+HuojiaId);
         GameDataManger.Instance._huojiaUIId.Add(HuojiaId);
         //Debug.Log("CustomerId++++++"+CustomerId);
         //Debug.Log(_CustomerXml.GetInt(CustomerId, "shelfId") + "*huojiaid+++++" + HuojiaId);
         APIData.ShopLevel = _ShopXml.GetInt(_shopid, "Level");
         _ShopLevelInt = APIData.ShopLevel;
         _ShopLevel.text = _ShopLevelInt.ToString();
         GetHuojiaUI();
     SaveMoney(APIData.ShopLevelName,APIData.ShopLevel);
     }
    // GetHuojiaUI();
     SaveMoney(APIData.ShopLevelExpName,APIData.ShopLevelExp);
 }
    /// <summary>
    /// 解锁顾客界面
    /// </summary>
    /// <param name="CustomerId"></param>
void JieSuoCustomer(int CustomerId)
{
    _CustomerFirstUi.transform.Find("CustomerSprite").GetComponent<UISprite>().spriteName = _CustomerXml.GetString(CustomerId, "cusName");
    _CustomerFirstUi.transform.Find("CustomerSprite/NameSprite/NameLabel").GetComponent<UILabel>().text = _CustomerXml.GetString(CustomerId, "cusName");
    OpenBuyPanelButton(_CustomerFirstUi, true);
    Transform _NeedParent = _CustomerFirstUi.transform.Find("CustomerSprite/BuyNeedSprite/GameObject");
    GameObject NeedObj = ((GameObject)Instantiate(Resources.Load("UI/CustomerNeedSprite")));
    NeedObj.transform.SetParent(_NeedParent);
    NeedObj.transform.Find("BuySprite").GetComponent<UISprite>().spriteName=_CustomerXml.GetString(CustomerId,"cusNeed");
    _CustomerSencondUi.transform.Find("peopleSprite").GetComponent<UISprite>().spriteName = _CustomerFirstUi.transform.Find("CustomerSprite/NameSprite/NameLabel").GetComponent<UILabel>().text + "头像";
    _CustomerSencondUi.transform.Find("LvSprite").GetComponent<UISprite>().spriteName=CustomerId.ToString();
}
    /// <summary>
    /// 玩家增加经验方法。
    /// </summary>
    /// <param name="_addExp"></param>
public void AddPlayerExpNum(int _addExp)
 {
       _playerid = 1000 + APIData.PlayerLevel;
       APIData.PlayerLevelExp += _addExp;
      //_PlayerExpInt += _addExp;
       _PlayerExpInt = APIData.PlayerLevelExp;
       _PlayerExp.text = _PlayerExpInt.ToString();
       if (_PlayerExpInt >= _PlayerXml.GetInt(_playerid, "Exp"))
       {
           APIData.PlayerLevel += 1;
           _PlayerLevelInt = APIData.PlayerLevel;
           _PlayerLevel.text = _PlayerLevelInt.ToString();
           //解锁地面。
           JiesuoDimian();
          SaveMoney(APIData.PlayerLevelName,APIData.PlayerLevel);      
       }
     //读表获取经验值
     //更当前经验对比是否需要升级口碑等级。
       SaveMoney(APIData.PlayerLevelExpName,APIData.PlayerLevelExp);
 }
//解锁地面。
Transform _Level2Floor;
Transform _Level3Floor;
Transform _Level4Floor;
Transform _Level5Floor;
Transform _Level6Floor;
Transform _level3zhuziL;
Transform _level3zhuziR;
Transform _level3qiangL;
Transform _level3qiangR;
Transform _level1zhuziL;
Transform _level1zhuziR;
void JiesuoDimian()
{
    Debug.Log("APIData.PalyerLevelNum:" + APIData.PlayerLevel);
    //Chu
    _Level2Floor = GameObject.Find("地板空节点").transform.Find("FirstLevel");
    _Level3Floor =GameObject.Find("地板空节点").transform.Find("SencondLevel");
    _Level4Floor = GameObject.Find("地板空节点").transform.Find("ThirdLevel");
    _Level5Floor=GameObject.Find("地板空节点").transform.Find("ForthLevel");
    _Level6Floor=GameObject.Find("地板空节点").transform.Find("FiveLevel");

    _level3zhuziL = GameObject.Find("墙空节点").transform.Find("柱子_left");
    _level3zhuziR = GameObject.Find("墙空节点").transform.Find("柱子_right");
    _level1zhuziL = GameObject.Find("墙空节点").transform.Find("柱子_zeroL");
    _level1zhuziR = GameObject.Find("墙空节点").transform.Find("柱子_zeroR");
    _level3qiangR = GameObject.Find("墙空节点").transform.Find("墙_3级");
    _level3qiangL = GameObject.Find("墙空节点").transform.Find("墙_4级");
    SetCameraSize();
    Debug.Log("jiesuodimain,,,,,,,,,,,,,");
    if (APIData.PlayerLevel == 1)
    {
        GetAllNullFloor("Chu");
    }
    if (APIData.PlayerLevel == 2)
    {
        _Level2Floor.gameObject.SetActive(true);
        _Level3Floor.gameObject.SetActive(true);
        GetAllFloor("FirstLevel");
        _Level2Floor.transform.Find("行6列6/suo").gameObject.SetActive(false);
        GetAllNullFloor("Chu");
        GetAllNullFloor("FirstLevel");
    }
    if (APIData.PlayerLevel == 3)
    {
        _Level3Floor.gameObject.SetActive(true);
        _Level4Floor.gameObject.SetActive(true);
        GetAllFloor("SencondLevel");
        _Level3Floor.transform.Find("行8列8/suo").gameObject.SetActive(false);
        GetAllNullFloor("Chu");
        GetAllNullFloor("FirstLevel");
        GetAllNullFloor("SencondLevel");
        _level1zhuziR.gameObject.SetActive(false);
        _level3qiangR.gameObject.SetActive(true);
        _level3zhuziR.gameObject.SetActive(true);
    }
    if (APIData.PlayerLevel == 4)
    {

        _Level4Floor.gameObject.SetActive(true);
        _Level5Floor.gameObject.SetActive(true);
        GetAllFloor("ThirdLevel");
        _Level4Floor.transform.Find("行3列11/suo").gameObject.SetActive(false);
        GetAllNullFloor("Chu");
        GetAllNullFloor("FirstLevel");
        GetAllNullFloor("SencondLevel");
        GetAllNullFloor("ThirdLevel");
    }
    if (APIData.PlayerLevel == 5)
    {
        _Level5Floor.gameObject.SetActive(true);
        _Level6Floor.gameObject.SetActive(true);
        GetAllFloor("ForthLevel");
        _Level5Floor.transform.Find("行11列3/suo").gameObject.SetActive(false);
        GetAllNullFloor("Chu");
        GetAllNullFloor("FirstLevel");
        GetAllNullFloor("SencondLevel");
        GetAllNullFloor("ThirdLevel");
        GetAllNullFloor("ForthLevel");


        _level1zhuziL.gameObject.SetActive(false);
        _level3qiangL.gameObject.SetActive(true);
        _level3zhuziL.gameObject.SetActive(true);
    }
    if (APIData.PlayerLevel == 6)
    {

        _Level6Floor.gameObject.SetActive(true);
        GetAllFloor("FiveLevel");
        _Level6Floor.transform.Find("行11列11/suo").gameObject.SetActive(false);
        GetAllNullFloor("Chu");
        GetAllNullFloor("FirstLevel");
        GetAllNullFloor("SencondLevel");
        GetAllNullFloor("ThirdLevel");
        GetAllNullFloor("ForthLevel");
        GetAllNullFloor("FiveLevel");
    }
    if (APIData.PlayerLevel == 7)
    {
        _Level6Floor.gameObject.SetActive(true);
        GetAllFloor("FiveLevel");
        _Level6Floor.transform.Find("行11列11/suo").gameObject.SetActive(false);
        GetAllNullFloor("Chu");
        GetAllNullFloor("FirstLevel");
        GetAllNullFloor("SencondLevel");
        GetAllNullFloor("ThirdLevel");
        GetAllNullFloor("ForthLevel");
        GetAllNullFloor("FiveLevel");
    }
    
}
    /// <summary>
    /// 找到所有现在标签为floor的空的地板，既可以放置货架的地板。
    /// </summary>
    /// <param name="_Levelfloorname"></param>
void GetAllNullFloor(string _Levelfloorname)
{
    Transform _Chu = GameObject.Find("地板空节点").transform.Find(_Levelfloorname);
    for (int i = 0; i < _Chu.childCount; i++)
    {
        if (_Chu.GetChild(i).tag == "Floor")
        {
            if (!GameDataManger.Instance.floorInit.ContainsKey(GetObjName(_Chu.GetChild(i).gameObject)))
            { 
            GameDataManger.Instance.floorInit.Add(GetObjName(_Chu.GetChild(i).gameObject), _Chu.GetChild(i).gameObject);
            }
        }
    }
    GameDataManger.Instance.floorSencond = GameDataManger.Instance.floorInit;
    GameDataManger.Instance.floor = GameDataManger.Instance.floorSencond;
}
/// <summary>
/// 升级时，解锁地板，解锁的地板需要添加
/// </summary>
void GetAllFloor(string _Levelfloorname)
{
    Transform childFloor=GameObject.Find("地板空节点").transform.Find(_Levelfloorname);
    for (int i = 0; i <childFloor.childCount; i++)
    {
        childFloor.GetChild(i).tag="Floor";
        childFloor.GetChild(i).GetComponent<SpriteRenderer>().color = Color.white;
    }
}
int GetObjName(GameObject _obj)
{
    int numInt1 = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(_obj.transform.name, @"[^0-9]+", ""));
    return numInt1;
}
void SetCameraSize()
{
    if (APIData.PlayerLevel <= 2)
    {
        Camera.main.GetComponent<Camera>().orthographicSize = 8;
        Camera.main.transform.localPosition = new Vector3(0,1,3);
    }
    if (APIData.PlayerLevel > 2)
    {
        Camera.main.GetComponent<Camera>().orthographicSize = 12;
        Camera.main.transform.localPosition = new Vector3(0, 1, 0);
    }
}
void OpenBuyPanelButton(Transform _buypanel, bool OpenOrClose)
{
    _buypanel.gameObject.SetActive(OpenOrClose);
    //在仓储界面显示出所有库存Ap内所有的物品
    if (_buypanel == _CangchuUI)
    {
        if (_buypanel.Find("cunfangUI").childCount <= 0)
        {
            foreach (var item in APIData.ShopStock)
            {
                if (item.Value != 0)
                {
                    CahngkuUI(item.Key, item.Value);
                }
            }
        }
    }
}
 void CloseScripts(Transform _huojia,bool isFalse)
 {
     _huojia.GetComponent<NewHuojiaFollow>().enabled = isFalse;
 }
 void OpenScripts(Transform _huojia, bool isFalse)
 {
     _huojia.GetComponent<HuojiaButton>().enabled = isFalse;
 }
    /// <summary>
    /// 读取经验表
    /// </summary>
 TableValue ReadExpXml(string _DataName)
 {
     Object ShopExpObj = Resources.Load("Xml/"+_DataName);
     XmlHelper.Instance.LoadFile(_DataName, ShopExpObj);
     TableValue ShopExpData = XmlHelper.Instance.ReadFile(_DataName);
     return ShopExpData;
 }
    //在UI上生成货架UI的方法。
 void GetHuojiaUI()
 { 
     _huojiaUIRoot = transform.Find("ShangCheng/ZhuangxiuButton/Tubiao/HuojiaGameObject");
     for (int i = 0; i < _huojiaUIRoot.childCount; i++)
     {
         Destroy(_huojiaUIRoot.GetChild(i).gameObject);
     }
     for (int i = 0; i <GameDataManger.Instance._huojiaUIId.Count; i++)
     {
         int iiii = i;
        // Debug.Log("huojia+++++++UIUIUI" + GameDataManger.Instance._huojiaUIId.Count + "//////////" + GameDataManger.Instance._huojiaUIId[i]);
         GameObject HuojiaObj = ((GameObject)Instantiate(Resources.Load("UI/HuojiaButton")));
         HuojiaObj.transform.SetParent(_huojiaUIRoot);
         _huojiaUIRoot.GetComponent<UIGrid>().enabled = true;
         HuojiaObj.transform.Find("huojiaSprite").GetComponent<UISprite>().spriteName = _HuojiaXml.GetString(GameDataManger.Instance._huojiaUIId[iiii], "name");
         HuojiaObj.transform.Find("MoneySprite/Num_Label").GetComponent<UILabel>().text = _HuojiaXml.GetString(GameDataManger.Instance._huojiaUIId[iiii], "coin");
         int huojiaTage = int.Parse(HuojiaObj.transform.Find("MoneySprite/Num_Label").GetComponent<UILabel>().text);
         HuojiaObj.transform.localPosition = Vector3.zero;
         HuojiaObj.transform.localScale = Vector3.one;
         //HuojiaObj.GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { BornHuojia(10001+i);}));
         Debug.Log("nnnnnnnnnnnhuojia/////" + i + "/////////" + GameDataManger.Instance._huojiaUIId[iiii]);
         HuojiaObj.GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { BornHuojia(GameDataManger.Instance._huojiaUIId[iiii]); }));
     }
 }
 int HuojiaGid=0;
 void BornHuojia(int _id)
 {
     //for (int i = 0; i < GameDataManger.Instance._huojiaUIId.Count; i++)
     //{
     //    Debug.Log("anjian++++++++++" + GameDataManger.Instance._huojiaUIId[i]);
     //    GameObject _huojia = ((GameObject)Instantiate(Resources.Load("NewHuojia/HuojiaT")));
     //    string HuojiaType = _HuojiaXml.GetString(GameDataManger.Instance._huojiaUIId[i], "type");
     //    string HuojiaName = _HuojiaXml.GetString(GameDataManger.Instance._huojiaUIId[i], "name");
     //    int Huojialevel = _HuojiaXml.GetInt(GameDataManger.Instance._huojiaUIId[i], "Level");
     //    int HuojiaMoney = _HuojiaXml.GetInt(GameDataManger.Instance._huojiaUIId[i], "coin");
     //    int Huojiasales = _HuojiaXml.GetInt(GameDataManger.Instance._huojiaUIId[i], "sales");
     //    int Huojiavolume = _HuojiaXml.GetInt(GameDataManger.Instance._huojiaUIId[i], "volume");
     //    _huojia.GetComponent<HuojiaModel>().HuojiaType = HuojiaType;
     //    _huojia.GetComponent<HuojiaModel>().HuojiaName = HuojiaName;
     //    _huojia.GetComponent<HuojiaModel>().HuojiaLevel = Huojialevel;
     //    _huojia.GetComponent<HuojiaModel>().HuojiaMoney = HuojiaMoney;
     //    _huojia.GetComponent<HuojiaModel>().HuojiaScale = Huojiavolume;
     //    _huojia.GetComponent<HuojiaModel>().HuojiaPower = Huojiasales;
     //    string _huojiaSpriteName = HuojiaName + "左";
     //    // changeSpriteByImage(_huojiaSprite,_huojiaSpriteName);

     //    GameObject _huojiaSpriteObj = ((GameObject)Instantiate(Resources.Load("新货架/水果货架/" + _huojiaSpriteName)));
     //    Debug.Log("wwwwwwwwwwwww" + _huojiaSpriteObj.transform.name);
     //    _huojiaSpriteObj.transform.SetParent(_huojia.transform);
     //    _huojiaSpriteObj.transform.localScale = Vector3.one;
     //    _huojiaSpriteObj.transform.name = "Huojia_Sprite";
     //    if (_huojiaSpriteObj.GetComponent<SpriteRenderer>().sprite.name == "饮料货架左")
     //    {
     //        _huojiaSpriteObj.transform.localPosition = new Vector3(0, 0.2f, 0);
     //    }
     //    else
     //    {
     //        _huojiaSpriteObj.transform.localPosition = Vector3.zero;
     //    }
     //    _BuyHuojiUIFirst.Find("NumLabel").GetComponent<UILabel>().text = HuojiaMoney.ToString();
     //    _BuyHuojiaUISencond.Find("NumLabel").GetComponent<UILabel>().text = _BuyHuojiUIFirst.Find("NumLabel").GetComponent<UILabel>().text;
     //    _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { CloseScripts(_huojia.transform, false); }));
     //    _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenScripts(_huojia.transform, true); }));
     //    _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { ReduceMoneyButton(HuojiaMoney); }));
     //    _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_BuyHuojiaUISencond.parent, false); }));
     //    _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { ReMoveSaveHuojiaFloorId(_huojia); }));
     //    _huojialist.Add(_huojia);
     //}
     ////////////////////////////////////////////////
     ////////////////////////////////////////////////
     Debug.Log("anjian++++++++++" + _id);
     GameObject _huojia = ((GameObject)Instantiate(Resources.Load("NewHuojia/HuojiaT")));
     string HuojiaType = _HuojiaXml.GetString(_id, "type");
     string HuojiaName = _HuojiaXml.GetString(_id, "name");
     int Huojialevel = _HuojiaXml.GetInt(_id, "Level");
     int HuojiaMoney = _HuojiaXml.GetInt(_id, "coin");
     int Huojiasales = _HuojiaXml.GetInt(_id, "sales");
     int Huojiavolume = _HuojiaXml.GetInt(_id, "volume");
     _huojia.GetComponent<HuojiaModel>().HuojiaType = HuojiaType;
     _huojia.GetComponent<HuojiaModel>().HuojiaName = HuojiaName;
     _huojia.GetComponent<HuojiaModel>().HuojiaLevel = Huojialevel;
     _huojia.GetComponent<HuojiaModel>().HuojiaMoney = HuojiaMoney;
     _huojia.GetComponent<HuojiaModel>().HuojiaScale = Huojiavolume;
     _huojia.GetComponent<HuojiaModel>().HuojiaPower = Huojiasales;
     string _huojiaSpriteName = HuojiaName+"1";
     GameObject _huojiaSpriteObj = ((GameObject)Instantiate(Resources.Load("新货架/货架/" + _huojiaSpriteName)));
     Debug.Log("wwwwwwwwwwwww" + _huojiaSpriteObj.transform.name);
     _huojiaSpriteObj.transform.SetParent(_huojia.transform);
     _huojiaSpriteObj.transform.localScale = Vector3.one;
     _huojiaSpriteObj.transform.name = "Huojia_Sprite";
     if (_huojiaSpriteObj.GetComponent<SpriteRenderer>().sprite.name == "饮料货架")
     {
         _huojiaSpriteObj.transform.localPosition = new Vector3(0, 0.2f, 0);
     }
     else
     {
         _huojiaSpriteObj.transform.localPosition = new Vector3(0, 0.2f, 0);
     }
     _BuyHuojiUIFirst.Find("NumLabel").GetComponent<UILabel>().text = HuojiaMoney.ToString();
     _BuyHuojiaUISencond.Find("NumLabel").GetComponent<UILabel>().text = _BuyHuojiUIFirst.Find("NumLabel").GetComponent<UILabel>().text;
     _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { CloseScripts(_huojia.transform, false); }));
     _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenScripts(_huojia.transform, true); }));
     _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { ReduceMoneyButton(HuojiaMoney); }));
     _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { OpenBuyPanelButton(_BuyHuojiaUISencond.parent, false); }));
     _BuyHuojiaUISencond.transform.Find("YesButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { ReMoveSaveHuojiaFloorId(_huojia); }));
     _huojialist.Add(_huojia);    
 }

 public void CahngkuUI(short _id, int _num)
 {
     GameObject CucunGoodsObj = ((GameObject)Instantiate(Resources.Load("UI/CangchuGood")));
     Transform _CunfangUI = _CangchuUI.Find("cunfangUI");
     CucunGoodsObj.transform.SetParent(_CunfangUI);
     CucunGoodsObj.transform.Find("huowuUI").GetComponent<UISprite>().spriteName = GoodsData.GetString(_id, "name");
     CucunGoodsObj.transform.Find("huowuUI/Label").GetComponent<UILabel>().text = _num.ToString();
     CucunGoodsObj.transform.localPosition = Vector3.zero;
     CucunGoodsObj.transform.localScale = Vector3.one;
     _CunfangUI.GetComponent<UIGrid>().enabled = true;
 }

 void ReMoveSaveHuojiaFloorId(GameObject _huojia)
 {
 _huojia.transform.parent.tag = "Untagged";
 int _removefloorid = _huojia.GetComponent<HuojiaModel>().FloorId;
 GameDataManger.Instance.floorSencond.Remove(_removefloorid);
 Debug.Log("_RemoveSaveHUojiaId"+_removefloorid);
 }

    /// <summary>
    /// 时间达到打来结算UI,增加各项数值，增加店铺收益。
    /// </summary>
 float _timeNum;
 void OpenJisuanUI()
 {  
        CancelInvoke("BornCustomer");
        OpenBuyPanelButton(_jiesuanUI, true);   
        _jiesuanUI.transform.Find("Mianban/KoubeiLabel/KoubeiNumLabel").GetComponent<UILabel>().text = "+100";
        _jiesuanUI.transform.Find("Mianban/jinbiLabel/jinbiNumLabel").GetComponent<UILabel>().text = "+1000";
        _jiesuanUI.transform.Find("Mianban/jinyanLabel/jinyanNumLabel").GetComponent<UILabel>().text = "+10";
        _jiesuanUI.transform.Find("Mianban/QuedingButton").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { CloseJiSuanUI(); }));
 }
 void CloseJiSuanUI()
 {
     _jiesuanUI.gameObject.SetActive(false);
     AddShopExpNum(100);
     AddPlayerExpNum(10);
     AddMonryButtoon(1000);

     _WelcomeButton.enabled = true;

 }
//给精灵动态更改图片方法
void changeSpriteByImage(GameObject _huojiaobj, string _name)
 {
     Texture2D Tex = Resources.Load("新货架/水果货架/" + _name) as Texture2D;
     SpriteRenderer spr = _huojiaobj.GetComponent<SpriteRenderer>();
     Sprite spriteA = Sprite.Create(Tex, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
     _huojiaobj.GetComponent<SpriteRenderer>().sprite = spriteA;
 }
 void TurnButton()
 {
     foreach (var item in _huojialist)
     {
         item.GetComponent<HuojiaMouseTurn>().enabled = true;
         item.GetComponent<HuojiaMousemove>().enabled = false;
         item.GetComponent<NewHuojiaFollow>().enabled = false;
     }
 }
 void MoveButton()
 {
     foreach (var item in _huojialist)
     {
         item.GetComponent<HuojiaMouseTurn>().enabled = false;
         item.GetComponent<HuojiaMousemove>().enabled = true;
         item.GetComponent<NewHuojiaFollow>().enabled = false;
     }
     //transform.parent.parent.transform.Find("yidongMove(Clone)").gameObject.SetActive(true) ;
    // _huojia.GetComponent<NewHuojiaFollow>().gameObject.SetActive(true);
 }

 GameObject _BornPlace;
    /// <summary>
    /// 迎客按钮。
    /// </summary>

 int CustomerNum=5;//生成顾客数量。
 GameObject _FloorObj;
 void WelcomeButton()
 {
     _WelcomeButton.enabled = false;
     _FloorObj = GameObject.Find("地板空节点");
     ////////////////////
    //sex > LongGuFemale  LongGuMale
     InvokeRepeating("BornCustomer", 2f, 4f);
     foreach (var item in GameDataManger.Instance.LujingPoint)
     {
         item.Value.GetComponent<SpriteRenderer>().color = Color.white;
     }
     GameDataManger.Instance.LujingPoint.Clear();
     _FloorObj.GetComponent<FloorManger>().AllFloorAddValue();
     _FloorObj.GetComponent<FloorManger>().Way_Finding2(16);
     //OpenJisuanUI();
     Invoke("OpenJisuanUI", 20f);
     // 编号    2
    //DragonBones.UnityArmatureComponent cusmm = LongGuManager.GetInstance().CreatLongGu("LongGuMale");
    //List<DragonBones.Slot> maleSlotAll = new List<DragonBones.Slot>();
    //maleSlotAll = LongGuManager.GetInstance().GetLongGuSlotAll(cusmm);
    //LongGuManager.GetInstance().ChangeClothesAll("LongGuMale", maleSlotAll, 2);
    //cusmm.gameObject.AddComponent<LongguFollow>()._CustomerMov = CustomerCubeObj;
     //cusmm.transform
     //////////////////////////////
 }

 
 int hh = 0;
 
    /// <summary>
    /// 生成顾客
    /// </summary>
 void BornCustomer()
 {
     hh = GameDataManger.Instance._LongguCustomerId[Random.Range(0, GameDataManger.Instance._LongguCustomerId.Count)];
     _BornPlace = GameObject.Find("CustomerBornPlace");
     GameObject CustomerCubeObj = (GameObject)Instantiate(Resources.Load("NewCustomer/CubeCustomer"), _BornPlace.transform.position, Quaternion.Euler(0, 0, 0));
     string longguSex = _CustomerXml.GetString(hh, "sex");
     short longguNum = _CustomerXml.GetShort(hh, "boneNum");
     DragonBones.UnityArmatureComponent cusmm = LongGuManager.GetInstance().CreatLongGu(longguSex);
     List<DragonBones.Slot> maleSlotAll = new List<DragonBones.Slot>();
     maleSlotAll = LongGuManager.GetInstance().GetLongGuSlotAll(cusmm);
     LongGuManager.GetInstance().ChangeClothesAll(longguSex, maleSlotAll, longguNum);
     CustomerCubeObj.GetComponent<NewCustomer>()._longgu = cusmm;
     cusmm.gameObject.AddComponent<LongguFollow>()._CustomerMov = CustomerCubeObj;
     //cusmm.transform.localRotation
     CustomerCubeObj.GetComponent<NewCustomer>().CustomerInit();
     Debug.Log("longgu++++++" + hh + "Count" + GameDataManger.Instance._LongguCustomerId.Count);

      //for (int i = 0; i <= GameDataManger.Instance._LongguCustomerId.Count; i++)
      //{
      //    int ss = i;
      //   _BornPlace = GameObject.Find("CustomerBornPlace");
      //   GameObject CustomerCubeObj = (GameObject)Instantiate(Resources.Load("NewCustomer/CubeCustomer"), _BornPlace.transform.position, Quaternion.Euler(90, 0, 0));
      //   string longguSex = _CustomerXml.GetString(GameDataManger.Instance._LongguCustomerId[ss], "sex");
      //   short longguNum = _CustomerXml.GetShort(GameDataManger.Instance._LongguCustomerId[ss], "boneNum");
      //   DragonBones.UnityArmatureComponent cusmm = LongGuManager.GetInstance().CreatLongGu(longguSex);
      //   List<DragonBones.Slot> maleSlotAll = new List<DragonBones.Slot>();
      //   maleSlotAll = LongGuManager.GetInstance().GetLongGuSlotAll(cusmm);
      //   LongGuManager.GetInstance().ChangeClothesAll(longguSex, maleSlotAll, longguNum);
      //   CustomerCubeObj.GetComponent<NewCustomer>()._longgu = cusmm;
      //   cusmm.gameObject.AddComponent<LongguFollow>()._CustomerMov = CustomerCubeObj;
      //   //cusmm.transform.localRotation
      //   CustomerCubeObj.GetComponent<NewCustomer>().CustomerInit();   
      //   Debug.Log("longgu++++++" + GameDataManger.Instance._LongguCustomerId[ss] + "Count" + GameDataManger.Instance._LongguCustomerId.Count);
      //}
 }
 GameObject _ShouyinyuanPlace;
    /// <summary>
    /// 生成收银员
    /// </summary>
 void BornGetMoneyPenplo()
 {
     _ShouyinyuanPlace = GameObject.Find("GetMoneyPenploPlace");
     GameObject CustomerCubeObj = (GameObject)Instantiate(Resources.Load("NewCustomer/Dianyuan"), _ShouyinyuanPlace.transform.position, Quaternion.Euler(90, 0, 0));
     DragonBones.UnityArmatureComponent Dianyuan = LongGuManager.GetInstance().CreatLongGu("LongGuFemale");
     List<DragonBones.Slot> maleSlotAll = new List<DragonBones.Slot>();
     maleSlotAll = LongGuManager.GetInstance().GetLongGuSlotAll(Dianyuan);
     LongGuManager.GetInstance().ChangeClothesAll("LongGuFemale", maleSlotAll, 11);
     Dianyuan.gameObject.AddComponent<LongguFollow>()._CustomerMov = CustomerCubeObj;
     Dianyuan.transform.GetComponent<DragonBones.UnityArmatureComponent>().sortingGroup.sortingOrder = 6;
 } 

}
