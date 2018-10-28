using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMap : MonoBehaviour {
    public Camera cam;

    public void addSize()
    {
        cam.fieldOfView-=3;
    }
    public void deleteSize()
    {
        cam.fieldOfView+=3;

    }
}
