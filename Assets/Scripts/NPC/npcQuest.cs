using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcQuest : NPC {
    public static npcQuest _instance;
    public TweenPosition tw;
    public GameObject okBtn;
    public GameObject cancelBtn;
    public GameObject acceptBtn;
    public PlayerStatus playerSta;
    public GameObject Quest;
    public UILabel desLabel;
    AudioSource audioS;

    private bool isQuesting=false;
    public int killCount = 0;
    private void Awake()
    {
        _instance = this;
        audioS = this.GetComponent<AudioSource>();
        Quest = GameObject.FindWithTag("Quest");
        Quest.SetActive(false);
    }
    private void Start()
    {
        okBtn.SetActive(false);
        playerSta = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioS.Play();
            if (isQuesting)
            {
                Quest.SetActive(true);
                acceptBtn.SetActive(false);
                cancelBtn.SetActive(false);
                okBtn.SetActive(true);
                desLabel.text = "任务:\n你已经杀死" + killCount + "\\10只狼\n奖励:\n1000金币";
            }
            else
            {
                acceptBtn.SetActive(true);
                cancelBtn.SetActive(true);
                okBtn.SetActive(false);
                desLabel.text = "任务:\n杀死十只小野狼\n奖励:\n1000金币";

            }
            showQuest();
        }
    }
    void showQuest()
    {
        tw.gameObject.SetActive(true);
        tw.PlayForward();
    }
   public void OnCloseButtonClick()
    {
        hideQuest();
    }
    public void OnAcceptButtonClick()
    {
        isQuesting = true;
        hideQuest();

    }
    public void OnCancelButtonClick()
    {
        hideQuest();
    }
    public void OnOkButtonClick()
    {
        if (killCount >= 10)
        {
            Inventory._instance.AddCoin(1000);
            isQuesting = false;
            killCount = 0;
        }

        hideQuest();
    }

    void hideQuest()
    {
        tw.PlayReverse();
    }

    public void OnKillWolf()
    {
        if (isQuesting)
        {
            killCount++;
        }
    }
}
