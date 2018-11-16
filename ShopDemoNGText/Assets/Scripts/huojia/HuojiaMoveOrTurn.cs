using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuojiaMoveOrTurn : MonoBehaviour {

    public Transform _huojia;
    UIButton _MoveButton;
    UIButton _TurnButton;
    SpriteRenderer _huojiaUI;
    int i;
    Transform _huojiaSprite;

    public bool _isMove;
    GameObject _huojiaParent;
	// Use this for initialization
	void Start () 
    {
        _MoveButton = transform.Find("MoveButton").GetComponent<UIButton>();
        _TurnButton = transform.Find("TurnButton").GetComponent<UIButton>();
        _MoveButton.onClick.Add(new EventDelegate(() => {MoveButtonEvent(); }));
        _TurnButton.onClick.Add(new EventDelegate(() => { TurnButtonEvent(); }));
        _huojiaUI = _huojia.Find("Huojia_Sprite").GetComponent<SpriteRenderer>();
        _huojiaSprite = _huojia.Find("Huojia_Sprite");

        _huojiaParent = _huojia.gameObject;
        i = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void MoveButtonEvent()
    {
        _isMove = !_isMove;
        //Debug.Log(_isMove);
        if (_isMove)
        {
            _huojia.GetComponent<NewHuojiaFollow>().enabled = true;
            _huojia.GetComponent<NewHuojiaFollow>().isMove = true;
        }
        else
        {
            transform.gameObject.SetActive(true);
        }
    }
    void TurnButtonEvent()
    {
        GameObject _BornSpriteHuojia = _huojia.Find("Huojia_Sprite").gameObject;
        i++;
        Debug.Log("jjjjjjj"+i);
        string _huojiaName = _huojia.GetComponent<HuojiaModel>().HuojiaName;
        if (i == 1)
        {
            //changeSpriteByImage(_huojiaName + i);
            //_huojiaUI.sprite.name = _huojiaName + i;
            //_huojiaSprite.transform.localPosition = new Vector3(0, 0f, 0);
            Destroy(_BornSpriteHuojia);
            GameObject _huojiaSpriteObj = ((GameObject)Instantiate(Resources.Load("新货架/货架/" + _huojiaName + i)));
            _huojiaSpriteObj.transform.SetParent(_huojia.transform);
            _huojiaSpriteObj.transform.localPosition = new Vector3(0, 0.2f, 0);
            _huojiaSpriteObj.transform.localScale = Vector3.one;
            _huojiaSpriteObj.transform.localRotation = Quaternion.identity;
            _huojiaSpriteObj.GetComponent<SpriteRenderer>().sortingOrder = _huojia.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
            _huojiaSpriteObj.transform.name = "Huojia_Sprite"; 
        }
        if (i == 2)
        {
            Destroy(_BornSpriteHuojia);
   
            //changeSpriteByImage(_huojiaName + i);
            //_huojiaUI.sprite.name = _huojiaName + i;
            //_huojiaSprite.transform.localPosition = new Vector3(0, 0f, 0);
            GameObject _huojiaSpriteObj = ((GameObject)Instantiate(Resources.Load("新货架/货架/" + _huojiaName + i)));
            _huojiaSpriteObj.transform.SetParent(_huojia.transform);
            _huojiaSpriteObj.transform.localPosition = new Vector3(0, 0.2f, 0);
            _huojiaSpriteObj.transform.localScale = Vector3.one;
            _huojiaSpriteObj.transform.localRotation = Quaternion.identity;
            _huojiaSpriteObj.GetComponent<SpriteRenderer>().sortingOrder = _huojia.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
            _huojiaSpriteObj.transform.name = "Huojia_Sprite"; 
        }
        if (i == 3)
        {
            Destroy(_BornSpriteHuojia);
            //changeSpriteByImage(_huojiaName + i);
            //_huojiaUI.sprite.name = _huojiaName + i;
            //_huojiaSprite.transform.localPosition = new Vector3(0, 0f, 0);
            GameObject _huojiaSpriteObj = ((GameObject)Instantiate(Resources.Load("新货架/货架/" + _huojiaName + i)));
            _huojiaSpriteObj.transform.SetParent(_huojia.transform);
            _huojiaSpriteObj.transform.localPosition = new Vector3(0, 0.2f, 0);
            _huojiaSpriteObj.transform.localScale = Vector3.one;
            _huojiaSpriteObj.transform.localRotation = Quaternion.identity;
            _huojiaSpriteObj.GetComponent<SpriteRenderer>().sortingOrder = _huojia.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
            _huojiaSpriteObj.transform.name = "Huojia_Sprite"; 
        }
        if (i == 4)
        {
            Destroy(_BornSpriteHuojia);
            //changeSpriteByImage(_huojiaName + i);
            //_huojiaUI.sprite.name = _huojiaName + i;
            //_huojiaSprite.transform.localPosition = new Vector3(0, 0, 0);
            //Destroy( _huojia.Find("Huojia_Sprite"));
            GameObject _huojiaSpriteObj = ((GameObject)Instantiate(Resources.Load("新货架/货架/" + _huojiaName + i)));
            _huojiaSpriteObj.transform.SetParent(_huojia.transform);
            _huojiaSpriteObj.transform.localPosition = new Vector3(0, 0.2f, 0);
            _huojiaSpriteObj.transform.localRotation = Quaternion.identity;
            _huojiaSpriteObj.transform.localScale = Vector3.one;
            _huojiaSpriteObj.GetComponent<SpriteRenderer>().sortingOrder = _huojia.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
            _huojiaSpriteObj.transform.name = "Huojia_Sprite";  
            i = 0;
        }
    }

    void changeSpriteByImage(string _name)
    {
        Texture2D Tex = Resources.Load("新货架/Goods/" + _name) as Texture2D;
        SpriteRenderer spr = _huojiaUI;
        Sprite spriteA = Sprite.Create(Tex, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        _huojiaUI.sprite = spriteA;
    }
}

