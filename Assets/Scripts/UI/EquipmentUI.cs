using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour {
    public static EquipmentUI _instance;
    TweenPosition twePos;
    bool isShow = false;

    private GameObject headgear;
    private GameObject armor;
    private GameObject rightHand;
    private GameObject leftHand;
    private GameObject shoe;
    private GameObject accessory;

    private PlayerStatus ps;

    public int attack = 0;
    public int def = 0;
    public int speed = 0;

    public GameObject equipmentItem;
    private void Awake()
    {
        _instance = this;
        twePos = this.GetComponent<TweenPosition>();

        headgear = transform.Find("Headgear").gameObject;
        armor = transform.Find("Armor").gameObject;
        rightHand = transform.Find("RightHand").gameObject;
        leftHand = transform.Find("LeftHand").gameObject;
        shoe = transform.Find("Shoe").gameObject;
        accessory = transform.Find("Accessory").gameObject;

        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void transformPlay()
    {
        if (isShow)
        {
            twePos.PlayReverse();
            isShow = false;

        }
        else
        {
            twePos.PlayForward();
            isShow = true;
        }
    }
    public bool Dress(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        if (info.Type != ObjectType.Equip)
        {
            return false;//穿戴不成功
        }
        if (ps.heroType == HeroType.Magican)
        {
            if (info.ApplicationType == ApplicationType.Swordman)
            {
                return false;
            }
        }
        if (ps.heroType == HeroType.Swordman)
        {
            if (info.ApplicationType == ApplicationType.Magican)
            {
                return false;
            }
        }

        GameObject parent = null;
        switch (info.DressType)
        {
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.Righthand:
                parent = rightHand;
                break;
            case DressType.Lefthand:
                parent = leftHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
            case DressType.Accessory:
                parent = accessory;
                break;
        }
        itemEquipment item = parent.GetComponentInChildren<itemEquipment>();
        if (item != null)
        {//说明已经穿戴了同样类型的装备
            Inventory._instance.GetId(item.id);//把已经穿戴的装备卸下，放回物品栏
            item.SetInfo(info);
        }
        else
        {//没有穿戴同样类型的装备
            GameObject itemGo = NGUITools.AddChild(parent, equipmentItem);
            itemGo.transform.localPosition = Vector3.zero;
            itemGo.GetComponent<itemEquipment>().SetInfo(info);
        }
        UpdateProperty();
        return true;
    }
    void UpdateProperty()
    {
        this.attack = 0;
        this.def = 0;
        this.speed = 0;

        itemEquipment headgearItem = headgear.GetComponentInChildren<itemEquipment>();
        PlusProperty(headgearItem);
        itemEquipment armorItem = armor.GetComponentInChildren<itemEquipment>();
        PlusProperty(armorItem);
        itemEquipment leftHandItem = leftHand.GetComponentInChildren<itemEquipment>();
        PlusProperty(leftHandItem);
        itemEquipment rightHandItem = rightHand.GetComponentInChildren<itemEquipment>();
        PlusProperty(rightHandItem);
        itemEquipment shoeItem = shoe.GetComponentInChildren<itemEquipment>();
        PlusProperty(shoeItem);
        itemEquipment accessoryItem = accessory.GetComponentInChildren<itemEquipment>();
        PlusProperty(accessoryItem);
    }
    void PlusProperty(itemEquipment item)
    {
        if (item != null)
        {
            ObjectInfo equipInfo = ObjectsInfo._instance.GetObjectInfoById(item.id);
            this.attack += equipInfo.Attack;
            this.def += equipInfo.Def;
            this.speed += equipInfo.Speed;
        }
    }
    public void TakeOff(int id, GameObject go)
    {
        Inventory._instance.GetId(id);
        GameObject.Destroy(go);
        UpdateProperty();
    }
}
