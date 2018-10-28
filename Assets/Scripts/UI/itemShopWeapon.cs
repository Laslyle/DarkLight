using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemShopWeapon : MonoBehaviour {
    private int id;

    public UISprite icon;
    public UILabel name;
    public UILabel effect;
    public UILabel price;
    ObjectInfo info;

    private void Awake()
    {

    }
    private void Start()
    {
    }
    public void setId(int id)
    {
        this.id = id;
        info = ObjectsInfo._instance.GetObjectInfoById(id);
        icon.spriteName = info.Icon_name;
        name.text = info.Name;
        if (info.Attack > 0)
        {
            effect.text = "+伤害" + info.Attack;
        }
        else if(info.Def>0){
            effect.text = "+防御" + info.Def;
        }
        else if(info.Speed>0){
            effect.text = "+速度" + info.Speed;
        }
        price.text = info.Price_buy.ToString();
    }

  public  void onBuyClick()
    {
        ShopWeaponUI._instance.OnBuyClick(id);
    }
}
