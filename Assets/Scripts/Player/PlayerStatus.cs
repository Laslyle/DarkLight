using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType{
    Swordman,
    Magican
}

public class PlayerStatus : MonoBehaviour {
    public HeroType heroType;
    public UILabel LV;

    public string name = "默认名称";
    public int hp = 100;
    public int mp = 100;

    public float hp_remain=100;
    public float mp_remain = 100;
    public float exp = 0;

    public GameObject prefab;
    public int grade = 1;

    public float attack = 20;
    public int attack_plus = 0;
    public float def = 20;
    public int def_plus = 0;
    public float speed = 0;
    public int speed_plus = 0;

    public int point_remain = 0;

    private void Start()
    {
        GetExp(0);
    }
    public bool GetPoint(int point = 1)
    {
        if (point_remain>0) {
            point_remain -= point;
            return true;
        }
        return false;
    }

 public   void GetDrag(int hp,int mp)
    {
        hp_remain += hp;
        mp_remain += mp;
        if (hp_remain > this.hp)
        {
            hp_remain = this.hp;
        }
        if (mp_remain > this.mp)
        {
            mp_remain = this.mp;
        }
        PlayerStatusUI._instance.updateShow();

    }
    public void GetExp(int exp)
    {
        this.exp += exp;
        int total_exp = 100 + this.grade * 30;
        while (this.exp >= total_exp)
        {
            this.grade++;
             prefab = null;
            GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
            this.exp -= total_exp;
            total_exp = 100 + this.grade * 30;
            updateShow();
        }
        expBar._instance.setValue(this.exp/ total_exp);
    }

    public bool TakeMp(int count)
    {
        if (mp_remain > count)
        {

            mp_remain -= count;
            PlayerStatusUI._instance.updateShow();
            return true;

        }
        else
        {
            return false;
        }
    }
    void updateShow()
    {
        LV.text = "LV." + grade+name;
    }
}
