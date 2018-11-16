using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBoss
{

    public enum BossState{
        prepar = 0,
        shoping
    }

    public static BossState State{
        set{
            if(value != State)
            {
                StateChange();
            }
            State = value;
        }
        get{
            return State;
        }
    }

    private static void StateChange()
    {
        switch(State){
            case(BossState.prepar):
                {
                    Debug.Log("正在购物中....");
                }
                break;
            case (BossState.shoping):
                {
                    Debug.Log("购物结束.....");
                }
                break;
        }
    }
}
