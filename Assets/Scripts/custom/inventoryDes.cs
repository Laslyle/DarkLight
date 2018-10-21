using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryDes : MonoBehaviour {
    public static inventoryDes _instance;
    UILabel labelDes;
    private void Awake()
    {
        this.gameObject.SetActive(false);
          _instance= this;
        labelDes = this.GetComponentInChildren<UILabel>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Show(int id)
    {
        this.gameObject.SetActive(true);

        transform.position=UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        string des = "";
        switch (info.Type)
        {
            case ObjectType.Drug:
             des=GetDrugDes(info);
                break;
        }
        labelDes.text = des;
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);

    }
    string GetDrugDes(ObjectInfo info)
    {
        string str = "";
        str += "名称:" + info.Name+"\n";
        str += "+HP:" + info.Hp + "\n";
        str += "+MP:" + info.Mp + "\n";
        str += "出售价:" + info.Price_sell + "\n";
        str += "购买价:" + info.Price_buy + "\n";

        return str;
    }
}
