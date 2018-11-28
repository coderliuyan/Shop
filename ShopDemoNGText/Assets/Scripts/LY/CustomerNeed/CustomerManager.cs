using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager> {

    public List<int> customerIDList = new List<int>();

    int customerNumber = 13;
	// Use this for initialization
	void Start () {
        //默认的小晴人物
        customerIDList.Add(3001);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
