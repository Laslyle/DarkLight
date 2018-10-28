using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusUI : MonoBehaviour {
    public static PlayerStatusUI _instance;

    private UILabel name;
    private UISlider hpBar;
    private UISlider mpBar;

    private UILabel hpLabel;
    private UILabel mpLabel;
    private PlayerStatus ps;
    private void Awake()
    {
        _instance = this;
        name = transform.Find("name").GetComponent<UILabel>();
        hpBar = transform.Find("HP").GetComponent<UISlider>();
        mpBar = transform.Find("MP").GetComponent<UISlider>();

        hpLabel = transform.Find("HP/Thumb/Label").GetComponent<UILabel>();
        mpLabel = transform.Find("MP/Thumb/Label").GetComponent<UILabel>();


    }
    private void Start()
    {
        ps = GameObject.FindWithTag(Tags.player).GetComponent<PlayerStatus>();
        updateShow();
    }
    public void updateShow()
    {
        name.text = "Lv." + ps.grade + " " + ps.name;
        hpBar.value = ps.hp_remain / ps.hp;
        mpBar.value = ps.mp_remain / ps.mp;
        hpLabel.text = ps.hp_remain + "/" + ps.hp;
        mpLabel.text = ps.mp_remain + "/" + ps.mp;
    }
}
