using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour {

    private readonly string wellcomeBtnPath = @"ShangCheng/GameBeginCstomer/WelcomButton";
    private UIButton wellcomeBtn;


    public static SelectPanel selectManager;

    Boss boss;


    private void Awake()
    {
        if(selectManager == null){
            selectManager = this;
        }
        boss = Boss.CreatBossWithProperty(Boss.GameState.preparShop);
    }

    // Use this for initialization
    void Start () {
        InitComponet();
	}

    void InitComponet()
    {
        wellcomeBtn = transform.Find(wellcomeBtnPath).GetComponent<UIButton>();
        wellcomeBtn.onClick.Add(new EventDelegate(WellComeBtnClick));

    }
	
    void WellComeBtnClick()
    {
        boss.State = Boss.GameState.Shoping;
        wellcomeBtn.enabled = false;
        Debug.Log("wellcome  supermarket!");

        Invoke("ShopEnd",5f);

    }


    void ShopEnd()
    {

        Debug.Log("买完啦...");
        boss.State = Boss.GameState.preparShop;
        wellcomeBtn.enabled = true;
    }

    public void StateChanged(){
        switch(boss.State)
        {
            case(Boss.GameState.preparShop):
                {
                    Debug.Log("我想去买东西...");
                }
                break;
            case (Boss.GameState.Shoping):
                {
                    Debug.Log("我正在买东西...");
                }
                break;
        }
    }


	// Update is called once per frame
	void Update () {
		
	}
}

public class Boss
{
    public enum GameState
    {
        preparShop,
        Shoping
    }
    private GameState _state = GameState.preparShop;

    public GameState State{
        set{
            if(value != _state)
            {
                SelectPanel.selectManager.StateChanged();
            }
            _state = value;
        }
        get{
            return _state;
        }
    }

    public static Boss CreatBossWithProperty(GameState state){
        Boss boss = new Boss();
        boss.State = state;
        return boss;
    }

}
