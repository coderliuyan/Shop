using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHuojiaTurn : MonoBehaviour {

    public Transform _huojia;//货架
   // public bool _isTurn;//是否旋转
    Transform _huojiaT;//货架的旋转按钮
    Transform _huojiaM;//货架的移动按钮
    SpriteRenderer _huojiaUI;
    int i;
	// Use this for initialization
	void Start () 
    {
        _huojiaM = _huojia.Find("Move");
        _huojiaT = _huojia.Find("Turn");
        _huojiaUI = _huojia.Find("Huojia_Sprite").GetComponent<SpriteRenderer>();
        i = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnClick()
    {
        ss();
    }

    void changeSpriteByImage(string _name)
    {
        Texture2D Tex = Resources.Load("新货架/Goods/" + _name) as Texture2D;
        SpriteRenderer spr = _huojiaUI;
        Sprite spriteA = Sprite.Create(Tex, spr.sprite.textureRect, new Vector2(0.5f, 0.5f));
        _huojiaUI.sprite = spriteA;
    }
    void ss()
    {
        GameObject _BornSpriteHuojia = _huojia.Find("Huojia_Sprite").gameObject;
        i++;
        Debug.Log("jjjjjjj" + i);
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
            _huojiaSpriteObj.transform.name = "Huojia_Sprite";
            i = 0;
        }
    }
}

