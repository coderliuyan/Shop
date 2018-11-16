using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuojiaMove : MonoBehaviour {
    public  bool _isMove;
     RaycastHit hit;
     GameObject _huojiaParent;
     GameObject _UIRoot;
     public Transform _huojia;
	// Use this for initialization
	void Start () 
    {      
 
        _huojiaParent = _huojia.gameObject;
       
	}	
	// Update is called once per frame
	void Update () 
    {       
	}
    void OnClick()
    {
        _isMove = !_isMove;            
        //Debug.Log(_isMove);
        if (_isMove)
        {
            _huojia.GetComponent<NewHuojiaFollow>().enabled = true;
            _huojia.GetComponent<NewHuojiaFollow>().isMove = true; 
        }     
    }
}
