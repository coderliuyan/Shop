using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigDefine;
using DragonBones;
using DG.Tweening;
public class SelectPanel : MonoBehaviour {

    #region 这里都是label 显示 一些数值
    private string moneyPath = @"ShangLan/Money_Jingbi/Money";
    [HideInInspector] public UILabel moneyLabel;

    private string playerLevelLabelPath = @"ShangLan/PalyerLevelNum_Sprite/PalyerLevelNum";
    [HideInInspector] public UILabel playerLevelLabel;

    private string playerExpLabelPath = @"ShangLan/PalyerLevelExp_Sprite/PalyerLevelExpNum";
    [HideInInspector] public UILabel playerExpLabel;

    private string diamondLabelPath = @"ShangLan/Money_zhuanshi/zuanshiNum";
    [HideInInspector] public UILabel diamondLabel;

    private string shopLevelLabelPath = @"ZhongjianButton/dainpu/ShopLevelNum_Sprite/ShopLevelNum";
    [HideInInspector] public UILabel shopLevelLabel;
    #endregion 以上都是存放数值的label 和路径


    #region 底部栏的 button 和路径
    private readonly string wellcomeBtnPath = @"ShangCheng/GameBeginCstomer/WelcomButton";
    private UIButton wellcomeBtn; //迎客按钮

    private string cangkuBtnPath = @"ShangCheng/CangkuButton";
    UIButton cangkuBtn;

    private string daojuBtnPath = @"ShangCheng/daojuButton";
    UIButton daojuBtn;

    private string zhuangxiuBtnPath = @"ShangCheng/ZhuangxiuButton";
    UIButton zhuangxiuBtn;

    private string haoyouBtnPath = @"ShangCheng/haoyouButton";
    UIButton haoyouBtn;

    private string jinhuoBtnPath = @"ShangCheng/jinhuoButton";
    UIButton jinhuoBtn;

    private string buyGoodsBtnPath = @"ShangCheng/CangkuButton/Diban/cangkuUI/BuyMore";
    UIButton buyGoodsBtn;

    #endregion

    #region 底部栏的UI transform 父级控制组件

    private string cangchuPath = @"ShangCheng/CangkuButton/Diban/cangkuUI";
    UnityEngine.Transform cangchuUI;

    private string zhuangxiuUIPath = @"ShangCheng/ZhuangxiuButton/Tubiao";
    UnityEngine.Transform zhuangxiuUI;
    #endregion


    #region 中间的目前尚没有用的按钮

    private string nongchangBtnPath = @"ZhongjianButton/nongchang";
    UIButton nongchangBtn;

    private string jiangbeiBtnPath = @"ZhongjianButton/jiangbei";
    UIButton jiangbeiBtn;

    private string guanguangBtnPath = @"ZhongjianButton/guanguang";
    UIButton guanguanBtn;

    private string renwuBtnPath = @"ZhongjianButton/renwu";
    UIButton renwuBtn;

    private string fensiBtnPaht = @"ZhongjianButton/fensi";
    UIButton fensiBtn;

    private string gongchangBtnPath = @"ZhongjianButton/gongchang";
    UIButton gongchangBtn;

    private string dianpuBtnPath = @"ZhongjianButton/dainpu";
    UIButton dianpuBtn;

    private string shangchengBtnPath = @"ZhongjianButton/shangcheng";
    UIButton shangchengBtn;

    private string jieri = @"ZhongjianButton/jieri";
    UIButton jieriBtn;

    private string koubeiBtnPath = @"ZhongjianButton/koubei";
    UIButton koubeiBtn;

    private string customerBtnPath = @"ZhongjianButton/Customer";
    UIButton customerBtn;

    private string yuangongBtnPath = @"ZhongjianButton/yuangong";
    UIButton yuangongBtn;

    #endregion 中间按钮 over




    List<UIButton> huojiaBtnList = new List<UIButton>(); //存放所有货架UI 上的 货架 button

    public static SelectPanel selectManager; //单例

