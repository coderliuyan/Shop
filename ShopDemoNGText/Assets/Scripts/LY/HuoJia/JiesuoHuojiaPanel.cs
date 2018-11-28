using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiesuoHuojiaPanel : MonoBehaviour {

    private string huojiaSpritePath = @"HuojiaSprite";
    UISprite huojiaSprite;

    private string typeSpritePath = @"HuojiaType/TubiaoSprite";
    UISprite huojiaTypeSprite;

    private string quedingBtnPath = @"QuedingButton";
    UIButton qudingBtn;

    public static int huojiaID;

    private void OnEnable()
    {
        qudingBtn = transform.Find(quedingBtnPath).GetComponent<UIButton>();
        qudingBtn.onClick.Add(new EventDelegate(ClickQuedingBtn));
    }

    void ClickQuedingBtn()
    {
        if (HuoJiaManager.Instance.huojiaIDList.Contains(huojiaID))
        {
            HuoJiaManager.Instance.huojiaIDList.Add(huojiaID);

            //更新货架UI
         
        }

        if (!DataManager.Instance.huojiaId.Contains(huojiaID))
        {
            DataManager.Instance.huojiaId.Add(huojiaID);
            //更新货架UI
            SelectPanel.selectManager.GetHuojiaUI();
        }

        gameObject.SetActive(false);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
