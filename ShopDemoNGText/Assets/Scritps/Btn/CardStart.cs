using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStart : MonoBehaviour {
    BoxCollider _MyCollider;
    GameObject _Money;
    TableValue CardBattleDate;//读表
    int mianbaohuojia;//商城的物品价格
    int tianpinhuojia;
    int Dangaohhuojia;
    int shucaihuojia;
    textMoney _money;
	// Use this for initialization
	void Start () 
    {
        _MyCollider = gameObject.GetComponent<BoxCollider>();
        _Money = GameObject.Find("money");
        Object obj = Resources.Load("Xml/huojiaType");
        XmlHelper.Instance.LoadFile("huojiaType", obj);
        CardBattleDate = XmlHelper.Instance.ReadFile("huojiaType");
        mianbaohuojia = CardBattleDate.GetInt(10001, "money");
        tianpinhuojia = CardBattleDate.GetInt(10002, "money");
        Dangaohhuojia = CardBattleDate.GetInt(10003, "money");
        shucaihuojia = CardBattleDate.GetInt(10004, "money");        
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnClick()
    {
        if (gameObject.transform.tag == "changtougui")
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
        textMoney.Instance._textMoney =  textMoney.Instance._Momey.ToString();
        _Money.GetComponent<UILabel>().text = textMoney.Instance._textMoney;
       //gameObject.SetActive(false);
        gameObject.transform.Find("Background").gameObject.SetActive(false);
        gameObject.transform.Find("Sprite").gameObject.SetActive(false);
        gameObject.transform.Find("zuanshiLable").gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.transform.Find("Use").gameObject.SetActive(true);
        //gameObject.transform.Find("num+1").gameObject.SetActive(true);
    }
}
