﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigDefine;
using DragonBones;
using DG.Tweening;
public class SelectPanel : MonoBehaviour {

    #region 这里都是label 显示 一些数值
    private string moneyPath = @"ShangLan/Money_Jingbi/Money";
    UILabel moneyLabel;

    private string playerLevelLabelPath = @"ShangLan/PalyerLevelNum_Sprite/PalyerLevelNum";
    UILabel playerLevelLabel;

    private string playerExpLabelPath = @"ShangLan/PalyerLevelExp_Sprite/PalyerLevelExpNum";
    UILabel playerExpLabel;

    private string diamondLabelPath = @"ShangLan/Money_zhuanshi/zuanshiNum";
    UILabel diamondLabel;

    private string shopLevelLabelPath = @"ZhongjianButton/dainpu/ShopLevelNum_Sprite/ShopLevelNum";
    UILabel shopLevelLabel;
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


    #endregion

    #region 底部栏的UI transform 父级控制组件

    private string cangchuPath = @"ShangCheng/CangkuButton/Diban/cangkuUI";
    UnityEngine.Transform cangchuUI;

    private string zhuangxiuUIPath = @"ShangCheng/ZhuangxiuButton/Tubiao";
    UnityEngine.Transform zhuangxiuUI;
    #endregion



    List<UIButton> huojiaBtnList = new List<UIButton>();

    public static SelectPanel selectManager;

    Boss boss;


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
                }
                break;

            case ("jinhuoButton"):
                {
                    UIManager.Instance.ShowBuyPanel();
                }
                break;
            case ("CangkuButton"):
                {
                    Debug.Log("点击了 仓库 按钮");
                }
                break;
            case ("ZhuangxiuButton"):
                {
                    Debug.Log("点击了 装修 按钮");
                }
                break;

        }

    }


    void OpenBuyPanelButton(UnityEngine.Transform _buypanel, bool OpenOrClose)
    {

        Debug.Log("ssssssssssssssssss");
        _buypanel.gameObject.SetActive(OpenOrClose);
        //在仓储界面显示出所有库存Ap内所有的物品
        if (_buypanel == cangchuUI)
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

    public void CahngkuUI(short _id, int _num)
    {
        //GameObject CucunGoodsObj = ((GameObject)Instantiate(Resources.Load("UI/CangchuGood")));
        //UnityEngine.Transform _CunfangUI = cangchuUI.Find("cunfangUI");
        //CucunGoodsObj.transform.SetParent(_CunfangUI);
        //CucunGoodsObj.transform.Find("huowuUI").GetComponent<UISprite>().spriteName = GoodsData.GetString(_id, "name");
        //CucunGoodsObj.transform.Find("huowuUI/Label").GetComponent<UILabel>().text = _num.ToString();
        //CucunGoodsObj.transform.localPosition = Vector3.zero;
        //CucunGoodsObj.transform.localScale = Vector3.one;
        //_CunfangUI.GetComponent<UIGrid>().enabled = true;
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

    }


	
    private void AddHuoJia(int huojiaId)
    {
       

        Debug.Log("d点击了货架。" + huojiaId);
      
        
        //首先 检查一下不能交互的区域
        if (FloorManager.Instance.FetchActiveWay())
        {

            //首先创建一个货架
            GameObject huojiaObj = Instantiate(Resources.Load("HJPrefab/HJFruit" + huojiaId) as GameObject);
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
        
        wellcomeBtn.enabled = shopstate;
       // huojiaBtn.enabled = shopstate;
        foreach(UIButton btn in huojiaBtnList)
        {
            btn.enabled = shopstate;
        }



    }



    void WellComeBtnClick()
    {

        CloseShop(false);

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

        StartCoroutine(StartGame());

        


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


    IEnumerator GoShoping()
    {

        GameObject outDoorObj = FloorManager.Instance.allFloor[Define.OUT_DOOR_POS];
        GameObject bornObj = FloorManager.Instance.allFloor[Define.BORN_POS];

        GameObject newCustomer = Instantiate(Resources.Load("CustomerPrefab/Customer1") as GameObject,Vector3.zero,Quaternion.identity);
        newCustomer.transform.SetParent(bornObj.transform);
        newCustomer.transform.localPosition = Vector3.zero;
        newCustomer.transform.localRotation = Quaternion.identity;

        UnityArmatureComponent arc = newCustomer.GetComponent<UnityArmatureComponent>();
        arc.animation.Play("face_walk", -1);
        arc._sortingOrder = bornObj.GetComponent<SpriteRenderer>().sortingOrder + 1;
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

            // Debug.Log(FloorManager.Instance.custormWay[i]);
            // int index = FloorManager.Instance.custormWay[i];
            // Vector3 nextPos = FloorManager.Instance.allFloor[index].transform.position;



            //到了结账的位置
            if (newCustomer.transform.position == outDoorObj.transform.position)
            {
                Debug.Log("到了结账的位置");
                arc.animation.Play("back_buy", -1);
                //旋转一个角度
                newCustomer.transform.localRotation = Quaternion.Euler(180f, 180f, 180f);
                Dianyuan.animation.Play("face_work", 1);
                yield return new WaitForSeconds(0.5f);
            }


            //走到下一块地板上时，看下左右前 是否有对应的货架 如果有 激发交互行为。
            int index = FloorManager.Instance.GetObjName(newCustomer.transform.parent.gameObject);



            i++;
        }

        yield return new WaitForSeconds(0.3f);

        Destroy(newCustomer);
    }




    void ShopEnd()
    {

        Debug.Log("买完啦...");
        CloseShop(true);
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
