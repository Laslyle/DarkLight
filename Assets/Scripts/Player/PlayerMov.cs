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
        }
        else
            state = PlayerState.Idle;
	}
}
