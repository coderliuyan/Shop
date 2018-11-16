using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHuojiaFollow : MonoBehaviour
{

    public bool isMove = true;
    Vector3 lastmousePosition = Vector3.zero;
    RaycastHit hit;
    string FloorNam;//获取地板名称
    GameObject _HuojiaMUI;//货架移动UI。
    GameObject _HuojiaTUI;//货架旋转UI。
    Transform _BuyHuojiaUI;
    GameObject _HuojiaButton;//货架补货这些按钮
    int _saveHuojiaFloorId;//有货架的地板Id。
    int _GetFloorCengji;//获取地板层级
    Transform _NullHuojiaLeftPos;//放置左边交互点的位置。
    Transform _NullHuojiaRightPos;//放置右边交互点的位置。



    bool _isfirst=true;//第一次
    // Use this for initialization
    void Start()
    {
        _BuyHuojiaUI = GameObject.Find("UIManger").transform.Find("BuyHuojia");
        _NullHuojiaLeftPos = transform.Find("HuojiaLeft");
        _NullHuojiaRightPos = transform.Find("HuojiaRight");
    }
    void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isMove);
        _HuojiaTUI = gameObject.transform.Find("Turn").GetComponent<UIFollowNG>().hud;
        _HuojiaMUI = gameObject.transform.Find("Move").GetComponent<UIFollowNG>().hud;
        _HuojiaButton = gameObject.transform.Find("ButtonObj").GetComponent<UIFollowNG>().hud;
     
        if (_HuojiaTUI != null)
        {
            _HuojiaTUI.SetActive(false);
        }
        if (isMove)
        {
            Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
            if ((Physics.Raycast(ray, out hit)))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.transform.tag == "Floor")
                {
                    gameObject.transform.parent = hit.transform.parent.parent;
                    //Debug.Log("hitFloor+++++"+hit.transform.name);
                    transform.localPosition = new Vector3(0, 0.3f, 0);
                    transform.localRotation = Quaternion.identity;
                    _GetFloorCengji = transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                    transform.Find("Huojia_Sprite").GetComponent<SpriteRenderer>().sortingOrder = _GetFloorCengji + 1;
                   // transform.Find("MoveOrTurnUI").GetComponent<UIFollowNG>().hud.gameObject.SetActive(true);
                    //if(hit.transform.tag=="huojia")
                    // {
                    //     isMove = false;
                    // }
                }
                if (_HuojiaMUI != null)
                {
                    _HuojiaMUI.SetActive(true);
                    _HuojiaButton.SetActive(false);
                }
                transform.Find("Move").GetComponent<UIFollowNG>().hud.gameObject.SetActive(false);
                transform.Find("ButtonObj").GetComponent<UIFollowNG>().hud.gameObject.SetActive(false);
              
            }
            else
            {
                _HuojiaMUI.gameObject.SetActive(false);
                // _HuojiaTUI.gameObject.SetActive(true);
                // _BuyHuojiaUI.gameObject.SetActive(true);
            }
            transform.Find("MoveOrTurn").GetComponent<UIFollowNG>().hud.gameObject.SetActive(false);
        }
    }
    void OnMouseDown()
    {
        // Debug.Log("鼠标按下了。。。。。");
        isMove = !isMove;
        //transform.Find("Move").GetComponent<UIFollowNG>().hud.GetComponent<HuojiaMove>()._isMove = true;
        //_HuojiaMUI.gameObject.SetActive(false);
        GetFloorId();
        if (this.enabled == true && _isfirst==true)
        { 
        _BuyHuojiaUI.gameObject.SetActive(true);
        transform.Find("MoveOrTurn").GetComponent<UIFollowNG>().hud.gameObject.SetActive(true);
        _isfirst = false;
        }
        //gameObject.GetComponent<NewHuojiaFollow>().enabled = false;
    }
    /// <summary>
    /// 获得货架所放位置的地板id
    /// </summary>
    void GetFloorId()
    {
        if (isMove == false)
        {
            _saveHuojiaFloorId = GetObjName(transform.parent.gameObject);
            Debug.Log("FloorId..........." + _saveHuojiaFloorId);
            gameObject.GetComponent<HuojiaModel>().FloorId = _saveHuojiaFloorId;
            //BornNullHuojiaObj();
        }
    }
    /// <summary>
    ///  获取objName.
    /// </summary>
    /// <param name="_obj"></param>
    /// <returns></returns>
    int GetObjName(GameObject _obj)
    {
        int numInt1 = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(_obj.transform.name, @"[^0-9]+", ""));
        return numInt1;
    }

    void BornNullHuojiaObj()
    {
        //if (_NullHuojiaLeftPos.childCount == 0 || _NullHuojiaRightPos.childCount == 0)
        //{
        //    GameObject huojiaNull = ((GameObject)Instantiate(Resources.Load("NewHuojia/HuojiaNullObj")));
        //    huojiaNull.transform.SetParent(_NullHuojiaLeftPos);
        //    huojiaNull.transform.localPosition = Vector3.zero;
        //    huojiaNull.transform.localScale = Vector3.one;
        //    huojiaNull.transform.localRotation = Quaternion.identity;
        //    GameObject huojiaNull2 = ((GameObject)Instantiate(Resources.Load("NewHuojia/HuojiaNullObj")));
        //    huojiaNull2.transform.SetParent(_NullHuojiaRightPos);
        //    huojiaNull2.transform.localPosition = Vector3.zero;
        //    huojiaNull2.transform.localScale = Vector3.one;
        //    huojiaNull2.transform.localRotation = Quaternion.identity;

        //    if (_HuojiaTUI.GetComponent<NewHuojiaTurn>()._isTurn == false)
        //    {
        //        huojiaNull2.SetActive(false);
        //    }
        //}
    }
}
