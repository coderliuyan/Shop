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
            FloorManager.Instance.FetchActiveFloor();
            Debug.Log(FloorManager.Instance.floorInterable.Count);
        }
    }

    void InitComponet()
    {
        wellcomeBtn = transform.Find(wellcomeBtnPath).GetComponent<UIButton>();
        wellcomeBtn.onClick.Add(new EventDelegate(WellComeBtnClick));

        huojiaBtn = transform.Find(huojiaBtnPath).GetComponent<UIButton>();
        huojiaBtn.onClick.Add(new EventDelegate(AddHuoJia));

    }
	
    private void AddHuoJia()
    {
       

        Debug.Log("d点击了货架。");
      
        
        //首先 检查一下不能交互的区域
        if (FloorManager.Instance.FetchActiveWay())
        {

            //首先创建一个货架
            GameObject huojiaObj = Instantiate(Resources.Load("HJPrefab/HJFruit1") as GameObject);
            Debug.Log("检查了路径，并有有效路径。");

            FloorManager.Instance.FetchActiveFloor();
            Debug.Log(" floor interface count =" + FloorManager.Instance.floorInterable.Count);
            //    foreach (GameObject obj in FloorManager.Instance.floorInterable.Values)
            //    {
            //        obj.GetComponent<SpriteRenderer>().color = Color.green;
            //    }

            //    foreach (int index in FloorManager.Instance.noBuildArea)
            //    {
            //        Debug.Log("-----------------------------");
            //        Debug.Log(index);
            //        if (FloorManager.Instance.allFloor.ContainsKey(index))
            //        {
            //            FloorManager.Instance.allFloor[index].GetComponent<SpriteRenderer>().color = Color.white;
            //        }
            //    }

        }

    }
    


    void WellComeBtnClick()
    {
        boss.State = Boss.GameState.Shoping;
        wellcomeBtn.enabled = false;

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
            //  Vector3 nextPos = FloorManager.Instance.allFloor[index].transform.position;



            i++;
        }

        yield return new WaitForSeconds(0.3f);

        Destroy(newCustomer);
    }




    void ShopEnd()
    {

        Debug.Log("买完啦...");
        boss.State = Boss.GameState.preparShop;
        wellcomeBtn.enabled = true;
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
