using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btton_Jian : MonoBehaviour {
    
    public short _GoodsId;
    Transform _Num;
    Transform _MoneyNum;
    string num_text;
   public int num_int;
    int Sum_Num;
    Num_GouText _numGou;
    public string _jiage;
    int _goodsjiage;
	// Use this for initialization
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnClick()
    {
        _Num = GameObject.Find("Num_Lable_Sum").transform.Find("Num_Lable");
        _MoneyNum = GameObject.Find("Money_Lable").transform.Find("Lable");
        num_text = _Num.GetComponent<UILabel>().text;
        num_int = int.Parse(num_text);
        Debug.Log(num_int);
        _goodsjiage = int.Parse(_jiage);
        if (transform.tag == "NumJia")
        {
            num_int += 1000;                
        }
        if (transform.tag == "NumJian")
        {
            if (num_int > 0)
            {
                num_int -= 1000;
            }
        }
        Sum_Num = _goodsjiage * num_int;
       // Debug.Log(num_int);
        _Num.GetComponent<UILabel>().text = num_int.ToString();
        _MoneyNum.GetComponent<UILabel>().text = Sum_Num.ToString();
    }
    public void ResetZero()
    {
        _Num = GameObject.Find("Num_Lable_Sum").transform.Find("Num_Lable");
        num_int = 0;
        _Num.GetComponent<UILabel>().text = num_int.ToString();
    }
}
