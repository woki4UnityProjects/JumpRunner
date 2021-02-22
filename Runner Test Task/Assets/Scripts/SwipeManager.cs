using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.Events;

public class SwipeManager : MonoBehaviour
{
    public UnityEvent swipeUpEvent;
    public UnityEvent touchEvent;

    private const float MIN_SWIPE = 100;
    private enum Directions
    {
        Left,
        Right,
        Down,
        Up
    };
    
    private bool[] swipes = new bool[4];

    private Vector2 startTouch; // позиция тача
    private Vector2 swipeDelta; // вектор свайпа
    private bool isTouchMoved; // начался свайп

    Vector2 touchPosition() { return Input.mousePosition; }
    
    bool isTouchedDown() { return Input.GetMouseButtonDown(0); }
    
    bool isTouchedUp() { return Input.GetMouseButtonUp(0); }
    
    bool isTouching() { return Input.GetMouseButton(0); }
    
    void Update()
    {
        if (isTouchedDown())
        {
            isTouchMoved = true;
            startTouch = touchPosition();
        }

        if (isTouchedUp() && isTouchMoved)
        {
            isTouchMoved = false;
            SendSwipe();
        }
        
        if (isTouching() && isTouchMoved)
        {
            swipeDelta = startTouch - touchPosition();
            
        }

        if (swipeDelta.magnitude > MIN_SWIPE)
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                // свайп лево / право
                swipes[(int) Directions.Left] = swipeDelta.x > 0;
                swipes[(int) Directions.Right] = swipeDelta.x < 0;
            }
            else
            {
                // свайп верх / низ
                swipes[(int) Directions.Down] = swipeDelta.y > 0;
                swipes[(int) Directions.Up] = swipeDelta.y < 0;
            }
            SendSwipe();
        }
    }

    private void SendSwipe()
    {
        if (swipes[0] || swipes[1] || swipes[2] || swipes[3]) 
        {
            if (swipes[3]) // если свайп вверх
            {
                swipeUpEvent.Invoke();
            }
        }
        else // иначе тач
        {
            touchEvent.Invoke();
        }
        Reset();
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isTouchMoved = false;
        
        for (int i = 0; i < swipes.Length; i++)
        {
            swipes[i] = false;
        }
    }
}
