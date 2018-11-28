using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiesuanPanel : MonoBehaviour {

    private string koubeiLabelPath = @"Mianban/KoubeiLabel/KoubeiNumLabel";
    UILabel koubeiLabel;

    private string jinbiLabelPath = @"Mianban/jinbiLabel/jinbiNumLabel";
    UILabel jinbiLabel;

    private string jinyanLabelPath = @"Mianban/jinyanLabel/jinyanNumLabel";
    UILabel jinyanLabel;

    private string quedingBtnPath = @"Mianban/QuedingButton";
    UIButton quedingBtn;

    private void Awake()
    {
        InitComponet();
    }

    void InitComponet()
    {
        koubeiLabel = transform.Find(koubeiLabelPath).GetComponent<UILabel>();
        jinbiLabel = transform.Find(jinbiLabelPath).GetComponent<UILabel>();
        jinyanLabel = transform.Find(jinyanLabelPath).GetComponent<UILabel>();
        quedingBtn = transform.Find(quedingBtnPath).GetComponent<UIButton>();
        quedingBtn.onClick.Add(new EventDelegate(ClickQuding));
    }

    void ClickQuding()
    {
        Player.ShopLevel += SelectPanel.selectManager.koubei;
        SelectPanel.selectManager.shopLevelLabel.text = Player.ShopLevel.ToString();
        Player.SavePlayerData();
  

        //读取表数据 看看 人物是否可以升级 
        int playerId = Player.PlayerLevel + 1000;
        Debug.Log(playerId);
        int nextExp = DataManager.Instance.playerXml.GetInt(playerId, "Exp");
        if(Player.PlayerExp >= nextExp)
        {
            Player.PlayerLevel++;
            SelectPanel.selectManager.playerLevelLabel.text = Player.PlayerLevel.ToString();
            Player.SavePlayerData();
        }

        //激活 select panel 底部 的button 

        SelectPanel.selectManager.ChangeButtonState(true);
        Debug.Log(Player.ShopLevel);
        if(SelectPanel.selectManager.koubei > 0)
        {
            JiesuoCustomer();
        }
        SelectPanel.selectManager.koubei = 0;
        SelectPanel.selectManager.jinbi = 0;
        SelectPanel.selectManager.exp = 0;
        gameObject.SetActive(false);

     
    }


    private void OnEnable()
    {
        koubeiLabel.text = "+" + SelectPanel.selectManager.koubei;
        jinbiLabel.text = "+" + SelectPanel.selectManager.jinbi;
        jinyanLabel.text = "+" + SelectPanel.selectManager.exp;
    }

    //依靠口碑等级 解锁顾客
    private void JiesuoCustomer()
    {
        switch (Player.ShopLevel)
        {
            case (1):
                {
                    CustomerFetchPanel.customerName = "樱石头像";
                    CustomerFetchPanel.customerID = 3002;
                    UIManager.Instance.ShowCustomerFetchPanel();
                }
                break;
            case (2):
                {
                    
                }
                break;
            default:
                Debug.Log("该等级没有可解锁的人物");
                break;

        }
    }





    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
