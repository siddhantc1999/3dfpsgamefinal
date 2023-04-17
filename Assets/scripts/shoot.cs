using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class shoot : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void shootbullet()
    //{
    //    FindObjectOfType<charactermove>().playershoots();
    //}
    //public void OnPointerDown()
    //{
    //    Debug.Log("here");
    //}
    public void shot()
    {
        FindObjectOfType<charactermove>().playershoots();
        //StartCoroutine();
       


    }
    
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    //}


}
