using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour {
    List<GameObject> _Cube = new List<GameObject>();
    GameObject[] _hh;//货架
    NavMeshAgent _agent;
    GameObject _lidianPlace;//离开的门的位置。
    int min = -1;//人物寻找货架的点。
    GameObject _HuojiaAgent;//货架目标点
    GameObject _MoneyTai;//收银台
    Transform _MoneyTaiPlace;//收银台前面的位置1
    Transform _MoneyTaiPlace2;//收银台前面的位置2
    Transform _MoneyTaiPlace3;//收银台前面的位置3
    RaycastHit HitInfo;
  public  Transform _lonnggu;
  GameObject _HeartNum;
    DragonBones.Animation _anim;
    bool _isFindWood = false;//是否找到相应的商品。
    bool _isHuojiaTurn;//判断货架是否旋转。
    bool _huxi;//呼吸
    Transform _thinkBorn;
    Transform _think;
    GameObject _imgThink;//龙骨动画拿的物品
    GameObject _timejingdutiao;//时间进度条
    Timer _timer;
    GameObject _Money;
    float timer_i;
    bool _IsCustomerMove;//判断是否是拖拽状态
    Vector3 screenPoint;
    Vector3 offset;
    bool _isMoneyTaiUse;//判断收银台1是否被使用。
    bool _isMoneyTaiUse2;//判断收银台2是否被使用。
	void Start () 
    {
        _hh = FindAllHuojia.Instance._allHuojia;
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _lidianPlace = GameObject.Find("LiKaiPlace");
        _MoneyTai = GameObject.Find("收银台");
        _MoneyTaiPlace = _MoneyTai.transform.Find("PeoplePlace");
        _MoneyTaiPlace2 = _MoneyTai.transform.Find("PeoplePlace 2");
        _MoneyTaiPlace3 = _MoneyTai.transform.Find("PeoplePlace 3");
        _anim = _lonnggu.GetComponent<DragonBones.UnityArmatureComponent>().animation;
        _thinkBorn = _lonnggu.transform.Find("ThinkBorn");
        _timer = Timer.createTimer("Timer");
        _huxi = false;
        _HeartNum = GameObject.Find("xinNum");
        _Money = GameObject.Find("money");
       //_timejingdutiao = _thinkBorn.GetComponent<UIFollowNG>().hud;
      // Debug.Log("1111"+_timejingdutiao);
       _timer = Timer.createTimer("Timer");                                                                                                                          
       _IsCustomerMove = false;      
        foreach (var item in _hh)
        {
            _Cube.Add(item);
        }
        if (_IsCustomerMove == false)
        {
            FindHuojia();
            InvokeRepeating("FindNextHuojia", 0f, 8f);
        }
        _lonnggu.localRotation = Quaternion.Euler(90, 0, 0);     
	}	
	// Update is called once per frame
	void Update () 
    {
        _isMoneyTaiUse = _MoneyTaiPlace.GetComponent<MoneyTai>()._isUse;
        _isMoneyTaiUse2 = _MoneyTaiPlace2.GetComponent<MoneyTai>()._isUse;
        Debug.Log(_isMoneyTaiUse);
       // _lonnggu.localRotation = Quaternion.Euler(90, 0, 0);
        CheckBarrier();       
       if (Vector3.Distance(transform.position, _MoneyTaiPlace.transform.position) <= 0.2)
        {
            //Debug.Log("到达收银台");
            CustomerRotate(0);
            PlayAnim(3);
            _think.gameObject.SetActive(false);
            _thinkBorn.GetComponent<UIFollowNG>().hud.SetActive(true);
            //_timeCanvas.gameObject.SetActive(true);
            if (timer_i <= 1)
            {
                timer_i = timer_i + Time.deltaTime * 0.2f;
                _thinkBorn.GetComponent<UIFollowNG>().hud.GetComponent<UIProgressBar>().value = timer_i;
            }
            else
            {
                CustomerXQgood();
            }
            Destroy(gameObject, 20.5f);
            _lonnggu.GetComponent<LongguFollow>().DesLongGu(20.5f);
            Invoke("WritingPay",20f);
       }
       if (Vector3.Distance(transform.position, _MoneyTaiPlace2.transform.position) <= 0.2)
       {
           CustomerRotate(0);
           PlayAnim(3);
           _think.gameObject.SetActive(false);
           if (_isMoneyTaiUse == false)
           {
               if (Vector3.Distance(transform.position, _MoneyTaiPlace.transform.position) > 0.2)
               {
                   MoveTohuojia(_MoneyTaiPlace.gameObject);
               }
           }
       }
	}
    void FindHuojia()//找到距离人物最近的货架
    {
        if (_Cube.Count== 0)
        {
            MoveTohuojia(_lidianPlace);
            Invoke("CustomerXQsad", 3f);
            HeartText.Instance._Num -= 1;
            if (HeartText.Instance._Num < 0)
            {
                HeartText.Instance._Num = 0;
            }
            HeartText.Instance._textHeart = HeartText.Instance._Num.ToString();
            _HeartNum.GetComponent<UILabel>().text = HeartText.Instance._textHeart;
            Destroy(gameObject, 10f);
            _lonnggu.GetComponent<LongguFollow>().DesLongGu(10f);
            return;
        }
        if (_Cube.Count == 1)
        {
            min = 0;
            _HuojiaAgent = _Cube[min].transform.Find("CustomerPlace").gameObject;
            MoveTohuojia(_HuojiaAgent);
            return;
        }
        for (int i = 0; i < _Cube.Count - 1; ++i)
        {
            min = i;          
            for (int j = i + 1; j < _Cube.Count; ++j)
            {
                if (GetDis(_Cube[j]) < GetDis(_Cube[min]))
                {
                    min = j;
                }
            }
            _HuojiaAgent = _Cube[min].transform.Find("CustomerPlace").gameObject;
            MoveTohuojia(_HuojiaAgent);
        }
        
    }
    float GetDis(GameObject Dis)//检测人物和货架的距离
    {
        return Vector3.Distance(Dis.transform.position, transform.position);
    }
    void MoveTohuojia(GameObject obj)//移动到目标货架
    {
        _agent.SetDestination(obj.transform.position);
    }
    void FindNextHuojia()//寻找下一个货架
    {
        if (GetDis(_Cube[min].transform.Find("CustomerPlace").gameObject) <= 0.5f)
        {
            if (_Cube[min].name == "蛋糕货架2(Clone)" && _lonnggu.GetComponent<CustomerThink>().Rad == 0)
            {
                PlayAnim(5);
                _think = _thinkBorn.Find("Think(Clone)");             
                _think.gameObject.SetActive(false);
                _thinkBorn.GetComponent<UIFollowNG>().hud.SetActive(true);
                _timer.startTiming(2, OnComplete, OnProcess);
                GameObject img = Resources.Load("ThinkPa/dangao") as GameObject;
                _imgThink = Instantiate(img) as GameObject;
                _imgThink.transform.SetParent(_lonnggu);
                _isFindWood = true;            
               // MoveTohuojia(_MoneyTaiPlace.gameObject);
            }
            else if (_Cube[min].name == "甜点货架2(Clone)" && _lonnggu.GetComponent<CustomerThink>().Rad == 1)
            {
                PlayAnim(5);
                _think = _thinkBorn.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _thinkBorn.GetComponent<UIFollowNG>().hud.SetActive(true);
                _timer.startTiming(2, OnComplete, OnProcess);
                GameObject img = Resources.Load("ThinkPa/tiandian") as GameObject;
                _imgThink = Instantiate(img) as GameObject;
                _imgThink.transform.SetParent(_lonnggu);
                _isFindWood = true;
               
            }
            else if (_Cube[min].name == "面包货架2(Clone)" && _lonnggu.GetComponent<CustomerThink>().Rad == 2)
            {
                PlayAnim(5);
                _think = _thinkBorn.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _thinkBorn.GetComponent<UIFollowNG>().hud.SetActive(true);
                _timer.startTiming(2, OnComplete, OnProcess);
                GameObject img = Resources.Load("ThinkPa/mianbao") as GameObject;
                _imgThink = Instantiate(img) as GameObject;
                _imgThink.transform.SetParent(_lonnggu);
                _isFindWood = true;               
            }
            else if (_Cube[min].name == "蔬菜货架2(Clone)" && _lonnggu.GetComponent<CustomerThink>().Rad == 3)
            {
                PlayAnim(5);
                _think = _thinkBorn.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _thinkBorn.GetComponent<UIFollowNG>().hud.SetActive(true);
                _timer.startTiming(2, OnComplete, OnProcess);
                GameObject img = Resources.Load("ThinkPa/Huluobo") as GameObject;
                _imgThink = Instantiate(img) as GameObject;
                _imgThink.transform.SetParent(_lonnggu);
                _isFindWood = true;               
            }
            else
            {
                _Cube.Remove(_Cube[min]);
                FindHuojia();
            }
        }
    }
    void OnComplete()
    {
        _thinkBorn.GetComponent<UIFollowNG>().hud.GetComponent<UIProgressBar>().value = 0;
        _thinkBorn.GetComponent<UIFollowNG>().hud.gameObject.SetActive(false);
        PlayAnim(5);
        if (_isMoneyTaiUse == false)
        {
            MoveTohuojia(_MoneyTaiPlace.gameObject);
        }
        else
        {
            MoveTohuojia(_MoneyTaiPlace2.gameObject);
        }
        //Invoke("CustomerXQgood", 3f);
        textMoney.Instance._Momey += 10;
        //Destroy(gameObject, 20f);
        //_Armature.GetComponent<LongguFollow>().DesLongGu(20f);
        HeartText.Instance._Num += 1;
        HeartText.Instance._textHeart = HeartText.Instance._Num.ToString();
        _HeartNum.GetComponent<UILabel>().text = HeartText.Instance._textHeart;
        textMoney.Instance._textMoney = textMoney.Instance._Momey.ToString();
        _Money.GetComponent<UILabel>().text = textMoney.Instance._textMoney;
    }
    void OnProcess(float p)
    {
        _thinkBorn.GetComponent<UIFollowNG>().hud.GetComponent<UIProgressBar>().value = p;
        PlayAnim(3);
    }
    //龙骨动画
    void PlayAnim(int Rad)
    {
        switch (Rad)
        {
            case 0:
                if (_anim.lastAnimationName != "face_breathe")
                {
                    _anim.Play("face_breathe");
                }
                break;
            case 1:
                if (_anim.lastAnimationName != "face_walk1")
                {
                    _anim.Play("face_walk1");
                }
                break;
            case 2:
                if (_anim.lastAnimationName != "face_walk2")
                {
                    _anim.Play("face_walk2");
                }
                break;
            case 3:
                if (_anim.lastAnimationName != "back_breathe")
                {
                    _anim.Play("back_breathe");
                }
                break;
            case 4:
                if (_anim.lastAnimationName != "back_walk1")
                {
                    _anim.Play("back_walk1");
                }
                break;
            case 5:
                if (_anim.lastAnimationName != "back_walk2")
                {
                    _anim.Play("back_walk2");
                }
                break;
            default:
                break;
        }
    }
    void CustomerRotate(int rad)
    {
        switch (rad)
        {
            case 0:
                _lonnggu.localRotation = Quaternion.Euler(-90, 180, 0);
                break;
            case 1:
                _lonnggu.localRotation = Quaternion.Euler(90, 0, 0);
                break;
        }
    }
    /// <summary>
    /// 根据射线射到墙上的位置变化，龙骨移动动画变化。
    /// </summary>
    void CheckBarrier()
    {
        Ray ray1 = new Ray(transform.Find("shexianPlace").position, transform.forward);
        if (Physics.Raycast(ray1, out HitInfo))
        {
            Debug.DrawLine(transform.Find("shexianPlace").position, HitInfo.transform.position, Color.red);
            Debug.Log(HitInfo.transform.name);
            if (HitInfo.transform.name == "Forword")
            {
                if (_isFindWood == false)
                {
                    PlayAnim(1);
                    CustomerRotate(1);                 
                }
                else
                {
                    PlayAnim(2);
                    CustomerRotate(1); 
                    _imgThink.transform.localRotation = Quaternion.identity;
                    _imgThink.transform.localPosition = new Vector3(0.2f,0.4f,0);
                    _imgThink.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
            }
            else if (HitInfo.transform.name == "Right")
            {
                if (_isFindWood == false)
                {
                    PlayAnim(1);
                    CustomerRotate(0);
                }
                else
                {
                    PlayAnim(2);
                    CustomerRotate(0);
                    _imgThink.transform.localRotation = Quaternion.identity;
                    _imgThink.transform.localPosition = new Vector3(0.2f, 0.4f, 0);
                    _imgThink.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
            }
            else if (HitInfo.transform.name == "Back")
            {
                if (_isFindWood == false)
                {
                    PlayAnim(4);
                    CustomerRotate(1);
                }
                else
                {
                    PlayAnim(5);
                    CustomerRotate(1);
                    _imgThink.transform.localRotation = Quaternion.identity;
                    _imgThink.transform.localPosition = new Vector3(-0.25f, 0.6f, 0);
                    _imgThink.GetComponent<SpriteRenderer>().sortingOrder = -1;
                }
            }
            else if (HitInfo.transform.name == "Left")
            {
                if (_isFindWood == false)
                {
                    PlayAnim(4);
                    CustomerRotate(0);
                }
                else
                {
                    PlayAnim(5);
                    CustomerRotate(0);
                    _imgThink.transform.localRotation = Quaternion.identity;
                    _imgThink.transform.localPosition = new Vector3(-0.25f, 0.6f, 0);
                    _imgThink.GetComponent<SpriteRenderer>().sortingOrder = -1;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Floor")
        {
            string floorNum = other.transform.parent.parent.name;
            string result = System.Text.RegularExpressions.Regex.Replace(floorNum, @"[^0-9]+", "");
            int ss = int.Parse(result);
            _lonnggu.GetComponent<DragonBones.UnityArmatureComponent>().sortingOrder = ss+11;
        }
    }
    Transform _xinqin;
    SpriteRenderer _xinqinTetrue;
    void CustomerXQgood()//人物心情方法
    {
        Debug.Log("心情好");
        _think = _thinkBorn.Find("Think(Clone)");
        _think.gameObject.SetActive(true);
        _thinkBorn.GetComponent<UIFollowNG>().hud.gameObject.SetActive(false);
        _xinqin = _think.Find("Pacture");
        _xinqinTetrue = _xinqin.GetComponent<SpriteRenderer>();
        Texture2D img = Resources.Load("ThinkPa/happy") as Texture2D;
        Sprite pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
        _xinqinTetrue.sprite = pic;
        _xinqin.transform.localPosition = new Vector3(0, 0, 0);
    }
    void CustomerXQsad()
    {
        Debug.Log("心情烦躁");
        _think = _thinkBorn.Find("Think(Clone)");
        _think.gameObject.SetActive(true);
        _xinqin = _think.Find("Pacture");
        _xinqinTetrue = _xinqin.GetComponent<SpriteRenderer>();
        Texture2D img = Resources.Load("ThinkPa/sad") as Texture2D;
        Sprite pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
        _xinqinTetrue.sprite = pic;
        _xinqin.transform.localPosition = new Vector3(0, 0, 0);
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    void OnMouseDrag()
    {
        _IsCustomerMove = true;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
    void OnMouseUp()
    {
        FindNextHuojia();
    }
    void WritingPay()//排队支付方法
    {
        _MoneyTaiPlace.GetComponent<MoneyTai>()._isUse=false;
    }
}
