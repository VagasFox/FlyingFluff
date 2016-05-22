using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HaneRotation : MonoBehaviour
{
    //private int count;
    
    void Start()
    {

    }

    void Update()
    {
        //count++;

        transform.Rotate(new Vector3(0f, 0f, 5));
    }
}
