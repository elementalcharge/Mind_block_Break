using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnityUnits = 16f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float minX = 1f;
    
    // Update is called once per frame
    //state
    private bool hasStarted;
    
    private void Start()
    {
        hasStarted = false;
        
    }

    void Update()
    {
        //Debug.Log(Input.mousePosition.x/Screen.width* screenWidthInUnityUnits);
        float mousePositionInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnityUnits;
        Vector2 paddlePos = new Vector2(mousePositionInUnits, transform.position.y);
        paddlePos.x = Mathf.Clamp(mousePositionInUnits, minX, maxX);
        transform.position = paddlePos;
        
    }

    public void startGame()
    {
        Debug.Log("message start game received");
        hasStarted = true;
    }
    
}
