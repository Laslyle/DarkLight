using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour {
    private bool isAnyKey = false;
    private GameObject buttonContainer;

	// Use this for initialization
	void Start () {
        buttonContainer = this.transform.parent.Find("buttonContainer").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAnyKey)
        {
            if (Input.anyKey)
            {
                buttonContainer.SetActive(true);
                this.gameObject.SetActive(false);
                
            }
        }
	}
}
