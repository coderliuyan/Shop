using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour {


    private string huojiaPath = @"UIHuoJia/Goods/fruit_huojia";
    private string fruitPath = @"UIHuoJia/Goods/";

    private bool isClickFunctionMenu = false;


    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }



    public TableValue _PlayerXml;
    public TableValue _ShopXml;
    public TableValue _HuojiaXml;
    public TableValue _CustomerXml;
    public TableValue GoodsData;
    public TableValue shopType;



    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
    }
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
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    int shopid = 2001;
        //   int str =   _ShopXml.GetInt(shopid,"Exp");
        //    Debug.Log(str);
        //}

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

                    isClickFunctionMenu = true;
                    return;
                }
                else
                {
                    isClickFunctionMenu = false;
                }

                if(hit.transform.tag == "upLevel")
                {
                    //升级货架
                    Debug.Log("升级货架");
                    UpdataHuoJia(hit.transform.parent.parent.gameObject);
                    isClickFunctionMenu = true;
                    return;
                }
                else
                {
                    isClickFunctionMenu = false ;
                }

                if(hit.transform.tag == "buhuo")
                {
                    //补货
                    Debug.Log("补货");
                    GameObject objP = hit.transform.parent.parent.gameObject;
                    BuhuoWork(objP);
                    isClickFunctionMenu = true;
                    return;
                }
                else
                {
                    isClickFunctionMenu = false;
                }


                if (hit.transform.tag == "turn")
                {
                    //旋转货架
                    Debug.Log("旋转货架");

                    //获取父物体的图片 进行旋转 
                    Transform pp = hit.transform.parent.parent;
                    TurnHuoJia(pp.gameObject);
                    isClickFunctionMenu = true;
                    return;
                }
                else
                {
                    isClickFunctionMenu = false;
                }


            }

            if (!isClickFunctionMenu)
            {
                //如果每点击功能面板 就把货架上面的UI关了 
                foreach (GameObject item in Player.huojiaObjs.Values)
                {
                    item.transform.Find("FunctionMenu").gameObject.SetActive(false);
                }
            }


        }
	}

    //升级货架
    void UpdataHuoJia(GameObject obj)
    {
        int pos = GetObjName(obj.transform.parent.gameObject);
        HuoJiaController hjc = obj.GetComponent<HuoJiaController>();
        int coins = DataManager.Instance.huojiaXml.GetInt(hjc.huojiaID + hjc.huojiaLevel, "coin");
        Debug.Log("coins = " +  coins);
        if(Player.GoldNum >= coins)
        {
#pragma 这个地方还要加上 最高级数判断 

            //可以升级货架
            Player.GoldNum -= coins;
            Player.SavePlayerData(ConfigDefine.Define.GOLD);
            hjc.huojiaLevel++;

            if (Player.huojiaLevel.ContainsKey(pos))
            {
                Player.huojiaLevel.Remove(pos);
            }
            Player.huojiaLevel.Add(pos, hjc.huojiaLevel);
            Player.SavePlayerData(ConfigDefine.Define.HUO_JIA_LEVEL);

            SelectPanel.selectManager.moneyLabel.text = Player.GoldNum.ToString();
            DataManager.Instance.msgText = "货架成功升级到" + hjc.huojiaLevel + "级！";
            UIManager.Instance.ShowMessagePanel();
        }
        else
        {
            DataManager.Instance.msgText = "你的钱不够！";
            UIManager.Instance.ShowMessagePanel();
        }

        

    }


    //补货
    private void BuhuoWork(GameObject obj)
    {
        //先获取货架信息，货架类型， 货架等级 ，可以摆放的物品，再匹配拥有的物品 如果有点击后进行拜访 如果无 进行提示
        GameObject goodsObj = obj.transform.Find("goods").gameObject;

        HuoJiaController hjc = obj.GetComponent<HuoJiaController>();

        string huojiaType = DataManager.Instance.huojiaXml.GetString(hjc.huojiaID,"name");

        string spriteName= "";
        int goodsNumber = 0;
        int tempKey = 0;
        switch (huojiaType)
        {
            case ("水果货架"):
                {
                    if(Player.ShopStock.ContainsKey(hjc.goodsType))
                    {
                        tempKey = hjc.goodsType;
                        goodsNumber = Player.ShopStock[hjc.goodsType];
                        break;
                    }
                    //没有同样的货的时候 拿最高级的货出来 
                    foreach(int key in Player.ShopStock.Keys)
                    {
                        if(DataManager.Instance.goodsData.GetString(key,"type") == "水果类")
                        {
                            spriteName = DataManager.Instance.goodsData.GetString(key,"name");
                            goodsNumber = Player.ShopStock[key];
                            tempKey = key;
                        }
                    }
                }
                break;
            case ("饮料货架"):
                {

                }
                break;
            case ("日用品货架"):
                {

                }
                break;
            case ("蔬菜货架"):
                {

                }
                break;
            case ("体育用品货架"):
                {

                }
                break;
            case ("奢侈品货架"):
                {

                }
                break;
            case ("电子产品货架"):
                {

                }
                break;
        }

        int huojiaId = hjc.huojiaLevel + hjc.huojiaID - 1;
        int saleTimes = DataManager.Instance.huojiaXml.GetInt(huojiaId, "sales");
        int maxGoodsNumber = hjc.huojiaLevel * 100;
        if(hjc.goodsNumber >= maxGoodsNumber)
        {
            DataManager.Instance.msgText = "不缺货呀，缺心眼吧。";
            UIManager.Instance.ShowMessagePanel();
            return;
        }

        if(hjc.goodsType != tempKey && hjc.goodsNumber != 0)
        {
            DataManager.Instance.msgText = "想卖更高级的货？先买完货架上的物品";
            UIManager.Instance.ShowMessagePanel();
            return;
        }


        int reduceGoodsNumber = maxGoodsNumber - hjc.goodsNumber;

        if (spriteName != "" && goodsNumber != 0)
        {
            string path = fruitPath + spriteName;
            goodsObj.GetComponent<SpriteRenderer>().sprite = LoadSpriteWithPath(path);
            goodsObj.GetComponent<SpriteRenderer>().sortingOrder = obj.GetComponent<SpriteRenderer>().sortingOrder + 10;

            if(goodsNumber >= reduceGoodsNumber)
            {
                goodsNumber -= reduceGoodsNumber;
                hjc.goodsNumber += reduceGoodsNumber;
                Player.ShopStock[tempKey] = goodsNumber;
            }
            else
            {
                saleTimes = saleTimes * goodsNumber / reduceGoodsNumber;
                hjc.goodsNumber += goodsNumber;
                goodsNumber = 0;
                Player.ShopStock.Remove(tempKey);
            }

            Player.SavePlayerData(ConfigDefine.Define.SHOP_STOCK);
            SelectPanel.selectManager.UpdateCangkuUI();
            hjc.saleTimes = saleTimes;
            hjc.goodsType = tempKey;
        }
        else
        {
            DataManager.Instance.msgText = @"没有货哟，去进点货吧。";
            UIManager.Instance.ShowMessagePanel();
        }

      

        //点击补货之后floor manager要计算出那些地方可以和顾客交互,交互的货架的物体也要存起来,当然还要存货架类型,对应类型才会停下来交互
        //int huojiaPos = GetObjName(obj.transform.parent.gameObject);
        //Debug.Log(huojiaPos);
        //int huojiaID = goodsObj.GetComponent<Goods>().huojiaID;
        //Debug.Log(huojiaID);

      //  HuoJiaController hjc = obj.GetComponent<HuoJiaController>();
      


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

        obj.GetComponent<HuoJiaController>().huojiaDirection = imgNumber;

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

    //获取物体名字中包含的数字
    public int GetObjName(GameObject _obj)
    {
        int numInt1 = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(_obj.transform.name, @"[^0-9]+", ""));
        return numInt1;
    }
}
