using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigDefine;
using System.Linq;
/// <summary>
/// 保存所有的地板空节点 
/// 有更新节点的方法
/// </summary>
public class FloorManager : MonoBehaviour {

    //单例
    private  static FloorManager _instance;
    public static FloorManager Instance
    {
        get
        {
            return _instance;
        }
    }

    //存放所有的地板
    [HideInInspector] public Dictionary<int, GameObject> allFloor;

    //存放空地板的字典，序号和物体对象
    [HideInInspector] public Dictionary<int, GameObject> floorInterable;


    //已经有建筑的区域 也是不能建造的地板
    [HideInInspector] public Dictionary<int, GameObject> buildings;

    //所有地方的父物体
    public Transform[] floors;

    //当前可用的地板的父物体
    [HideInInspector] public List<Transform> aciveFloors;


    //存放所有的路径
    [HideInInspector] public List<List<int>> allTheWays;

    //顾客的行走路径
    [HideInInspector] public  List<int> custormWay ;

    //不能建造的区域的位置
    [HideInInspector] public  List<int> noBuildArea ;

 



    //获取所有的地板
    public void FetchAllFloor()
    {
        allFloor.Clear();
        foreach (Transform obj in floors)
        {
            if (obj.gameObject.activeSelf)
            {
                for (int i = 0; i < obj.childCount; i++)
                {
                    GameObject go = obj.GetChild(i).gameObject;
                    int index = GetObjName(go);
                    allFloor.Add(index, go);
                }
            }
        }
    }



    //可交互的所有的地板 
    public void FetchActiveFloor()
    {
        floorInterable.Clear();
        foreach(Transform obj in floors)
        {
            if (obj.gameObject.activeSelf)
            {
                for(int i = 0; i< obj.childCount; i++)
                {
                    GameObject go = obj.GetChild(i).gameObject;
                    if(go.transform.childCount> 1)
                    {
                        continue;
                    }
                    int index = GetObjName(go);
                    floorInterable.Add(index,go);
                }
            }
        }
    }


    //所有有建筑的地板 
    public void FetchBuildingFloor()
    {
        buildings.Clear();
        foreach (Transform obj in floors)
        {
            if (obj.gameObject.activeSelf)
            {
                for (int i = 0; i < obj.childCount; i++)
                {
                    GameObject go = obj.GetChild(i).gameObject;
                    if (go.transform.childCount > 1)
                    {
                        int index = GetObjName(go);
                        buildings.Add(index,go);
                    }

                }
            }
        }
    }

