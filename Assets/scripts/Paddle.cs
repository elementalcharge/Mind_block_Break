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
    private GameSession _gameSession;
    private Ball _ball;
    
    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        //Debug.Log(Input.mousePosition.x/Screen.width* screenWidthInUnityUnits);
        
        Vector2 paddlePos = new Vector2(transform.position.x , transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
        
    }

    private float GetXPos()
    {
        if (_gameSession.IsAutoPlayEnabled())
        {
            return _ball.transform.position.x;

        }
        else
        {
            
            return Input.mousePosition.x / Screen.width * screenWidthInUnityUnits;
        }
    }
    
    
}
