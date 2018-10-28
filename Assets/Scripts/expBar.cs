using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expBar : MonoBehaviour {

    public static expBar _instance;
    public UISlider bar;
    private void Awake()
    {
        _instance = this;
        bar = this.GetComponent<UISlider>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void setValue(float value)
    {
        bar.value = value;
    }
}
