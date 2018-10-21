using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemInventory : UIDragDropItem {
    private UISprite sprite;
    private int id;
    // Use this for initialization
    private void Awake()
    {
        base.Awake();
        sprite = this.GetComponent<UISprite>();

    }
    void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            if (surface.tag == Tags.inventory_grid)//拖动到了一个空格子里
            {
                if (surface == this.transform.parent.gameObject)//拖动到了自己
                {
                    resetPos();
                }
                else//拖动到别的空格子，进行移动
                {
                    itemGird oldGird = this.transform.parent.GetComponent<itemGird>();
                    this.transform.parent = surface.transform;
                    resetPos();
                    itemGird newGird=this.transform.parent.GetComponent<itemGird>();
                    newGird.SetId(oldGird.id, oldGird.num);

                    oldGird.Clear();

                }

            }
            else if(surface.tag == Tags.inventory_item)//拖动到了一个有物品的格子里，二者交换
            {
                itemGird oldGird = this.transform.parent.GetComponent<itemGird>();
                itemGird newGird = surface.transform.parent.GetComponent<itemGird>();
                int num = oldGird.num;
                int id = oldGird.id;
                oldGird.SetId(newGird.id, newGird.num);
                newGird.SetId(id,num);
                resetPos();

            }
            else
            {
                resetPos();
            }
        }
        else
        {
            resetPos();
        }
    }

    void resetPos()
    {
        transform.localPosition = Vector3.zero;
    }
    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        sprite.spriteName = info.Icon_name;
    }
    public void SetSpriteName(int id,string icon_name)
    {
        sprite.spriteName = icon_name;
        this.id = id;
    }

  public  void OnHoverOver()
    {
        inventoryDes._instance.Show(id);
    }
  public   void OnHoverExit()
    {
        inventoryDes._instance.Hide();
    }

}
