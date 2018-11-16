using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumBtn : MonoBehaviour {
   public GameObject _num;
    GameObject _Money;
    TableValue CardBattleDate;//读表
    int mianbaohuojia;//商城的物品价格
    int tianpinhuojia;
    int Dangaohhuojia;
    int shucaihuojia;
    string i;
   public int s;//货架数量；
    Transform _Lesshuojia;
	void Start () 
    {
        _num = transform.Find("num").gameObject;
        _Money = GameObject.Find("money");
        Object obj = Resources.Load("Xml/ShopType");
        XmlHelper.Instance.LoadFile("ShopType", obj);
        _Lesshuojia = transform.parent.parent;
        CardBattleDate = XmlHelper.Instance.ReadFile("ShopType");
        mianbaohuojia = CardBattleDate.GetInt(101, "money");
        tianpinhuojia = CardBattleDate.GetInt(102, "money");
        Dangaohhuojia = CardBattleDate.GetInt(103, "money");
        shucaihuojia = CardBattleDate.GetInt(104, "money");
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnClick()
    {
        i = _num.GetComponent<UILabel>().text;
        int _ss = int.Parse(i);
        if (textMoney.Instance._Momey >= 0)
        {
            int _aa = ++_ss;
            _num.GetComponent<UILabel>().text = _aa.ToString();
            Debug.Log("hhhhhhhhh"+_aa);
            if (transform.tag == "changtougui")
            {
                textMoney.Instance._Momey -= mianbaohuojia;
            }
            if (transform.tag == "sofa")
            {
                textMoney.Instance._Momey -= tianpinhuojia;
            }
            if (transform.tag == "guizi")
            {
                textMoney.Instance._Momey -= Dangaohhuojia;
            }
            if (transform.tag == "Twohuogui")
            {
                textMoney.Instance._Momey -= shucaihuojia;
            }
        }
        else
        {
            Debug.Log("没钱了！！！");
        }
        textMoney.Instance._textMoney = textMoney.Instance._Momey.ToString();
        _Money.GetComponent<UILabel>().text = textMoney.Instance._textMoney;
    }
}
