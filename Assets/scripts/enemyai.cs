using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyai : MonoBehaviour
{
    NavMeshAgent mynavmeshagent;
    [SerializeField] Transform targetposition;
    [SerializeField] float chasethreshold;
    [SerializeField] float radius;
    [SerializeField] bool isprovoked;
    [SerializeField] float chasedistance;
    Animator myanimator;
    AnimatorClipInfo[] clipinfo;
    string currentanimation;
    [SerializeField] Transform player;
    bool delayturnonnavmesh = true;
    bool chasetargetdelay = true;
    //patroling mypatroling;
    bool walking = false;
    int isprovokedcount=0;
    int coroutinedelaycount = 0;
    [SerializeField] float waittime;
    [SerializeField] float thresholddesitanceafterwhichitshouldrunifhit;


    public float getthresholddesitanceafterwhichitshouldrunifhit
    {
        get { return thresholddesitanceafterwhichitshouldrunifhit; }
    }
    public float getchasethreshold
    {
        get { return chasethreshold; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        myanimator = GetComponent<Animator>();
        mynavmeshagent = GetComponent<NavMeshAgent>();
        clipinfo = myanimator.GetCurrentAnimatorClipInfo(0);
        currentanimation = clipinfo[0].clip.name;
        //mypatroling = GetComponent<patroling>();
       /* if(mynavmeshagent==null)
        {
            Debug.Log("null");
        }*/
    }

    // Update is called once per frame
    void Update()
    {
     /*   if(walking)
        { 
       */ 
        //no pint in changing chasetarget, change isprovoked


        //NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) , hit think of it as physics raycast hit, the hit will present the detials of the closes psotion hitted
       chasedistance = Vector3.Distance(transform.position,targetposition.position);
   
       //now what is happenig when we r crossing chasethreshold 6f isprovoked is getting triggered true,but when are reaching or less than stopping distance the enemy start shooting,
       //again after we go out of stopping desitance is provoked is getting false but we neeed a delay in it , also when we are getting out of chase distance isprovoked is getting tvokedrue immediatedly but here also we neeed a delay
       //once provoked the provoed should remain provoked 
       //i think is provoked should be changed
        if (isprovoked)
        {

            /* StartCoroutine();*/
            //not using because someitmes the funcn runs and yield return new waitforsecids is causing to wait and again run ,so slowing and running again
            //hence using timer instead
            /*timer = 0;
            if()*/


            //tried everything only is provoked has to chnages
            engagetarget();
        }
        
        if(chasedistance<= chasethreshold)
        {
            /*mypatroling.getkeepwalking = false;*/
           /* mypatroling.stoppingpseed();*/
            //first to idle animation

            if (chasedistance <= mynavmeshagent.stoppingDistance)
            {
                //mypatroling.getkeepwalking = false;
                myanimator.SetLayerWeight(1, 1);
                myanimator.SetBool("shoot", true);
                //in chasetarget shoot become false but after isprovoked is turned true
            }
            if (chasedistance<=mynavmeshagent.stoppingDistance)
            {
                //problem here for
                //"SetDestination" can only be called on an active agent that has been placed on a NavMesh.
                
                transform.LookAt(new Vector3(player.position.x,this.transform.position.y, player.position.z));
                GetComponent<NavMeshAgent>().enabled = false;
                GetComponent<NavMeshObstacle>().enabled = true;

                isprovoked = false;
            }
           else if(chasedistance > mynavmeshagent.stoppingDistance )
            {
               /* Debug.Log("here");*/
                
                    coroutinedelaycount += 1;
              

                if (delayturnonnavmesh == true)
                {
                    delayturnonnavmesh = false;
             /*       Debug.Log("here in turn on mesh");*/
                    StartCoroutine(turnonnavmesh());
                }
            }
        
        }
       
       if(chasedistance > chasethreshold)
        {
         
           /* transform.LookAt(player);*/
            GetComponent<NavMeshAgent>().enabled = true;

            StartCoroutine(provokedtrue());
          
          
        }
        
        
       /* }*/

    }

    IEnumerator provokedtrue()
    {
        //cause delay when chasedistance > chasethreshold
        yield return new WaitForSeconds(2f);
        
        if (isprovoked)
        {

            isprovoked = true;
        }
        else
        {

            isprovoked = false;
        }
    }



    IEnumerator turnonnavmesh()
    {
          waittime = coroutinedelaycount==1?0.5f:2f;
        /*Debug.Log("the wartime "+waittime);*/
        /// below is for delayturnonnavmesh chasedistance > mynavmeshagent.stoppingDistance
        yield return new WaitForSeconds(waittime);
        //mypatroling.getkeepwalking = false;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshObstacle>().enabled = false;
        isprovoked = true;
        delayturnonnavmesh = true;
    }


    private void engagetarget()
    {
        //stopping distance not chasethreshold
        if(chasedistance > mynavmeshagent.stoppingDistance)
        {


            chasetarget();
        }
     
    }

    /*private void attack()
    {
        
        myanimator.SetLayerWeight(1, 1);
        myanimator.SetBool("shoot",true);
    }*/

    private void chasetarget()
    {

        transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z));
        myanimator.SetBool("shoot", false);
        myanimator.SetLayerWeight(1, 0);
        myanimator.SetBool("enemyrun", true);


        if (mynavmeshagent)
        {
          //enemyai should be destroyed with a delay or dere shoukd be a boolean
            mynavmeshagent.SetDestination(player.position);//if i use startcoruotine thiswould create a problem
        }
    }
    /*IEnumerator chasetarget()
     {

         yield return new WaitForSeconds(3f);

         Debug.Log("after waitforseconds");
         GetComponent<NavMeshAgent>().enabled = true;
         transform.LookAt(player);
         myanimator.SetBool("shoot", false);
         myanimator.SetLayerWeight(1, 0);
         myanimator.SetBool("enemyrun", true);


         if (mynavmeshagent)
         {


             mynavmeshagent.SetDestination(targetposition.position);

         }

         *//*chasetargetdelay = true;*//*
     }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
    //if in case the person is shoot 
    public void isshoot()
    {
        if (isprovokedcount <= 1)
        {
          
            isprovokedcount += 1;
            isprovoked = true;
        }
    }
}
