using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HJFloow : MonoBehaviour {

    private bool isMove = true;
    RaycastHit hit = new RaycastHit();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isMove)
        {
            Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
           
            if ((Physics.Raycast(ray, out hit)))
            {
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.transform.tag == "Floor")
                {
                    gameObject.transform.parent = hit.transform.parent.parent;
                    transform.localPosition = new Vector3(0, 0.3f, 0);
                    transform.localRotation = Quaternion.identity;
                    transform.GetComponent<SpriteRenderer>().sortingOrder = transform.parent.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                }


                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("count ==== "+ hit.transform.parent.parent.childCount);
                    if (hit.transform.tag == "Floor" && hit.transform.parent.parent.childCount <= 2)
                    {



                        Debug.Log(hit.transform.parent.parent.gameObject.name);
                        int index = GetObjName(hit.transform.parent.parent.gameObject);
                        if (FloorManager.Instance.floorInterable.ContainsKey(index))
                        {
                            isMove = false;
                        }


                        if (!FloorManager.Instance.FetchActiveWay())
                        {
                            Debug.Log("这个地方不能建造！");
                            Destroy(gameObject);
                            return;
                        }

                        //foreach (GameObject obj in FloorManager.Instance.floorInterable.Values)
                        //{
                        //    obj.GetComponent<SpriteRenderer>().color = Color.white;
                        //}

                    }
                    else
                    {
                        Debug.Log("不能拜访货架， 销毁货架。");
                        Destroy(gameObject);
                    }


                }
        }
       

       
        }
    }

    //获取物体名字中包含的数字
    int GetObjName(GameObject _obj)
    {
        int numInt1 = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(_obj.transform.name, @"[^0-9]+", ""));
        return numInt1;
    }

}