    Boss boss; //店铺 虚拟状态


    #region 临时值 做为结算UI显示所用
    public int koubei = 0;
    public int jinbi = 0;
    public int exp = 0;


    //存放所有产生的顾客
    List<GameObject> tempCustomers = new List<GameObject>();
    #endregion


    //底部栏的选中状态
    enum SelectState
    {
        huojiaState,
        goodsState,
        haoyouState,
        jinhuState,
        daojuState
    }

    SelectState currentState = SelectState.huojiaState;

    void InitComponet()
    {


        #region 获取底部栏的 UI 父级控件
        cangchuUI = transform.Find(cangchuPath);
        zhuangxiuUI = transform.Find(zhuangxiuUIPath);
        #endregion


        #region 获取按钮并绑定事件
        wellcomeBtn = transform.Find(wellcomeBtnPath).GetComponent<UIButton>();
        wellcomeBtn.onClick.Add(new EventDelegate(WellComeBtnClick));

        cangkuBtn = transform.Find(cangkuBtnPath).GetComponent<UIButton>();
        cangkuBtn.onClick.Add(new EventDelegate(() => { ClickToolBarButton(cangkuBtn); }));

        daojuBtn = transform.Find(daojuBtnPath).GetComponent<UIButton>();
        daojuBtn.onClick.Add(new EventDelegate(() => { ClickToolBarButton(daojuBtn); }));

        zhuangxiuBtn = transform.Find(zhuangxiuBtnPath).GetComponent<UIButton>();
        zhuangxiuBtn.onClick.Add(new EventDelegate(() => { ClickToolBarButton(zhuangxiuBtn); }));

        haoyouBtn = transform.Find(haoyouBtnPath).GetComponent<UIButton>();
        haoyouBtn.onClick.Add(new EventDelegate(() => { ClickToolBarButton(haoyouBtn); }));

        jinhuoBtn = transform.Find(jinhuoBtnPath).GetComponent<UIButton>();
        jinhuoBtn.onClick.Add(new EventDelegate(() => { ClickToolBarButton(jinhuoBtn); }));

        buyGoodsBtn = transform.Find(buyGoodsBtnPath).GetComponent<UIButton>();
        buyGoodsBtn.onClick.Add(new EventDelegate(() => { ClickToolBarButton(buyGoodsBtn); }));
        #endregion 获取按钮 绑定事件结束




        #region 获取 label 并 赋值
        moneyLabel = transform.Find(moneyPath).GetComponent<UILabel>();
        playerLevelLabel = transform.Find(playerLevelLabelPath).GetComponent<UILabel>();
        playerExpLabel = transform.Find(playerExpLabelPath).GetComponent<UILabel>();
        diamondLabel = transform.Find(diamondLabelPath).GetComponent<UILabel>();
        shopLevelLabel = transform.Find(shopLevelLabelPath).GetComponent<UILabel>();

        moneyLabel.text = Player.GoldNum.ToString();
        playerLevelLabel.text = Player.PlayerLevel.ToString();
        playerExpLabel.text = Player.PlayerExp.ToString();
        diamondLabel.text = Player.DiamondNum.ToString();
        shopLevelLabel.text = Player.ShopLevel.ToString();

        #endregion 获取label 并赋值 结束
        nongchangBtn = transform.Find(nongchangBtnPath).GetComponent<UIButton>();
        nongchangBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        jiangbeiBtn = transform.Find(jiangbeiBtnPath).GetComponent<UIButton>();
        jiangbeiBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        guanguanBtn = transform.Find(guanguangBtnPath).GetComponent<UIButton>();
        guanguanBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        renwuBtn = transform.Find(renwuBtnPath).GetComponent<UIButton>();
        renwuBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        fensiBtn = transform.Find(fensiBtnPaht).GetComponent<UIButton>();
        fensiBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        gongchangBtn = transform.Find(gongchangBtnPath).GetComponent<UIButton>();
        gongchangBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        dianpuBtn = transform.Find(dianpuBtnPath).GetComponent<UIButton>();
        dianpuBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        shangchengBtn = transform.Find(shangchengBtnPath).GetComponent<UIButton>();
        shangchengBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        jieriBtn = transform.Find(jieri).GetComponent<UIButton>();
        jieriBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        koubeiBtn = transform.Find(koubeiBtnPath).GetComponent<UIButton>();
        koubeiBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        customerBtn = transform.Find(customerBtnPath).GetComponent<UIButton>();
        customerBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        yuangongBtn = transform.Find(yuangongBtnPath).GetComponent<UIButton>();
        yuangongBtn.onClick.Add(new EventDelegate(UIManager.Instance.NoOpen));

        #region 一些目前没有开发的 按钮


        #endregion


        BornGetMoneyPeople();

    }


