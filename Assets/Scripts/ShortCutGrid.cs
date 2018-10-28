using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShortCutType
{
    Skill,
    Drug,
    None
}
public class ShortCutGrid : MonoBehaviour {
    public KeyCode keycode;
    private ShortCutType type = ShortCutType.None;
    private UISprite icon;
    private int id;
    private SkillInfo info;
    private ObjectInfo objectInfo;
    private PlayerStatus ps;
    private void Awake()
    {

        icon = transform.Find("icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        ps = GameObject.FindWithTag(Tags.player).GetComponent<PlayerStatus>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keycode))
        {
            if (type == ShortCutType.Drug)
            {
                OnDrugUse();
            }
            else if(type == ShortCutType.Skill)
            {

            }

        }
	}
   public void setSkillById(int id)
    {
        this.id = id;
        this.info = SkillsInfo._instance.GetSkillInfoById(id);
        icon.gameObject.SetActive(true);
        icon.spriteName = info.Icon_name;
        type = ShortCutType.Skill;
    }

    public void setDrugById(int id)
    {
        this.id = id;
        objectInfo = ObjectsInfo._instance.GetObjectInfoById(id);
        if (objectInfo.Type == ObjectType.Drug)
        {
            icon.gameObject.SetActive(true);
            icon.spriteName = objectInfo.Icon_name;
            type = ShortCutType.Drug;
        }
    }
    void OnDrugUse()
    {
        bool success = Inventory._instance.MinusId(id, 1);
        if (success)
        {
            ps.GetDrag(objectInfo.Hp, objectInfo.Mp);
            type = ShortCutType.None;
            icon.gameObject.SetActive(false);
            id = 0;
            info = null;
            objectInfo = null;
        }
        else
        {
            type = ShortCutType.None;
            icon.gameObject.SetActive(false);
            id = 0;
            info = null;
            objectInfo = null;
        }
    }
}
