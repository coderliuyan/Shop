using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFetchPanel : MonoBehaviour {

    private string lvPath = @"LvSprite";
    UISprite lvSprite;

    private string touxiangPath = @"peopleSprite";
    UISprite touXiangSprite;

    private string quedingPath = @"QuedingButton";
    UIButton quedingBtn;

    public static string customerName;
    public static int customerID;
	// Use this for initialization
	void Start () {

    }

    private void OnEnable()
    {
        lvSprite = transform.Find(lvPath).GetComponent<UISprite>();
        touXiangSprite = transform.Find(touxiangPath).GetComponent<UISprite>();
        quedingBtn = transform.Find(quedingPath).GetComponent<UIButton>();

        lvSprite.spriteName = (3001 + Player.ShopLevel).ToString();
        touXiangSprite.spriteName = customerName;
        quedingBtn.onClick.Add(new EventDelegate( () => { ClickQuding(); }));
    }

    void ClickQuding()
    {
        //添加到顾客数组
        CustomerManager.Instance.customerIDList.Add(customerID);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
