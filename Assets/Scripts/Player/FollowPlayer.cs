using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public GameObject player;
    public Vector3 offsetion;
    public float distance;
    public float followSpeed = 10;
    private bool isRoating=false;
    public float rotateSpeed = 5;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        transform.LookAt(player.transform.position);
        offsetion = transform.position - player.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + offsetion;
        scrollView();
        RotateView();
    }
    void scrollView()
    {
        distance = offsetion.magnitude;
        distance += Input.GetAxis("Mouse ScrollWheel") * followSpeed;
        distance = Mathf.Clamp(distance, 2, 18);
        offsetion = offsetion.normalized * distance;
    }
    void RotateView()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRoating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRoating = false;
        }
        if (isRoating)
        {
            transform.RotateAround(player.transform.position,player.transform.up, rotateSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.transform.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;
            if (x > 80 || x < 10)
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
            offsetion = transform.position - player.transform.position;

        }

    }
}
