using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMov : MonoBehaviour {

    public enum PlayerState
    {
        Idle,
        Moving
    }
    
    private CharacterController controller;
    private PlayerDir playDir;
    public float speed = 2;
    public PlayerState state = PlayerState.Idle;
    public bool isMoving = false;


    /// <summary>
    /// 获取角色控制器和控制角色方向脚本
    /// </summary>
    // Use this for initialization
    void Start () {
        playDir = this.GetComponent<PlayerDir>();
        controller = this.GetComponent<CharacterController>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(playDir.targetPosition, transform.position) > 0.1f)
        {
            controller.SimpleMove(transform.forward * speed);
            state = PlayerState.Moving;
            isMoving = true;
        }
        else { 
            state = PlayerState.Idle;
        isMoving = false;}

    }
}
