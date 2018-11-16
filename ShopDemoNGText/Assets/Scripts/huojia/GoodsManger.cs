using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GoodsManger:MonoBehaviour
    {
        public static GoodsManger instance;
        private GoodsModel goodsmodel;
        private GoodsViews goodsviews;
        TableValue GoodsData;
        private HuojiaManger _huojiaManger;
        bool _isLevel;
        //private HuojiaView _huojiaView;
        int _Level;
        UILabel _jiesuan;//结算合计金额Text。
        int _intPrice;//金额
        GameObject _UIParent;
        void Awake()
        {
            instance = this;
            goodsmodel = GetComponent<GoodsModel>();//绑定model层
            //goodsviews = GameObject.Find("Buyhuowu").GetComponent<GoodsViews>();//绑定Goodsview层
           _huojiaManger=GetComponent<HuojiaManger>();
         // _huojiaView = GameObject.Find("huo_huazhaungpinButton").GetComponent<HuojiaView>();
          _UIParent = GameObject.Find("ShuiguoLei");
        }
        void Start()
        {
            //goodsmodel.updateDataGoodsEvent += goodsviews.UpdateGoodsViewData;//添加方法
            Object GoodsObj = Resources.Load("Xml/DataType_Goods");
            XmlHelper.Instance.LoadFile("DataType_Goods", GoodsObj);
            GoodsData = XmlHelper.Instance.ReadFile("DataType_Goods");
            Debug.Log(GoodsObj+"11111"+GoodsData);
            _jiesuan = GameObject.Find("BuyPanel/Gouwuche").transform.Find("heji_lable/heji/ajinbiNum").GetComponent<UILabel>();
            //货物类100开头为水果。
           // goodsmodel.GoodsId =1001;
            //goodsmodel.NowKuCun = 0;
           // GoodsInit();          
        }
        void Update()
        {                   
          // GoodsInit();
           // _isLevel = _huojiaManger._isgoodsLevel;
        }
        void GoodsInit()
        {
            //goodsmodel.GoodsName = GoodsData.GetString(goodsmodel.GoodsId, "name");
            //goodsmodel.GoodsBuyMoney = GoodsData.GetInt(goodsmodel.GoodsId, "buy");
            //goodsmodel.GoodsSetMoney = GoodsData.GetInt(goodsmodel.GoodsId, "sell");
            //goodsmodel.GoodsUnlockLevel = GoodsData.GetInt(goodsmodel.GoodsId, "level");
            //goodsmodel.MaxKuCun = 5000;
        }
        //按钮触发事件，货物是否结锁和游戏等级有关。
        public void OnButtonClick()
        {
                  
        }
        public void OnBuittonClickLevel()
        {
        }
        byte fruitsNum = 0;
        /// <summary>
        /// 选择商品按钮。
        /// </summary>
        public void OnButtonChoose(int type)
        {
            Debug.Log("1111"+GoodsData);
            DestoryChild();
            foreach (LineValue item in GoodsData)
            {
                short goodsId = short.Parse(item.lineName);
                short goodsType = item.GetShort("type");
                string goodsName = item.GetString("name");
                int goodsBuy = item.GetInt("buy");
                int goodsSet = item.GetInt("sell");
                int goodsLevel = item.GetInt("level");
                if (goodsType == type)
                {
                    GameObject obj = ((GameObject)Instantiate(Resources.Load("UI/pingguo")));
                    obj.transform.SetParent(_UIParent.transform);
                    obj.transform.Find("pingguoLabel").GetComponent<UILabel>().text = goodsName;
                    obj.transform.Find("shoujia").GetComponent<UILabel>().text = goodsBuy.ToString();
                    obj.transform.Find("SetJiage").GetComponent<UILabel>().text = goodsSet.ToString();
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localScale = Vector3.one;
                    obj.name = fruitsNum.ToString();
                    fruitsNum += 1;
                    obj.GetComponent<UIDragScrollView>().scrollView =_UIParent.transform.parent.GetComponent<UIScrollView>();
                    obj.transform.Find("Button").GetComponent<BtnOpen>()._GoodsId = goodsId;
                    obj.transform.Find("Button").GetComponent<BtnOpen>()._goodsdata = GoodsData;
                }
            }
            _UIParent.GetComponent<UIGrid>().enabled = true;
            GameObject.Find("Bar").GetComponent<UIScrollBar>().value = 1f;
            Debug.Log(GameObject.Find("Bar"));
        }
        void DestoryChild()
        {
            for (int i = 0; i < _UIParent.transform.childCount; i++)
            {
                Destroy(_UIParent.transform.GetChild(i).gameObject);
            }
        }
        public void OnButtonChooseVegetables()
        {
 
        }
        /// <summary>
        /// 合计金额的方法
        /// </summary>
        public void SumPrice(int _price)
        {
         _intPrice+=_price;
         _jiesuan.text = _intPrice.ToString();
        }
        /// <summary>
        /// 删除一行后减少的金额
        /// </summary>
        /// <param name="_quitprice"></param>
        public void JianPrice(int _quitprice)
        {
            _intPrice -= _quitprice;
            _jiesuan.text = _intPrice.ToString();
        }
        /// <summary>
        /// 结算按钮
        /// </summary>
        public void BalancePrice()
        {
           
        }
    }
