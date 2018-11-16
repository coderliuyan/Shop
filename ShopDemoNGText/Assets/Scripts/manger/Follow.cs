using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Follow : MonoBehaviour {
    private static Follow _instance = null;
    RaycastHit hit;
    private Transform Icon;
    public bool isMove;
    GameObject _Creatfloor;
    Transform _Turnbtton;
    Transform _TurnbttonChild;
    List<int> sum = new List<int>();
    GameObject _gameObject;
    string FloorNam;//获取地板名称
    int ss;//地板名称转化成int值
    int Twoss;//两块地板的层级
    GameObject sencondFloor;
    int huojia_null;
    GameObject obj;//生成null的货架，为了给两个的货架添加一个空的物体。
    bool _isTwohuojiaTurn;
    GameObject _hud;
    void Start()
    {
        _Creatfloor = GameObject.Find("Floor");
        //Debug.Log(_Creatfloor.transform.name);
        _Turnbtton = transform.Find("huojia");
        _TurnbttonChild = _Turnbtton.GetChild(1);
        isMove = true;
        if (_Creatfloor.transform.childCount !=36)
        {           
           TestCreat.Instance.CreatSomeThing();
        }   
        obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_null"));        
    }
    private void Awake()
    {
        _instance = this;            
    }
    public static Follow Instance
    {
        get
        {
            return _instance;
        }
    }
    void Update()
   {
       _hud = _TurnbttonChild.GetComponent<UIFollowNG>().hud;
       if (_hud != null)
       {
           _isTwohuojiaTurn = _hud.GetComponent<TurnBtn>()._isTurn;
       }
        if (isMove)
        {
            Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
            if ((Physics.Raycast(ray, out hit, 500)))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (gameObject.transform.name != "huojia_twoge(Clone)")
                {
                    if (hit.transform.tag == "Floor" && hit.transform.childCount == 0)
                    {
                        IsfloorColor();
                        gameObject.transform.parent = hit.transform;
                        gameObject.transform.localPosition = new Vector3(0, 7.5f, 0);
                        if (hit.transform != null)
                        {
                            FloorNam = hit.transform.name;
                            string result = FloorNam;
                            //int.TryParse(result,out ss);
                            ss = int.Parse(result);
                            _Turnbtton.GetComponent<SpriteRenderer>().sortingOrder = ss;
                        }
                    }
                }
             else
                {
                  if (hit.transform.tag == "Floor" && hit.transform.childCount == 0)
                  {
                      IsfloorColor();
                      if (hit.transform != null)
                      {
                          FloorNam = hit.transform.name;                          
                          //Debug.Log(FloorNam);
                          string result = FloorNam;
                          //int.TryParse(result,out ss);
                          ss = int.Parse(result);                                                  
                          _Turnbtton.GetComponent<SpriteRenderer>().sortingOrder =ss;
                      }
                  }
                }
              }
            }
       // Debug.Log(transform.parent.name);
        if (transform.parent != null)
        { 
        if (transform.parent.name == "10" || transform.parent.name == "20")
        {
            isMove = true;
        }
        }
        PutNullHUojia();                                                            
            if (Input.GetKey(KeyCode.Delete))
            {
                Destroy(this.gameObject);
            }
        } 
    void OnMouseDown()
    {
        OnMouseDownTrue();    
    }
   public void OnMouseDownTrue()
    {
        isMove = false;
       gameObject.transform.localPosition = new Vector3(0, 7.5f, 0);
       if (transform.parent.name == "10" || transform.parent.name == "20")
       {
           isMove = true;
       }
    }  
   void FindSencondFloor()
   {      
       if (gameObject.transform.name == "huojia_twoge(Clone)")
       {          
           sencondFloor = GameObject.Find(huojia_null.ToString());
           if (huojia_null < 10)
           {
               sencondFloor = GameObject.Find("0" + huojia_null.ToString());
           }
           if (sencondFloor != null && sencondFloor.transform.childCount == 0)
           {
               gameObject.transform.parent = hit.transform;
               gameObject.transform.localPosition = new Vector3(0, 7.5f, 0);
           }
           else
           {
               if (hit.transform.name != ss.ToString())
               {
                   gameObject.transform.parent = hit.transform;
                   gameObject.transform.localPosition = new Vector3(0, 7.5f, 0);                 
               }
           }
       }
   }
   void CloneNullHuojia()
   {
       if (gameObject.transform.name == "huojia_twoge(Clone)")
       {
           obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_null"));
           obj.transform.SetParent(sencondFloor.transform);
           obj.transform.localPosition = new Vector3(0, 7.5f, 0);
           obj.transform.localRotation = Quaternion.identity;
       }
   }
    /// <summary>
    /// 旋转在不同位置放置空货架
    /// </summary>
   void PutNullHUojia()
   {
       if (_isTwohuojiaTurn == false)
       {
           if (obj != null)
           {
               Destroy(obj);
           }
           huojia_null = ss + 10;
           FindSencondFloor();
           if (isMove == false)
           {
               CloneNullHuojia();
           }
       }
       if (_isTwohuojiaTurn == true)
       {
           if (obj != null)
           {
               Destroy(obj);
           }
           huojia_null = ss + 1;
           FindSencondFloor();
           if (isMove == false)
           {
               CloneNullHuojia();
           }
       }         
   }
   Transform _icon;
   SpriteRenderer _spriteFloor;
   Texture2D img;
   Sprite pic;
    //根据木板颜色观察是否可以放置
   void IsfloorColor()
   {
       if (hit.transform.name == "10" || hit.transform.name == "20")
       {
           _icon = transform.GetChild(1);
           Debug.Log(_icon.name);
           _spriteFloor = _icon.GetComponent<SpriteRenderer>();
          img = Resources.Load("floor/00") as Texture2D;
           pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
           _spriteFloor.sprite = pic;
       }
       else
       {
           _icon = transform.GetChild(1);
           Debug.Log(_icon.name);
           _spriteFloor = _icon.GetComponent<SpriteRenderer>();
          img = Resources.Load("floor/01") as Texture2D;
          pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
           _spriteFloor.sprite = pic;
       }
   }
}
