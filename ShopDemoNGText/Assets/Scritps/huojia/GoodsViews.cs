using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UpdateDataEventGoods(int id, int type,string name, int buyMoney, int setMoney, int unlockLevel,int maxKucun,int nowKucun);//因为是事件（通知改变）所以用到委托
public class GoodsViews : MonoBehaviour 
{
    GameObject _kucun;
    UILabel _kucunLable;
	void Start () 
    {
        _kucun = GameObject.Find("kucun");
       // _kucunLable = _kucun.GetComponent<UILabel>();
	}
	
	// Update is called once per frame
    void OnClick()
    {
        if (transform.tag == "Fruit")
        {
            GoodsManger.instance.OnButtonChoose(100);
        }
        if (transform.tag == "Orange")
        {
            GoodsManger.instance.OnBuittonClickLevel();
        }
	}
    //更新数据，通知改变（从model到view）是一个事件（通知改变）
    public void UpdateGoodsViewData(int goodsId,int goodsType,string goodsName,int buyMoney,int setMoney,int unLockLevel,int maxKucun,int nowKucun)
    {
       
        
       // _kucunLable.text = nowKucun + "/" + maxKucun;
    }
}
