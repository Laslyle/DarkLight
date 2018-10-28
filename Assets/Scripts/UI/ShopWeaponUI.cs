using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeaponUI : MonoBehaviour {
    public static ShopWeaponUI _instance;
    TweenPosition twe;
    bool isShow = false;
    public int[] weaponArr;
    public GameObject itemWeapon;
    public GameObject numberDialog;
    public UIInput numberInput;
    private int buyId;

    public UIGrid gird;
    private void Awake()
    {
        _instance = this;
        numberDialog.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        twe = this.GetComponent<TweenPosition>();
        initShopWeapon();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
  public  void transfromPlay()
    {
        if (isShow)
        {
            twe.PlayReverse();
            isShow = false;
        }
        else
        {
            twe.PlayForward();
            isShow = true;

        }
    }

    void initShopWeapon()
    {
        foreach (int id in weaponArr)
        {
            GameObject itemGo = NGUITools.AddChild(gird.gameObject, itemWeapon);
            gird.AddChild(itemGo.transform);
            itemGo.GetComponent<itemShopWeapon>().setId(id);
        }
    }
   public void OnBuyClick(int id)
    {
        this.buyId = id;
        numberDialog.SetActive(true);
        numberInput.value = "0";
    }
  public  void OnBtnOKClick()
    {
        int count = int.Parse(numberInput.value);
       ObjectInfo info=  ObjectsInfo._instance.GetObjectInfoById(buyId);
        int price = info.Price_buy;
        int total_price = price * count;
        if (count > 0)
        {
            bool success = Inventory._instance.GetCoin(total_price);
            if (success)
            {
                Inventory._instance.GetId(buyId,count);
            }
        }
        buyId = 0;
        numberInput.value = "0";
        numberDialog.SetActive(false);
    }
    public void OnClick()
    {
        numberDialog.SetActive(false);

    }
    
}
