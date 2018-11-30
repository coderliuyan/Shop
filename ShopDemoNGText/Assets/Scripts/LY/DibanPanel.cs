using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DibanPanel : MonoBehaviour {


    private string kuojianBtnPath = @"mianbanSprite/KuojianButton";
    UIButton kuojianBtn;

    private string cancelBtnPath = @"mianbanSprite/CancelButton";
    UIButton cancelBtn;

	// Use this for initialization
	void Start () {
        kuojianBtn = transform.Find(kuojianBtnPath).GetComponent<UIButton>();
        cancelBtn = transform.Find(cancelBtnPath).GetComponent<UIButton>();

        kuojianBtn.onClick.Add(new EventDelegate(ClickKuojian));
        cancelBtn.onClick.Add(new EventDelegate(ClickCancel));
	}

    private void ClickKuojian()
    {
        DataManager.Instance.msgText = "尚未开发";
        UIManager.Instance.ShowMessagePanel();
        UIManager.Instance.ShowDibanPanel();
        gameObject.SetActive(false);
    }

    private void ClickCancel()
    {
        gameObject.SetActive(false);
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
