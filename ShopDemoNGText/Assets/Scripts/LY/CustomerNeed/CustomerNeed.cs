using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerNeed : MonoBehaviour {

    //顾客ID
    public int customerID;

    //买的次数
    public int buyTimes = 0;

    //每次最多买多少
    public int buyTimesNumber = 100;


    //买的货物类型 比如说水果 
    public string goodsType;

    //买到的货物具体的种类 和数量
    public Dictionary<int, int> goods = new Dictionary<int, int>();

    //说的话 目前还没有
    public string sayWord;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
