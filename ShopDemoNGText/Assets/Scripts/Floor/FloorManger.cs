using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManger : MonoBehaviour
{
    // Use this for initialization
   // Dictionary<int,GameObject> floor = new Dictionary<int,GameObject>();
  // List<GameObject> floor=new List<GameObject>();

  public   Dictionary<int, GameObject> temporaryAllFloor = new Dictionary<int, GameObject>();
    int selfFloorId;
    int targetFloorId;
    public Color WhichColor=Color.red;
    void Start()
    {     
        //GetAllNullFloor("Chu");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    AllFloorAddValue();
        //    Debug.LogError("temporaryAllFloor.count=-==" + temporaryAllFloor.Count);
        //    Debug.LogError(" GameDataManger.Instance.floorSencond.count=-==" + GameDataManger.Instance.floorSencond.Count);
        //    Way_Finding2(16);
        //}

        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //cqwe.CustomerInit();
        //}
    }
    public NewCustomer cqwe;
   public void AllFloorAddValue()
    {
        foreach (var item in GameDataManger.Instance.floorSencond)
        {
            if(!temporaryAllFloor.ContainsKey(item.Key))
            {
                 temporaryAllFloor.Add(item.Key,item.Value);
            }
        }
        foreach (var item1 in temporaryAllFloor)
            {
            item1.Value .GetComponent<SpriteRenderer>().color =Color.white;
            
            }
      //  Debug.Log("cccccccccccccccccccc" + temporaryAllFloor[3].GetComponent<SpriteRenderer>().color);
    }
    /// <summary>
    /// _Levelfloorname,每一等级父节点的名字。
    ///    //获取所有空的地板格，可以放货架或者走的。
    /// </summary>
    void GetAllNullFloor(string _Levelfloorname)
    {
        Transform _Chu = transform.Find(_Levelfloorname);
        for (int i = 0; i < _Chu.childCount; i++)
        {
            if (_Chu.GetChild(i).tag == "Floor")
            {
                GameDataManger.Instance.floorInit.Add(GetObjName(_Chu.GetChild(i).gameObject), _Chu.GetChild(i).gameObject);              
            }
        }
        GameDataManger.Instance.floorSencond = GameDataManger.Instance.floorInit;
        GameDataManger.Instance.floor = GameDataManger.Instance.floorSencond;
    }
    /// <summary>
    /// 获取objName
    /// </summary>
    /// <param name="_obj"></param>
    /// <returns></returns>
   int GetObjName(GameObject _obj)
    { 
         int numInt1 = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(_obj.transform.name, @"[^0-9]+", ""));
         return numInt1;
    }
    /// <summary>
    /// 货物目标位置在Player位置的首要方向，和次要方向（如：目标在一象限，方向为上，右），判断Player需要走的方向
    /// </summary>
    /// <param name="_selfFloorId"></param>
    /// <param name="_targetFloorId"></param>
    /// <returns></returns>
    int CompareFloorId2(int _selfFloorId, int _targetFloorId)
    {
        int selfDecade = _selfFloorId / 10;
        int selfUnit = _selfFloorId % 10;
        int targetDecade = _targetFloorId / 10;
        int targetUnit = _targetFloorId % 10;
        //四象限开始 右上到右下逆时针1——4
        Debug.Log(selfDecade + "@" + selfUnit + "@" + targetDecade + "@" + targetUnit + "@");
        if (System.Math.Abs(selfDecade - targetDecade) <= 0 && System.Math.Abs(selfUnit - targetUnit) <= 0)
        {
            return 0;
        }
        if (selfDecade < targetDecade)
        {
            if (selfUnit < targetUnit)
            {
                return 4;
            }
            else if (selfUnit == targetUnit)
            {
                return 34;
            }
            else
            {
                return 3;
            }
        }
        else if (selfDecade == targetDecade)
        {
            if (selfUnit < targetUnit)
            {
                return 41;
            }
            else if (selfUnit == targetUnit)
            {
                return 0;
            }
            else
            {
                return 23;
            }
        }
        else
        {
            if (selfUnit < targetUnit)
            {
                return 1;
            }
            else if (selfUnit == targetUnit)
            {
                return 12;
            }
            else
            {
                return 2;
            }
        }

    }
    /// <summary>
    /// 
    /// </summary>
  public void Way_Finding2(int _NowFloorid)
    {
        selfFloorId = _NowFloorid;
        //selfFloorId = playerState.NowFloorId;//玩家所占位置的地板id.
       // GameObject targetFloor = GameManager.Instance.AllPlaces[targetPlaceId].transform.parent.gameObject;//
        targetFloorId = 61;
        //targetFloorId = GameManager.Instance.FloorNameId(targetFloor);//目标地板
        temporaryAllFloor.Remove(selfFloorId);

        switch (CompareFloorId2(selfFloorId, targetFloorId))
        {
            case 0:
                Debug.Log("到了");
                break;
            case 1:
                if (temporaryAllFloor.ContainsKey(selfFloorId + 1))
                {
                    MoveTo(selfFloorId + 1);
                    temporaryAllFloor.Remove(selfFloorId + 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 10))
                {
                   // MoveTo(temporaryAllFloor[selfFloorId - 10].transform.position);
                    MoveTo(selfFloorId - 10);
                    temporaryAllFloor.Remove(selfFloorId - 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 10))
                {
                    MoveTo(selfFloorId + 10);
                    temporaryAllFloor.Remove(selfFloorId + 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 1))
                {
                    MoveTo(selfFloorId - 1);
                    temporaryAllFloor.Remove(selfFloorId - 1);
                }
                else
                {
                    Debug.LogError("在困死的点@@@@@@@@@@");
                    KunSi();
                }
                break;
            case 2:
                if (temporaryAllFloor.ContainsKey(selfFloorId - 1))
                {
                    MoveTo(selfFloorId - 1);
                    temporaryAllFloor.Remove(selfFloorId - 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 10))
                {
                    MoveTo(selfFloorId - 10);
                    temporaryAllFloor.Remove(selfFloorId - 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 10))
                {
                    MoveTo(selfFloorId + 10);
                    temporaryAllFloor.Remove(selfFloorId + 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 1))
                {
                    MoveTo(selfFloorId + 1);
                    temporaryAllFloor.Remove(selfFloorId + 1);
                }
                else
                {
                    Debug.LogError("在困死的点");
                    KunSi();
                }
                break;
            case 3:
                if (temporaryAllFloor.ContainsKey(selfFloorId - 1))
                {
                    MoveTo(selfFloorId - 1);
                    temporaryAllFloor.Remove(selfFloorId - 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 10))
                {
                    MoveTo(selfFloorId + 10);
                    temporaryAllFloor.Remove(selfFloorId + 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 10))
                {
                    MoveTo(selfFloorId - 10);
                    temporaryAllFloor.Remove(selfFloorId - 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 1))
                {
                    MoveTo(selfFloorId + 1);
                    temporaryAllFloor.Remove(selfFloorId + 1);
                }
                else
                {
                    Debug.LogError("在困死的点");
                    KunSi();
                }
                break;
            case 4:
                if (temporaryAllFloor.ContainsKey(selfFloorId + 1))
                {
                    MoveTo(selfFloorId + 1);
                    temporaryAllFloor.Remove(selfFloorId + 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 10))
                {
                    MoveTo(selfFloorId + 10);
                    temporaryAllFloor.Remove(selfFloorId + 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 10))
                {
                    MoveTo(selfFloorId - 10);
                    temporaryAllFloor.Remove(selfFloorId - 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 1))
                {
                    MoveTo(selfFloorId - 1);
                    temporaryAllFloor.Remove(selfFloorId - 1);
                }
                else
                {
                    Debug.LogError("在困死的点");
                    KunSi();
                }
                break;
            case 12:
                if (temporaryAllFloor.ContainsKey(selfFloorId - 10))
                {
                    MoveTo(selfFloorId - 10);
                    temporaryAllFloor.Remove(selfFloorId - 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 1))
                {
                    MoveTo(selfFloorId + 1);
                    temporaryAllFloor.Remove(selfFloorId + 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 1))
                {
                    MoveTo(selfFloorId - 1);
                    temporaryAllFloor.Remove(selfFloorId - 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 10))
                {
                    MoveTo(selfFloorId + 10);
                    temporaryAllFloor.Remove(selfFloorId + 10);
                }
                else
                {
                    Debug.LogError("在困死的点");
                    KunSi();
                }
                break;
            case 23:
                if (temporaryAllFloor.ContainsKey(selfFloorId - 1))
                {
                    MoveTo(selfFloorId - 1);
                    temporaryAllFloor.Remove(selfFloorId - 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 10))
                {
                    MoveTo(selfFloorId - 10);
                    temporaryAllFloor.Remove(selfFloorId - 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 10))
                {
                    MoveTo(selfFloorId + 10);
                    temporaryAllFloor.Remove(selfFloorId + 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 1))
                {
                    MoveTo(selfFloorId + 1);
                    temporaryAllFloor.Remove(selfFloorId + 1);
                }
                else
                {
                    Debug.LogError("在困死的点");
                    KunSi();
                }
                break;
            case 34:
                if (temporaryAllFloor.ContainsKey(selfFloorId + 10))
                {
                    MoveTo(selfFloorId + 10);
                    temporaryAllFloor.Remove(selfFloorId + 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 1))
                {
                    MoveTo(selfFloorId + 1);
                    temporaryAllFloor.Remove(selfFloorId + 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 1))
                {
                    MoveTo(selfFloorId - 1);
                    temporaryAllFloor.Remove(selfFloorId - 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 10))
                {
                    MoveTo(selfFloorId - 10);
                    temporaryAllFloor.Remove(selfFloorId - 10);
                }
                else
                {
                    Debug.LogError("在困死的点");
                    KunSi();
                }
                break;
            case 41:
                if (temporaryAllFloor.ContainsKey(selfFloorId + 1))
                {
                    MoveTo(selfFloorId + 1);
                    temporaryAllFloor.Remove(selfFloorId + 1);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 10))
                {
                    MoveTo(selfFloorId - 10);
                    temporaryAllFloor.Remove(selfFloorId - 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId + 10))
                {
                    MoveTo(selfFloorId + 10);
                    temporaryAllFloor.Remove(selfFloorId + 10);
                }
                else if (temporaryAllFloor.ContainsKey(selfFloorId - 1))
                {
                    MoveTo(selfFloorId - 1);
                    temporaryAllFloor.Remove(selfFloorId - 1);
                }
                else
                {
                
                    Debug.LogError("在困死的点");
                    KunSi();
                }
                break;
            default:
                break;
        }
    }

    void MoveTo(int _selfFloorId)
    {
        //变颜色的代码
        temporaryAllFloor[_selfFloorId].GetComponent<SpriteRenderer>().color = WhichColor;

        Debug.Log("FloorNAme" + temporaryAllFloor[_selfFloorId].transform.name + "@@@@@@@@" + temporaryAllFloor[_selfFloorId].GetComponent<SpriteRenderer>().color);
        GameDataManger.Instance.SetLujing(_selfFloorId, temporaryAllFloor[_selfFloorId]);
        selfFloorId = _selfFloorId;
        if (CompareFloorId2(selfFloorId, targetFloorId) != 0)
        {
            Way_Finding2(_selfFloorId);
        }
        else
        {
            Debug.Log("到了");
         //  foreach (var item in GameDataManger.Instance.LujingPoint)
         //{
         //Debug.Log("LUJING++++" + item.Key);
         //}       
        }
    }
    void KunSi()
    {
    GameDataManger.Instance.RemoveLujing(selfFloorId);
    AllFloorAddValue();
    Debug.LogError("temporaryAllFloor.count=-==" + temporaryAllFloor.Count);
    Debug.LogError(" GameDataManger.Instance.floorSencond.count=-==" + GameDataManger.Instance.floorSencond.Count);
    Way_Finding2(selfFloorId);
    //Debug.LogError( CompareFloorId2(15, 61));
    }
}
