using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour
{

    public GameObject effect_prefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray,out hitinfo))
            {
                if (hitinfo.collider.tag==Tags.ground)
                {
                    GameObject.Instantiate(effect_prefab,hitinfo.point,Quaternion.identity);
                }
            }
        }
    }
}