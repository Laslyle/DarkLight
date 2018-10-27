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
            case ObjectType.Equip:
             des = GetEquipDes(info);
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
    string GetEquipDes(ObjectInfo info)
    {
        string str = "";
        str += "名称:" + info.Name + "\n";
        switch (info.DressType)
        {
            case DressType.Headgear:
                str += "穿戴类型:头盔\n";
                break;
            case DressType.Armor:
                str += "穿戴类型:盔甲\n";
                break;
            case DressType.Lefthand:
                str += "穿戴类型:左手\n";
                break;
            case DressType.Righthand:
                str += "穿戴类型:右手\n";
                break;
            case DressType.Shoe:
                str += "穿戴类型:鞋\n";
                break;
            case DressType.Accessory:
                str += "穿戴类型:饰品\n";
                break;
        }
        switch (info.ApplicationType)
        {
            case ApplicationType.Swordman:
                str += "适用类型:剑士\n";

                break;
            case ApplicationType.Magican:
                str += "适用类型:魔法师\n";

                break;
            case ApplicationType.Common:
                str += "适用类型:通用\n";

                break;
            
        }

        str += "伤害值:" + info.Attack + "\n";
        str += "防御值:" + info.Def + "\n";

        str += "速度值:" + info.Speed + "\n";

        str += "出售价:" + info.Price_sell + "\n";
        str += "购买价:" + info.Price_buy + "\n";

        return str;
    }
}
