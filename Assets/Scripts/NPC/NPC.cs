using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    private void OnMouseEnter()
    {

        CursorManage._instance.SetNpcTalk();
    }
    private void OnMouseExit()
    {
        CursorManage._instance.SetNormal();

    }
}
