using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCustomer : MonoBehaviour
{
    private int nowFloorId;
    public int NowFloorId
    {
        get
        {
            return nowFloorId;
        }
    }
    float MoveSpeed = 5;
    Dictionary<int, GameObject> _canmoveFloor = new Dictionary<int, GameObject>();
    List<int> _floorid = new List<int>();
    public  bool ismove;
    public GameObject _diban;
    public DragonBones.UnityArmatureComponent _longgu;
    int start = 0;
    DragonBones.Animation _anim;

    // Use this for initialization
    void Start()
    {
        _anim = _longgu.GetComponent<DragonBones.UnityArmatureComponent>().animation;
    }

    // Update is called once per frame
    void Update()
    {
        if (ismove)
        {
            MoveTo(_floorid[start]);
            CheckBarrier();
        }
    }
    //顾客层级根据所到地板的层级的变化而变化。
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.parent.tag == "Floor")
        {
            string floorNum = other.transform.parent.parent.name;
            string result = System.Text.RegularExpressions.Regex.Replace(floorNum, @"[^0-9]+", "");
            int ss = int.Parse(result);
            nowFloorId = ss;
            Debug.Log("collllllll+////"+other.transform.parent.parent.name);
            _longgu.GetComponent<DragonBones.UnityArmatureComponent>().sortingGroup.sortingOrder = other.transform.parent.parent.GetComponent<SpriteRenderer>().sortingOrder + 2;
            // _lonnggu.GetComponent<DragonBones.UnityArmatureComponent>().sortingOrder = ss + 11;
        }
        //bool _isTrigger=true;
        if (other.transform.tag == "Dead")
        {
            Debug.Log("dead+++++++++++++++++++");
            Destroy(gameObject,0.5f);
            Destroy(_longgu.gameObject,0.5f);
        }
    }
    /// <summary>
    /// 顾客移动方法。只在生成的路径上移动。
    /// </summary>
    /// <param name="_flooridd"></param>
    void MoveTo(int _flooridd)
    {
        transform.position = Vector3.MoveTowards(transform.position, _canmoveFloor[_flooridd].transform.position, MoveSpeed * Time.deltaTime);
        if (_canmoveFloor.Count > 0 && _floorid.Count > 0)
        {
            if (Vector3.Distance(transform.position, _canmoveFloor[_floorid[start]].transform.position) < 0.1f)
            {
                //预留交互点。
                if (start == 3 || start == 8)
                {
                    Debug.Log("交互" + start);
                    ismove = false;
                    Invoke("StartMove", 0.5f);
                }

                //_canmoveFloor.Remove(_floorid[0]);
                start += 1;
                Debug.Log("@@@@" + start);
                if (_floorid.Count== start)
                {
                    ismove = false;
                }
                else
                {
                    MoveTo(_floorid[start]);
                }
            }
        }
    }
    void StartMove()
    {
        ismove = true;
    }
    void HowToMove()
    {

    }
   /// <summary>
   /// 找到所有顾客可以移动的地板。添加到可以移动的地板的字典中。
   /// </summary>
    public void CustomerInit()
    {
        foreach (var item in GameDataManger.Instance.LujingPoint)
        {
            if (!_canmoveFloor.ContainsKey(item.Key))
            {
                _canmoveFloor.Add(item.Key, item.Value);
                _floorid.Add(item.Key);
            }
        }
        Debug.Log(_floorid.Count + "$$$$" + _canmoveFloor.Count);
        ismove = true;
    }
    /// <summary>
    /// 龙骨动画状态。0:正面呼吸。1:背面呼吸。2:左面呼吸。3：右面呼吸
    ///               4：正面行走。5：背面行走。6：左面行走。7：右面行走
    /// </summary>
    void PlayAnim(int Rad)
    {
        switch (Rad)
        {
            case 0:
                if (_anim.lastAnimationName != "face_stand")
                {
                    _longgu.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    _anim.Play("face_stand");
                }
                break;
            case 1:
                if (_anim.lastAnimationName != "back_stand")
                {
                    _longgu.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    _anim.Play("back_stand");
                }
                break;
            case 2:
                if (_anim.lastAnimationName != "back_stand")
                {
                    _longgu.transform.localRotation = Quaternion.Euler(-90, 180, 0);
                    _anim.Play("back_stand");
                }
                break;
            case 3:
                if (_anim.lastAnimationName != "face_stand")
                {
                    _longgu.transform.localRotation = Quaternion.Euler(-90, 180, 0);
                    _anim.Play("face_stand");
                }
                break;
            case 4:
                if (_anim.lastAnimationName != "face_walk")
                {
                    _longgu.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    _anim.Play("face_walk");
                }
                break;
            case 5:
                if (_anim.lastAnimationName != "back_walk")
                {
                    _longgu.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    _anim.Play("back_walk");
                }
                break;
            case 6:
                if (_anim.lastAnimationName != "back_walk")
                {
                    _longgu.transform.localRotation = Quaternion.Euler(-90, 180, 0);
                  
                    _anim.Play("back_walk");
                }
                break;
            case 7:
                if (_anim.lastAnimationName != "face_walk")
                {
                    _longgu.transform.localRotation = Quaternion.Euler(-90, 180, 0);
                    _anim.Play("face_walk");
                }
                break;
        }
    }
     /// <summary>
    /// 根据射线射到墙上的位置变化，龙骨移动动画变化。
    /// </summary>
    RaycastHit HitInfo;
    void CheckBarrier()
    {
        Ray ray1 = new Ray(transform.Find("shexianPlace").position,transform.forward);
        if (Physics.Raycast(ray1, out HitInfo))
        {
            Debug.DrawLine(transform.Find("shexianPlace").position, HitInfo.transform.position, Color.red);
            Debug.Log("射线方向+射线方向+射线方向+射线方向" + HitInfo.transform.name);
            if (HitInfo.transform.name == "ForwordQiang")
            {
               Debug.Log("4");
               PlayAnim(5);
               Debug.Log("角度，，，，，" + _longgu.transform.localRotation);
            }
            if (HitInfo.transform.name == "BackQiang")
            {
                Debug.Log("5");
                PlayAnim(4);
                Debug.Log(_longgu.transform.name);
                Debug.Log("角度，，，，，" + _longgu.transform.localRotation);
            }
            if (HitInfo.transform.name == "LeftQiang")
            {
                Debug.Log("6");
                PlayAnim(6);
                Debug.Log("角度，，，，，" + _longgu.transform.localRotation);
            }
            if (HitInfo.transform.name == "RightQiang")
            {
                Debug.Log("7");
                PlayAnim(7);
                Debug.Log("角度，，，，，" + _longgu.transform.localRotation);
            }
        }
    }
}
