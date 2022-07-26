using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathScaling : MonoBehaviour
{
    [SerializeField] private float sizeScaler = 1f;
    
    [SerializeField] private float brathFlow = 1f;
    
    [SerializeField] private float breathLenght = 10f;
    
    [SerializeField] private float maxScaling = 3f;

    [SerializeField] private float cooldownTimeBetweenInversion = 1.3f;

    //auto play configuration
    private float timedInvertionForAutoplay = 1.5f;
    private float cooldownBetweenInversion = 1.5f;
    private float timeRemaining;
    // Start is called before the first frame update
    //state
    private bool _hasStarted;
    //cached 
    private Vector3 _scaleChange;
    private Vector3 maxSize;
    private GameSession _gameSession;

    void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _scaleChange = new Vector3(sizeScaler, sizeScaler, sizeScaler);
        maxSize = new Vector3(maxScaling, maxScaling, maxScaling);
        
        timeRemaining = timedInvertionForAutoplay;
    }
    
    private void CheckBreath()
    {
        if (!_hasStarted) return;
        invertFlow();
        //Debug.Log("breath condition "+(maxSize.magnitude +" local magnitude:"+ transform.localScale.magnitude) );
        if ( (maxSize.magnitude >= transform.localScale.magnitude && brathFlow >0)  ||
             ( (transform.localScale.x > 0 && transform.localScale.y > 0 && transform.localScale.z > 0 ) && brathFlow <0 ) )
        {
            transform.localScale += (_scaleChange * (brathFlow*Time.deltaTime/breathLenght));
        }
        

        //Debug.Log(breath);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _hasStarted != true)
        {
            _hasStarted = true;
        }

        CheckBreath();
    }
    
    private void invertFlow()
    {
        cooldownBetweenInversion -= Time.deltaTime;

        if (_gameSession.IsAutoPlayEnabled())
        {
            timedInvertion();
        }
        else if (Input.GetKeyUp("space") && cooldownBetweenInversion<=0)
        {
            brathFlow *= -1f;
            cooldownBetweenInversion = cooldownTimeBetweenInversion;
            //Debug.Log("breath changed order");
        }
    }
    
    private void timedInvertion()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            brathFlow *= -1f;
            timeRemaining = timedInvertionForAutoplay;
        }
    }
}
