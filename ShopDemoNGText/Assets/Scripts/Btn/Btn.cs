using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour {
     GameObject _gam;
	// Use this for initialization
	void Start () 
    {
        _gam = GameObject.Find("ShangCheng");
	}	
	// Update is called once per frame
	void Update () 
    {
        if (_gam != null)
        { 
        _gam.SetActive(IshuojiaFollow.Instance._moving);
        }
	}
    void OnClick()
    {
        
    }
      
}
