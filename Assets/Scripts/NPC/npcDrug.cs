using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcDrug : NPC {
    AudioSource audioS;
    private void OnMouseOver()
    {
        audioS = this.GetComponent<AudioSource>();
        if (Input.GetMouseButtonDown(0))
        {
            audioS.Play();
            ShopDrug._instance.transformPlay();
        }
    }
}
