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
            //List<int> a = new List<int>();
            //a.Add(bornIndex);
            //if (Test(a)) {
            //    Debug.Log("到达目的地了");
            //}
            FetchActiveWay();
         
        }

        if(Input.GetKeyDown(KeyCode.Q)) {

           StartCoroutine(MoveAnimation(BornCustomer()));
        }
    }


    IEnumerator  MoveAnimation(GameObject obj)
    {

        yield return new WaitForSeconds(0.3f);
             
    }


    //初始点 16
    int bornIndex = 16;

    //终点 
    int overIndex = 61;

    //存放所有的路径
    List<List<int>> allTheWays = new List<List<int>>();

    //寻找路径
    void FetchActiveWay()
    {
        allTheWays.Clear();
        List<int> born = new List<int>();
        born.Add(bornIndex);
        allTheWays.Add(born);
        if (FindWays(born))
        {
            Debug.Log("有正确的路径。" + allTheWays.Count);
            foreach(List<int>  t in allTheWays)
            {
                if (t.Contains(overIndex))
                {
                    Debug.Log("打印路径。");
                    foreach(int hh in t)
                    {
                        Debug.Log(hh);
                    }

                    Debug.Log("打印路径结束。");
                  

                }
            }
        }
        else
        {
            Debug.Log("没有找到对应的路径");
        }

    }

    bool FindWays(List<int> arr)
    {
      
        List<int> array = new List<int>();
        bool arrived = false;
        for (int i = 0; i < arr.Count; i++)
        {
            int a = arr[i];
            int left = a - 1;
            int right = a + 1;
            int forward = a + 10;


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
                    if (left == overIndex)
                    {
                        array.Add(left);
                        tempList.Add(overIndex);
                        arrived = true;
                        allTheWays.RemoveAt(j);
                        allTheWays.Add(tempList);
                        break;
                    }

                    if (right == overIndex)
                    {
                        array.Add(right);
                        tempList.Add(overIndex);
                        arrived = true;
                        allTheWays.RemoveAt(j);
                        allTheWays.Add(tempList);
                        break;
                    }

                    if (forward == overIndex)
                    {
                        array.Add(forward);
                        tempList.Add(overIndex);
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
                        Debug.Log("!!!!!!!!!!!!!!!!" + right);
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


                    if (isAdd)
                    {
                        allTheWays.RemoveAt(j);
                    }
                   
                  

                }
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


     
        return FindWays(array);
    }



    //检测不能建造的地面
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
   
    GameObject BornCustomer()
    {

      
        GameObject _BornPlace = GameObject.Find("CustomerBornPlace");
        GameObject CustomerCubeObj = (GameObject)Instantiate(Resources.Load("Test"), _BornPlace.transform.position, Quaternion.Euler(0, 0, 0));

        return CustomerCubeObj;

    }


}
