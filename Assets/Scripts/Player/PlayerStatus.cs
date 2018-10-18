using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    public int hp = 100;
    public int mp = 100;
    public int coin = 200;
    public int grade = 1;
    public void GetCoin(int coins)
    {
        coin += coins;
    }
}
