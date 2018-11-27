using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFetchPanel : Singleton<CustomerFetchPanel> {

    private string touxiangPath = @"peopleSprite";
    UISprite touXiangSprite;

    private string quedingPath = @"QuedingButton";
    UIButton quedingBtn;

    public string customerName;
    public int customerID;
	// Use this for initialization
	void Start () {
        Transform t = transform.Find(touxiangPath);
        //.GetComponent<UISprite>();
        Transform tt = transform.Find(quedingPath);
        quedingBtn = tt.GetComponent<UIButton>();
        quedingBtn.onClick.Add(new EventDelegate (()=> { ClickQuding(); }));
	}

    private void OnEnable()
    {
        touXiangSprite.spriteName = customerName;
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
