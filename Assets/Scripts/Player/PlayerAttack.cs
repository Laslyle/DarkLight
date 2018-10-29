using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack,
    Death
}
public enum AttackState
{
    Moving,
    Idle,
    Attack
}
public class PlayerAttack : MonoBehaviour {

    public static PlayerAttack _instance;
    public PlayerState playerSta = PlayerState.ControlWalk;
    public AttackState attack_state = AttackState.Idle;

    public string aniname_normalattack;
    public string aniname_idle;
    public string aniname_now;

    private Animation anim;
    public float time_normalattack;
    private float timer = 0;
    public float min_distance = 5;
    public float rate_normalattack = 1;
    private Transform target_normalattack;
    private PlayerMov move;
    private bool isShowEffect = false;
    public GameObject effect;
    private PlayerStatus ps;
    public float rate_miss = 0.5f;

    private GameObject hudTextFollow;
    public GameObject hudTextPrefab;
    private GameObject hudTextGo;
    private HUDText hudtext;
    public AudioClip miss_sound;
    public GameObject body;
    private Color normal;
    public string aniname_death;

    public GameObject[] efxArr;
    private Dictionary<string, GameObject> efxDict = new Dictionary<string, GameObject>();
    public bool isLocking = false;
    private SkillInfo info = null;
    private void Awake()
    {
        move = this.GetComponent<PlayerMov>();
        anim = this.GetComponent<Animation>();
        ps = this.GetComponent<PlayerStatus>();
        hudTextFollow = transform.Find("HUDText").gameObject;
        normal = body.GetComponent<Renderer>().material.color;
        foreach (GameObject go in efxArr)
        {
            efxDict.Add(go.name, go);
        }
        _instance = this;
    }
    private void Start()
    {
        hudTextGo = NGUITools.AddChild(HUDtextParent._instance.gameObject, hudTextPrefab);

        hudtext = hudTextGo.GetComponent<HUDText>();
        UIFollowTarget followTarget = hudTextGo.GetComponent<UIFollowTarget>();
        followTarget.target = hudTextFollow.transform;
        followTarget.gameCamera = Camera.main;
    }
    private void Update()
    {
        
        if (isLocking==false&& Input.GetMouseButton(0)&&(playerSta != PlayerState.Death))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == Tags.enemy)
                {
                    target_normalattack = hitInfo.collider.transform;
                    playerSta = PlayerState.NormalAttack;
                    timer = 0;
                    isShowEffect = false;
                }
                else
                {
                    playerSta = PlayerState.ControlWalk;
                    target_normalattack = null;

                }
            }
        }

        if (playerSta == PlayerState.NormalAttack)
        {
            if (target_normalattack == null)
            {
                playerSta = PlayerState.ControlWalk;
            } else {
                float disance = Vector3.Distance(transform.position, target_normalattack.position);
                if (disance < min_distance)
                {
                    transform.LookAt(target_normalattack.position);
                    attack_state = AttackState.Attack;
                    timer += Time.deltaTime;
                    anim.CrossFade(aniname_now);
                    if (timer >= time_normalattack)
                    {
                        aniname_now = aniname_idle;
                        if (isShowEffect == false)
                        {
                            isShowEffect = true;
                            GameObject.Instantiate(effect, target_normalattack.position, Quaternion.identity);
                            target_normalattack.GetComponent<wolfBaby>().TakeDamage(getAttack());
                        }
                    }
                    if (timer >= (1f / rate_normalattack))
                    {
                        timer = 0;
                        isShowEffect = false;
                        aniname_now = aniname_normalattack;
                    }
                }
                else
                {
                    attack_state = AttackState.Moving;
                    move.simpleMove(target_normalattack.position);
                }
            }
        }
        else if(playerSta == PlayerState.Death)
        {
            anim.CrossFade(aniname_death);
        }

        if (Input.GetMouseButtonDown(0) && isLocking)
        {
            OnLockTarget();
        }
    }
    public int getAttack()
    {
        return (int)(EquipmentUI._instance.attack + ps.attack + ps.attack_plus);
    }

    public void TakeDamgge(int attack)
    {
        if (playerSta == PlayerState.Death) return;
        float def = EquipmentUI._instance.def + ps.def + ps.def_plus;
        float temp = attack * ((200 - def) / 200);
        if (temp < 1)
            temp = 1;
        float value = Random.Range(0f, 1f);
        if (value < rate_miss)
        {
            AudioSource.PlayClipAtPoint(miss_sound, transform.position);
            hudtext.Add("MISS", Color.red, 1);
        }
        else
        {
            hudtext.Add("-" + temp, Color.red, 1);

            ps.hp_remain -= (int)temp;
            StartCoroutine(showBodyRed());
            if (ps.hp_remain <= 0)
            {
                playerSta = PlayerState.Death;
            }

        }
        PlayerStatusUI._instance.updateShow();
    }
    IEnumerator showBodyRed()
    {
        body.transform.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(1f);
        
        body.transform.GetComponent<Renderer>().material.color = normal;
    }
    private void OnDestroy()
    {
        GameObject.Destroy(hudTextGo);
    }
    public void UseSkill(SkillInfo info)
    {
        if (HeroType.Magican == ps.heroType)
        {
            if (info.ApplicableRole == ApplicableRole.Swordman)
            {
                return;
            }
        }
        if (HeroType.Swordman == ps.heroType)
        {
            if (info.ApplicableRole == ApplicableRole.Magician)
            {
                return;
            }

        }


        switch (info.ApplyType)
        {
            case ApplyType.Passive:
                StartCoroutine(OnPassSkillUse(info));
                break;
            case ApplyType.Buff:
                StartCoroutine(OnBuffSkillUse(info));
                break;
            case ApplyType.SingleTarget:
                OnSingleTargetSkillUse(info);
                break;
            case ApplyType.MultiTarget:
                OnMultiTargetUse(info);
                break;
            default:
                break;
        }

    }

    IEnumerator  OnPassSkillUse(SkillInfo info)
    {
        playerSta = PlayerState.SkillAttack;
        anim.CrossFade(info.Aniname);
        yield return new WaitForSeconds(info.Anitime);
        playerSta = PlayerState.ControlWalk;
        int hp = 0, mp = 0;
        if (info.ApplyProperty == ApplyProperty.HP)
        {
            hp = info.ApplyValue;
        }
        else if (info.ApplyProperty == ApplyProperty.MP)
        {
            mp = info.ApplyValue;
        }
        ps.GetDrag(hp,mp);

        GameObject prefab = null;
        efxDict.TryGetValue(info.Efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
    }
    IEnumerator OnBuffSkillUse(SkillInfo info)
    {
        playerSta = PlayerState.SkillAttack;
        anim.CrossFade(info.Aniname);
        yield return new WaitForSeconds(info.Anitime);
        playerSta = PlayerState.ControlWalk;
        //特效
        GameObject prefab = null;
        efxDict.TryGetValue(info.Efx_name, out prefab);
        GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
        switch (info.ApplyProperty)
        {
            case ApplyProperty.Attack:
                ps.attack *= (info.ApplyValue/100f);
                break;
            case ApplyProperty.Def:
                ps.def *= (info.ApplyValue / 100f);
                break;
            case ApplyProperty.Speed:
                move.speed*= (info.ApplyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack *= (info.ApplyValue / 100f);
                break;
            case ApplyProperty.HP:
                break;
            case ApplyProperty.MP:
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(info.ApplyTime);
        switch (info.ApplyProperty)
        {
            case ApplyProperty.Attack:
                ps.attack /= (info.ApplyValue / 100f);
                break;
            case ApplyProperty.Def:
                ps.def /= (info.ApplyValue / 100f);
                break;
            case ApplyProperty.Speed:
                move.speed /= (info.ApplyValue / 100f);
                break;
            case ApplyProperty.AttackSpeed:
                rate_normalattack /= (info.ApplyValue / 100f);
                break;
            case ApplyProperty.HP:
                break;
            case ApplyProperty.MP:
                break;
            default:
                break;
        }
    }
    void OnSingleTargetSkillUse(SkillInfo info)
    {
        playerSta = PlayerState.SkillAttack;

        CursorManage._instance.SetLockTarget();
        isLocking = true;
        this.info = info;
    }

    void OnLockTarget()
    {
        isLocking = false;
        switch (info.ApplyType)
        {
            case ApplyType.Passive:
                break;
            case ApplyType.Buff:
                break;
            case ApplyType.SingleTarget:
                StartCoroutine(OnLockSingleTarget());
                break;
            case ApplyType.MultiTarget:
                StartCoroutine(OnLockMultiTarget());
                break;
            default:
                break;
        }
    }
    IEnumerator OnLockSingleTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == Tags.enemy)
            {
                anim.CrossFade(info.Aniname);
                yield return new WaitForSeconds(info.Anitime);
                playerSta = PlayerState.ControlWalk;
                GameObject prefab = null;
                efxDict.TryGetValue(info.Efx_name, out prefab);
                GameObject.Instantiate(prefab, hitInfo.collider.transform.position, Quaternion.identity);


                hitInfo.collider.GetComponent<wolfBaby>().TakeDamage((int)(getAttack()*(info.ApplyValue/100f)));
            }
            else
            {
                playerSta = PlayerState.NormalAttack;
            }
            CursorManage._instance.SetNormal();
        }
    }
    void OnMultiTargetUse(SkillInfo info)
    {
        playerSta = PlayerState.SkillAttack;

        CursorManage._instance.SetLockTarget();
        isLocking = true;
        this.info = info;
    }
    IEnumerator OnLockMultiTarget()
    {
        CursorManage._instance.SetNormal();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo,12))
        {
            anim.CrossFade(info.Aniname);
            yield return new WaitForSeconds(info.Anitime);
            playerSta = PlayerState.ControlWalk;
            GameObject prefab = null;
            efxDict.TryGetValue(info.Efx_name, out prefab);
            GameObject go = GameObject.Instantiate(prefab, hitInfo.point + Vector3.up * 0.5f, Quaternion.identity);
            go.GetComponent<MagicSphere>().attack = getAttack() * (info.ApplyValue / 100f);

        }
        else
        {
            playerSta = PlayerState.ControlWalk;

        }
    }
}