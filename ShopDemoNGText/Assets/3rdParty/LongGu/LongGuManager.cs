using DragonBones;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGuManager 
{
    private static LongGuManager ms_Ptr = null;
    public static LongGuManager GetInstance ()
    {
        if (ms_Ptr == null)
        {
            ms_Ptr = new LongGuManager();
            ms_Ptr.Start();
        }
        return ms_Ptr;
    }
    // Use this for initialization
    void Start ()
    {

    }
    /// <summary>
    /// 所加载龙骨资源对应的ske资源名
    /// </summary>
    Dictionary<string, string> skeDataName = new Dictionary<string, string>();
    /// <summary>
    /// 所加载龙骨资源对应的tex资源名
    /// </summary>
    Dictionary<string, string> texDataName = new Dictionary<string, string>();

    /// <summary>
    /// 角色所有动画
    /// 0-6正面：普通站立，普通走，持有站，持有走，拾取，放置，工作
    /// 7-13背面：普通站立，普通走，持有站，持有走，拾取，放置，工作
    /// </summary>
    string[] animName = { "face_stand", "face_walk","face_hold_stand","face_hold_walk", "face_pick","face_throw","face_work",
                                      "back_stand","back_walk","back_hold_stand", "back_hold_walk","back_pick","back_throw","back_work"};
bool isLoadData = false;
    /// <summary>
    /// 加载龙骨数据资源
    /// </summary>
    public void LoadLongGuData (string _whichPlayerData)
    {
        DragonBonesData whichSke=null;
        UnityTextureAtlasData whichTex=null;
        whichSke= UnityFactory.factory.LoadDragonBonesData("LongGu/"+ _whichPlayerData + "/"+ _whichPlayerData + "_ske");
        whichTex= UnityFactory.factory.LoadTextureAtlasData("LongGu/" + _whichPlayerData + "/" + _whichPlayerData + "_tex");
        if (!skeDataName.ContainsKey(_whichPlayerData))
        {
            skeDataName.Add(_whichPlayerData, whichSke.name);
        }
        if (!texDataName.ContainsKey(_whichPlayerData))
        {
            texDataName.Add(_whichPlayerData, whichTex.name);
        }
        isLoadData = true;
        Debug.Log("加载龙骨资源" + _whichPlayerData+ ",whichSke="+ whichSke.name+ ",whichTex="+ whichTex.name);

    }
    /// <summary>
    /// 创建龙骨角色
    /// </summary>
    public UnityArmatureComponent CreatLongGu (string _whichPlayer)
    {
        if (isLoadData==false)
        {
            LoadLongGuData(_whichPlayer);
        }
        Debug.Log("创建龙骨"+ _whichPlayer);
        UnityArmatureComponent whichLongGu = null;
        whichLongGu = UnityFactory.factory.BuildArmatureComponent(_whichPlayer);
        if (whichLongGu==null)
        {
            Debug.LogError(_whichPlayer+"is  null");
            return null;
        }
        whichLongGu.CloseCombineMeshs();
        whichLongGu.animation.Play("face_stand");
        //whichLongGu.transform.localPosition = new Vector3(730, -2.0f, 0.0f);
        whichLongGu.GetComponent<UnityEngine.Transform>().localScale = new Vector3(1, 1, 0);
        whichLongGu.GetComponent<UnityEngine.Transform>().localRotation =Quaternion.Euler(90,0,0);
        whichLongGu.sortingMode = SortingMode.SortByOrder;
        return whichLongGu;
    }
    /// <summary>
    /// 播放龙骨动画 
    /// 哪个龙骨 哪个动画 
    /// 0-6正面：普通站立，普通走，持有站，持有走，拾取，放置，工作
    /// 7-13背面：普通站立，普通走，持有站，持有走，拾取，放置，工作
    /// </summary>
    public void PlayPlayerAnim (UnityArmatureComponent _whichLongGu, byte _whichAnim)
    {
        int _playNum = 0;
        if (_whichAnim==4|| _whichAnim == 5|| _whichAnim == 11|| _whichAnim == 12)
        {
            _playNum = 1;
        }

        string _whichAnimName = animName[_whichAnim];
        //Debug.Log(_whichLongGu + "播放动画" + _whichAnim);
        DragonBones.Animation anim;
        anim = _whichLongGu.animation;
        //Animator anim1;
        //anim1.updateMode = AnimatorUpdateMode.UnscaledTime;
        if (anim.lastAnimationName != _whichAnimName)
        {
            anim.Play(_whichAnimName, _playNum);
        }
    }
    /// <summary>
    /// 播放龙骨动画 
    /// .哪个龙骨 哪个动画 执行几次(0为循环播放)
    /// </summary>
    /// <param name="_whichLongGu"></param>
    /// <param name="_whichAnim"></param>
    public void PlayAnim (UnityArmatureComponent _whichLongGu, string _whichAnimName,int _playNum)
    {
        if (_whichLongGu==null)
        {
            Debug.LogError(_whichLongGu+"is null");
            return;
        }
        //Debug.Log(_whichLongGu + "播放动画" + _whichAnim);
        DragonBones.Animation anim;
        anim = _whichLongGu.animation;
        //Animator anim1;
        //anim1.updateMode = AnimatorUpdateMode.UnscaledTime;
        if (anim.lastAnimationName != _whichAnimName)
        {
            anim.Play(_whichAnimName, _playNum);
        }
    }
    /// <summary>
    /// 定义某个龙骨动画的哪个骨骼
    /// </summary>
    /// <param name="_whichLongGu"></param>
    /// <param name="_whichSlot"></param>
    /// <returns></returns>
    public Slot SetLongGuSlot (UnityArmatureComponent _whichLongGu,string _whichSlot)
    {
        Slot newSlot = null;
        newSlot = _whichLongGu.armature.GetSlot(_whichSlot);
        Debug.Log("newSlot=" + newSlot.name);
        return newSlot;
    }
    /// <summary>
    /// 获取某个龙骨动画的所有骨骼
    /// </summary>
    public List<Slot> GetLongGuSlotAll (UnityArmatureComponent _whichLongGu)
    {
        List<Slot> slotAll = new List<Slot>();
        int childCount = _whichLongGu.transform.childCount;
        //Debug.LogError("childCount" +childCount);
        for (int i = 1; i < childCount; i++)//==1是为了去掉脚下的阴影
        {
            string childName = _whichLongGu.transform.GetChild(i).name;
            //Debug.LogError(i+".name=" + childName);
            Slot slot = null;
            slot = LongGuManager.GetInstance().SetLongGuSlot(_whichLongGu, childName);
            //maleSlotName.Add(childName, slot);
            slotAll.Add(slot);
            //Debug.LogError(i + "maleSlotName.name=" + childName + "@"+ maleSlotName[childName].name);
        }
        return slotAll;
        //Debug.Log("maleSlotName="+ maleSlotName.Count);
    }
    /// <summary>
    /// 换某个龙骨动画的所有骨骼节点下的图片
    /// 龙骨资源名，所有骨骼的集合，衣服的ID
    /// </summary>
    /// <param name="_longGuDataname"></param>
    /// <param name="_slotAll"></param>
    /// <param name="_clothesId"></param>
    public void ChangeClothesAll (string _longGuDataname,List<Slot> _slotAll,short _clothesId)
    {
        for (int i = 0; i < _slotAll.Count; i++)
        {
            LongGuManager.GetInstance().ChangeClothes(_longGuDataname, _longGuDataname, _slotAll[i].name, _slotAll[i].name + _clothesId.ToString(), _slotAll[i]);
        }
    }
    /// <summary>
    /// 换某个骨骼节点下的图片
    /// 龙骨资源名， 龙骨实例名， 获取所换图片的骨骼目标， 该骨骼下的图片名， 哪个骨骼节点需换图片
    /// </summary>
    public void ChangeClothes (string _dataName,string _playerName,string _whichSlotname,string _imageName,Slot _targetSlot)
    {
        UnityFactory.factory.ReplaceSlotDisplay(_dataName, _playerName, _whichSlotname, _imageName, _targetSlot);
    }
    /// <summary>
    /// 卸载龙骨资源
    /// </summary>
    public void UnLoadLongGuData (string _whichPlayerData, UnityArmatureComponent _whichLongGu)
    {
        Debug.Log("先删除obj"+ _whichLongGu + "，再卸载资源"+ _whichPlayerData);
        MonoBehaviour.Destroy(_whichLongGu.gameObject);
        UnityFactory.factory.RemoveDragonBonesData(skeDataName[_whichPlayerData], true);
        UnityFactory.factory.RemoveTextureAtlasData(texDataName[_whichPlayerData], true);
    }
    public void UnLoadLongGuData ()
    {
        foreach (var item in skeDataName)
        {
            UnityFactory.factory.RemoveDragonBonesData(item.Value, true); 
        }
        foreach (var item in texDataName)
        {
            UnityFactory.factory.RemoveTextureAtlasData(item.Value, true);
        }
    }
}
