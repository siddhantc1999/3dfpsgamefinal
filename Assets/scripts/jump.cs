using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnMouseDown()
    //{
    //    Debug.Log("here in down");
    //   /* FindObjectOfType<charactermove>().playerjump();*/
    //}
    public void playerjump()
    {
        FindObjectOfType<charactermove>().playerjump();
    }
}
