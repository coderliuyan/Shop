using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHuojiaButton : MonoBehaviour {

    public Transform _huojia;
    UIButton _ChangeGoodsButton;
    UIButton _LevelButton;
    UIButton _BuhuoButton;
    Transform _goodsobj;
    TableValue GoodsData;
    Transform _SelectPanel;
	// Use this for initialization
	void Start () 
    {
        Object GoodsObj = Resources.Load("Xml/DataType_Goods");
        XmlHelper.Instance.LoadFile("DataType_Goods", GoodsObj);
        GoodsData = XmlHelper.Instance.ReadFile("DataType_Goods");
        _ChangeGoodsButton = transform.Find("ChangeButton").GetComponent<UIButton>();
        _LevelButton = transform.Find("LevelButton").GetComponent<UIButton>();
        _BuhuoButton = transform.Find("BuHuoButton").GetComponent<UIButton>();
        _BuhuoButton.onClick.Add(new EventDelegate(() => { BuHuoButton();}));
        _LevelButton.onClick.Add(new EventDelegate(() => { LevelButton(); }));
        _SelectPanel = transform.parent.Find("UIManger").transform.Find("SelectPanel");
        OpenBuyPanelButton(transform, false);
    }	
	// Update is called once per frame
	void Update () 
    {
	}
    void ChangeGoodsButton()
    {
    }
    void LevelButton()
    {
        _huojia.GetComponent<HuojiaModel>().AddLevel();
        _SelectPanel.GetComponent<SelectPanelUI>().ReduceMoneyButton(_huojia.GetComponent<HuojiaModel>().HuojiaMoney);
        Debug.Log("hhhhhhhhhhh"+_huojia.GetComponent<HuojiaModel>().HuojiaId);
        OpenBuyPanelButton(transform, false);
    }
    void BuHuoButton()
    {
     _huojia.GetComponent<HuojiaModel>().BuHuo();
     _goodsobj=_huojia.transform.Find("GoodsObj");
     if (_goodsobj.childCount == 0)
     { 
     //GameObject HuojiaObj = ((GameObject)Instantiate(Resources.Load("NewGoods/Goods")));
     //HuojiaObj.transform.SetParent(_goodsobj);
     //HuojiaObj.transform.localPosition = Vector3.zero;
     //HuojiaObj.transform.localRotation = Quaternion.identity;
     //HuojiaObj.transform.localScale = Vector3.one;
   // HuojiaObj.GetComponent<SpriteRenderer>().sortingOrder = _huojia.transform.Find("Huojia_Sprite").GetComponent<SpriteRenderer>().sortingOrder+1;
   // changeSpriteByImage(HuojiaObj,_goodsname);
    string _goodsname=GoodsData.GetString(_huojia.GetComponent<HuojiaModel>().huowuId,"name");
    GameObject HuojiaGoodsObj = ((GameObject)Instantiate(Resources.Load("新货架/货架/good/" + _goodsname)));
    HuojiaGoodsObj.transform.SetParent(_goodsobj);
    HuojiaGoodsObj.transform.localRotation = Quaternion.identity;
    HuojiaGoodsObj.transform.localScale = Vector3.one;
    HuojiaGoodsObj.transform.localPosition = _huojia.transform.Find("Huojia_Sprite").localPosition;
    HuojiaGoodsObj.GetComponent<SpriteRenderer>().sortingOrder = _huojia.transform.Find("Huojia_Sprite").GetComponent<SpriteRenderer>().sortingOrder + 1;  
   // Debug.Log(HuojiaGoodsObj.GetComponent<SpriteRenderer>().sprite.name);
    }
     OpenBuyPanelButton(transform,false);
   }
    void changeSpriteByImage(GameObject _huojiaobj,string _name)
    {
        Texture2D Tex = Resources.Load("新货架/水果货架/" + _name) as Texture2D;
        SpriteRenderer spr = _huojiaobj.GetComponent<SpriteRenderer>();
        Sprite spriteA = Sprite.Create(Tex, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        _huojiaobj.GetComponent<SpriteRenderer>().sprite = spriteA;
    }
    void OpenBuyPanelButton(Transform _buypanel, bool OpenOrClose)
    {
        _buypanel.gameObject.SetActive(OpenOrClose);
    }
}

