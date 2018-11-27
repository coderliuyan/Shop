using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigDefine;
public class HJFloow : MonoBehaviour {

    public bool isMove = true;
    RaycastHit hit = new RaycastHit();
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (isMove)
        {
#if UNITY_EDITOR
              Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
#endif


#if !UNITY_EDITOR 
              Vector2 pos = Vector2.zero;
            if (Input.touchCount <= 0)
                return;
            if (Input.touchCount == 1) //单点触碰移动摄像机
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                    pos = Input.touches[0].position;   //记录手指刚触碰的位置
                if (Input.touches[0].phase == TouchPhase.Moved) //手指在屏幕上移动
                {
                    pos = Input.touches[0].deltaPosition;
                    transform.Translate(new Vector3( Input.touches[0].deltaPosition.x * Time.deltaTime, Input.touches[0].deltaPosition.y * Time.deltaTime, 0));

                }
            }
            Ray ray = (Camera.main.ScreenPointToRay(pos));
#endif

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

                        HuoJiaController hjc = transform.GetComponent<HuoJiaController>();
                        int huojiaId =  hjc.huojiaID;
                        int coins = DataManager.Instance.huojiaXml.GetInt(huojiaId,"coin");
                        if(Player.GoldNum < coins)
                        {
                            DataManager.Instance.msgText = "钱不够，不能建造！";
                            UIManager.Instance.ShowMessagePanel();
                            return;
                        }

                        Debug.Log(hit.transform.parent.parent.gameObject.name);
                        int index = GetObjName(hit.transform.parent.parent.gameObject);
                        if (FloorManager.Instance.floorInterable.ContainsKey(index))
                        {
                            isMove = false;
                            this.gameObject.AddComponent<SphereCollider>().radius = 0.5f;
                            this.gameObject.tag = "huojia";

                            Debug.Log(Player.GoldNum);
                            Player.GoldNum -= coins;
                            Debug.Log(Player.GoldNum);
                            SelectPanel.selectManager.moneyLabel.text = Player.GoldNum.ToString();

                            //货架位置和物体保存一下
                            if (Player.huojiaObjs.ContainsValue(gameObject)) {
                                 foreach(int key in Player.huojiaObjs.Keys)
                                {
                                    if(Player.huojiaObjs[key] == gameObject)
                                    {
                                        Player.huojiaObjs.Remove(key);
                                    }
                                }
                            }
                            Player.huojiaObjs.Add(GetObjName(gameObject.transform.parent.gameObject) , gameObject);

                            if (Player.huojiaType.ContainsKey(index))
                            {
                                Player.huojiaType.Remove(index);
                            }
                            Player.huojiaType.Add(index,hjc.huojiaID);
                            if (Player.huojiaDiretion.ContainsKey(index))
                            {
                                Player.huojiaDiretion.Remove(index);
                            }
                            Player.huojiaDiretion.Add(index,hjc.huojiaDirection);
                            if (Player.huojiaLevel.ContainsKey(index))
                            {
                                Player.huojiaLevel.Remove(index);
                            }
                            Player.huojiaLevel.Add(index, hjc.huojiaLevel);
                           

                            Player.SavePlayerData(Define.GOLD);
                            Player.SavePlayerData(Define.HUOJIA_TYPE);
                            Player.SavePlayerData(Define.HUO_JIA_DIRECTION);
                            Player.SavePlayerData(Define.HUO_JIA_LEVEL);



                        }
                        else
                        {
                            Debug.Log("这个地方不能建造！");
                            DataManager.Instance.msgText = "检查位置否正确  建造失败";
                            UIManager.Instance.ShowMessagePanel();
                            Destroy(gameObject);
                            return;
                        }


                        if (!FloorManager.Instance.FetchActiveWay())
                        {
                            Debug.Log("这个地方不能建造！");
                            DataManager.Instance.msgText = "检查位置否正确  建造失败";
                            UIManager.Instance.ShowMessagePanel();
                            Destroy(gameObject);
                            return;
                        }

                    }
                    else
                    {
                        Debug.Log("不能拜访货架， 销毁货架。");
                        DataManager.Instance.msgText = "检查位置否正确  建造失败";
                        UIManager.Instance.ShowMessagePanel();
                        Destroy(gameObject);
                    }


                    Debug.Log("点击的物体是" + hit.transform.name + "tag ==" + hit.transform.tag);
                    if(hit.transform.tag == "huojia")
                    {
                        Debug.Log("场景中的货架被点击了。");
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
