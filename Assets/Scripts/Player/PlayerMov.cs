using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlWalkState
{
    Idle,
    Moving
}

public class PlayerMov : MonoBehaviour {

    
    
    private CharacterController controller;
    private PlayerAttack attack;
    private PlayerDir playDir;
    public float speed = 2;
    public ControlWalkState state = ControlWalkState.Idle;
    public bool isMoving = false;


    /// <summary>
    /// 获取角色控制器和控制角色方向脚本
    /// </summary>
    // Use this for initialization
    void Start () {
        playDir = this.GetComponent<PlayerDir>();
        controller = this.GetComponent<CharacterController>();
        attack = this.GetComponent<PlayerAttack>();
    }
	
	// Update is called once per frame
	void Update () {
        if (attack.playerSta == PlayerState.ControlWalk)
        { 
        if (Vector3.Distance(playDir.targetPosition, transform.position) > 0.1f)
          {
            controller.SimpleMove(transform.forward * speed);
            state = ControlWalkState.Moving;
            isMoving = true;
          }
        else { 
            state = ControlWalkState.Idle;
        isMoving = false;
            }
        }
    }
    public void simpleMove(Vector3 tarPos)
    {
        transform.LookAt(tarPos);
        controller.SimpleMove(transform.forward * speed);

    }
}
