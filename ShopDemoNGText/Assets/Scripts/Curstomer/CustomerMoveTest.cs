using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerMoveTest : MonoBehaviour {

    int min = -1;
    List<GameObject> _Cube = new List<GameObject>();
    GameObject[] _hh;
    GameObject _MoneyPosition;
    Transform _think;
    Transform _Born;
    GameObject _timeCanvas;
    Timer _timer;
    bool _MoveMoney;
    NavMeshAgent _agent;
    DragonBones.Animation _anim;
    //Animation _anim;
    public Transform _Armature;
    bool _Huxi;
    private Vector3 screenPoint;
    private Vector3 offset;
    RaycastHit HitInfo;
    GameObject _lidian;
    GameObject _Money;
    GameObject _HeartNum;
    bool _IsCustomerMove;//判断是否是拖拽状态
    GameObject _zhuaitarget;//拖拽的Target
    //Transform _TimeCanvas;
       bool _isFindWood=false;
       GameObject imgThink;//龙骨动画拿的物品
       int layer;//物品层级
    void Start()
    {
        _hh = FindAllHuojia.Instance._allHuojia;
        _MoneyPosition = GameObject.Find("PPD");
        _Born = transform.Find("Born");
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _lidian = GameObject.Find("lidian");
        _Huxi = false;
        _Money = GameObject.Find("money");
        _HeartNum = GameObject.Find("xinNum");
        _IsCustomerMove = false;
        //_FilpX = _Armature.GetComponent<DragonBones.UnityArmatureComponent>().flipX;
        foreach (var item in _hh)
        {
            _Cube.Add(item);
        }
        if (_IsCustomerMove == false)
        {
            FindHuojia();
            InvokeRepeating("FindNext", 0f, 6f);
        }
    }
    void Awake()
    {
        //  _Armature = gameObject.transform.FindChild("Armature");
        if (_Armature != null)
        {
            _anim = _Armature.GetComponent<DragonBones.UnityArmatureComponent>().animation;
        }

    }

   // float time_f;
    float timer_i;
    // Update is called once per frame
    void LateUpdate()
    {
        CheckBarrier();
        //if (_Huxi == false)
        //{
        //    CheckBarrier();
        //}
        if (Vector3.Distance(transform.position, _MoneyPosition.transform.position) <= 4)
        {
            //Debug.Log("到达收银台");
            CustomerRotate(0);
            PlayAnim(3);            
            _think.gameObject.SetActive(false);
            _timeCanvas.gameObject.SetActive(true);
            if (timer_i <= 1)
            {
                timer_i = timer_i + Time.deltaTime * 0.2f;      
            }
            else
            {
                CustomerXQgood();
            }
           
        }
    }
    float GetDis(GameObject Dis)//检测人物和货架的距离
    {
        return Vector3.Distance(Dis.transform.position, transform.position);
    }
    void FindHuojia()//找到距离人物最近的货架
    {
       // Debug.Log("FindHuojia");
        if (_Cube.Count == 0)
        {
            MoveToHuojia(_lidian);
            Invoke("CustomerXQsad",3f);
            HeartText.Instance._Num -= 1;
            if (HeartText.Instance._Num < 0)
            {
                HeartText.Instance._Num = 0;
            }
            HeartText.Instance._textHeart = HeartText.Instance._Num.ToString();
            _HeartNum.GetComponent<UILabel>().text = HeartText.Instance._textHeart;
            Destroy(gameObject, 10f);
            _Armature.GetComponent<LongguFollow>().DesLongGu(10f);
            return;
        }
        if (_Cube.Count == 1)
        {
            min = 0;
            MoveToHuojia(_Cube[min]);
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
            MoveToHuojia(_Cube[min]);
        }
         //FindNext();
    }
    int _pos;
    void MoveToHuojia(GameObject obj)//移动到货架
    {
       // Debug.Log("111111" + obj);
        _agent.SetDestination(obj.transform.position);
        //PlayAnim(_pos);
    }
    public void FindNext()//寻找下一个货架
    {
        // _Huxi = !_Huxi;
        if (GetDis(_Cube[min]) <= 6)
        {
            if (_Cube[min].name == "huojia__shechipin(Clone)" && CustomerThink.Instance.Rad == 0)
            {
                _Huxi = true;
                 Debug.Log("找到1");
                _think = _Born.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _timeCanvas.gameObject.SetActive(true);
                GameObject img = Resources.Load("ThinkPa/dangao") as GameObject;
                imgThink = Instantiate(img) as GameObject;
                //Transform img =Instantiate( Resources.Load("ThinkPa/mianbao"))as Transform;
               // Debug.Log(_Armature.GetComponent<LongguFollow>()._Foodget);
                Debug.Log("0000"+img);
                imgThink.transform.SetParent(_Armature);
                _isFindWood = true;
               // img.transform.SetParent(_Armature.GetComponent<LongguFollow>()._Foodget);
                            
                _timer.startTiming(2, OnComplete, OnProcess);
            }
            else if (_Cube[min].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
            {
                _Huxi = true;
                Debug.Log("找到2");
                _think = _Born.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _timeCanvas.gameObject.SetActive(true);
                GameObject img = Resources.Load("ThinkPa/tiandian") as GameObject;
                imgThink = Instantiate(img) as GameObject;
                Debug.Log("111"+img);
                imgThink.transform.SetParent(_Armature);
                _isFindWood = true;
           
                _timer.startTiming(2, OnComplete, OnProcess);
            }
            else if (_Cube[min].name == "huojia_huazhuangpin(Clone)" && CustomerThink.Instance.Rad == 2)
            {
                _Huxi = true;
                Debug.Log("找到3");
                _think = _Born.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _timeCanvas.gameObject.SetActive(true);
                GameObject img = Resources.Load("ThinkPa/mianbao")as GameObject;
                imgThink = Instantiate(img) as GameObject;
                //Transform img = Instantiate(Resources.Load("ThinkPa/mianbao")) as Transform;
                //Debug.Log(_Armature.GetComponent<LongguFollow>()._Foodget);
                Debug.Log("222"+img);
               // img.transform.SetParent(_Armature.GetComponent<LongguFollow>()._Foodget);
                imgThink.transform.SetParent(_Armature);
                _isFindWood = true;
                _timer.startTiming(2, OnComplete, OnProcess);
            }
            else if (_Cube[min].name == "huojia_twoge(Clone)" && CustomerThink.Instance.Rad == 3)
            {
                _Huxi = true;
                _think = _Born.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _timeCanvas.gameObject.SetActive(true);            
                GameObject img = Resources.Load("ThinkPa/Huluobo") as GameObject;
                imgThink = Instantiate(img) as GameObject;
                //Transform img =Instantiate(Resources.Load("ThinkPa/Huluobo"))as Transform;
                 //Debug.Log(_Armature.GetComponent<LongguFollow>()._Foodget);
                Debug.Log("333"+img);
                imgThink.transform.SetParent(_Armature);
                _isFindWood = true;
                _timer.startTiming(2, OnComplete, OnProcess);
            }
            else
            {
                _Cube.Remove(_Cube[min]);
                FindHuojia();
            }
        }
    }

    void PlayAnim(int Rad)
    {
        if (_Armature == null)
        {
            return;

        }
        if (_anim == null)
        {
            _anim = _Armature.GetComponent<DragonBones.UnityArmatureComponent>().animation;
        }
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
                _Armature.transform.rotation = Quaternion.Euler(0, 225, 0);
                break;
            case 1:
                _Armature.transform.rotation = Quaternion.Euler(0, 45, 0);
                break;
        }
    }
    /// <summary>
    /// 龙骨移动动画根据射线射到墙上的位置判断。
    /// </summary>
    void CheckBarrier()
    {
        Ray ray1 = new Ray(transform.Find("shexianchufadian").position, transform.forward);

        if (Physics.Raycast(ray1, out HitInfo))
        {
            Debug.DrawLine(transform.position, HitInfo.transform.position, Color.red);
            //Debug.Log(HitInfo.transform.name);
            if (HitInfo.transform.name == "forward")
            {
                _pos = 1;
                if (_isFindWood == false)
                {
                    PlayAnim(1);
                    CustomerRotate(1);
                }
                else
                {
                    PlayAnim(2);
                    imgThink.transform.localRotation = Quaternion.identity;
                    imgThink.transform.localScale = new Vector3(1,1,1);
                    Debug.Log(1111111111);
                    imgThink.transform.localPosition = new Vector3(0.116f,0.367f,0);
                    Debug.Log(imgThink.transform.localPosition);
                  imgThink.GetComponent<SpriteRenderer>().sortingOrder=layer;
                    layer = _Armature.GetComponent<DragonBones.UnityArmatureComponent>().sortingOrder + 1;
                }
            }

            else if (HitInfo.transform.name == "right")
            {
                _pos = 1;
                if (_isFindWood == false)
                {
                    PlayAnim(1);
                    CustomerRotate(0);
                }
                else
                {
                    PlayAnim(2);
                }
            }
            else if (HitInfo.transform.name == "back")
            {
                _pos = 4;
                if (_isFindWood == false)
                {
                    PlayAnim(4);
                    CustomerRotate(1);
                }
                else
                {
                    PlayAnim(5);
                    imgThink.transform.localRotation = Quaternion.identity;
                    imgThink.transform.localScale = new Vector3(1, 1, 1);
                    imgThink.transform.localPosition = new Vector3(0.15f, 0.59f, 0);
                    Debug.Log(imgThink.transform.localPosition);
                    Debug.Log(_Armature.GetComponent<DragonBones.UnityArmatureComponent>().sortingOrder);
                    layer = _Armature.GetComponent<DragonBones.UnityArmatureComponent>().sortingOrder - 1;
                    imgThink.GetComponent<SpriteRenderer>().sortingOrder=layer;                 
                }
            }
            else if (HitInfo.transform.name == "left")
            {
                _pos = 4;
                if (_isFindWood == false)
                {
                    PlayAnim(4);
                    CustomerRotate(0);
                }
                else
                {
                    PlayAnim(5);
                  //  _Armature.GetComponent<LongguFollow>()._Foodget.position = new Vector3(-0.15f, 0.59f, 0);
                }
            }
        }
    }
    /// <summary>
    /// 龙骨动画的呼吸状态
    /// </summary>
    public void CustomerBreath()
    {
        Ray ray1 = new Ray(transform.Find("shexianchufadian").position, transform.forward);

        if (Physics.Raycast(ray1, out HitInfo))
        {
            if (_Huxi == true)
            {
                if (HitInfo.transform.name == "forward" || HitInfo.transform.name == "right")
                {
                    PlayAnim(0);
                }
                else if (HitInfo.transform.name == "back" || HitInfo.transform.name == "left")
                {
                    PlayAnim(3);
                }
            }
        }
    }
    void CheckGetWood()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "huojia")
        {
            _Huxi = true;
            CustomerBreath();
            if (_IsCustomerMove == true)
            {
                ChufaPanDuan(other);
            }
        }
        if (other.transform.tag == "Shouyin")
        {
           // Debug.Log(other.transform);
            _Huxi = true;
            CustomerBreath();
        }
        if(other.transform.tag=="Floor")
        {
            string floorNum = other.transform.name;
            string result = System.Text.RegularExpressions.Regex.Replace(floorNum, @"[^0-9]+", "");
            int ss = int.Parse(result);
            _Armature.GetComponent<DragonBones.UnityArmatureComponent>().sortingOrder=ss;
        }
    }
    void OnTriggerStay(Collider other)
    {
        //if (other.transform.tag == "huojia")
        //{
        //    _Huxi = true;
        //    CustomerBreath();

        //}
        //if (other.transform.tag == "Shouyin")
        //{
        //    //Debug.Log(other.transform);
        //    _Huxi = true;
        //    CustomerBreath();
        //}
    }
    void OnTriggerExit(Collider collider)
    {
        _Huxi = !_Huxi;
    }
    //人物拖拽移动
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
        if (_zhuaitarget != null)
        {
            MoveToHuojia(_zhuaitarget);
            //Debug.Log("MovetoHuojia");
            //CancelInvoke("FindNext");
        }
    }
    void ChufaPanDuan(Collider other)
    {
       // Debug.Log("ChuFaPanDudan");
        if (other.transform.name == "huojia__shechipin(Clone)" && CustomerThink.Instance.Rad == 0)
        {
            _Huxi = true;
            _think = _Born.Find("Think(Clone)");
            _think.gameObject.SetActive(false);
            _timeCanvas.gameObject.SetActive(true);
            _isFindWood = true;
            _zhuaitarget = other.gameObject;
            _timer.startTiming(2, OnComplete, OnProcess);
        }
        else if (other.transform.name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
        {
            _Huxi = true;
            _think = _Born.Find("Think(Clone)");
            _think.gameObject.SetActive(false);
            _timeCanvas.gameObject.SetActive(true);
            _zhuaitarget = other.gameObject;
            _isFindWood = true;
            _timer.startTiming(2, OnComplete, OnProcess);

        }
        else if (other.transform.name == "huojia_huazhuangpin(Clone)" && CustomerThink.Instance.Rad == 2)
        {
            _Huxi = true;
            //Debug.Log("找到3");
            _think = _Born.Find("Think(Clone)");
            _think.gameObject.SetActive(false);
            _timeCanvas.gameObject.SetActive(true);
            _zhuaitarget = other.gameObject;
            _isFindWood = true;
            _timer.startTiming(2, OnComplete, OnProcess);
        }
        else if (other.transform.name == "huojia_twoge(Clone)" && CustomerThink.Instance.Rad == 3)
        {
            _Huxi = true;
            _think = _Born.Find("Think(Clone)");
            _think.gameObject.SetActive(false);
            _timeCanvas.gameObject.SetActive(true);
            _zhuaitarget = other.gameObject;
            _isFindWood = true;
            _timer.startTiming(2, OnComplete, OnProcess);
        }
        else
        {
            GetDis(_lidian);
        }
    }
    void OnComplete()
    {
        //_timeSlider.value = 0;
        _timeCanvas.gameObject.SetActive(false);
        PlayAnim(5);
        MoveToHuojia(_MoneyPosition);
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
    // 计时器的进程  
    void OnProcess(float p)
    {
        //_timeSlider.value = p;
        CustomerRotate(1);
        PlayAnim(3);
    }
    Transform _xinqin;
    SpriteRenderer _xinqinTetrue;
    void CustomerXQgood()//人物心情方法
    {
        Debug.Log("心情好");    
        _think.gameObject.SetActive(true);
        _timeCanvas.gameObject.SetActive(false);
        _xinqin = _think.Find("Pacture");
        _xinqinTetrue = _xinqin.GetComponent<SpriteRenderer>();                     
         Texture2D img = Resources.Load("ThinkPa/happy") as Texture2D;
         Sprite pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
         _xinqinTetrue.sprite = pic;
         _xinqin.transform.localPosition = new Vector3(0,0,0);
      }
    void CustomerXQsad()
    {
       Debug.Log("心情烦躁");
       _think = _Born.Find("Think(Clone)");
       _xinqin = _think.Find("Pacture");
       _xinqinTetrue = _xinqin.GetComponent<SpriteRenderer>();
       Texture2D img = Resources.Load("ThinkPa/sad") as Texture2D;
       Sprite pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
       _xinqinTetrue.sprite = pic;
       _xinqin.transform.localPosition = new Vector3(0,0,0);
    }       
    
}
