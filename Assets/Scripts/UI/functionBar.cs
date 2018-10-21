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

    }
    public void skillBtnClick()
    {

    }
    public void settingBtnClick()
    {

    }
    public void bagBtnClick()
    {
        Inventory._instance.transformStatu();
    }
}
