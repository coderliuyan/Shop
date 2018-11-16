using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class HuojiaManger:MonoBehaviour
    {
        public static HuojiaManger instance;
        private HuojiaModel model;
        private HuojiaView view;
        TableValue huojiaData;
        public bool  _isgoodsLevel;
        int _huojiaId;
        string _huojiaName;
        int _huojiaLevel;
        int _huojiaMoney;
        int _huojiaPower;
        int _huojiaScale;
        
        void Awake()
        {
            instance = this;
            model = GetComponent<HuojiaModel>();//绑定model
            view = GameObject.Find("huo_huazhaungpinButton").GetComponent<HuojiaView>();//绑定View.
        }
        void Start()
        {
          // model.updateDataEvent += view.UpdateViewData;
            //加载Xml货架表
            Object huojiaObj = Resources.Load("Xml/huojiaType");
            XmlHelper.Instance.LoadFile("huojiaType", huojiaObj);
            huojiaData = XmlHelper.Instance.ReadFile("huojiaType");          
            //货架Id为1000开头的为水果货架.                  
            model.HuojiaId = 10001;
        }
        void Update()
        {
            ModelInit();
        }
        void ModelInit()
            //附上初始值
        {
            model.HuojiaName= huojiaData.GetString(model.HuojiaId, "name");
            model.HuojiaLevel = huojiaData.GetInt(model.HuojiaId, "Level");
            model.HuojiaMoney = huojiaData.GetInt(model.HuojiaId, "money");
            model.HuojiaPower = huojiaData.GetInt(model.HuojiaId, "power");
            model.HuojiaScale = huojiaData.GetInt(model.HuojiaId, "scaleNum");
        }      
        //按钮的触发事件
        public void OnButtonClickLevel()
        {          
            //Controller通知model状态发生改变
            model.HuojiaId = model.HuojiaId+1;
            _isgoodsLevel = !_isgoodsLevel;    
        }      
        /// <summary>
        /// 进货品按钮触发的事件
        /// </summary>
        /// id为1000开头的货架只能放id为100开头的货品。即为水果货架上放水果。
        public void OnButtonClickStock()
        {

        }
    }

