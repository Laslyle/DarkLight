using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInfo : MonoBehaviour {

    public static ObjectsInfo _instance;

    private Dictionary<int, ObjectInfo> objectInfoDic = new Dictionary<int, ObjectInfo>();

    public TextAsset objectsInfoListText;

    private void Awake()
    {
        _instance = this;
        readInfo();
        //print(objectInfoDic.Keys.Count);
        foreach (int key in objectInfoDic.Keys)
        {
            System.Console.WriteLine("key: {0}  value{1}", key, objectInfoDic[key]); 
        }
    }

    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectInfoDic.TryGetValue(id,out info);//从字典里根据ID得到物品对象的值
        return info;
    }

    void readInfo()
    {
        string text = objectsInfoListText.text;
        string[] strArr = text.Split('\n');
        foreach (var item in strArr)
        {
            string[] proArr = item.Split(',');
            ObjectInfo objectinfo = new ObjectInfo();
            objectinfo.Id = int.Parse(proArr[0]);
            objectinfo.Name = proArr[1];
            objectinfo.Icon_name = proArr[2];
            string str_type = proArr[3];
            ObjectType type = ObjectType.Drug;
            switch (str_type)
            {
                case "Drug":
                    type = ObjectType.Drug;
                    break;
                case "Equip":
                    type = ObjectType.Equip;
                    break;
                case "Mat":
                    type = ObjectType.Mat;
                    break;
              
            }
            if (type == ObjectType.Drug)
            {
                objectinfo.Hp = int.Parse(proArr[4]);
                objectinfo.Mp = int.Parse(proArr[5]);
                objectinfo.Price_sell = int.Parse(proArr[6]);
                objectinfo.Price_buy = int.Parse(proArr[7]);
            }
            objectInfoDic.Add(objectinfo.Id, objectinfo);
        }

    }

  
}


public enum ObjectType
{
    Drug,
    Equip,
    Mat
}

public class ObjectInfo
{
    
    private int id;
    private string name;
    private string icon_name;
    private ObjectType type;
    private int hp;
    private int mp;
    private int price_sell;
    private int price_buy;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Icon_name
    {
        get
        {
            return icon_name;
        }

        set
        {
            icon_name = value;
        }
    }

    public ObjectType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public int Hp
    {
        get
        {
            return hp;
        }

        set
        {
            hp = value;
        }
    }

    public int Mp
    {
        get
        {
            return mp;
        }

        set
        {
            mp = value;
        }
    }

    public int Price_sell
    {
        get
        {
            return price_sell;
        }

        set
        {
            price_sell = value;
        }
    }

    public int Price_buy
    {
        get
        {
            return price_buy;
        }

        set
        {
            price_buy = value;
        }
    }
}