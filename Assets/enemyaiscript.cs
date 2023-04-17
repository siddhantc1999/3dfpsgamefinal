using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyaiscript : MonoBehaviour
{
    NavMeshAgent mynavmeshagent;
    [SerializeField] GameObject player;
    Animator myanimator;
    [SerializeField] float chasedistance;
    AnimatorClipInfo[] animatorclipinfo;
    public string currentanimation;
    public float chasethreshold;
    public bool runvalue;
    public bool keepdelay=true;

    public int health = 5;
    bool isdead = false;
    bool finaldead = false;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        mynavmeshagent = GetComponent<NavMeshAgent>();
        chasedistance = Vector3.Distance(player.transform.position,transform.position);
        myanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animatorclipinfo = myanimator.GetCurrentAnimatorClipInfo(0);
        currentanimation = animatorclipinfo[0].clip.name;
        //mynavmeshagent.SetDestination();
        //chasedistance = Vector3.Distance(player.transform.position, transform.position);
        //if(chasedistance<=)
        //{

        //}

        chasedistance = Vector3.Distance(player.transform.position, transform.position);
        runvalue = myanimator.GetBool("run");
        if(health<=0)
        {
            if (isdead)
                return;
            else
            {
                isdead = true;
                myanimator.SetTrigger("death");
            }
        }
        if (chasedistance<= chasethreshold && runvalue!=true)
        {
            
            myanimator.SetBool("run",true);
            mynavmeshagent.SetDestination(player.transform.position);
            transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
        }
        if(chasedistance<= mynavmeshagent.stoppingDistance)
        {
            myanimator.SetBool("shoot", true);
        }
        else
        {
            if (keepdelay)
            {
                keepdelay = false;
                StartCoroutine(turnoff());
            }
         
        }
        if(runvalue == true && myanimator.GetBool("shoot")==false)
        {
            mynavmeshagent.SetDestination(player.transform.position);
            transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
        }
        if (health <= 0)
        {
            finaldead = true;
            if (finaldead)
            {
                StartCoroutine(enemydie());
            }
        }
    }

   IEnumerator enemydie()
    {
       yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        finaldead = false;
    }

    IEnumerator turnoff()
    {
        yield return new WaitForSeconds(2f);
        keepdelay = true;
        myanimator.SetBool("shoot", false);
    }
}