    void ClickToolBarButton(UIButton btn)
    {
        Debug.Log(btn.name);
        switch (btn.name)
        {
            case ("haoyouButton"):
            case ("daojuButton"):
                {
                    UIManager.Instance.NoOpen();
                    currentState = SelectState.haoyouState;
                }
                break;

            case ("jinhuoButton"):
                {
                    currentState = SelectState.jinhuState;
                    UIManager.Instance.ShowBuyPanel();
                   
                }
                break;
            case ("CangkuButton"):
                {
                    Debug.Log("点击了 仓库 按钮");
                    //仓库里面所有的内容 显示出来 对应的货架的要隐藏
                    currentState = SelectState.goodsState;
                    OpenBuyPanelButton(cangchuUI,true);
                    OpenBuyPanelButton(zhuangxiuUI,false);
                   
                }
                break;
            case ("ZhuangxiuButton"):
                {
                    currentState = SelectState.huojiaState;
                    Debug.Log("点击了 装修 按钮");
                    OpenBuyPanelButton(cangchuUI, false);
                    OpenBuyPanelButton(zhuangxiuUI, true);
                  
                }
                break;
            case ("BuyMore"):
                {
                    Debug.Log("点击了 仓库 按钮");
                    //仓库里面所有的内容 显示出来 对应的货架的要隐藏
                    currentState = SelectState.goodsState;
                    UIManager.Instance.ShowBuyPanel();
                    //OpenBuyPanelButton(cangchuUI, true);
                    //OpenBuyPanelButton(zhuangxiuUI, false);

                }
                break;

        }

    }


    void OpenBuyPanelButton(UnityEngine.Transform _buypanel, bool OpenOrClose)
    {

        if(OpenOrClose){
            if(_buypanel.gameObject.activeSelf){
                return;
            }
        }

        _buypanel.gameObject.SetActive(OpenOrClose);

        //在仓储界面显示出所有库存  Shop stock 里面 所有的物品
        if (_buypanel == cangchuUI  && OpenOrClose)
        {
            UnityEngine.Transform ptran = _buypanel.Find("cunfangUI");

            Debug.Log(ptran.childCount);
            for (int i = ptran.childCount; i > 0 ; i--)
            {
                Destroy(ptran.GetChild(i-1).gameObject);
            }

            foreach (var item in Player.ShopStock)
            {
                Debug.Log(item.Key);  
                if (item.Value != 0)
                {
                    CahngkuUI(item.Key, item.Value);
                }
            }
        }

        //显示 货架 
        if(_buypanel == zhuangxiuUI)
        {

        }



    }

    public void UpdateCangkuUI()
    {
        UnityEngine.Transform ptran = cangchuUI.Find("cunfangUI");

        Debug.Log(ptran.childCount);
        for (int i = ptran.childCount; i > 0; i--)
        {
            Destroy(ptran.GetChild(i - 1).gameObject);
        }

        foreach (var item in Player.ShopStock)
        {
            Debug.Log(item.Key);
            if (item.Value != 0)
            {
                CahngkuUI(item.Key, item.Value);
            }
        }
    }


