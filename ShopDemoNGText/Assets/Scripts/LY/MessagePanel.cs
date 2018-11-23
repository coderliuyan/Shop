using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MessagePanel : MonoBehaviour {

    private string msgLabelPath = @"Label";
    Transform msgLabel;
	// Use this for initialization
	void Awake () {
        msgLabel = transform.Find(msgLabelPath);
	}

    private void OnEnable()
    {
        Debug.Log("msg msg ++++++++++++");
        msgLabel.GetComponent<UILabel>().text = DataManager.Instance.msgText;
        msgLabel.DOLocalMoveY(-180f, 1f).SetEase(Ease.Linear).OnComplete(delegate {
            msgLabel.localPosition = new Vector3(0, -212f, 0);
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update () {
		
	}
}
