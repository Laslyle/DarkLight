using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour {
    public static SkillUI _instance;
    private TweenPosition tween;
    private bool isShow = false;
    private PlayerStatus ps;

    public UIGrid grid;
    public GameObject skillItemPrefab;
    public int[] magicianSkillIdList;
    public int[] swordmanSkillIdList;
    private void Awake()
    {

        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        this.transform.position=new Vector3(700,0,0);
    }
    // Use this for initialization
    void Start () {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        int[] idList = null;
        switch (ps.heroType)
        {
            case HeroType.Magican:
                idList = magicianSkillIdList;
                break;
            case HeroType.Swordman:
                idList = swordmanSkillIdList;
                break;
        }
        foreach (int id in idList)
        {
            GameObject itemGo = NGUITools.AddChild(grid.gameObject, skillItemPrefab);
            grid.AddChild(itemGo.transform);
            //itemGo.transform.parent = grid.transform;
            itemGo.GetComponent<SkillItem>().setId(id);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void TransformState()
    {
        if (isShow)
        {
            tween.PlayReverse(); isShow = false;
        }
        else
        {
            tween.PlayForward(); isShow = true;
            updateShow();

        }
    }
    void updateShow()
    {
        SkillItem[] items = this.GetComponentsInChildren<SkillItem>();
        foreach (SkillItem item in items)
        {
            item.updateShow(ps.grade);
        }
    }
}
