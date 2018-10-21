using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {
    public static Status _instance;
    public TweenPosition twePos;
    private bool isStating=false;
    public UILabel attackLabel;
    public UILabel defLabel;
    public UILabel speedLabel;
    public UILabel pointRemainLabel;
    public UILabel summaryLabel;

    public GameObject attackButtonGo;
    public GameObject defButtonGo;
    public GameObject speedButtonGo;

    private PlayerStatus ps;

    private void Awake()
    {
         _instance=this;
        twePos = this.GetComponent<TweenPosition>();
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();

        /*
        attackLabel = transform.Find("attack").GetComponent<UILabel>();
        defLabel = transform.Find("def").GetComponent<UILabel>();
        speedLabel = transform.Find("speed").GetComponent<UILabel>();
        pointRemainLabel = transform.Find("remainPointVlue").GetComponent<UILabel>();
        summaryLabel = transform.Find("summaryValue").GetComponent<UILabel>();
        attackButtonGo = transform.Find("attack_plus").gameObject;
        defButtonGo = transform.Find("def_plus").gameObject;
        speedButtonGo = transform.Find("speed_plus").gameObject;
        */

        this.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}
    void UpdateShow()
    {
        attackLabel.text = ps.attack + " + " +ps.attack_plus;
        defLabel.text = ps.def + " + " + ps.def_plus;
        speedLabel.text = ps.speed + " + " + ps.speed_plus;

        pointRemainLabel.text = ps.point_remain.ToString();

        summaryLabel.text = "伤害：" + (ps.attack + ps.attack_plus)
            + "  " + "防御：" + (ps.def + ps.def_plus)
            + "  " + "速度：" + (ps.speed + ps.speed_plus);

        if (ps.point_remain > 0)
        {
            attackButtonGo.SetActive(true);
            defButtonGo.SetActive(true);
            speedButtonGo.SetActive(true);
        }
        else
        {
            attackButtonGo.SetActive(false);
            defButtonGo.SetActive(false);
            speedButtonGo.SetActive(false);
        }

    }

    void Show()
    {
        this.gameObject.SetActive(true);

        twePos.PlayForward();
    }
    void Hide()
    {
        twePos.PlayReverse();
    }
    public void transformStatus()
    {
        if (isStating)
        {
            
            Hide();
            isStating = false;
        }
        else
        {
            Show();
            isStating = true;
            UpdateShow();

        }
    }
    public void OnAttackPlusClick()
    {
        bool success = ps.GetPoint();
        if (success)
        {
            ps.attack_plus++;
            UpdateShow();
        }
    }
    public void OnDefPlusClick()
    {
        bool success = ps.GetPoint();
        if (success)
        {
            ps.def_plus++;
            UpdateShow();
        }
    }
    public void OnSpeedPlusClick()
    {
        bool success = ps.GetPoint();
        if (success)
        {
            ps.speed_plus++;
            UpdateShow();
        }
    }
}
