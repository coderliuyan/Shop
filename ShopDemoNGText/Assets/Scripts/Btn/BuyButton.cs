using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour {
    GameObject _gouwuChe;
    Transform _buyCar;
    string  _name;
    string _Num;
    string _price;//二级界面的价格
     UILabel  _jisuan;//结算
	// Use this for initialization
	void Start ()
    {
         
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnClick()
    {
        BornBuyLable();

    }
    void BornBuyLable()
    {
        _gouwuChe = GameObject.Find("BuyPanel/Gouwuche");
        _buyCar = _gouwuChe.transform.Find("Buy_Scroll View/BuyCar");
        _jisuan = _gouwuChe.transform.Find("heji_lable/heji").transform.Find("ajinbiNum").GetComponent<UILabel>();
        _name = transform.parent.parent.transform.Find("Name_Lable").GetComponent<UILabel>().text;
        _Num = transform.parent.parent.transform.Find("Num_Lable_Sum").transform.Find("Num_Lable").GetComponent<UILabel>().text;
        _price = transform.parent.parent.transform.Find("Money_Lable").transform.Find("Lable").GetComponent<UILabel>().text;
        if (int.Parse(_Num) <= 0)
        {
            Debug.Log("购买数量不能为0");
            return;
        }
        GameObject obj = ((GameObject)Instantiate(Resources.Load("UI/Buy_Lable")));
        obj.transform.SetParent(_buyCar.transform);
        obj.transform.Find("FirstName").GetComponent<UILabel>().text =_name;
        obj.transform.Find("FirstNameNum").GetComponent<UILabel>().text = _Num+"个";
        obj.transform.Find("jinbi/ajinbiNum").GetComponent<UILabel>().text =_price;
       // obj.transform.Find("SetJiage").GetComponent<UILabel>().text = goodsSet.ToString();
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.Find("Quit").GetComponent<UIButton>().onClick.Add(new EventDelegate(() => {AddQuitClick(obj); }));
        _buyCar.GetComponent<UIGrid>().enabled = true;
        gameObject.transform.parent.parent.gameObject.SetActive(false);
        GoodsManger.instance.SumPrice(int.Parse(_price));
        //obj.name = fruitsNum.ToString();
    }
   
    public void AddQuitClick(GameObject _BuyLable)
    {
        string _priceJianText;//购物车内预制体价格。
        int _priceJianInt;
        _priceJianText = _BuyLable.transform.Find("jinbi/ajinbiNum").GetComponent<UILabel>().text;
        _priceJianInt = int.Parse(_priceJianText);
        GoodsManger.instance.JianPrice(_priceJianInt);
        Destroy(_BuyLable);
        _buyCar.GetComponent<UIGrid>().enabled = true;
    }
}
