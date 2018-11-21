using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour {


    private string huojiaPath = @"UIHuoJia/Goods/fruit_huojia";
    private string fruitPath = @"UIHuoJia/Goods/fruit";




    TableValue _PlayerXml;
    TableValue _ShopXml;
    TableValue _HuojiaXml;
    TableValue _CustomerXml;
    TableValue GoodsData;
    TableValue shopType;
    // Use this for initialization
    void Start () {
        _PlayerXml = ReadExpXml("DataType_playerLevel");
        _ShopXml = ReadExpXml("DataType_shopLevel");
        _HuojiaXml = ReadExpXml("DataType_rackLevel");
        _CustomerXml = ReadExpXml("DataType_cusLevel");
        GoodsData = ReadExpXml("DataType_Goods");
        shopType = ReadExpXml("ShopType");
    }

    TableValue ReadExpXml(string _DataName)
    {
        Object ShopExpObj = Resources.Load("Xml/" + _DataName);
        XmlHelper.Instance.LoadFile(_DataName, ShopExpObj);
        TableValue ShopExpData = XmlHelper.Instance.ReadFile(_DataName);
        return ShopExpData;
    }

    // Update is called once per frame
    void Update () {
        //test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int shopid = 2001;
           int str =   _ShopXml.GetInt(shopid,"Exp");
            Debug.Log(str);
        }

        //test over

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));
            RaycastHit hit;
            if ((Physics.Raycast(ray, out hit)))
            {
                Debug.DrawLine(ray.origin, hit.point);

                Debug.Log("点击的物体是" + hit.transform.name + "tag ==" + hit.transform.tag);
                if (hit.transform.tag == "huojia" )
                {
                    Debug.Log("场景中的货架被点击了。");

                    //获取功能面板的所有子物体 添加层级
                    if (!hit.transform.GetChild(0).gameObject.activeSelf)
                    {
                        Transform functionMenu = hit.transform.Find("FunctionMenu");

                        int orderLayerNumber = hit.transform.GetComponent<SpriteRenderer>().sortingOrder;
                        for (int i = 0; i < functionMenu.childCount; i++)
                        {
                            Transform subTran = functionMenu.GetChild(i);
                            subTran.gameObject.GetComponent<SpriteRenderer>().sortingOrder = orderLayerNumber+10 + i;
                        }
                    }

                    

                    //展开功能面板
                    hit.transform.GetChild(0).gameObject.SetActive(!hit.transform.GetChild(0).gameObject.activeSelf);



                }

                if(hit.transform.tag == "upLevel")
                {
                    //升级货架
                    Debug.Log("升级货架");
                }

                if(hit.transform.tag == "buhuo")
                {
                    //补货
                    Debug.Log("补货");
                    GameObject objP = hit.transform.parent.parent.gameObject;
                    BuhuoWork(objP);


                }

                if(hit.transform.tag == "turn")
                {
                    //旋转货架
                    Debug.Log("旋转货架");

                    //获取父物体的图片 进行旋转 
                    Transform pp = hit.transform.parent.parent;
                    TurnHuoJia(pp.gameObject);
                   

                }

            }
        }
	}

    

    private void BuhuoWork(GameObject obj)
    {
        //先获取货架信息，货架类型， 货架等级 ，可以拜访的物品，再匹配拥有的物品 如果有点击后进行拜访 如果无 进行提示
        GameObject goodsObj = obj.transform.Find("goods").gameObject;

        int playerLevel = 1;
        string path = fruitPath + playerLevel;
        goodsObj.GetComponent<SpriteRenderer>().sprite = LoadSpriteWithPath(path);
        goodsObj.GetComponent<SpriteRenderer>().sortingOrder = obj.GetComponent<SpriteRenderer>().sortingOrder + 10;

    }


    /// <summary>
    /// 更换货架的图片 旋转货架  
    /// </summary>
    /// <param name="obj">货架的图片挂在的父物体</param>
    private void TurnHuoJia(GameObject obj)
    {
        int imgNumber = GetIndexWithString(obj.GetComponent<SpriteRenderer>().sprite.name);
        imgNumber++;
        if(imgNumber > 4)
        {
            imgNumber = 1;
        }

        obj.gameObject.GetComponent<SpriteRenderer>().sprite = LoadSpriteWithPath(huojiaPath+imgNumber);
    }

    /// <summary>
    /// 加载sprite 的方法
    /// </summary>
    /// <param name="path"> resource 下面的路径</param>
    /// <returns></returns>
    private Sprite LoadSpriteWithPath(string path)
    {
        Sprite sp = new Sprite();
        sp = Resources.Load<Sprite>(path);
        return sp;
    }


    public int GetIndexWithString(string str)
    {
        double d = 0;
        Match m = Regex.Match(str, "\\d+(\\.\\d+){0,1}");
        double.TryParse(m.Groups[0].ToString(), out d);
        int result = int.Parse (d.ToString());
        return result;
    }
}
