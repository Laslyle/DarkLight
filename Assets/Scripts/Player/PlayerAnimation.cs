using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    public PlayerMov playMov;
    public Animation animation;
	// Use this for initialization
	void Start () {
        playMov = this.GetComponent<PlayerMov>();
        animation = this.GetComponent<Animation>();

    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (playMov.state == PlayerMov.PlayerState.Idle)
        {
            PlayAnim("Idle");
        }
        else if (playMov.state == PlayerMov.PlayerState.Moving)
        {
            PlayAnim("Walk");
        }

    }

    void PlayAnim(string animationName)
    {
        animation.CrossFade(animationName);
    }
}
