using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuojiaMousemove : MonoBehaviour {

    GameObject _HuojiaMoveUI;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
            _HuojiaMoveUI = gameObject.transform.Find("Move").GetComponent<UIFollowNG>().hud;
            _HuojiaMoveUI.SetActive(true);
            Debug.Log("2222222222222222222");
        }
    }

}
