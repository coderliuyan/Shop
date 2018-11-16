using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOpen : MonoBehaviour {
    public Transform  _MyGoodsCard;
    public short _GoodsId;
   public  TableValue _goodsdata;
   UILabel _name;
   UILabel _type;
   UILabel _jiage;
   Btton_Jian _bttonNumJian;
   Btton_Jian _bttonNumJia;
   string _jiagebegin;
   string _Numbegin;
   int _jiazhi;
   int _numbegin;
	void Start () 
    {
        _MyGoodsCard = GameObject.Find("UI Root ").transform.Find("BuySencondPanel");
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnClick()
    {
        _MyGoodsCard.gameObject.SetActive(true);
        GoodsInit();      
    }
    void GoodsInit()
    {       
        _name = _MyGoodsCard.transform.Find("Name_Lable").GetComponent<UILabel>();
        _type = _MyGoodsCard.transform.Find("Leixing_Lable").transform.Find("Lable").GetComponent<UILabel>();
        _jiage = _MyGoodsCard.transform.Find("Money_Lable").transform.Find("Lable").GetComponent<UILabel>();
        _bttonNumJian = _MyGoodsCard.transform.Find("Num_Lable_Sum").transform.Find("Button_jian-").GetComponent<Btton_Jian>();
        _bttonNumJia = _MyGoodsCard.transform.Find("Num_Lable_Sum").transform.Find("Button_jia+").GetComponent<Btton_Jian>();
        _bttonNumJia.ResetZero();
        _Numbegin = _bttonNumJia.num_int.ToString();
        _name.text = _goodsdata.GetString(_GoodsId,"name");
        _type.text = _goodsdata.GetString(_GoodsId,"type");
        _jiagebegin = _goodsdata.GetString(_GoodsId, "buy");
         _jiazhi = int.Parse(_jiagebegin);
         _numbegin = int.Parse(_Numbegin);
         Debug.Log(_jiazhi);
         Debug.Log(_numbegin);
        _jiage.text = (_jiazhi * _numbegin).ToString();
        _bttonNumJian._jiage = _jiagebegin;
        _bttonNumJia._jiage = _jiagebegin;
    }
}
