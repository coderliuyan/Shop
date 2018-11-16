using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCard : MonoBehaviour {
    GameObject _Money;
    string _i;
    Vector3 screenposition;
    Vector3 mousePositionOnScreen;
    Vector3 mousePositionInWorld;
    GameObject _shangcheng;
    Transform _NumHuojia;
    TableValue CardBattleDate;//读表
    int mianbaohuojia;//商城的物品价格
    int tianpinhuojia;
    int Dangaohhuojia;
    int shucaihuojia;
     textMoney _money;
	// Use this for initialization
	void Start () 
    {
        _shangcheng = GameObject.Find("ShangCheng");
        _Money = GameObject.Find("Money");
        Object obj = Resources.Load("Xml/huojiaType");
        XmlHelper.Instance.LoadFile("huojiaType", obj);
        CardBattleDate = XmlHelper.Instance.ReadFile("huojiaType");
        mianbaohuojia = CardBattleDate.GetInt(10001, "money");
        tianpinhuojia = CardBattleDate.GetInt(20001, "money");
        Dangaohhuojia = CardBattleDate.GetInt(20001, "money");
        shucaihuojia = CardBattleDate.GetInt(10001,"money");
	}
	
	// Update is called once per frame
	void Update () 
    {
        screenposition = Camera.main.WorldToScreenPoint(transform.position);
        mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = screenposition.z + 2;
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
	}
   int num;
    void OnClick()
    {
        Debug.Log("按下"+111111111);   
        //Debug.Log(_num);
     
        if (transform.tag == "Apple")
        {
            textMoney.Instance._Momey -= mianbaohuojia;
            GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/面包货架2"), mousePositionInWorld, Quaternion.Euler(90, 0, 0));
            //_shangcheng.SetActive(false);
        }
        if (transform.tag == "Tomato")
        {
            textMoney.Instance._Momey -= tianpinhuojia; 
            GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/蛋糕货架2"), mousePositionInWorld, Quaternion.Euler(90, 0, 0));
            //_shangcheng.SetActive(false);
        }
        if (transform.tag == "guizi")
        {
            textMoney.Instance._Momey -= Dangaohhuojia;
            GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/甜点货架2"), mousePositionInWorld, Quaternion.Euler(90, 0, 0));
            //_shangcheng.SetActive(false);
        }
        if (transform.tag == "Twohuogui")
        {
            textMoney.Instance._Momey -= shucaihuojia;
            GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/蔬菜货架2"), mousePositionInWorld, Quaternion.Euler(90, 0, 0));
            //_shangcheng.SetActive(false);
        }
        //else
        //{
        //    Debug.Log("请购买货架");
        //}
        textMoney.Instance._textMoney = "" + textMoney.Instance._Momey.ToString();
        _Money.GetComponent<UILabel>().text = textMoney.Instance._textMoney;
    }
}
