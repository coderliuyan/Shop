using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuojiaMouseTurn : MonoBehaviour {
    GameObject _HuojiaTurnUI;
    GameObject _HuojiaButton;
    GameObject _HuojiaMoveOrTurn;
    GameObject _HUojiaMove;
	// Use this for initialization
	void Start () 
    {
        _HuojiaButton = gameObject.transform.Find("ButtonObj").GetComponent<UIFollowNG>().hud;
        _HuojiaMoveOrTurn = gameObject.transform.Find("MoveOrTurn").GetComponent<UIFollowNG>().hud;
        _HUojiaMove = gameObject.transform.Find("Move").GetComponent<UIFollowNG>().hud;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //_HuojiaTurnButton = gameObject.transform.Find("ButtonObj").GetComponent<UIFollowNG>().hud;
        //_HuojiaTurnUI = gameObject.transform.Find("Turn").GetComponent<UIFollowNG>().hud;
	}
    void OnMouseDown()
    {
        SSSS();
    }
   public void SSSS()
    {
        if (this.enabled == true)
        {
            if (_HuojiaButton != null)
            {
                _HuojiaButton.SetActive(false);
            }
            if (_HuojiaMoveOrTurn != null)
            {
                _HuojiaMoveOrTurn.SetActive(false);
            }
            if (_HUojiaMove != null)
            {
                _HUojiaMove.SetActive(false);
            }
            _HuojiaTurnUI = gameObject.transform.Find("Turn").GetComponent<UIFollowNG>().hud;
            _HuojiaTurnUI.SetActive(true);
        }
    }

}
