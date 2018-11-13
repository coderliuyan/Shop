using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAllHuojia : MonoBehaviour {
    private static FindAllHuojia _instance = null;
   public GameObject []_allHuojia;
   public GameObject[] _allHuojiaButton;
  // public GameObject[] _allPlayer;
    int i;
	// Use this for initialization
    void Awake()
    {
        _instance = this;
        _allHuojia = GameObject.FindGameObjectsWithTag("huojia");
        _allHuojiaButton = GameObject.FindGameObjectsWithTag("HuojiaButton");
        //for (int i = 0; i < _allHuojia.Length; i++)
        //{
        //    Debug.Log(_allHuojia[i]);
        //}
        //for (int i = 0; i < _allHuojiaButton.Length; i++)
        //{
        //    Debug.Log(_allHuojiaButton.Length);
        //}
      
    }
    public static FindAllHuojia Instance
    {
        get
        {
            return _instance;
        }
    }

	
	// Update is called once per frame
	void Update ()
    {
       
	}
}