    //获取物体名字中包含的数字
    public int GetObjName(GameObject _obj)
    {
        int numInt1 = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(_obj.transform.name, @"[^0-9]+", ""));
        return numInt1;
    }


    // Use this for initialization
    private void Awake () {
		if(_instance == null)
        {
            _instance = this;
        }
	}

    private void Start()
    {
        //进行初始化
        InitComponent();
    }

    //初始化方法
    public void InitComponent()
    {


        allFloor = new Dictionary<int, GameObject>();
        floorInterable = new Dictionary<int, GameObject>();
        allTheWays = new List<List<int>>();
        custormWay = new List<int>();
        noBuildArea = new List<int>();


        //赋值
        FetchAllFloor();
        FetchActiveFloor();
    }
    // Update is called once per frame
    void Update () {
      
    }

    //寻找路径
    public bool FetchActiveWay()
    {
        allTheWays.Clear();
        List<int> born = new List<int>();
        born.Add(Define.BORN_POS);
        allTheWays.Add(born);
        FetchActiveFloor();
        noBuildArea.Clear();
        custormWay.Clear();

        if (FindWays(born))
        {
            Debug.Log("有正确的路径。" + allTheWays.Count);

            foreach (List<int>  t in allTheWays)
            {
                if (t.Contains(Define.OUT_DOOR_POS))
                {
                    custormWay.Clear();
                    foreach(int num in t)
                    {
                        custormWay.Add(num);
                    }

                    return true;
                }
            }
            return true;
        }
        else
        {
            Debug.Log("没有找到对应的路径");
            return false;
        }

    }

    private bool FindWays(List<int> arr)
    {
      
        List<int> array = new List<int>();
        bool arrived = false;
        for (int i = 0; i < arr.Count; i++)
        {
            int a = arr[i];
            int left = a - 1;
            int right = a + 1;
            int forward = a + 10;
            int back = a - 10; 


            //看看数组里面那个有 ，然后添加后续路径
            for (int j = allTheWays.Count - 1; j>=0; j--)
            {

                ///
                //Debug.Log(allTheWays.Count);
                //Debug.Log("test start!");
                //foreach (int fff in allTheWays[j])
                //{
                //    Debug.Log(fff);
                //}
                //Debug.Log("test over");

                List<int> tempList = new List<int>();
                tempList = allTheWays[j];
                if (tempList.Contains(a))
                {

                    //是否进行了添加
                    bool isAdd = false;


                    //这个数组里面有这个数 是他的分支
                    if (left == Define.OUT_DOOR_POS)
                    {
                        array.Add(left);
                        tempList.Add(Define.OUT_DOOR_POS);
                        arrived = true;
                        allTheWays.RemoveAt(j);
                        allTheWays.Add(tempList);
                        break;
                    }

                    if (right == Define.OUT_DOOR_POS)
                    {
                        array.Add(right);
                        tempList.Add(Define.OUT_DOOR_POS);
                        arrived = true;
                        allTheWays.RemoveAt(j);
                        allTheWays.Add(tempList);
                        break;
                    }

                    if (forward == Define.OUT_DOOR_POS)
                    {
                        array.Add(forward);
                        tempList.Add(Define.OUT_DOOR_POS);
                        arrived = true;
                        allTheWays.RemoveAt(j);
                        allTheWays.Add(tempList);
                        break;
                    }

                    if (back == Define.OUT_DOOR_POS)
                    {
                        array.Add(back);
                        tempList.Add(Define.OUT_DOOR_POS);
                        arrived = true;
                        allTheWays.RemoveAt(j);
                        allTheWays.Add(tempList);
                        break;
                    }

                    if (floorInterable.ContainsKey(left))
                    {
                        array.Add(left);
                        List<int> l = new List<int>();
                       for(int g = 0; g < tempList.Count; g++)
                        {
                            l.Add(tempList[g]);
                        }
                        l.Add(left);
                        allTheWays.Add(l);
                        floorInterable.Remove(left);
                        isAdd = true;
                    }

                    if (floorInterable.ContainsKey(right))
                    {
                        array.Add(right);
                        List<int> l = new List<int>();
                        for (int g = 0; g < tempList.Count; g++)
                        {
                            l.Add(tempList[g]);
                        }
                        l.Add(right);
                        allTheWays.Add(l);
                        floorInterable.Remove(right);
                        isAdd = true;
                    }

                    if (floorInterable.ContainsKey(forward))
                    {
                        array.Add(forward);
                        List<int> l = new List<int>();
                        for (int g = 0; g < tempList.Count; g++)
                        {
                            l.Add(tempList[g]);
                        }
                        l.Add(forward);
                        allTheWays.Add(l);
                        floorInterable.Remove(forward);
                        isAdd = true;
                    }

                    if (floorInterable.ContainsKey(back))
                    {
                        array.Add(back);
                        List<int> l = new List<int>();
                        for (int g = 0; g < tempList.Count; g++)
                        {
                            l.Add(tempList[g]);
                        }
                        l.Add(back);
                        allTheWays.Add(l);
                        floorInterable.Remove(back);
                        isAdd = true;
                    }

                    if (isAdd)
                    {
                        allTheWays.RemoveAt(j);
                    }
                   
                  

                }
            }


        }

        if (array.Count == 1)
        {
            // Debug.Log("这个地方不能建造货架！" + array[0]);
            noBuildArea.Add(array[0]);
        }


        if (array.Count == 0)
        {
            return false;
        }

        if (arrived)
        {
            return true;
        }


     
        return FindWays(array);
    }


    public bool CheckBuild()
    {
        List<int> bornList = new List<int>();
        bornList.Add(Define.BORN_POS);

        noBuildArea.Clear();
        FetchActiveFloor();
        Debug.Log(floorInterable.Count);
        return CheckBuildFloor(bornList);
    }


    //检测不能建造的地面
    private bool CheckBuildFloor(List<int> arr)
    {
        List<int> array = new List<int>();
        bool arrived = false;
        for(int i = 0; i<arr.Count; i++)
        {
            int a = arr[i];
            int left = a - 1;
            int right = a + 1;
            int forward = a + 10;
            int back = a - 10;

            if(left == Define.OUT_DOOR_POS)
            {
                array.Add(left);
                arrived = true;
            }

            if (right == Define.OUT_DOOR_POS)
            {
                array.Add(right);
                arrived = true;
            }

            if (forward == Define.OUT_DOOR_POS)
            {
                array.Add(forward);
                arrived = true;
            }

            if (back == Define.OUT_DOOR_POS)
            {
                array.Add(back);
                arrived = true;
            }

            if (floorInterable.ContainsKey(left)) {
                array.Add(left);
                floorInterable.Remove(left);
            }

            if (floorInterable.ContainsKey(right))
            {
                array.Add(right);
                floorInterable.Remove(right);
            }

            if (floorInterable.ContainsKey(forward))
            {
                array.Add(forward);
                floorInterable.Remove(forward);
            }

            if (floorInterable.ContainsKey(back))
            {
                array.Add(back);
                floorInterable.Remove(back);
            }



        }
        if(array.Count > 1)
        {
            array.Union(array).ToList<int>();
        }

        if (array.Count == 1)
        {
            Debug.Log("这个地方不能建造货架！" + array[0]);
            if ( !noBuildArea.Contains(array[0]))
            {
                noBuildArea.Add(array[0]);
            }
          
            //if (allFloor.ContainsKey(array[0]))
            //{
            //    allFloor[array[0]].GetComponent<SpriteRenderer>().color = Color.red;
            //}
        }


        if (array.Count == 0)
        {
            return false;
        }

        if (arrived)
        {
            return true;
        }


        //Debug.Log("--------------------------");
        //foreach (int a in array) {
            
        //    Debug.Log("===================" + a);
          
        //}

        //Debug.Log("--------------------------");
        return CheckBuildFloor(array);
    }

    ///下面都是要删掉的 测试用
    ///
    /// <summary>
    /// 生成顾客
    /// </summary>
    /// 
   
    GameObject BornCustomer()
    {

      
        GameObject _BornPlace = GameObject.Find("CustomerBornPlace");
        GameObject CustomerCubeObj = (GameObject)Instantiate(Resources.Load("Test"), _BornPlace.transform.position, Quaternion.Euler(0, 0, 0));

        return CustomerCubeObj;

    }


}
