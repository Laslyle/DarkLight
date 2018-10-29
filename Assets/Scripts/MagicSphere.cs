using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour {
    public float attack=0;

    private List<wolfBaby> wolfList = new List<wolfBaby>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.enemy)
        {
            wolfBaby baby = other.GetComponent<wolfBaby>();
            int index = wolfList.IndexOf(baby);
            if (index == -1)
            {
                baby.TakeDamage((int)attack);
                wolfList.Add(baby);
            }
        }
    }
}
