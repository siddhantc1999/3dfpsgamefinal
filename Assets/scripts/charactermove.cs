using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class charactermove : MonoBehaviour
{
    //public CharacterController mycharactercontroller;
    public float sideways;
    public float forward;
    public float xmouse;
    public float ymouse;
    //public Transform upperbody;
    public float rotationvalue;
    Animator myanimator;
    Rigidbody myrigidbody;
    public Joystick myjoystick;
    public float zvalue;
    public float xvalue;
    public GameObject directionpointer;
    bool isgrounded;
    [SerializeField] Transform targetposition;
    int layermask;
    int layermask1;
    AnimatorClipInfo[] myanimatorclipinfo;
    string currentanimation;
    Ray ray;
    [SerializeField] GameObject myparticlesystem;
    [SerializeField] Transform particlepos;
    [SerializeField] GameObject bloodenemy;
    public int blood;
    [SerializeField] Transform bloodparent;
    // Start is called before the first frame update
    void Start()
    {
        myanimator = GetComponent<Animator>();
        myrigidbody = GetComponent<Rigidbody>();
        layermask = LayerMask.GetMask("target");
        layermask1= LayerMask.GetMask("ground");
    }

    // Update is called once per frame
    void Update()
    {

        //myanimatorclipinfo = myanimator.GetCurrentAnimatorClipInfo(0);
        //currentanimation = myanimatorclipinfo[0].clip.name;
        //if (currentanimation != "Firing Rifle 1")
        //{
        //    myrig.weight = 0f;
        //}
        //ray = new Ray(Camera.main.transform.position,Camera.main.transform.forward*100f);
        ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));

        
        Debug.DrawRay(ray.origin,ray.direction*100f,Color.black);
        if(Physics.Raycast(ray,out RaycastHit myhitinfo,layermask))
        {
            Debug.Log("raycast");
           /* Debug.Log("the name "+ myhitinfo.rigidbody.gameObject.tag);*/
          /*  if (myhitinfo.rigidbody.gameObject.tag == "targets")
            {
                Debug.Log("here in targets");
            }*/
        }

        sideways = myjoystick.Horizontal;
        //forward = Input.GetAxisRaw("Vertical");
        forward = myjoystick.Vertical;
        //Debug.DrawRay();
        //////////////////////////////////////////
        if (forward != 0)
        {
            
           
            if (forward > 0.9f || forward < -0.9f)
            {

                if (forward < 0f)
                {
                    zvalue = -1f;
                }
                else
                if(forward>0f)
                {
                    zvalue = 1f;
                }
            }
            else
            {
              
                zvalue = forward;
            }
        }
        else
        if (forward == 0)
        {
            zvalue = 0f;
        }
        ////////////////////////////////////////////////


        if (sideways != 0)
        {
            xvalue = sideways;
        }
        else
        {
            xvalue = 0f;
        }
          
        myanimator.SetFloat("movez", zvalue);
        myanimator.SetFloat("movex", xvalue);
        if (sideways != 0 && forward != 0)
        {

            transform.forward = directionpointer.transform.forward;
           /* Vector3 forwardmovement = transform.forward;
            myrigidbody.velocity = new Vector3(sideways*2f, myrigidbody.velocity.y, forward* 2f);*/
           //we have to add here up 
            myrigidbody.velocity = (transform.forward * 2f* forward)+(transform.right * 2f * sideways);
        }
        else
        {
            myrigidbody.velocity = new Vector3(sideways, myrigidbody.velocity.y, forward);
        }

        ///////////////////////////////////////////////


        
    }
    public void playerjump()
    {
        if (isgrounded)
        {

            myrigidbody.velocity = new Vector3(myrigidbody.velocity.x, 4f, myrigidbody.velocity.z);


            myanimator.SetTrigger("jump");
        }
    }
    public void playershoots()
    {
        transform.forward = directionpointer.transform.forward;

        
        myanimator.SetTrigger("fire");
            //if(Physics.Raycast(ray.origin,ray.direction,100f))
        StartCoroutine(Instantiatefire());
        if (Physics.Raycast(ray, out RaycastHit myhitinfo, 100f, layermask))
        {
          

            GameObject enemyblood = Instantiate(bloodenemy, new Vector3(myhitinfo.point.x, myhitinfo.point.y,myhitinfo.point.z), Quaternion.LookRotation(myhitinfo.normal));
            enemyblood.transform.parent = bloodparent.transform;
            myhitinfo.rigidbody.gameObject.GetComponent<enemyaiscript>().health -= 1;
            /*Debug.Log("target posititon "+targetposition.position);
            Debug.Log("target posititon " + myhitinfo.transform.position);*/
        }


    }

    IEnumerator Instantiatefire()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject newparticlesystem = Instantiate(myparticlesystem, particlepos.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("name "+collision.gameObject.name);
        if (collision.collider.gameObject.layer== layermask1)
        {
            
            isgrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.name == "ground")
        {
            isgrounded = false;
        }
    }
}
