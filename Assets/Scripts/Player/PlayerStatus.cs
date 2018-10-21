using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    public int hp = 100;
    public int mp = 100;
    public int coin = 200;
    public int grade = 1;

    public int attack = 20;
    public int attack_plus = 0;
    public int def = 20;
    public int def_plus = 0;
    public int speed = 0;
    public int speed_plus = 0;

    public int point_remain = 0;

    public void GetCoin(int coins)
    {
        coin += coins;
    }
    public bool GetPoint(int point = 1)
    {
        if (point_remain>0) {
            point_remain -= point;
            return true;
        }
        return false;
    }
}
