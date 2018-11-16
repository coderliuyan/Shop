using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBtn : MonoBehaviour
{
    private static CardBtn _instance = null;
    private void Awake()
    {
        _instance = this;
    }
    public static CardBtn Instance
    {
        get
        {
            return _instance;
        }
    }
    Vector3 screenposition;
    Vector3 mousePositionOnScreen;
    Vector3 mousePositionWorld;
    BoxCollider _MyCollider;
    GameObject _Longgucanvans;
    Follow _ismove;
    GameObject _canvans;
    GameObject _shangcheng;
    //GameObject shafa;
    // Use this for initialization
    void Start()
    {
        _MyCollider = gameObject.GetComponent<BoxCollider>();
        _Longgucanvans = GameObject.Find("LongguCanvas");
        _canvans = GameObject.Find("UI Root (1)");
        _shangcheng = GameObject.Find("ShangCheng");
       
    }

    // Update is called once per frame
    void Update()
    {
        screenposition = Camera.main.WorldToScreenPoint(transform.position);
        mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = screenposition.z;
        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
    }
    void OnClick()
    {         
        //Debug.Log("OnClick");
        if (_MyCollider.tag == "changtougui")
        {

            if (IshuojiaFollow.Instance._moving == true)
            {
                //Debug.Log(_MyCollider.name);
                GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_huazhuangpin"), mousePositionWorld, Quaternion.identity);
                Destroy(gameObject);
                //gameObject.SetActive(false);
                //obj.GetComponent<MeshRenderer>().enabled = false;
                obj.GetComponent<Follow>().OnMouseDownTrue();
                //_shangcheng.GetComponent<Btn>().enabled = true;
                //GameObject UIhuojia = (GameObject)Instantiate(Resources.Load("huojia/huojiaUI"));
                //UIhuojia.transform.parent = _Longgucanvans.transform;
                //UIhuojia.GetComponent<HuojiaUI>()._UIHuojia = obj.transform;
            }
        }
        if (_MyCollider.tag == "sofa")
        {
            if (IshuojiaFollow.Instance._moving ==true)
            {
                GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia__shechipin"), mousePositionWorld, Quaternion.identity);
                Destroy(gameObject);
                GameObject UIButton = (GameObject)Instantiate(Resources.Load("HuojiaButton/huo_shechipinButton"));
                UIButton.transform.parent = _canvans.transform;
                //UIButton.GetComponent<UIArmaMove>().UIPoint = obj.transform;
                //gameObject.SetActive(false);
                obj.GetComponent<Follow>().OnMouseDownTrue();
               // _shangcheng.GetComponent<Btn>().enabled = true;
            }
            //obj.GetComponent<MeshRenderer>().enabled = false;
            //GameObject UIhuojia = (GameObject)Instantiate(Resources.Load("huojia/huojiaUI 2"));
            //UIhuojia.transform.parent = _Longgucanvans.transform;
            //UIhuojia.GetComponent<HuojiaUI>()._UIHuojia = obj.transform;
        }
        if (_MyCollider.tag == "guizi")
        {
            if (IshuojiaFollow.Instance._moving == true)
            {
                GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_choose"), mousePositionWorld, Quaternion.identity);
                Destroy(gameObject);
                //gameObject.SetActive(false);
                obj.GetComponent<Follow>().OnMouseDownTrue();
               // _shangcheng.GetComponent<Btn>().enabled = true;
            }
            //obj.GetComponent<MeshRenderer>().enabled = false;
            //GameObject UIhuojia = (GameObject)Instantiate(Resources.Load("huojia/huojiaUI 3"));
            //UIhuojia.transform.parent = _Longgucanvans.transform;
            //UIhuojia.GetComponent<HuojiaUI>()._UIHuojia = obj.transform;

        }
        if (_MyCollider.tag == "Twohuogui")
        {

            if (IshuojiaFollow.Instance._moving == true)
            {
                //Debug.Log(_MyCollider.name);
                GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_twoge"), mousePositionWorld, Quaternion.identity);
                gameObject.SetActive(false);
                //obj.GetComponent<MeshRenderer>().enabled = false;
                obj.GetComponent<Follow>().OnMouseDownTrue();

                //GameObject UIhuojia = (GameObject)Instantiate(Resources.Load("huojia/huojiaUI"));
                //UIhuojia.transform.parent = _Longgucanvans.transform;
                //UIhuojia.GetComponent<HuojiaUI>()._UIHuojia = obj.transform;
            }
        }
    }
}
