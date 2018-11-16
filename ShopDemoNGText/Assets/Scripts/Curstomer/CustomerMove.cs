using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//using DragonBones;
public class CustomerMove : MonoBehaviour
{
    int min = -1;
    List<GameObject> _Cube = new List<GameObject>();
    GameObject[] _hh;
    GameObject _MoneyPosition;
    Transform _think;
    Transform _Born;
    GameObject _timeCanvas;
    Timer _timer;
    Transform _Slider;
    Slider _timeSlider;
    bool _MoveMoney;
    NavMeshAgent _agent; 
    DragonBones.Animation _anim;
    //Animation _anim;
    public Transform _Armature;
    bool _Huxi;
    RaycastHit HitInfo;
    GameObject _lidian;
    GameObject _Money;
    //Transform _TimeCanvas;
    void Start()
    {
        _hh = FindAllHuojia.Instance._allHuojia;
        _MoneyPosition = GameObject.Find("PPD");
        _Born = transform.Find("Born");
        _timeCanvas = _Born.Find("TimeCanvas").gameObject;
        _timer = Timer.createTimer("Timer");
        _Slider = _timeCanvas.transform.Find("Slider");
         //Debug.Log(_Slider + "111111");
        _timeSlider = _Slider.GetComponent<Slider>();
        _agent = gameObject.GetComponent<NavMeshAgent>();
        _lidian = GameObject.Find("lidian");
        _Huxi = false;
        _Money = GameObject.Find("money");
        //_FilpX = _Armature.GetComponent<DragonBones.UnityArmatureComponent>().flipX;
        foreach (var item in _hh)
        {
            _Cube.Add(item);
        }
        FindHuojia();
        InvokeRepeating("FindNext", 0f, 15f);
    }
    void Awake()
    {
        //  _Armature = gameObject.transform.FindChild("Armature");
        if (_Armature != null)
        {
            _anim = _Armature.GetComponent<DragonBones.UnityArmatureComponent>().animation;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (_Huxi == false)
        { 
        CheckBarrier();
         }
       // transform.LookAt(Camera.main.transform.position );
   }
    float GetDis(GameObject Dis)//检测人物和货架的距离
    {
        return Vector3.Distance(Dis.transform.position, transform.position);
    }
    void FindHuojia()//找到距离人物最近的货架
    {
        if (_Cube.Count == 0)
        {
            MoveToHuojia(_lidian);
            Destroy(gameObject, 10f);
            //LongguFollow.Instance.DesLongGu(8f);
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
        // FindNext();
    }
    void MoveToHuojia(GameObject obj)//移动到货架
    {
        _agent.SetDestination(obj.transform.position);
 
    }
    void FindNext()//寻找下一个货架
    {
        if (GetDis(_Cube[min]) <= 6)
        {
            if (_Cube[min].name == "huojia__shechipin(Clone)" && CustomerThink.Instance.Rad == 0)
            {
                _Huxi = true;
                // Debug.Log("找到1");
                _think = _Born.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _timeCanvas.gameObject.SetActive(true);
                _timer.startTiming(2, OnComplete, OnProcess);              
            }
            else if (_Cube[min].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
            {
                _Huxi = true;
                //Debug.Log("找到2");
                _think = _Born.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _timeCanvas.gameObject.SetActive(true);
                _timer.startTiming(2, OnComplete, OnProcess);               
            }
            else if (_Cube[min].name == "huojia_huazhuangpin(Clone)" && CustomerThink.Instance.Rad == 2)
            {
                _Huxi = true;
                //Debug.Log("找到3");
                _think = _Born.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _timeCanvas.gameObject.SetActive(true);
                _timer.startTiming(2, OnComplete, OnProcess);               
            }
            else if (_Cube[min].name == "huojia_twoge(Clone)" && CustomerThink.Instance.Rad == 3)
            {
                _Huxi = true;
                _think = _Born.Find("Think(Clone)");
                _think.gameObject.SetActive(false);
                _timeCanvas.gameObject.SetActive(true);
                _timer.startTiming(2,OnComplete,OnProcess);
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
        Debug.Log("走向收银台");     
        MoveToHuojia(_MoneyPosition);
        textMoney.Instance._Momey += 10;
        Destroy(gameObject,15f);
       // LongguFollow.Instance.DesLongGu(15f);
        textMoney.Instance._textMoney = ":" + textMoney.Instance._Momey.ToString();
        _Money.GetComponent<UILabel>().text = textMoney.Instance._textMoney;
    }      
    // 计时器的进程  
    void OnProcess(float p)
    {
        // Debug.Log(p);
        // _time.GetComponent<UISprite>().fillAmount = p;
        _timeSlider.value = p;
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
                if (_anim.lastAnimationName != "face_Breath")
                {
                    _anim.Play("face_Breath");
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
                if (_anim.lastAnimationName != "back_Breath")
                {
                    _anim.Play("back_Breath");

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
    void CheckBarrier()
    {      
       Ray ray1 = new Ray(transform.Find("shexianchufadian").position, transform.forward);

            if (Physics.Raycast(ray1, out HitInfo))
            {
                Debug.DrawLine(transform.position, HitInfo.transform.position, Color.red);
                //Debug.Log(HitInfo.transform.name);
                if (HitInfo.transform.name == "forward")
                {
                   // Debug.Log("FFF");
                    PlayAnim(1);
                    CustomerRotate(0);
                }
                else if (HitInfo.transform.name == "right")
                {
                    //Debug.Log("RRRRR");
                    PlayAnim(1);
                    CustomerRotate(1);
                }
                else if (HitInfo.transform.name == "back")
                {
                    //Debug.Log("BBBBB");
                    PlayAnim(4);
                    CustomerRotate(1);
                }
                else if (HitInfo.transform.name == "left")
                {
                    //Debug.Log("LLLLLLL");
                    PlayAnim(4);
                    CustomerRotate(0);
                }
            }
        
    }
   public void CustomerBreath()
    {
        Ray ray1 = new Ray(transform.Find("shexianchufadian").position, transform.forward);

        if (Physics.Raycast(ray1, out HitInfo))
        {
                if (_Huxi==true)
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
   void OnTriggerEnter(Collider other)
   {
       if (other.transform.tag == "huojia")
       {
           _Huxi = true;
           CustomerBreath();
       }
       if (other.transform.tag == "Shouyin")
       {
           Debug.Log(other.transform);
           _Huxi = true;
           CustomerBreath();
       }
   }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "huojia")
        {
            _Huxi = true;
            CustomerBreath();
        }
        if (other.transform.tag == "Shouyin")
        {
            Debug.Log(other.transform);
            _Huxi = true;
            CustomerBreath();
        }
    }
    void OnTriggerExit(Collider collider)
    {
        _Huxi =!_Huxi;
    }  
}                                  
