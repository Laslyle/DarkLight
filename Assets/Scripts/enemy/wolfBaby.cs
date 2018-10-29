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
    private float timer=0;
     float speed = 1;
    bool isAttack=false;
    public int hp=100;
    public int attack = 10;
    public float miss_rate=0.2f;
    private Color normal;
  
    public GameObject wolf;
    public AudioClip miss_sound;

    private GameObject hudTextFollow;

    private GameObject hudTextGo;
    public GameObject hudTextPrefab;

    private HUDText hudtext;
    private UIFollowTarget followTarget;

    public string animate_attack_now;

    public string aniname_normalattack;
    public float time_normalattack; 
    public string aniname_crazyattack;
    public float time_crazyattack;
    public float crazyattack_rate;

    public int attack_rate=1;
    private float attack_timer = 0;

    public Transform target;
    public float minDis=2;
    public float maxDis = 5;
    public wolfSpawn spawn;
    public int exp = 20;
    public PlayerStatus ps;

    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        animation_now = animation_idle;
        cc = this.GetComponent<CharacterController>();
        //wolf = transform.Find("Wolf_Baby").gameObject;
        normal = wolf.transform.GetComponent<Renderer>().material.color;
        hudTextFollow = transform.Find("HUDText").gameObject;
        ps = GameObject.FindWithTag(Tags.player).GetComponent<PlayerStatus>();
    }
    // Use this for initialization
    void Start () {
        //wolf = transform.Find("Wolf_Baby").gameObject;
        //wolf.transform.GetComponent<Renderer>().material.color = Color.red;
        //hudTextGo= GameObject.Instantiate(hudTextPrefab, Vector3.zero, Quaternion.identity);
        //hudTextGo.transform.parent = HUDtextParent._instance.gameObject.transform;
        hudTextGo = NGUITools.AddChild(HUDtextParent._instance.gameObject, hudTextPrefab);

        hudtext = hudTextGo.GetComponent<HUDText>();
        followTarget = hudTextGo.GetComponent<UIFollowTarget>();
        followTarget.target = hudTextFollow.transform;
        followTarget.gameCamera = Camera.main;
        //followTarget.uiCamera = UICamera.current.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update () {
        if (status == wolfState.Death)//死亡
        {
            anim.CrossFade(animation_death);
        }
        else if (status == wolfState.Attack)//攻击
        {
            autoAttack();
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


        /*
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(1);
        }*/

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
        if (status == wolfState.Death)
        {
            return;
        }
        target = GameObject.FindWithTag(Tags.player).transform;
        status = wolfState.Attack;
        if (value < miss_rate)//MISS
        {
            AudioSource.PlayClipAtPoint(miss_sound, transform.position);
            hudtext.Add("Miss", Color.gray, 1);
        }
        else//打中
        {
            hudtext.Add("-"+attack, Color.red, 1);

            this.hp -= attack;
            StartCoroutine(showBodyRed());
            isAttack = true;
            attack_timer = 0;
            if (hp <= 0)
            {
                status = wolfState.Death;
                Destroy(this.gameObject, 2);
            }
        }
    }
    IEnumerator showBodyRed()
    {
        wolf.transform.GetComponent<Renderer>().material.color = Color.red;
        yield return new  WaitForSeconds(1f);
        wolf.transform.GetComponent<Renderer>().material.color = normal;
    }

    private void OnDestroy()
    {
        spawn.MinusNumber();
        ps.GetExp(exp);
        npcQuest._instance.OnKillWolf();
        GameObject.Destroy(hudTextGo);
    }
    void RandomAttack()
    {
        float value = Random.Range(0f, 1f);
        if (value < crazyattack_rate)
        {
            animate_attack_now = aniname_crazyattack;
        }
        else
        {
            animate_attack_now = aniname_normalattack;

        }
    }
   void autoAttack()
    {
        if (target != null)
        {
            PlayerState playerState = target.GetComponent<PlayerAttack>().playerSta;
            if (playerState == PlayerState.Death) return;
            float dis = Vector3.Distance(target.position, transform.position);
            if (dis > maxDis)
            {
                target = null;
                status = wolfState.Idle;

            }
            else if (dis <= minDis)
            {
                attack_timer += Time.deltaTime;
                anim.CrossFade(animate_attack_now);
                if (animate_attack_now == aniname_normalattack)
                {
                    if (attack_timer > time_normalattack)
                    {
                        target.GetComponent<PlayerAttack>().TakeDamgge(attack);
                        animate_attack_now = animation_idle;
                    }
                }
                else if(animate_attack_now == aniname_crazyattack)
                {
                    if (attack_timer > time_crazyattack)
                    {
                        target.GetComponent<PlayerAttack>().TakeDamgge(attack);

                        animate_attack_now = animation_idle;
                    }
                }
                if (attack_timer > (1f / attack_rate)){
                    RandomAttack();
                    attack_timer = 0;
                }
            }
            else
            {
                transform.LookAt(target);
                cc.SimpleMove(transform.forward * speed);
                anim.CrossFade(animation_walk);
            }
        }
        else
        {
            status = wolfState.Idle;
        }
    }
    private void OnMouseEnter()
    {
        if (PlayerAttack._instance.isLocking == false) 
        CursorManage._instance.SetAttack();
    }
    private void OnMouseExit()
    {
        if (PlayerAttack._instance.isLocking == false)

            CursorManage._instance.SetNormal();

    }
   
}
