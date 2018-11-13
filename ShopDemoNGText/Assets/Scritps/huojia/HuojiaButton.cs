using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuojiaButton : MonoBehaviour {
    GameObject _HuojiaButton;
    GameObject _HuojiaTUI;//货架旋转UI。
    GameObject _HuojiaMoveOrTurn;
    GameObject _HUojiaMove;
	// Use this for initialization
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        _HuojiaButton = gameObject.transform.Find("ButtonObj").GetComponent<UIFollowNG>().hud;
        _HuojiaTUI = gameObject.transform.Find("Turn").GetComponent<UIFollowNG>().hud;
        _HuojiaMoveOrTurn = gameObject.transform.Find("MoveOrTurn").GetComponent<UIFollowNG>().hud;
        _HUojiaMove = gameObject.transform.Find("Move").GetComponent<UIFollowNG>().hud;
	}
    void OnMouseDown()
    {
        if (transform.GetComponent<HuojiaMousemove>().enabled == false && transform.GetComponent<HuojiaMouseTurn>().enabled == false&&transform.GetComponent<NewHuojiaFollow>())
        {
            if (this.enabled == true)
            {
                if (_HuojiaTUI != null)
                {
                    _HuojiaTUI.SetActive(false);
                }
                if (_HuojiaMoveOrTurn != null)
                {
                    _HuojiaMoveOrTurn.SetActive(false);
                }
                if (_HUojiaMove != null)
                {
                    _HUojiaMove.SetActive(false);
                }
                if (_HuojiaButton != null)
                {
                    _HuojiaButton.SetActive(true);
                }
            }
        }
    }
}
