using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour {

    public int id;
    private SkillInfo info;

    private UISprite iconname_sprite;
    private UILabel name_label;
    private UILabel applytype_label;
    private UILabel des_label;
    private UILabel mp_label;

    private GameObject icon_mask;

     void Initialized()
    {
        iconname_sprite = transform.Find("icon_name").GetComponent<UISprite>();
        name_label = transform.Find("property/name_bg/name").GetComponent<UILabel>();
        applytype_label = transform.Find("property/applyType_bg/applyType").GetComponent<UILabel>();
        des_label = transform.Find("property/des_bg/des").GetComponent<UILabel>();
        mp_label = transform.Find("property/mp_bg/mp").GetComponent<UILabel>();
        icon_mask = transform.Find("icon_mask").gameObject;
        icon_mask.SetActive(false);
    }
    public void setId(int id)
    {
        Initialized();
        this.id = id;
        info = SkillsInfo._instance.GetSkillInfoById(id);
       
        iconname_sprite.spriteName = info.Icon_name;
        name_label.text = info.Name;
        switch (info.ApplyType)
        {
            case ApplyType.Passive:
                applytype_label.text = "增益";
                break;
            case ApplyType.Buff:
                applytype_label.text = "增强";
                break;
            case ApplyType.SingleTarget:
                applytype_label.text = "单个目标";
                break;
            case ApplyType.MultiTarget:
                applytype_label.text = "群体技能";
                break;
        }
        des_label.text = info.Des;
        mp_label.text = info.Mp + "MP";
}
   public void updateShow(int level)
    {
        if (info.Level <= level)
        {
            icon_mask.SetActive(false);
            iconname_sprite.GetComponent<skillItemIcon>().enabled = true;
        }
        else
        {
            icon_mask.SetActive(true);
            iconname_sprite.GetComponent<skillItemIcon>().enabled = false;
        }
    }

   


}
   

