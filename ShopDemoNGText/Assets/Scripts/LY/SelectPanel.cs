using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigDefine;
using DragonBones;
using DG.Tweening;
public class SelectPanel : MonoBehaviour {

    private readonly string wellcomeBtnPath = @"ShangCheng/GameBeginCstomer/WelcomButton";
    private UIButton wellcomeBtn;

    private string huojiaBtnPath = @"ShangCheng/ZhuangxiuButton/Tubiao/HuojiaGameObject/HuojiaButton";
    private UIButton huojiaBtn;

  


    public static SelectPanel selectManager;

    Boss boss;


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
        if (Input.GetKeyDown(KeyCode.W))
        {
           // GameDataManger.Instance._huojiaUIId.Add(10001);
            GetHuojiaUI();

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameDataManger.Instance._huojiaUIId.Add(20001);
            GameDataManger.Instance._huojiaUIId.Add(30001);
            GameDataManger.Instance._huojiaUIId.Add(40001);
            GameDataManger.Instance._huojiaUIId.Add(50001);
            GameDataManger.Instance._huojiaUIId.Add(60001);
            GameDataManger.Instance._huojiaUIId.Add(70001);
            GameDataManger.Instance._huojiaUIId.Add(80001);
            GameDataManger.Instance._huojiaUIId.Add(90001);
            GetHuojiaUI();

        }
    }

    void InitComponet()
    {
        wellcomeBtn = transform.Find(wellcomeBtnPath).GetComponent<UIButton>();
        wellcomeBtn.onClick.Add(new EventDelegate(WellComeBtnClick));

        //huojiaBtn = transform.Find(huojiaBtnPath).GetComponent<UIButton>();
        //huojiaBtn.onClick.Add(new EventDelegate(AddHuoJia));
 
        BornGetMoneyPeople();

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
            HuojiaObj.GetComponent<UIButton>().onClick.Add(new EventDelegate(() => { AddHuoJia(num); }));

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
