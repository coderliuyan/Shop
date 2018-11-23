using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoOpenPanel : MonoBehaviour {

    private string quitBtnPath = @"mianbanSprite/QuitButton";
    UIButton quitBtn;


	// Use this for initialization
	void Start () {
        quitBtn = transform.Find(quitBtnPath).GetComponent<UIButton>();
        quitBtn.onClick.Add(new EventDelegate(ClosePanel));
	}
	
    void ClosePanel()
    {
        gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
