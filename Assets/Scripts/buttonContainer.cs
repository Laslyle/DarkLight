using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonContainer : MonoBehaviour {

	// Use this for initialization
    public void  OnNewGame() {
        PlayerPrefs.SetInt("DateFromSave", 0);

    }

    // Update is called once per frame
    public void OnLoadGame() {
        PlayerPrefs.SetInt("DateFromSave", 1);	
	}
}
