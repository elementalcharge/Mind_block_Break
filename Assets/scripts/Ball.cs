using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //config parameters
    [SerializeField] Paddle paddle1;

    [SerializeField] float rightImpulse = 10f;
    [SerializeField] float upperImpulse = 15f;
    [SerializeField] float breathLenght = 10f;
    [SerializeField] AudioClip[] ballSounds;


    //state
    Vector2 paddleToBallVector;
    private float brathFlow = 1f;
    bool hasStarted;
    private float breath;

    //cached component reference
    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        hasStarted = false;
        paddleToBallVector = transform.position - paddle1.transform.position;
        breath = 0;
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        LockBallToPaddle();
        LaunchOnMouseClick();
        checkBreath();
    }

    private void checkBreath()
    {
        if (hasStarted)
            {
            if (Input.GetKeyUp("space"))
                {
                brathFlow *= -1f;
                }
            breath += breathLenght*Time.deltaTime;
            Debug.Log(breath);

            }
    }

    private void LockBallToPaddle()
    {
        if (!hasStarted )
            { 
            Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
            transform.position = paddlePos + paddleToBallVector;
            }
    }
    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0) && hasStarted != true)
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(rightImpulse, upperImpulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
        }
    }
}
