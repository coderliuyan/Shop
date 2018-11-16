using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBtn : MonoBehaviour {

    Transform _shangcheng;
    Transform _MyGoods;
	void Start () 
    {		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnClick()
    {
        Application.Quit();
    }
}
