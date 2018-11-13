using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveTest : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 _target;//目标位置
    private bool _ismove = true;
    GameObject[] MovetoHuojia;
    GameObject _MoneyTai;
    GameObject _PPD;
    GameObject _Lidian;
    int rad;
    bool _MoveMoney;
    Transform _Born;
    Transform _think;
    GameObject _Timeroot;
    GameObject _timeCanvas;
    Transform _TimeCanvas;
    Timer _timer;
    Transform _Slider;
    Slider _timeSlider;
    float _value;
    bool _Likai;
    //public Transform target;
    //public float angle = 60f;
    //public float distance = 5f;
    void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        MovetoHuojia = FindAllHuojia.Instance._allHuojia;
        _MoneyTai = GameObject.Find("MoneyTai");
        _PPD = GameObject.Find("PPD");
        _Lidian = GameObject.Find("lidian");
        rad = 0;
        _Born = transform.Find("Born");
        InvokeRepeating("CustomerMove", 1f, 15f);
        _MoveMoney = false;
        _Likai = false;
        // _Timeroot = GameObject.Find("Born");
        _timeCanvas = _Born.Find("TimeCanvas").gameObject;
        Debug.Log(_timeCanvas.name);
        _timer = Timer.createTimer("Timer");
        _Slider = _timeCanvas.transform.Find("Slider");
        _timeSlider = _Slider.GetComponent<Slider>();
        Debug.Log(_timeSlider);
      
    }
    void Update()
    {
        //Vector3 direction = target.position - transform.position;
        if (_MoveMoney)
        {
            float step = 8 * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, _PPD.transform.position, step);
        }
        if (_Likai)
        {
            float step = 8 * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, _Lidian.transform.position, step);
        }
    }

    void CustomerMove()
    {
        if (MovetoHuojia.Length == 5)
        {
            switch (rad)
            {
                case 0:
                    if (Vector3.Distance(transform.position, MovetoHuojia[0].transform.position) > 3)
                    {
                        agent.SetDestination(MovetoHuojia[0].transform.position);
                    }
                    if ( MovetoHuojia[0].name == "huojia__shechipin(Clone)"&&CustomerThink.Instance.Rad == 0 )
                    {
                        rad = 5;
                    }                   
                    else if (MovetoHuojia[0].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                    {
                        rad = 5;
                    }
                    else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[0].name == "huojia_huazhuangpin(Clone)")
                    {
                        rad = 5;
                    }
                    else
                    {
                        rad = 1;
                    }
                    break;
                case 1:
                    if (Vector3.Distance(transform.position, MovetoHuojia[1].transform.position) > 3)
                    {
                        agent.SetDestination(MovetoHuojia[1].transform.position);
                    }
                    if (MovetoHuojia[1].name == "huojia__shechipin(Clone)" && CustomerThink.Instance.Rad == 0)
                    {
                        rad = 5;
                    }
                    else if (MovetoHuojia[1].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                    {
                        rad = 5;
                    }
                    else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[1].name == "huojia_huazhuangpin(Clone)")
                    {
                        rad = 5;
                    }
                    else
                    {
                        rad = 2;
                    }
                    break;
                case 2:
                    if (Vector3.Distance(transform.position, MovetoHuojia[2].transform.position) > 6)
                    {
                        agent.SetDestination(MovetoHuojia[2].transform.position);
                    }
                    if (CustomerThink.Instance.Rad == 0 && MovetoHuojia[2].name == "huojia__shechipin(Clone)")
                    {
                        rad = 5;
                    }                 
                    else if (MovetoHuojia[2].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                    {
                        rad = 5;
                    }
                    else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[2].name == "huojia_huazhuangpin(Clone)")
                    {
                        rad = 5;
                    }
                    else
                    {
                        rad = 3;
                    }
                    break;
                case 3:
                    if (Vector3.Distance(transform.position, MovetoHuojia[3].transform.position) > 3)
                    {
                        agent.SetDestination(MovetoHuojia[3].transform.position);
                    }
                    if (CustomerThink.Instance.Rad == 0 && MovetoHuojia[3].name == "huojia__shechipin(Clone)")
                    {
                        rad = 5;
                    }
                    else if (MovetoHuojia[3].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                    {
                        rad = 5;
                    }
                    else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[3].name == "huojia_huazhuangpin(Clone)")
                    {
                        rad = 5;
                    }
                    else
                    {
                        rad = 4;
                    }
                    break;
                case 4:
                    if (Vector3.Distance(transform.position, MovetoHuojia[4].transform.position) > 3)
                    {
                        agent.SetDestination(MovetoHuojia[3].transform.position);
                    }
                    rad = 5;
                    break;
                case 5:
                     _think = _Born.Find("Think(Clone)");
                     _think.gameObject.SetActive(false);                      
                     _timeCanvas.gameObject.SetActive(true);
                     _timer.startTiming(5, OnComplete, OnProcess);
                       // _timeCanvas.gameObject.SetActive(false);
                    _MoveMoney = true;
                    Destroy(gameObject,18f);
                    break;
            }
        }
            else if (MovetoHuojia.Length == 4)
            {
                switch (rad)
                {
                    case 0:
                        if (Vector3.Distance(transform.position, MovetoHuojia[0].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[0].transform.position);
                        }
                        if (MovetoHuojia[0].name == "huojia__shechipin(Clone)" && CustomerThink.Instance.Rad == 0)
                        {
                            rad = 4;
                        }
                        else if (MovetoHuojia[0].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                        {
                            rad = 4;
                        }
                        else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[0].name == "huojia_huazhuangpin(Clone)")
                        {
                            rad = 4;
                        }
                        else
                        {
                            rad = 1;
                        }
                      
                        break;
                    case 1:
                        if (Vector3.Distance(transform.position, MovetoHuojia[1].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[1].transform.position);
                        }
                        if (CustomerThink.Instance.Rad == 0 && MovetoHuojia[1].name == "huojia__shechipin(Clone)")
                        {
                            rad = 4;
                        }
                        else if (CustomerThink.Instance.Rad == 1 && MovetoHuojia[1].name == "huojia_choose(Clone)")
                        {
                            rad = 4;
                        }                      
                        else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[1].name == "huojia_huazhuangpin(Clone)")
                        {
                            rad = 4;
                        }
                        else
                        {
                            rad = 2;
                        }
                        break;
                    case 2:
                        if (Vector3.Distance(transform.position, MovetoHuojia[2].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[2].transform.position);
                        }
                        if (CustomerThink.Instance.Rad == 0 && MovetoHuojia[2].name == "huojia__shechipin(Clone)")
                        {
                            rad = 4;
                        }                        
                       else if (CustomerThink.Instance.Rad == 1 && MovetoHuojia[2].name == "huojia_choose(Clone)")
                        {
                            rad = 4;
                        }                      
                        else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[2].name == "huojia_huazhuangpin(Clone)")
                         {
                             rad = 4;
                         }
                         else
                         {
                             rad = 3;
                         }
                        break;
                    case 3:
                        if (Vector3.Distance(transform.position, MovetoHuojia[3].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[3].transform.position);
                        }
                        if (MovetoHuojia[2].name == "huojia__shechipin(Clone)" && CustomerThink.Instance.Rad == 0)
                        {
                            rad = 4;

                        }
                        else if (MovetoHuojia[2].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                        {
                            rad = 4;
                        }
                        else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[2].name == "huojia_huazhuangpin(Clone)")
                        {
                            rad = 4;
                        }
                        else
                        {
                            rad = 5;
                        }
                
                        break;
                    case 4:
                        _think = _Born.Find("Think(Clone)");
                        _think.gameObject.SetActive(false);                      
                        _timeCanvas.gameObject.SetActive(true);
                        _timer.startTiming(5, OnComplete, OnProcess);
                        //_timeCanvas.gameObject.SetActive(false);
                       // _MoveMoney = true;
                        Destroy(gameObject,13f);
                        break;
                    case 5:
                        agent.SetDestination(_Lidian.transform.position);
                        Destroy(gameObject,15f);
                        break;
                }
            }
         else if (MovetoHuojia.Length == 3)
            {
               
                switch (rad)
                {
                    case 0:
                        if (Vector3.Distance(transform.position, MovetoHuojia[0].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[0].transform.position);
                            Debug.Log(MovetoHuojia[0].name);

                        }                       
                        if ( MovetoHuojia[0].name == "huojia__shechipin(Clone)"&&CustomerThink.Instance.Rad == 0)
                        {                         
                            rad = 3;
                        }
                       else if (MovetoHuojia[0].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                        {

                            rad = 3;
                        
                        }                           
                       else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[0].name == "huojia_huazhuangpin(Clone)")
                        {
                          
                            rad = 3;
                        }
                        else
                        {
                            rad = 1;
                        }
                        break;
                    case 1:
                        if (Vector3.Distance(transform.position, MovetoHuojia[1].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[1].transform.position);
                            Debug.Log(MovetoHuojia[1].name);
                        }
                        if ( MovetoHuojia[1].name == "huojia__shechipin(Clone)"&&CustomerThink.Instance.Rad == 0)
                        {
                            //_think = _Born.FindChild("Think(Clone)");
                            //_think.gameObject.SetActive(false);
                            //_timeCanvas.gameObject.SetActive(true);
                            rad = 3;
                          
                        }                   
                       else if ( MovetoHuojia[1].name == "huojia_choose(Clone)"&&CustomerThink.Instance.Rad == 1)
                        {
                            //_think = _Born.FindChild("Think(Clone)");
                            //_think.gameObject.SetActive(false);
                            //_timeCanvas.gameObject.SetActive(true);
                            rad = 3;
                           
                        }                        
                       else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[1].name == "huojia_huazhuangpin(Clone)")
                        {
 
                            rad = 3;
                            
                        }
                        else
                        {
                            rad = 2;
                        }
                        break;
                    case 2:
                        if (Vector3.Distance(transform.position, MovetoHuojia[2].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[2].transform.position);
                            Debug.Log(MovetoHuojia[2].name);
                        }
                        if (MovetoHuojia[2].name == "huojia__shechipin(Clone)" && CustomerThink.Instance.Rad == 0)
                        {
                            rad = 3;

                        }
                        else if (MovetoHuojia[2].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                        {
                            rad = 3;
                        }
                        else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[2].name == "huojia_huazhuangpin(Clone)")
                        {
                            rad = 3;
                        }
                       else
                        {
                            rad = 4;
                        }
   
                        break;
                    case 3:
                         _think = _Born.Find("Think(Clone)");
                        _think.gameObject.SetActive(false);                      
                        _timeCanvas.gameObject.SetActive(true);
                        _timer.startTiming(5, OnComplete, OnProcess);
                       // _timeCanvas.gameObject.SetActive(false);
                        Destroy(gameObject, 20f);
                        break;
                    case 4:
                        agent.SetDestination(_Lidian.transform.position);
                        Destroy(gameObject,20f);
                        break;
                }
            }
          else if (MovetoHuojia.Length == 2)
            {
                switch (rad)
                {
                    case 0:
                        if (Vector3.Distance(transform.position, MovetoHuojia[0].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[0].transform.position);
                        }
                        if (CustomerThink.Instance.Rad == 0 && MovetoHuojia[0].name == "huojia__shechipin(Clone)")
                        {                           
                            rad = 2;
                        }                       
                        else if (MovetoHuojia[0].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                        {                        
                            rad = 2;
                        }
                        else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[0].name == "huojia_huazhuangpin(Clone)")
                        {                
                            rad = 2;
                        }
                        else
                        {
                            rad = 1;
                        }                   
                        break;
                    case 1:
                        if (Vector3.Distance(transform.position, MovetoHuojia[1].transform.position) > 3)
                        {
                            agent.SetDestination(MovetoHuojia[1].transform.position);
                            Debug.Log("11111111");
                        }

                        if (CustomerThink.Instance.Rad == 0 && MovetoHuojia[1].name == "huojia__shechipin(Clone)")
                        {
                            rad = 2;
                        }
                        else if (MovetoHuojia[1].name == "huojia_choose(Clone)" && CustomerThink.Instance.Rad == 1)
                        {
                            rad = 2;
                        }
                        else if (CustomerThink.Instance.Rad == 2 && MovetoHuojia[1].name == "huojia_huazhuangpin(Clone)")
                        {
                            rad = 2;
                        }
                        else
                        {
                            rad = 3;
                        }
                        break;
                    case 2:           
                        _think = _Born.Find("Think(Clone)");
                        _think.gameObject.SetActive(false);                      
                        _timeCanvas.gameObject.SetActive(true);
                        _timer.startTiming(5, OnComplete, OnProcess);
                        //_timeCanvas.gameObject.SetActive(false);
                       // _timeCanvas.gameObject.SetActive(true);                       
                        Destroy(gameObject,15f);
                        break;
                    case 3:
                        agent.SetDestination(_Lidian.transform.position);
                        Destroy(gameObject,15f);
                        break;
                }
            }
          else if (MovetoHuojia.Length == 1)
            {
              switch(rad)
              {
                  case 0:
                      agent.SetDestination(MovetoHuojia[0].transform.position);
                      rad = 1;
                      break;
                  case 1:
                      _think = _Born.Find("Think(Clone)");
                        _think.gameObject.SetActive(false);                      
                        _timeCanvas.gameObject.SetActive(true);
                        _timer.startTiming(5, OnComplete, OnProcess);
                       // _timeCanvas.gameObject.SetActive(false);
                      Destroy(gameObject, 3f);
                      break;
              }
             
            }
            else  if (MovetoHuojia.Length == 0)
            {
                Debug.Log("Nothing Find storage,Please Close Shop");
            }
        }

    void OnComplete()
    {
        _MoveMoney = true;
       // Debug.Log("CloseShop");
    }
    // 计时器的进程  
    void OnProcess(float p)
    {
        Debug.Log(p);
       // _time.GetComponent<UISprite>().fillAmount = p;
         _timeSlider.value=p;
    }
}

    /*
    GameObject _huojia;
    GameObject _huojia1;
    GameObject _huojia2;
    GameObject _huojia3;
    int Rad;
	void Start () 
    {
		_huojia=GameObject.Find("huojia0");
        _huojia1=GameObject.Find("huojia1");
        _huojia2 = GameObject.Find("huojia2");
        _huojia3 = GameObject.Find("huojia3");
         agent = gameObject.GetComponent<NavMeshAgent>();
         Rad = Random.Range(0,3);
         InvokeRepeating("RandomMove",1,5);
	}
	
	// Update is called once per frame
    void Update()
    {
        
    }
    void RandomMove()
    {
        switch (Rad)
        { 
            case 0:
                 if (Vector3.Distance(transform.position, _huojia.transform.position) > 6)
                   {
                       agent.SetDestination(_huojia.transform.position);
                    }
                 Rad = 1;
                break;
            case 1:
                if (Vector3.Distance(transform.position, _huojia1.transform.position) > 6)
                  {
                       agent.SetDestination(_huojia1.transform.position);
                   }
                Rad = 2;
                break;
            case 2:
                if (Vector3.Distance(transform.position, _huojia2.transform.position) > 6)
                 {
                   agent.SetDestination(_huojia2.transform.position);
                  }
                Rad = 3;
                break;
            case 3:
                 if(Vector3.Distance(transform.position, _huojia3.transform.position) > 6)
                   {
                      agent.SetDestination(_huojia3.transform.position);
                   }
                 Rad = 0;
                break;

        }
       
    }
      */
     
