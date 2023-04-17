using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cameramovement : MonoBehaviour
{
    [SerializeField] GameObject target;
    public Vector3 offset;
    public Vector3 previousposition;
    public Vector3 nextposition;
    public Vector3 threshold;
    public float thresholdy;
    public float yrotationvalue;
    public float xrotationvalue;
    public Joystick myjoystick;
    public float mouseymin;
    public float mouseymax;
    public float clampedxangle;
    public float xrotate;
    public float yrotate;
    [SerializeField] GameObject direction;
    [SerializeField] GameObject upperbody;
    public Vector2 mouspos;
    public Vector2 screenpos;
    public float xoffset=0f;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("the angles " + Quaternion.Euler(new Vector3(0f,30f,0f)));
       
    }

    // Update is called once per frame
    void Update()
    {
     //if  joystick value is greater than 0 than do not rotate

#if !UNITY_ANDROID
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            if (Input.GetMouseButtonDown(1))
            {
            Debug.Log("here");
                previousposition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButton(1))
            {
                threshold = previousposition - Camera.main.ScreenToViewportPoint(Input.mousePosition);


            }
            else
            {
                previousposition = Vector3.zero;
                threshold = Vector3.zero;
            }
            rotation();
           
        

        }

#endif

        /*#if !UNITY_ANDROID*/
        if (Input.touches.Length == 1)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && (myjoystick.Horizontal==0 && myjoystick.Vertical == 0))
            {


                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {

                    previousposition = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                    Debug.Log("previous position "+ previousposition);
                   
                }
                else
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {


                    nextposition= Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                    threshold = previousposition - Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
                    

                }
                else if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    previousposition = Vector3.zero;
                    threshold = Vector3.zero;
                }
                rotation();



            }
        }
        else
        if (Input.touches.Length > 1)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
            {


                if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(1).phase == TouchPhase.Began)
                {

                    previousposition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    threshold = previousposition - Camera.main.ScreenToViewportPoint(Input.mousePosition);


                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    previousposition = Vector3.zero;
                    threshold = Vector3.zero;
                }
                rotation();
              



            }
        }

        /*
        #endif*/



    }

    public void rotation()
    {
        xrotate = threshold.y * 7f;
        yrotate = threshold.x * 7f;
        transform.Rotate(xrotate, yrotate, 0f);
        direction.transform.Rotate(0f, yrotate, 0f);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
      /*  upperbody.transform.eulerAngles = new Vector3(xrotate, 0f, upperbody.transform.eulerAngles.z);*/
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x+ xoffset, target.transform.position.y, target.transform.position.z);
    }
   
}
