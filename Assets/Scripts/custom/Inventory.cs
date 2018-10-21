using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    private TweenPosition twePos;
    private bool isShow;
    public static Inventory  _instance;
    public int coinCount = 1000;
    public List<itemGird> itemGirdList;
    public UILabel coinLabel;
    public GameObject inventoryItem;
    private void Awake()
    {
        _instance = this;
        twePos = this.GetComponent<TweenPosition>();
        twePos.enabled = false;
    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetId(Random.Range(1001,1004));
        }
	}
  public  void GetId(int id,int count=1)
    {
        itemGird gird = null;
        foreach (itemGird temp in itemGirdList)//查找是否有相同物品
        {
            if (temp.id == id)
            {
                gird = temp;
                break;
            }
        }
        if (gird != null)//存在了相同物品，数量+1
        {
            gird.PlusNum();
        }
        else//不存在相同物品，查找空的gird，然后放入新的物品
        {
            foreach (itemGird temp in itemGirdList)
            {
                if (temp.id == 0)
                {
                    gird = temp;
                    break;
                }
            }
            if (gird != null)
            {
               GameObject itemGo= NGUITools.AddChild(gird.gameObject,inventoryItem);
                itemGo.transform.localPosition = Vector3.zero;
                gird.SetId(id,count);
            }
        }
    }
    void Show()
    {
        isShow = true;
        twePos.enabled = true;
        twePos.PlayForward();

    }
    void Hide()
    {
        isShow = false;
        twePos.PlayReverse();
    }
     /// <summary>
     /// true是取款成功，false代表失败
     /// </summary>
     /// <param name="count"></param>需要的钱
     /// <returns></returns>
    public bool GetCoin(int count) {
        if (coinCount >= count)
        {
            coinCount -= count;
            coinLabel.text = coinCount.ToString();//购买成功后余额
            return true;
        }
        return false;
    }
   public void transformStatu()
    {
        if (isShow == false)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
   
}
