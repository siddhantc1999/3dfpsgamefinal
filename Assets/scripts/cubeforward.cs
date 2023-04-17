using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeforward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position;
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);
    
    }
}
