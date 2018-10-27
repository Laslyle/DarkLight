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
    private void Awake()
    {

        icon = transform.Find("icon").GetComponent<UISprite>();
        icon.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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

    }
}
