using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuoJiaController : MonoBehaviour {

    private string backGroundPath = @"FunctionMenu/background";
    private string buhuoPath = @"FuncitonMenu/buhuo";
    private string upLevelPath = @"FunctionMenu/upLevel";
    private string turnPath = @"FunctionMenu/turn";


    //货架 ID
    public int huojiaID = 0;

    //货架的等级
    public int huojiaLevel = 0;

    //货架的朝向 1 是默认方向 。  1 - 4 为北 西 南 动 
    public int huojiaDirection = 1;

    //添加的货物类型
    public int goodsType;

    //货物的数量 
    public int goodsNumber = 0;

    //货物还可以被拿的次数 
    public int saleTimes = 0;

   

	// Use this for initialization
	void Start () {
      
	}


    void InitComponent()
    {
   

    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
