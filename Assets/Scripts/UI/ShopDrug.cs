using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrug : MonoBehaviour {
    public static ShopDrug _instance;
    public GameObject numberDia;
    public UIInput numberInput;
    TweenPosition twePos;
    bool isShopping=false;
    private int buy_id = 0;
    private void Awake()
    {
        _instance = this;
        twePos = this.GetComponent<TweenPosition>();
        numberDia.SetActive(false);
      
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   public void transformPlay()
    {
        if (isShopping)
        {
            twePos.PlayReverse();
            isShopping = false;
        }
        else
        {
            twePos.PlayForward();
            isShopping = true;
        }
    }
    public void OnBuyId1001()
    {
        Buy(1001);
    }
    public void OnBuyId1002()
    {
        Buy(1002);

    }
    public void OnBuyId1003()
    {
        Buy(1003);

    }
    void Buy(int id)
    {
        buy_id = id;
        showNumberDia();
    }
    public void OnOkBtnClick()
    {
        int count = int.Parse(numberInput.value);
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(buy_id);
        int price = info.Price_buy;
        int price_total = price * count;
        bool success = Inventory._instance.GetCoin(price_total);
        if (success)
        {
            if (count > 0)
            {
                Inventory._instance.GetId(buy_id, count);
            }
        }
        numberDia.SetActive(false);
    }
    void showNumberDia()
    {
        numberDia.SetActive(true);
        numberInput.value = "0";
    }

}
