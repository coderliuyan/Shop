using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartText : MonoBehaviour {

    private static HeartText _instance = null;
    private void Awake()
    {
        _instance = this;
    }
    public static HeartText Instance
    {
        get
        {
            return _instance;
        }
    }
    public string _textHeart;
    public int _Num;

    void Start()
    {
        // _instance = this;
        _Num = 0;
        _textHeart = ":" + _Num.ToString();
        this.GetComponent<UILabel>().text = _textHeart;
    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
