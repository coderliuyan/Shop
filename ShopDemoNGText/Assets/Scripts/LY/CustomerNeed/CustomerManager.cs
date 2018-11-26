using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : Singleton<CustomerManager> {

    public List<int> customerIDList = new List<int>();

    int customerNumber = 13;
	// Use this for initialization
	void Start () {
        for (int i = 1; i <= customerNumber; i++)
        {
            customerIDList.Add(3000 + i);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
