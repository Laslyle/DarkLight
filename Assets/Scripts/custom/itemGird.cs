﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemGird : MonoBehaviour {
    public int id;
    public ObjectInfo info = null;
    public int num = 0;
    public UILabel numLabel;
    private void Awake()
    {
        numLabel = this.GetComponentInChildren<UILabel>();

    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetId(int id,int num=1)
    {
        this.id = id;
        info = ObjectsInfo._instance.GetObjectInfoById(id);
        itemInventory item = this.GetComponentInChildren<itemInventory>();
        item.SetSpriteName(id,info.Icon_name);
        numLabel.enabled = true;
        this.num = num;
        numLabel.text = num.ToString();

    }
    public void Clear()
    {
        id = 0;
        num = 0;
        info = null;
        numLabel.enabled=false;
    }

    public void PlusNum(int num=1)
    {
        this.num += num;
        numLabel.text = this.num.ToString();
    }
    public bool MinusNumber(int num = 1)
    {
        if (this.num >= num)
        {
            this.num -= num;
            numLabel.text = this.num.ToString();
            if (this.num == 0)
            {
                //要清空这个物品格子
                Clear();//清空存储的信息 
                GameObject.Destroy(this.GetComponentInChildren<itemInventory>().gameObject);//销毁物品格子
            }
            return true;
        }
        return false;
    }
}
