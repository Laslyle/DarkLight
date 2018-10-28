using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum wolfState
{
    Idle,
    Walk,
    Attack,
    Death
}
public class wolfBaby : MonoBehaviour {
    public wolfState status = wolfState.Idle;
    private CharacterController cc;
    public Animation anim;
    public string animation_death;
    public string animation_now;
    public string animation_walk;
    public string animation_idle;
    public float time=1;
    public float timer=0;
     float speed = 1;
    public int hp=100;
    public float miss_rate=0.2f;

    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        animation_now = animation_idle;
        cc = this.GetComponent<CharacterController>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (status == wolfState.Death)//死亡
        {
            anim.CrossFade(animation_death);
        }
        else if (status == wolfState.Attack)//攻击
        {

        }
        else//游走
        {
            anim.CrossFade(animation_now);
            
            if (animation_now == animation_walk)
            {
                cc.SimpleMove(this.transform.forward * speed);

            }
            timer += Time.deltaTime;
            if (timer >= time)
            {
                timer = 0;
                RandomState();
            }
        }
    }
    void RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            animation_now = animation_idle;
        }
        else
        {
            if(animation_now!= animation_walk)
            {
                transform.Rotate(transform.up * Random.Range(0, 361));
            }
            animation_now = animation_walk;

        }
    }
    public void TakeDamage(int attack)
    {
        float value = Random.Range(0f, 1f);
        if (value < miss_rate)
        {

        }
        else
        {
            this.hp -= attack;
            if (hp <= 0)
            {
                status = wolfState.Death;
                Destroy(this.gameObject, 2);
            }
        }
    }
}
