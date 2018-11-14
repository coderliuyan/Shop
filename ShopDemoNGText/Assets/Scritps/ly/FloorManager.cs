using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //存放空地板的字典，序号和物体对象
    public Dictionary<int, GameObject> floorInterable;

    //所有地方的父物体
    public Transform[] floors;

    //可交互的所有的地板 
    //---------------------------还需要刨出去没有解锁的地板--------------------------！！！！！！！！！！！！！
    public void FetchFloor()
    {
        floorInterable.Clear();
        foreach(Transform obj in floors)
        {
            if (obj.gameObject.activeSelf)
            {
                for(int i = 0; i< obj.childCount; i++)
                {
                    GameObject go = obj.GetChild(i).gameObject;
                    int index = GetObjName(go);
                    floorInterable.Add(index,go);
                }
            }
        }

        floorInterable.Remove(bornIndex);
        floorInterable.Remove(overIndex);
    }


    int GetObjName(GameObject _obj)
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
        InitComponent();
    }

    void InitComponent()
    {
        floorInterable = new Dictionary<int, GameObject>();
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.A)) {
            List<int> a = new List<int>();
            a.Add(bornIndex);
            if (Test(a)) {
                Debug.Log("到达目的地了");
            }
        }

        if(Input.GetKeyDown(KeyCode.Q)) {

            BornCustomer();
        }
    }

    //初始点 16
    int bornIndex = 16;

    //终点 
    int overIndex = 61;

    //寻找路径
    bool FetchActiveWay()
    {
        

        Dictionary<int, GameObject> tempFloor = floorInterable;

        if(tempFloor.Count < 0 || !tempFloor.ContainsKey(bornIndex) || !tempFloor.ContainsKey(overIndex))
        {
            return false;
        }
        tempFloor.Remove(bornIndex);
        tempFloor.Remove(overIndex);

        int left = bornIndex - 1;
        int right = bornIndex + 1;
        int forword = bornIndex + 10;

        if (tempFloor.ContainsKey(left))
        {
            tempFloor.Remove(left);

        }





        return false;
    }


    bool Test(List<int> arr)
    {
        List<int> array = new List<int>();
        bool arrived = false;
        for(int i = 0; i<arr.Count; i++)
        {
            int a = arr[i];
            int left = a - 1;
            int right = a + 1;
            int forward = a + 10;

            if(left == overIndex)
            {
                array.Add(left);
                arrived = true;
            }

            if (right == overIndex)
            {
                array.Add(right);
                arrived = true;
            }

            if (forward == overIndex)
            {
                array.Add(forward);
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

           


        }

        if (array.Count == 1)
        {
            Debug.Log("这个地方不能建造货架！" + array[0]);
        }


        if (array.Count == 0)
        {
            return false;
        }

        if (arrived)
        {
            return true;
        }


        Debug.Log("--------------------------");
        foreach (int a in array) {
            
            Debug.Log("===================" + a);
          
        }

        Debug.Log("--------------------------");
        return Test(array);
    }

    ///下面都是要删掉的 测试用
    ///
    /// <summary>
    /// 生成顾客
    /// </summary>
    /// 
   
    void BornCustomer()
    {

      
        GameObject _BornPlace = GameObject.Find("CustomerBornPlace");
        GameObject CustomerCubeObj = (GameObject)Instantiate(Resources.Load("Test"), _BornPlace.transform.position, Quaternion.Euler(0, 0, 0));
      

    }


}
