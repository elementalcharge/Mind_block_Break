using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathScaling : MonoBehaviour
{
    [SerializeField] private float sizeScaler = 1f;
    
    [SerializeField] private float brathFlow = 1f;
    
    [SerializeField] private float breathLenght = 10f;
    
    [SerializeField] private float maxScaling = 3f;

    // Start is called before the first frame update
    //state
    private bool _hasStarted;
    //cached 
    private Vector3 _scaleChange;
    private Vector3 maxSize;

    void Start()
    {
        _scaleChange = new Vector3(sizeScaler, sizeScaler, sizeScaler);
        maxSize = new Vector3(maxScaling, maxScaling, maxScaling);
    }
    
    private void CheckBreath()
    {
        if (!_hasStarted) return;
        if (Input.GetKeyUp("space"))
        {
            brathFlow *= -1f;
            Debug.Log("breath changed order");
        }
        Debug.Log("breath condition "+(maxSize.magnitude +" local magnitude:"+ transform.localScale.magnitude) );
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
}
