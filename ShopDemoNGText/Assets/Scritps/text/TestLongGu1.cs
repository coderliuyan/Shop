using DragonBones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLongGu1 : MonoBehaviour
{
    //public Text textTest;



    public byte whichCloth = 1;

    public UnityEngine.Transform maleTF;
    public UnityEngine.Transform femaleTF;

    UnityArmatureComponent male;
    UnityArmatureComponent female;

    List<Slot> maleSlotAll = new List<Slot>();
    List<Slot> femaleSlotAll = new List<Slot>();
    //public     List<int> tesst = new List<int>() ;

    UnityArmatureComponent zhaohuo;
    UnityArmatureComponent miehuo;

    // Use this for initialization
    void Start ()
    {

        //textTest.fontSize = 150;

        //Debug.Log(textTest.transform.Find("Image") + "@@@" + UITool.GetUIComponent<UnityEngine.Transform>(textTest.gameObject, "Image1") );
        //tesst.Add(0);
        //tesst.Add(1);
        //tesst.Add(2);
        //tesst.Add(3);
        //tesst.Add(4);
        //tesst.Add(5);
        //int targetPos = tesst.IndexOf(4);
        //Debug.LogError("targetPos="+ targetPos);
        //transform.Find("TTT").SetSiblingIndex(targetPos);
        LoadData();
        CreatPlayer();
        SetAllSlot();

    }
    void LoadData ()
    {
        LongGuManager.GetInstance().LoadLongGuData("LongGuMale");
        LongGuManager.GetInstance().LoadLongGuData("LongGuFemale");


     
    }
    void CreatPlayer ()
    {
        male = LongGuManager.GetInstance().CreatLongGu("LongGuMale");
       // male.gameObject.AddComponent<ObjectFollow>().Target = maleTF;
        female = LongGuManager.GetInstance().CreatLongGu("LongGuFemale");
       // female.gameObject.AddComponent<ObjectFollow>().Target = femaleTF;


        
    }
    void SetAllSlot ()
    {
        maleSlotAll = LongGuManager.GetInstance().GetLongGuSlotAll(male);
        femaleSlotAll = LongGuManager.GetInstance().GetLongGuSlotAll(female);
    }
    void ChangeAllClothes (byte _whichClothes)
    {
        LongGuManager.GetInstance().ChangeClothesAll("LongGuMale", maleSlotAll, _whichClothes);
        LongGuManager.GetInstance().ChangeClothesAll("LongGuFemale", femaleSlotAll, _whichClothes);
    }
    byte _whichAnim = 0;
    void PlayAnim (byte _whichAnim)
    {
        LongGuManager.GetInstance().PlayPlayerAnim(male, _whichAnim);
        LongGuManager.GetInstance().PlayPlayerAnim(female, _whichAnim);
    }
    // Update is called once per frame
    void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeAllClothes(whichCloth);
            PlayAnim(_whichAnim);

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeAllClothes(whichCloth);
            PlayAnim((byte)(_whichAnim+7));
            _whichAnim += 1;
            if (_whichAnim == 7)
            {
                _whichAnim = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAnim(4);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayAnim(5);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayAnim(6);
        }
    }

}
