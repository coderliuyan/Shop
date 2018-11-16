using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textMoney : MonoBehaviour
{
    private static textMoney _instance = null;
    private void Awake()
    {
        _instance = this;
    }
    public static textMoney Instance
    {
        get
        {
            return _instance;
        }
    }
	// Use this for initialization
    public string _textMoney;
    public int _Momey;//初始资金
    

	void Start ()
    {
       // _instance = this;
         _Momey = 0;
        _textMoney =""+_Momey.ToString();
        this.GetComponent<UILabel>().text=_textMoney;      
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    //判断要购买的商品，从而确定价格
    void BuyGoods()
    {

    }
}