    public void CahngkuUI(int _id, int _num)
    {
        GameObject CucunGoodsObj = ((GameObject)Instantiate(Resources.Load("UI/CangchuGood")));
        UnityEngine.Transform _CunfangUI = cangchuUI.Find("cunfangUI");
        CucunGoodsObj.transform.SetParent(_CunfangUI);
        CucunGoodsObj.transform.Find("huowuUI").GetComponent<UISprite>().spriteName = DataManager.Instance.goodsData.GetString(_id, "name");
        CucunGoodsObj.transform.Find("huowuUI/Label").GetComponent<UILabel>().text = _num.ToString();
        CucunGoodsObj.transform.localPosition = Vector3.zero;
        CucunGoodsObj.transform.localScale = Vector3.one;
        _CunfangUI.GetComponent<UIGrid>().enabled = true;
        if(currentState != SelectState.goodsState)
        {
            cangchuUI.gameObject.SetActive(false);
        }
        else
        {
            cangchuUI.gameObject.SetActive(true);
        }
        
    }

    private void Awake()
    {
        if(selectManager == null){
            selectManager = this;
        }
        boss = Boss.CreatBossWithProperty(Boss.GameState.preparShop);
    }

    // Use this for initialization
    void Start ()
    {
        InitComponet();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            //Player.ShopStock.Add(1001,100);
            //Player.ShopStock.Add(1002, 100);
            //Player.ShopStock.Add(1003, 100);
            //Player.ShopStock.Add(1004, 100);
            //Player.ShopStock.Add(1005, 100);
            //Player.ShopStock.Add(1006, 100);
            //Player.ShopStock.Add(1007, 100);
            //Player.ShopStock.Add(1008, 100);
            //Player.ShopStock.Add(1010, 100);
            Player.GoldNum += 1000;
            Player.PlayerLevel += 3;
        }

    }


	
    private void AddHuoJia(int huojiaId)
    {
       

        Debug.Log("d点击了货架。" + huojiaId);

        int huojiaMoney = DataManager.Instance.huojiaXml.GetInt(huojiaId,"coin");
        Debug.Log(huojiaMoney);
        if(Player.GoldNum < huojiaMoney)
        {
            DataManager.Instance.msgText = "想啥呢，钱不够！";
            UIManager.Instance.ShowMessagePanel();
            return;
        }
        
        //钱够的话，把这个数字要传进去 点击建造的时候 要在玩家身上减去 

        
        //首先 检查一下不能交互的区域
        if (FloorManager.Instance.FetchActiveWay())
        {

            //首先创建一个货架
            GameObject huojiaObj = Instantiate(Resources.Load("HJPrefab/HJFruit" + huojiaId) as GameObject);

            HuoJiaController hjc = huojiaObj.GetComponent<HuoJiaController>();
            hjc.huojiaLevel = 1;
            hjc.huojiaDirection = 1;
            //int saleTimes = DataManager.Instance.huojiaXml.GetInt(huojiaId,"sales"); //在补货的时候再添加
            //hjc.saleTimes = saleTimes;
            hjc.huojiaID = huojiaId;



            //获取到创建物体下面的goods 
            //GameObject goods = huojiaObj.transform.Find("goods").gameObject;
            //Goods go =  goods.GetComponent<Goods>();
            //go.huojiaID = huojiaId;

            Debug.Log("检查了路径，并有有效路径。");

            FloorManager.Instance.FetchActiveFloor();
            Debug.Log(" floor interface count =" + FloorManager.Instance.floorInterable.Count);

        }

    }

    /// <summary>
    /// 开始迎客时调用 
    /// </summary>
    /// <param name="shopstate">迎客时为close 结束时为 true</param>
    
    private void CloseShop(bool shopstate)
    {
        if (shopstate)
        {
            boss.State = Boss.GameState.preparShop;
        }
        else
        {
            boss.State = Boss.GameState.Shoping;
        }

        //wellcomeBtn.enabled = shopstate;
        //foreach (UIButton btn in huojiaBtnList)
        //{
        //    btn.enabled = shopstate;
        //}

    }



    void WellComeBtnClick()
    {

        CloseShop(false);
        ChangeButtonState(false);

        //获取可交互的地板
        FloorManager.Instance.FetchActiveFloor();
        Debug.Log(FloorManager.Instance.floorInterable.Count);

        //生成路径
        FloorManager.Instance.FetchActiveWay();

        Debug.Log("wellcome  supermarket!");

        //创建顾客

        //顾客沿着路径进行移动 根据移动的方向 播放不同的动画 协程

        //顾客移动到相应的点位 进行购买行为 / 支付行为 根据交互对象的位置 播放不同的动画

        //顾客这个动画播放完成之后 销毁对象

        jinbi = Player.GoldNum;
        exp = Player.PlayerExp;
        koubei = 1;
        StartCoroutine(StartGame());

        

        //SHOP TIME 应该根据最后一个顾客行动完 生成 一个时间
        Invoke("ShopEnd",Define.SHOP_TIME);

    }

    IEnumerator StartGame()
    {
        while(boss.State == Boss.GameState.Shoping)
        {
            yield return new WaitForSeconds(Define.CUSTOMER_TIME);
           
            StartCoroutine(GoShoping());
        }
       
    }

    //根据不同的时间 和 结账 时间 来控制整个 购买过程
    IEnumerator GoShoping()
    {

        GameObject outDoorObj = FloorManager.Instance.allFloor[Define.OUT_DOOR_POS];
        GameObject bornObj = FloorManager.Instance.allFloor[Define.BORN_POS];
        int randomNum = 0;
        randomNum = Random.Range(0, CustomerManager.Instance.customerIDList.Count);
        int customerId = CustomerManager.Instance.customerIDList[randomNum];

        customerId = CustomerManager.Instance.customerIDList[randomNum];

        //创建的顾客会是随机产生 在这里 需要创建多个 顾客预设体 ,然后随机数调用
        GameObject newCustomer = Instantiate(Resources.Load("CustomerPrefab/Customer" + customerId) as GameObject,Vector3.zero,Quaternion.identity);
        newCustomer.transform.SetParent(bornObj.transform);
        newCustomer.transform.localPosition = Vector3.zero;
        newCustomer.transform.localRotation = Quaternion.identity;

        CustomerNeed ctn = newCustomer.GetComponent<CustomerNeed>();
        ctn.customerID = customerId;
        ctn.buyTimes = DataManager.Instance.customerXml.GetInt(customerId, "boneNum");
        ctn.goodsType = DataManager.Instance.customerXml.GetString(customerId,"cusNeed");
        tempCustomers.Add(newCustomer);

        UnityArmatureComponent arc = newCustomer.GetComponent<UnityArmatureComponent>();
        arc.animation.Play("face_walk", -1);
        arc._sortingOrder = bornObj.GetComponent<SpriteRenderer>().sortingOrder + 10;
        int i = 0;

        //每一个顾客走到最后
        while (newCustomer.transform.position != outDoorObj.transform.position)
        {

            if (FloorManager.Instance.custormWay.Count <= 0)
            {
                Debug.Log("没有生成路径。。。");
                break;
            }
            GameObject nextObj = FloorManager.Instance.allFloor[FloorManager.Instance.custormWay[i]];
            Vector3 nextPos = nextObj.transform.position;

            //Debug.Log((newCustomer.transform.position - nextPos).normalized);
            Vector3 jugeVector = (newCustomer.transform.position - nextPos).normalized;
            if(jugeVector.z > 0)
            {
                arc.animation.Play("face_walk", -1);
                newCustomer.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                arc.animation.Play("back_walk", -1);
                //旋转一个角度
                newCustomer.transform.localRotation = Quaternion.Euler(180f, 0, 180f);

            }
            newCustomer.transform.DOMove(nextPos, 0.3f).SetEase(Ease.Linear).OnComplete(delegate
            {
                newCustomer.transform.SetParent(nextObj.transform);
            });


            yield return new WaitForSeconds(0.3f);

            //走到了下一个点位，看看有没有能交互的货架
            int left = FloorManager.Instance.custormWay[i] - 1;
            int right = FloorManager.Instance.custormWay[i] + 1;
            int forward = FloorManager.Instance.custormWay[i] + 10;

            //判断是否是顾客需要的货架，然后再判断货架上是否有货物  

            if (Player.huojiaType.ContainsKey(left))
            {
                int huojiaTypeId = Player.huojiaType[left];
                GameObject huojiaObj = Player.huojiaObjs[left];
                Debug.Log(huojiaObj.name);
                int huojiaSaleTimes =  huojiaObj.GetComponent<HuoJiaController>().saleTimes;


                int customerNeedHuojiaId = DataManager.Instance.customerXml.GetInt(customerId, "shelfId");
                if (huojiaTypeId == customerNeedHuojiaId && ctn.buyTimes > 0 && huojiaSaleTimes > 0) {
                    Debug.Log("左边有货架" + huojiaTypeId);
                    arc.animation.Play("back_work", -1);
                    newCustomer.transform.localRotation = Quaternion.Euler(180f, 0, 180f);
                    yield return new WaitForSeconds(1f);
                    ctn.buyTimes--;
                    huojiaSaleTimes--;
                    huojiaObj.GetComponent<HuoJiaController>().saleTimes = huojiaSaleTimes;
                    huojiaObj.GetComponent<HuoJiaController>().goodsNumber -= 100;
                    if (ctn.goods.ContainsKey(huojiaObj.GetComponent<HuoJiaController>().goodsType))
                    {
                        ctn.goods[huojiaObj.GetComponent<HuoJiaController>().goodsType] += 100;
                    }
                    else
                    {
                        ctn.goods.Add(huojiaObj.GetComponent<HuoJiaController>().goodsType, 100);
                    }
                }
                if (huojiaSaleTimes == 0)
                {
                    UnityEngine.Transform goodsObj = huojiaObj.transform.Find("goods");
                    goodsObj.GetComponent<SpriteRenderer>().sprite = null;
                }
            }

            if (Player.huojiaType.ContainsKey(right))
            {
                int huojiaTypeId = Player.huojiaType[right];
                GameObject huojiaObj = Player.huojiaObjs[right];
                int huojiaSaleTimes = huojiaObj.GetComponent<HuoJiaController>().saleTimes;

                int customerNeedHuojiaId = DataManager.Instance.customerXml.GetInt(customerId, "shelfId");
                if (huojiaTypeId == customerNeedHuojiaId && ctn.buyTimes > 0 && huojiaSaleTimes > 0)
                {
                
                    Debug.Log("右边有货架");
                    arc.animation.Play("face_work", -1);
                    newCustomer.transform.localRotation = Quaternion.Euler(0f, 180f, 0);
                    yield return new WaitForSeconds(1f);
                    ctn.buyTimes--;
                    huojiaSaleTimes--;
                    huojiaObj.GetComponent<HuoJiaController>().saleTimes = huojiaSaleTimes;
                    huojiaObj.GetComponent<HuoJiaController>().goodsNumber -= 100;
                    if (ctn.goods.ContainsKey(huojiaObj.GetComponent<HuoJiaController>().goodsType))
                    {
                        ctn.goods[huojiaObj.GetComponent<HuoJiaController>().goodsType] += 100;
                    }
                    else
                    {
                        ctn.goods.Add(huojiaObj.GetComponent<HuoJiaController>().goodsType, 100);
                    }
                }
                if (huojiaSaleTimes == 0)
                {
                    UnityEngine.Transform goodsObj = huojiaObj.transform.Find("goods");
                    goodsObj.GetComponent<SpriteRenderer>().sprite = null;
                }


            }
            if (Player.huojiaType.ContainsKey(forward))
            {
                int huojiaTypeId = Player.huojiaType[forward];
                GameObject huojiaObj = Player.huojiaObjs[forward];
                int huojiaSaleTimes = huojiaObj.GetComponent<HuoJiaController>().saleTimes;
                int customerNeedHuojiaId = DataManager.Instance.customerXml.GetInt(customerId, "shelfId");

                if (huojiaTypeId == customerNeedHuojiaId && ctn.buyTimes> 0 && huojiaSaleTimes > 0)
                {
                    Debug.Log("前面有货架");
                    arc.animation.Play("face_work", -1);
                    newCustomer.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    yield return new WaitForSeconds(1f);
                    ctn.buyTimes--;
                    huojiaSaleTimes--;
                    huojiaObj.GetComponent<HuoJiaController>().saleTimes = huojiaSaleTimes;
                    huojiaObj.GetComponent<HuoJiaController>().goodsNumber -= 100;
                    if (ctn.goods.ContainsKey(huojiaObj.GetComponent<HuoJiaController>().goodsType))
                    {
                        ctn.goods[huojiaObj.GetComponent<HuoJiaController>().goodsType] += 100;
                    }
                    else
                    {
                        ctn.goods.Add(huojiaObj.GetComponent<HuoJiaController>().goodsType, 100);
                    }
                }

                if (huojiaSaleTimes == 0)
                {
                    UnityEngine.Transform goodsObj = huojiaObj.transform.Find("goods");
                    goodsObj.GetComponent<SpriteRenderer>().sprite = null;
                }


            }


            //到了结账的位置
            if (newCustomer.transform.position == outDoorObj.transform.position)
            {
                Debug.Log("到了结账的位置");
                arc.animation.Play("back_buy", -1);
                //旋转一个角度
                newCustomer.transform.localRotation = Quaternion.Euler(180f, 180f, 180f);
                Dianyuan.animation.Play("face_work", 1);
                yield return new WaitForSeconds(0.5f);

                //结账结束后 添加玩家金钱 其实是要根据每一个顾客是否买到了对应产品 是否满意来进行操作的
                
                if(ctn.buyTimes > 0)
                {
                    //没有买到足够的商品 不结账
                    DataManager.Instance.msgText = "没有买到足够的商品，摔货。";
                    UIManager.Instance.ShowMessagePanel();
                    koubei = 0; //只要有一个没有完美接待的 口碑不涨。
                }
                else
                {
                    foreach (int goodsNum in ctn.goods.Keys)
                    {
                        int salePrice = DataManager.Instance.goodsData.GetInt(goodsNum,"sell");
                        Player.GoldNum += salePrice * ctn.goods[goodsNum];

                    }
                    Player.PlayerExp += 1;
                }


                ctn.goods.Clear();
                moneyLabel.text = Player.GoldNum.ToString();
                playerExpLabel.text = Player.PlayerExp.ToString();



            }


            //走到下一块地板上时，看下左右前 是否有对应的货架 如果有 激发交互行为。
            int index = FloorManager.Instance.GetObjName(newCustomer.transform.parent.gameObject);



            i++;
        }

        yield return new WaitForSeconds(0.3f);

        tempCustomers.Remove(newCustomer);
        Destroy(newCustomer);
    }




    void ShopEnd()
    {

        CloseShop(true);
        StartCoroutine(StartJisuan());
       
    }

    IEnumerator  StartJisuan()
    {
        while(true){
            yield return new WaitForSeconds(Define.CUSTOMER_TIME);
            if(tempCustomers.Count == 0){

                jinbi = Player.GoldNum - jinbi;
                exp = Player.PlayerExp - exp;
                
                //到最后一个人结束 在调用 
                UIManager.Instance.ShowJiesuanPanel();

                Debug.Log("买完啦...");

              
                break;
            }
        }
 
    }

    public void ChangeButtonState(bool state)
    {
        wellcomeBtn.enabled = state;
        foreach (UIButton btn in huojiaBtnList)
        {
            btn.enabled = state;
        }
    }


    public void StateChanged(){
        switch(boss.State)
        {
            case(Boss.GameState.preparShop):
                {
                    Debug.Log("我想去买东西...");
                }
                break;
            case (Boss.GameState.Shoping):
                {
                    Debug.Log("我正在买东西...");
                }
                break;
        }
    }


    //---------------------------------------------||||||||||||||||||||||||-----------------------------------------
    DragonBones.UnityArmatureComponent Dianyuan;
    void BornGetMoneyPeople()
    {
        GameObject _ShouyinyuanPlace = GameObject.Find("GetMoneyPenploPlace");
        GameObject CustomerCubeObj = (GameObject)Instantiate(Resources.Load("NewCustomer/Dianyuan"), _ShouyinyuanPlace.transform.position, Quaternion.Euler(90, 0, 0));
        Dianyuan = LongGuManager.GetInstance().CreatLongGu("LongGuFemale");
        List<DragonBones.Slot> maleSlotAll = new List<DragonBones.Slot>();
        maleSlotAll = LongGuManager.GetInstance().GetLongGuSlotAll(Dianyuan);
        LongGuManager.GetInstance().ChangeClothesAll("LongGuFemale", maleSlotAll, 11);
        Dianyuan.gameObject.AddComponent<LongguFollow>()._CustomerMov = CustomerCubeObj;
        Dianyuan.transform.GetComponent<DragonBones.UnityArmatureComponent>().sortingGroup.sortingOrder = 6;
    }

    //在UI上生成货架UI的方法。
    public void GetHuojiaUI()
    {
        UnityEngine.Transform _huojiaUIRoot = transform.Find("ShangCheng/ZhuangxiuButton/Tubiao/HuojiaGameObject");
        for (int i = 0; i < _huojiaUIRoot.childCount; i++)
        {
            Destroy(_huojiaUIRoot.GetChild(i).gameObject);
        }

        Debug.Log("=====================货架的数量"+DataManager.Instance.huojiaId.Count);

        huojiaBtnList.Clear();
        for (int i = 0; i < DataManager.Instance.huojiaId.Count; i++)
        {
            GameObject HuojiaObj = (GameObject)Instantiate(Resources.Load("UI/HuojiaButton"));
            HuojiaObj.transform.SetParent(_huojiaUIRoot);
            _huojiaUIRoot.GetComponent<UIGrid>().enabled = true;
            HuojiaObj.transform.Find("huojiaSprite").GetComponent<UISprite>().spriteName =DataManager.Instance.huojiaXml.GetString(DataManager.Instance.huojiaId[i], "name");
            HuojiaObj.transform.Find("MoneySprite/Num_Label").GetComponent<UILabel>().text = DataManager.Instance.huojiaXml.GetString(DataManager.Instance.huojiaId[i], "coin");
            int huojiaTage = int.Parse(HuojiaObj.transform.Find("MoneySprite/Num_Label").GetComponent<UILabel>().text);
            HuojiaObj.transform.localPosition = Vector3.zero;
            HuojiaObj.transform.localScale = Vector3.one;
            //HuojiaObj.GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { BornHuojia(GameDataManger.Instance._huojiaUIId[iiii]); }));
            int num = DataManager.Instance.huojiaId[i];
            UIButton btn = HuojiaObj.GetComponent<UIButton>();
            btn.onClick.Add(new EventDelegate(() => { AddHuoJia(num); }));
            huojiaBtnList.Add(btn);
            Debug.Log("货架id = " + DataManager.Instance.huojiaId[i]);
        }

    }




}

public class Boss
{
    public enum GameState
    {
        preparShop,
        Shoping
    }
    private GameState _state = GameState.preparShop;

    public GameState State{
        set{
            if(value != _state)
            {
                SelectPanel.selectManager.StateChanged();
            }
            _state = value;
        }
        get{
            return _state;
        }
    }

    public static Boss CreatBossWithProperty(GameState state){
        Boss boss = new Boss();
        boss.State = state;
        return boss;
    }

}
