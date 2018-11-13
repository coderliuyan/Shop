using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBtn : MonoBehaviour {

   BoxCollider _MyCollider;
   GameObject _Money;
    TableValue CardBattleDate;//读表
    int mianbaohuojia;//商城的物品价格
    int tianpinhuojia;
    int Dangaohhuojia;
    int shucaihuojia;
     textMoney _money;
     UISprite _CardIcon;
     public UIAtlas atals;
     GameObject _MyGrid;
     Vector3 screenposition;
     Vector3 mousePositionOnScreen;
     Vector3 mousePositionWorld;
  public int huojiamianbaoArea;
  public int huojiatianpingArea;
  public int huojiaDangaoArea;
  public  int huojiaShucaiArea;
	void Awake ()
    {
        _Money = GameObject.Find("money");
        _MyCollider = gameObject.GetComponent<BoxCollider>();
        Object obj = Resources.Load("ShopType");
        XmlHelper.Instance.LoadFile("ShopType", obj);
        CardBattleDate = XmlHelper.Instance.ReadFile("ShopType");
        mianbaohuojia = CardBattleDate.GetInt(101, "money");
        tianpinhuojia = CardBattleDate.GetInt(102, "money");
        Dangaohhuojia = CardBattleDate.GetInt(103, "money");
        shucaihuojia = CardBattleDate.GetInt(104,"money");
        huojiamianbaoArea = CardBattleDate.GetInt(101, "area");
        huojiatianpingArea = CardBattleDate.GetInt(102,"area");
        huojiaDangaoArea = CardBattleDate.GetInt(103, "area");
        huojiaShucaiArea = CardBattleDate.GetInt(104, "area");
        //_CardIcon = gameObject.GetComponent<UISprite>();
        _MyGrid = GameObject.Find("CardGrid");
	}
	
	// Update is called once per frame
	void Update () 
    {
        screenposition = Camera.main.WorldToScreenPoint(transform.position);
        mousePositionOnScreen = Input.mousePosition;
        mousePositionOnScreen.z = screenposition.z;
        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
	}
    void OnClick()
    {       
        if (_MyCollider.tag == "changtougui")
        {
            textMoney.Instance._Momey -= mianbaohuojia;
            if (IshuojiaFollow.Instance._moving == true)
            {
                GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_huazhuangpin"), mousePositionWorld, Quaternion.identity);
                obj.GetComponent<Follow>().OnMouseDownTrue();
                //Debug.Log(huojiamianbaoArea);
            }
        }

        if (_MyCollider.tag == "sofa")
        {
            textMoney.Instance._Momey -= tianpinhuojia;      
            if (IshuojiaFollow.Instance._moving == true)
            {
                GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia__shechipin"), mousePositionWorld, Quaternion.identity);           
                obj.GetComponent<Follow>().OnMouseDownTrue();
                Debug.Log(huojiatianpingArea);
            }
        }
        if (_MyCollider.tag == "guizi")
        {
            textMoney.Instance._Momey -= Dangaohhuojia;
            if (IshuojiaFollow.Instance._moving == true)
            {
                GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_choose"), mousePositionWorld, Quaternion.identity);
               // Destroy(gameObject);
                obj.GetComponent<Follow>().OnMouseDownTrue();
                Debug.Log(huojiaDangaoArea);
            }
        }
        if (_MyCollider.tag == "Twohuogui")
        {
            textMoney.Instance._Momey -= shucaihuojia;
            if (IshuojiaFollow.Instance._moving == true)
            {
                GameObject obj = (GameObject)Instantiate(Resources.Load("huojia/huojia_twoge"), mousePositionWorld, Quaternion.identity);
                //gameObject.SetActive(false);
                obj.GetComponent<Follow>().OnMouseDownTrue();
               // Debug.Log(huojiaShucaiArea);
            }
        }
        textMoney.Instance._textMoney =":" + textMoney.Instance._Momey.ToString();
        _Money.GetComponent<UILabel>().text = textMoney.Instance._textMoney;      
    }
}
