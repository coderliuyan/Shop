using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTai : MonoBehaviour {
   public  bool _isUse;
	void Start () 
    {
        _isUse = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
           //Debug.Log("刚进收银台");
            _isUse = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag=="Player")
        {
            Debug.Log("进入收银台");
            _isUse = true;
        }
    }
}
