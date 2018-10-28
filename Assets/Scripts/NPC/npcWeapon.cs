using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcWeapon : NPC {

    AudioSource audioS;
    private void OnMouseOver()
    {
        audioS = this.GetComponent<AudioSource>();
        if (Input.GetMouseButtonDown(0))
        {
            audioS.Play();
            ShopWeaponUI._instance.transfromPlay();
        }
    }
}
