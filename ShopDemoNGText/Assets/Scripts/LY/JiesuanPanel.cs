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
        Player.SavePlayerData();
        SelectPanel.selectManager.koubei = 0;
        SelectPanel.selectManager.jinbi = 0;
        SelectPanel.selectManager.exp = 0;

        //读取表数据 看看 人物是否可以升级 
        int nextExp = DataManager.Instance.playerXml.GetInt(Player.PlayerLevel + 1, "Exp");
        if(Player.PlayerExp >= nextExp)
        {
            Player.PlayerLevel++;
            SelectPanel.selectManager.playerExpLabel.text = Player.PlayerLevel.ToString();
            Player.SavePlayerData();
        }


        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        koubeiLabel.text = "+" + SelectPanel.selectManager.koubei;
        jinbiLabel.text = "+" + SelectPanel.selectManager.jinbi;
        jinyanLabel.text = "+" + SelectPanel.selectManager.exp;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.A)){
            Debug.Log("A");
            Debug.Log( DataManager.Instance.playerXml.GetInt(1001,"Exp"));
        }
	}
}
