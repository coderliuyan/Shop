using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UpdateDataEventHandle(int id, string name, int Level,int Money, int power, int scal);//因为是事件（通知改变）所以用到委托
public class HuojiaView : MonoBehaviour 
{
  
    void Start()
    {
      
    }
    void OnClick()
    {
       
    }
    //更新数据，通知改变（从model到view）是一个事件（通知改变）
    public void UpdateViewData(int huojiaid,string huojianame,int huojiaLevel,int huojiamoney,int huojiapower,int huojiascal)
    {
                                   
    }
}
