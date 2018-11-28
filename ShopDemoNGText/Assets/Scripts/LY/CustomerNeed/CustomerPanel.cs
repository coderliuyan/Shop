using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPanel : MonoBehaviour {

    private string customerSpritePath = @"CustomerSprite";
    UISprite cusomerSprite;

    private string quedingBtnPath = @"QuedingButton";
    UIButton qudingBtn;

    private string nameLabelPath = @"CustomerSprite/NameSprite/NameLabel";
    UILabel nameLabel;

    private string needBuyPath = @"CustomerSprite/BuyNeedSprite/GameObject/CustomerNeedSprite/BuySprite";
    UISprite needBuySprite;


    public static int customerID;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        cusomerSprite = transform.Find(customerSpritePath).GetComponent<UISprite>();
        qudingBtn = transform.Find(quedingBtnPath).GetComponent<UIButton>();
        nameLabel = transform.Find(nameLabelPath).GetComponent<UILabel>();
        needBuySprite = transform.Find(needBuyPath).GetComponent<UISprite>();

        cusomerSprite.spriteName = DataManager.Instance.customerXml.GetString(customerID, "cusName");
        nameLabel.text = DataManager.Instance.customerXml.GetString(customerID, "cusName");

        string needStr = DataManager.Instance.customerXml.GetString(customerID, "cusNeed");

        needBuySprite.spriteName = needStr;

        qudingBtn.onClick.Add(new EventDelegate(() => { ClickQudingBtn(); }));

    }

    void ClickQudingBtn()
    {
        CustomerManager.Instance.customerIDList.Add(customerID);
        CheckHuoJiaFresh();
        gameObject.SetActive(false);
    }

    void CheckHuoJiaFresh()
    {
        int huojiaID = DataManager.Instance.customerXml.GetInt(customerID, "shelfId");
        if (!HuoJiaManager.Instance.huojiaIDList.Contains(huojiaID))
        {
            //HuoJiaManager.Instance.huojiaIDList.Add(huojiaID);
            //人物解锁货架
            //打开货架UI界面
            JiesuoHuojiaPanel.huojiaID = huojiaID;
            UIManager.Instance.ShowHuojiaPanel();
        }

    }

}
