using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capsule : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = default;
    [SerializeField] private float yJumpSpeed = 7;
    [SerializeField] private float xJumpSpeed = -5;
    [SerializeField] private Animator animator = default;
    [SerializeField] private Slider slider = default;

    private ExtraSlider extraSlider;

    private bool isGrounded;
    private bool isClicked;
    private bool isSwiped;
    private bool superFlip;
    
    private int flipsCounter;

    private void Start()
    {
        extraSlider = slider.GetComponent<ExtraSlider>();
    }

    public void TouchEvent()
    {
        isClicked = true;
        superFlip = false;
        if (isGrounded)
        {
            DOFlip(); 
        }
    }

    public void SwipeUpEvent()
    {
        isSwiped = true;
        superFlip = true;
        if (isGrounded)
        {
            DOFlip();  
        }
    }

    private void DOFlip()
    {
        if (superFlip)
        {
            float value = slider.value;
            
            yJumpSpeed += value;
            xJumpSpeed -= value * 2;
            
            value = 0;
            slider.value = value;
        }
        else
        {
            extraSlider.Bonus();
            yJumpSpeed += 0.1f;
            xJumpSpeed -= 0.2f; 
        }
        rb.velocity = new Vector3(xJumpSpeed, yJumpSpeed, 0);
        
        int flips = (int) (2 * yJumpSpeed/4);
        
        if (flips % 2 == 0)
        {
            flips++;
        }
        flipsCounter = 0;
        
        animator.SetBool("Flip", true);
        StartCoroutine(CheckFlips(flips));
        
        
        isSwiped = false;
        isClicked = false;
    }
    
    IEnumerator CheckFlips(int flips)
    {
        while (true)
        {
            if (flipsCounter >= flips)
            {
                animator.SetBool("Flip", false);
                break;
            }
            yield return null;
        }
    }

    public void FlipsCount()
    {
        flipsCounter++;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
        
        if (isClicked || isSwiped)
        {
            DOFlip();
        }

        isSwiped = false;
        isClicked = false;
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
}
