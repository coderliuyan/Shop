using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Num_GouText : MonoBehaviour {

    private static Num_GouText _instance = null;
    private void Awake()
    {
        _instance = this;
    }
    public static Num_GouText Instance
    {
        get
        {
            return _instance;
        }
    }
   public string _NumWupin;
   public int _Num_WuPinint;
   public string _Money;
   public int _Money_num;
   Transform _Num_Lable;
   Transform Money_Lable;
    void Start()
    {
        _Num_WuPinint = 0;
        _NumWupin = _Num_WuPinint.ToString();
        _Num_Lable = transform.Find("Num_Lable_Sum").transform.Find("Num_Lable");
        _Num_Lable.GetComponent<UILabel>().text = _NumWupin;
        Money_Lable = transform.Find("Money_Lable").transform.Find("Lable");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
