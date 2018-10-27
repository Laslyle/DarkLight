using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemEquipment : MonoBehaviour {
    private UISprite sprite;
    public int id;
    private bool isHover = false;
    private void Awake()
    {
        sprite = this.GetComponent<UISprite>();

    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isHover)
        {
            if (Input.GetMouseButtonDown(1))
            {
                EquipmentUI._instance.TakeOff(id,this.gameObject);
            }
        }
	}
    public void SetId(int id)
    {
        this.id = id;
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        SetInfo(info);
    }
    public void SetInfo(ObjectInfo info)
    {
        this.id = info.Id;

        sprite.spriteName = info.Icon_name;
    }

    public void OnHover(bool isOver)
    {
        isHover = isOver;
    }
   
}
