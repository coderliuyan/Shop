using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour {
    GameObject MyGoods;
   // GameObject Cards;
    GameObject shangcheng;
    GameObject _startbtn;//开启店铺按钮
    public  GameObject _Closebtn;//关闭店铺按钮
    GameObject _lable;
    Timer timer;//计时器
    GameObject[] Shades;
    //Timer timer1;
    GameObject GamePrefab;
    //float GreatTime=0;
    public GameObject _Born;
    Transform _time;//计时图片
    TableValue CardBattleDate;
    string Customername;
    string Customername1;
    string Customername2;
    GameObject _floor;
    GameObject[] _HuojiaButton;
    GameObject _qiang;
    GameObject _Longgucanvans;
	void Start () 
    {       
        MyGoods = GameObject.Find("MyGoods");
        //Cards = GameObject.Find("CardCnotainer");
        Shades = GameObject.FindGameObjectsWithTag("shade");
        shangcheng = GameObject.Find("ShangCheng");
        timer = Timer.createTimer("Timer");
        //timer1 = Timer.createTimer("Timer1");
        _Born = GameObject.Find("CustomerBornPlace");
        _time = GameObject.Find("Timer").transform.Find("Sprite") ;
        _floor = GameObject.Find("FloorGameManger");//找到地面     
        //Debug.Log(_time.name);
        _startbtn = GameObject.Find("OpenShop");
       //_Closebtn = GameObject.Find("CloseShop");
       // _Closebtn.SetActive(false);
        //加载Xml顾客类型表
        Object obj = Resources.Load("Xml/CustomerType");
        XmlHelper.Instance.LoadFile("CustomerType", obj);
        CardBattleDate = XmlHelper.Instance.ReadFile("CustomerType");
        Customername = CardBattleDate.GetString(1001, "name");
        Customername1 = CardBattleDate.GetString(1002, "name");
        Customername2 = CardBattleDate.GetString(1003, "name");    
        //Debug.Log(name);  
        _Longgucanvans = GameObject.Find("LongguCanvas");
	}	
	// Update is called once per frame
	void Update () 
    { 

	}
    void OnClick()
    {
        shangcheng.SetActive(false);
        _floor.AddComponent<FindAllHuojia>();//添加脚本找到所有货架。
        _HuojiaButton = FindAllHuojia.Instance._allHuojiaButton;
        foreach (GameObject HJbutton in _HuojiaButton)
        {
            Destroy(HJbutton);
            //HJbutton.SetActive(false);
        }
        if (_Closebtn != null)
        {
            _Closebtn.SetActive(true);
        }

        foreach (GameObject sha in Shades)
        {
            sha.SetActive(false);
        }
        // 计时器启动
        timer.startTiming(120, OnComplete, OnProcess);
        AddColider();
        //调用顾客随机方法,每隔5秒执行一次。
        InvokeRepeating("RandomCustomer", 5f, 20f);        
    }
    //计时完成
    void OnComplete()
    {
        Debug.Log("CloseShop");
    }
    // 计时器的进程  
    void OnProcess(float p)
    {
       // Debug.Log(p);
         _time.GetComponent<UISprite>().fillAmount=p;
       
    }
    //随机顾客
    void RandomCustomer()
    {
        //int ran = Random.Range(0,4);
        int ran = 0;
        //Debug.Log(ran);
            switch (ran)
            {
                case 0:
                    GamePrefab = (GameObject)Instantiate(Resources.Load("Customer/CustomerMan"), _Born.transform.position, Quaternion.Euler(90, 0, 0));
                    GameObject _man = (GameObject)Instantiate(Resources.Load("Customer/LongguMan"));
                    _man.GetComponent<LongguFollow>()._CustomerMov = GamePrefab;
                    GamePrefab.GetComponent<Move>()._lonnggu = _man.transform;                                            
                    break;
                case 1:
                    GamePrefab = (GameObject)Instantiate(Resources.Load("Customer/Customerwoman"), _Born.transform.position, Quaternion.Euler(90, 0, 0));
                    GameObject _woman = (GameObject)Instantiate(Resources.Load("Customer/LongguWoman"));
                    _woman.GetComponent<LongguFollow>()._CustomerMov = GamePrefab;
                    GamePrefab.GetComponent<Move>()._lonnggu = _woman.transform; 
                    break;
                case 2:
                    GamePrefab = (GameObject)Instantiate(Resources.Load("Customer/Customerwoman2"), _Born.transform.position,Quaternion.Euler(90,0,0));
                    GameObject _woman1 = (GameObject)Instantiate(Resources.Load("Customer/LongguWoman2"));
                    _woman1.GetComponent<LongguFollow>()._CustomerMov = GamePrefab;
                    GamePrefab.GetComponent<Move>()._lonnggu = _woman1.transform;
                    break;
                case 3:
                    GamePrefab = (GameObject)Instantiate(Resources.Load("Customer/CustomerMan2"), _Born.transform.position, Quaternion.Euler(90, 0, 0));
                    GameObject _man1 = (GameObject)Instantiate(Resources.Load("Customer/LongguMan2"));
                    _man1.GetComponent<LongguFollow>()._CustomerMov = GamePrefab;
                    GamePrefab.GetComponent<Move>()._lonnggu = _man1.transform;
                    break;
            }
          
        }
    GameObject[] _allFloor;
    List<GameObject> _newfloor= new List<GameObject>();
    GameObject _addCollider;
    void AddColider()
    {
        _allFloor = GameObject.FindGameObjectsWithTag("Floor");
        for (int i = 0; i < _allFloor.Length; i++)
            {
                _allFloor[i].GetComponent<BoxCollider>().enabled = false;
                _addCollider = (GameObject)Instantiate(Resources.Load("floor/Place1x1"));
                _addCollider.transform.parent = _allFloor[i].transform;
                _addCollider.transform.localPosition = Vector3.zero;
                _addCollider.transform.localRotation = Quaternion.identity;               
            }
             
    }
  }

