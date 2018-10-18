using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour
{

    public GameObject effect_prefab;
    public Vector3 targetPosition = Vector3.zero;

    private bool isMoving = false;

   

    // Update is called once per frame
    private void Start()
    {
        targetPosition = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray,out hitinfo))
            {
                if (hitinfo.collider.tag==Tags.ground && UICamera.isOverUI==false)
                {
                    isMoving = true;
                    GameObject.Instantiate(effect_prefab,hitinfo.point,Quaternion.identity);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                if (hitinfo.collider.tag == Tags.ground)
                {
                    targetPosition = hitinfo.point;
                    targetPosition = new Vector3(targetPosition.x,transform.position.y,targetPosition.z);
                }
            }
            this.transform.LookAt(targetPosition);
        }
    }

}