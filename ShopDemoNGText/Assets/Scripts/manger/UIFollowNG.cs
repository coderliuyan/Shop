using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowNG : MonoBehaviour {

    public GameObject PrefabsTurn;
    public GameObject hud;//实例化出来的旋转按钮
    private float Fomat;//主摄像机到当前位置的距离
    Vector3 pos;
	void Start () 
    {
        pos = transform.position;
        hud = GameObject.Instantiate(PrefabsTurn,pos,Quaternion.identity)as GameObject;//实例化按钮
        Fomat = Vector3.Distance(pos,Camera.main.transform.position);
        if (hud.transform.name == "MoveOrTurnUI(Clone)")        
        {
            hud.GetComponent<HuojiaMoveOrTurn>()._huojia = gameObject.transform.parent;
        }
        if (hud.transform.name == "yidongMove(Clone)")
        {
            hud.GetComponent<HuojiaMove>()._huojia =gameObject.transform.parent;
        }
        if (hud.transform.name == "ButtonHuojia(Clone)")
        {
            hud.GetComponent<NewHuojiaButton>()._huojia = gameObject.transform.parent;
        }
        if (hud.transform.name == "xuanzhuanTurn(Clone)")
        {
            hud.GetComponent<NewHuojiaTurn>()._huojia = gameObject.transform.parent;
        }
        //hud.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (hud)
        {
            hud.transform.position = WorldToUI(transform.position);
        }
	}
    public Vector3 WorldToUI(Vector3 point)
    {
        Vector3 pt = Camera.main.WorldToScreenPoint(point);
        pt.z = 0;
        Vector3 ff = NGUITools.FindCameraForLayer(hud.layer).ScreenToWorldPoint(pt);
        return ff;
    }
}
