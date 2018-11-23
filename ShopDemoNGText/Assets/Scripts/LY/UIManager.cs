using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    private string noOpenPanelPath = @"NoOpenPanel";
    GameObject noOpenPanel;

    private string buyPanelPath = @"BuyPanel_all";
    GameObject buyPanel;

    private string jiesuanUIPath = @"jiesuanUI";
    GameObject jiesuanPanel;

    private string messagePanelPath = @"MessagePanel";
    GameObject messagePanel;

	// Use this for initialization
	void Start () {
        //初始化组件
        InitPanel();
	}

    void InitPanel()
    {
        noOpenPanel = transform.Find(noOpenPanelPath).gameObject;
        buyPanel = transform.Find(buyPanelPath).gameObject;
        jiesuanPanel = transform.Find(jiesuanUIPath).gameObject;
        messagePanel = transform.Find(messagePanelPath).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(Player.GoldNum);
            Debug.Log(Player.PlayerLevel);
        }
	}


    public void NoOpen()
    {
        noOpenPanel.SetActive(true);
    }


    public void ShowBuyPanel()
    {
        buyPanel.SetActive(true);
    }

    public void ShowJiesuanPanel()
    {
        jiesuanPanel.SetActive(true);
    }

    public void ShowMessagePanel()
    {
        messagePanel.SetActive(true);
    }
}
