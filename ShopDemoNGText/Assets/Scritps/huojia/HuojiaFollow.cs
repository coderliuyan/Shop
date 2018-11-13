using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuojiaFollow : MonoBehaviour {
    public  bool isMove = true;
    Vector3 lastmousePosition = Vector3.zero;
    RaycastHit hit;
    string FloorNam;//获取地板名称
    int ss;//地板名称转化成int值
    int Twoss;//两块地板的层级
    GameObject sencondFloor;
    int huojia_null;
    GameObject obj;//生成null的货架，为了给两个的货架添加一个空的物体。
    bool _isTwohuojiaTurn;
    GameObject _hud;
    Transform _Turnbtton;
    GameObject _UIRoot;
   public Transform _shengcheng;
	// Use this for initialization
	void Start () 
    {
        _UIRoot = GameObject.Find("SelectPanel");
        _shengcheng = _UIRoot.transform.Find("ShangCheng");
        obj = (GameObject)Instantiate(Resources.Load("huojia/Null"));
        _Turnbtton = gameObject.transform.Find("Turn");
        gameObject.GetComponent<HuojiaFollow>().isMove = true;
	}
	
	// Update is called once per frame
	void Update () 
    {    
        _hud = gameObject.transform.Find("Turn").GetComponent<UIFollowNG>().hud;
        if (_hud != null)
        {
            _isTwohuojiaTurn = _hud.GetComponent<HuojiaTurn>()._isTurn;
            //Debug.Log("2222222"+_isTwohuojiaTurn);
        }
        if (isMove)
        {
            Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
            if ((Physics.Raycast(ray, out hit)))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (gameObject.transform.name != "蔬菜货架2(Clone)")
                {
                    if (hit.transform.tag == "Floor" && hit.transform.childCount == 0&&hit.transform.name!="00")
                    {
                        ChangeFloorColor();
                        if (transform.name == "面包货架2(Clone)")
                        {
                            gameObject.transform.parent = hit.transform;
                            transform.localPosition = new Vector3(0.01f, 0.52f, 0);
                            transform.localRotation = Quaternion.identity;
                        }
                        if (transform.name == "蛋糕货架2(Clone)")
                        {
                            gameObject.transform.parent = hit.transform;
                            transform.localPosition = new Vector3(0.01f, 0.72f, 0);
                            transform.localRotation = Quaternion.identity;
                        }
                        if (transform.name == "甜点货架2(Clone)")
                        {
                            gameObject.transform.parent = hit.transform;
                            transform.localPosition = new Vector3(0.01f, 0.35f, 0);
                            transform.localRotation = Quaternion.identity;
                        }
                        if (hit.transform != null)
                        {
                            FloorNam = hit.transform.name;
                            string result = FloorNam;
                            ss = int.Parse(result);
                            gameObject.GetComponent<SpriteRenderer>().sortingOrder = ss + 10;
                        }
                    }
                }
                else
                {
                    if (hit.transform.tag == "Floor" && hit.transform.childCount == 0)
                    {
                        //if (_Turnbtton.GetComponent<UIFollowNG>().hud.GetComponent<HuojiaTurn>()._isTurn)
                        //{
                        //    if (hit.transform.name != "50" && hit.transform.name != "51" && hit.transform.name != "52" && hit.transform.name != "53" && hit.transform.name != "54" && hit.transform.name != "55")
                        //    {
                        //        gameObject.transform.parent = hit.transform;
                        //        transform.localPosition = new Vector3(0.05f, 0.22f, 0);
                        //        transform.localRotation = Quaternion.identity;
                        //    }
                        //}
                        //else
                        //{
                        //    if (hit.transform.name != "05" && hit.transform.name != "15" && hit.transform.name != "25" && hit.transform.name != "35" && hit.transform.name != "45" && hit.transform.name != "55")
                        //    {
                        //        gameObject.transform.parent = hit.transform;
                        //        transform.localPosition = new Vector3(0.05f, 0.22f, 0);
                        //        transform.localRotation = Quaternion.identity;
                        //    }
                        //}
                        if (hit.transform != null)
                        {
                            FloorNam = hit.transform.name;
                            string result = FloorNam;
                            ss = int.Parse(result);
                            gameObject.GetComponent<SpriteRenderer>().sortingOrder = ss + 10;
                        }
                    }
                }
            }
        PutNullHUojia();
        }
        if (transform.parent != null)
        {
            if (transform.parent.name == "10" || transform.parent.name == "20")
            {
                isMove = true;
            }
        }
        IsShangchengOff();
     }
  public  bool huojiaUI;
    void OnMouseDown()
    {
        OnMouseDownTrue();
        huojiaUI = !huojiaUI;
        if (huojiaUI==false)
        {
            transform.Find("Move").GetComponent<UIFollowNG>().hud.SetActive(true);
            transform.Find("Turn").GetComponent<UIFollowNG>().hud.SetActive(true);
            _shengcheng.gameObject.SetActive(false);
            huojiaUI = false;
        }
       // Debug.Log(huojiaUI);
    }
    public void OnMouseDownTrue()
    {
        if (transform.parent != null)
        {
         isMove = false;
         transform.Find("Move").GetComponent<UIFollowNG>().hud.GetComponent<HuojiaMove>()._isMove = false;
        }      
    }
    void PutNullHUojia()
    {
       // Debug.Log("11111"+_isTwohuojiaTurn);
        if (_isTwohuojiaTurn == false)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
            huojia_null = ss +1;
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
            huojia_null = ss + 10;
            FindSencondFloor();
            if (isMove == false)
            {
                CloneNullHuojia();
            }
        }
    }
    void FindSencondFloor()
    {
        if (gameObject.transform.name == "蔬菜货架2(Clone)")
        {
            sencondFloor = GameObject.Find(huojia_null.ToString());
            if (huojia_null < 10)
            {
                sencondFloor = GameObject.Find("0" + huojia_null.ToString());
            }
            if (sencondFloor != null && sencondFloor.transform.childCount == 0)
            {
                if (hit.transform.tag == "Floor" && hit.transform.childCount == 0&&hit.transform.name!="00")
                {
                    if (_Turnbtton.GetComponent<UIFollowNG>().hud.GetComponent<HuojiaTurn>()._isTurn)
                    {
                        if (hit.transform.name != "50" && hit.transform.name != "51" && hit.transform.name != "52" && hit.transform.name != "53" && hit.transform.name != "54" && hit.transform.name != "55")
                        {
                            gameObject.transform.parent = hit.transform;
                            transform.localPosition = new Vector3(0.05f, 0.22f, 0);
                            transform.localRotation = Quaternion.identity;
                        }
                    }
                    else
                    {
                        if (hit.transform.name != "05" && hit.transform.name != "15" && hit.transform.name != "25" && hit.transform.name != "35" && hit.transform.name != "45" && hit.transform.name != "55")
                        {
                            gameObject.transform.parent = hit.transform;
                            transform.localPosition = new Vector3(0.05f, 0.22f, 0);
                            transform.localRotation = Quaternion.identity;
                        }
                    }
                }
            }
            else
            {
                if (hit.transform.name != ss.ToString())
                {
                    if (hit.transform.tag == "Floor"&&hit.transform.childCount == 0&&hit.transform.name!="00")
                    {
                        if (_Turnbtton.GetComponent<UIFollowNG>().hud.GetComponent<HuojiaTurn>()._isTurn)
                        {
                            if (hit.transform.name != "50" && hit.transform.name != "51" && hit.transform.name != "52" && hit.transform.name != "53" && hit.transform.name != "54" && hit.transform.name != "55")
                            {
                                gameObject.transform.parent = hit.transform;
                                transform.localPosition = new Vector3(0.05f, 0.22f, 0);
                                transform.localRotation = Quaternion.identity;
                            }
                        }
                        else
                        {
                            if (hit.transform.name != "05" && hit.transform.name != "15" && hit.transform.name != "25" && hit.transform.name != "35" && hit.transform.name != "45" && hit.transform.name != "55")
                            {
                                gameObject.transform.parent = hit.transform;
                                transform.localPosition = new Vector3(0.05f, 0.22f, 0);
                                transform.localRotation = Quaternion.identity;
                            }
                        }
                    }
                }
            }
        }
    }
    void CloneNullHuojia()
    {
        if (gameObject.transform.name == "蔬菜货架2(Clone)")
        {
            obj = (GameObject)Instantiate(Resources.Load("huojia/Null"));
            obj.transform.SetParent(sencondFloor.transform);
           // Debug.Log("000000"+sencondFloor.transform);
        }
    }
    void IsShangchengOff()
    {    
        if (isMove == true)
        {
            _shengcheng.gameObject.SetActive(false);
            transform.Find("Move").GetComponent<UIFollowNG>().hud.SetActive(false);
            transform.Find("Turn").GetComponent<UIFollowNG>().hud.SetActive(false);
        }
        else
        {
            _shengcheng.gameObject.SetActive(true);       
            transform.GetComponent<HuojiaFollow>().enabled = false;
        }
       
    }

    Transform _icon;
    SpriteRenderer _spriteFloor;
    Texture2D img;
    Sprite pic;
    void ChangeFloorColor()
    {
        if (hit.transform.name == "10" || hit.transform.name == "20")
        {
            _icon = transform.Find("Floorpcture");
            Debug.Log(_icon.name);
            _spriteFloor = _icon.GetComponent<SpriteRenderer>();
            img = Resources.Load("floor/00") as Texture2D;
            pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
            _spriteFloor.sprite = pic;
        }
        else
        {
            _icon = transform.Find("Floorpcture");
            Debug.Log(_icon.name);
            _spriteFloor = _icon.GetComponent<SpriteRenderer>();
            img = Resources.Load("floor/01") as Texture2D;
            pic = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
            _spriteFloor.sprite = pic;
        }
    }   
}
