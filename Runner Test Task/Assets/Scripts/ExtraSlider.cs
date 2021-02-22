using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraSlider : MonoBehaviour
{
    private Slider slider;
    void Start()
    { 
        slider = gameObject.GetComponent<Slider>();
    }

    
    void Update()
    {
        slider.value -= 0.05f * Time.deltaTime;
    }

    public void Bonus()
    {
        slider.value += 0.4f;
    }
}
