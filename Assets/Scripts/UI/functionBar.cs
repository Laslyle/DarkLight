using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class functionBar : MonoBehaviour {

	public void statusBtnClick()
    {
        Status._instance.transformStatus();
    }
    public void equipBtnClick()
    {
        EquipmentUI._instance.transformPlay();
    }
    public void skillBtnClick()
    {
        SkillUI._instance.TransformState();
    }
    public void settingBtnClick()
    {

    }
    public void bagBtnClick()
    {
        Inventory._instance.transformStatu();
    }
}
