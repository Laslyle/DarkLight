using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    private void OnMouseEnter()
    {

        CursorManage._instance.SetNpcTalk();//控制鼠标指针谈话NPC形态
    }
    private void OnMouseExit()
    {
        CursorManage._instance.SetNormal();//控制鼠标指针正常形态

    }
}
