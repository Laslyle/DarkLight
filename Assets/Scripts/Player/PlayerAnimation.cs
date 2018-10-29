using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    public PlayerMov playMov;
    public Animation animation;
    public PlayerAttack attack;
	// Use this for initialization
	void Start () {
        playMov = this.GetComponent<PlayerMov>();
        animation = this.GetComponent<Animation>();
        attack= this.GetComponent<PlayerAttack>();

    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (attack.playerSta == PlayerState.ControlWalk) { 
        if (playMov.state == ControlWalkState.Idle)
        {
            PlayAnim("Idle");
        }
        else if (playMov.state == ControlWalkState.Moving)
        {
            PlayAnim("Walk");
        }
        }
        else if(attack.playerSta == PlayerState.NormalAttack)
        {
            if (attack.attack_state == AttackState.Moving)
            {
                PlayAnim("Run");

            }
        }
    }

    void PlayAnim(string animationName)
    {
        animation.CrossFade(animationName);
    }
}
