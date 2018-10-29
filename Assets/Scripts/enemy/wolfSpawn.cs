using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfSpawn : MonoBehaviour {
    public GameObject prefab;
    public int maxnum = 5;
    private int currentnum = 0;
    public float time = 3;
    private float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentnum < maxnum)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-5, 5);
                pos.z += Random.Range(-5, 5);
                GameObject go= GameObject.Instantiate(prefab, pos, Quaternion.identity);
                go.GetComponent<wolfBaby>().spawn = this;
                timer = 0;
                currentnum++;
            }
        }	
	}
    public void MinusNumber()
    {
        this.currentnum--;
    }
}
