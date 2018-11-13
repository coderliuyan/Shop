using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBtn : MonoBehaviour {
    public GameObject _quit;
    GameObject _openShop;
   // GameObject _gameObject;
	// Use this for initialization
	void Start ()
    {
       // _openShop = GameObject.Find("OpenShop");
      // _gameObject = GameObject.Find("GameObject");	
	}
	
	// Update is called once per frame
    void OnClick()
    {
        //_openShop.GetComponent<Btn>().enabled = false;
        _quit.SetActive(false);
    }
}
