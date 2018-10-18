using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManage : MonoBehaviour {
    public static CursorManage _instance;
    public Texture2D cursor_normal;
    public Texture2D cursor_npc_talk;
    public Texture2D cursor_attack;
    public Texture2D cursor_lockTarget;
    public Texture2D cursor_pick;

    private void Start()
    {
        _instance = this;
    }
    public void SetNormal()
    {
        Cursor.SetCursor(cursor_normal, Vector2.zero, CursorMode.Auto);
    }
    public void SetNpcTalk()
    {
        Cursor.SetCursor(cursor_npc_talk, Vector2.zero, CursorMode.Auto);
    }
}
