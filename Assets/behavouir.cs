using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Behavouir : MonoBehaviour
{
    private bool foundPlayer = true;
    private GameObject player;
    private NavMeshAgent agent;
    private bool alreadySeen = false;
    private int ammoClip = 7;
    private int maxAmmo = 7;
    private int highest;
    private bool goingToHide = false;
    private bool reload = false;
    private bool hidereload = false;
    private bool allowedToShoot = true;
    private float health = 100;

    [SerializeField] private Transform[] hideplaces;
    [SerializeField] private float randomnumber;
    [SerializeField] private HidingSpots[] hidingSpotsScript;
    [SerializeField] private ParticleSystem shooting;

    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public void SetFoundplayer(bool setbool)
    {
        foundPlayer = setbool;
    }

    // Update is called once per frame
    void Update()
    {
        //second step call checkifIseePlayer() when first step  foundplayer is set to true.
        if(foundPlayer || alreadySeen)
        {
            CheckIfISeePlayer();
        }
        LookToPlayer();
    }


    // third step it will check if the AI is possible to see the player 
    public void CheckIfISeePlayer()
    {
        NavMeshHit hit;
        if (!agent.Raycast(player.transform.position, out hit))
        {
            print("ik zie je");
            if (randomnumber >= 50)
            {
                if (!alreadySeen)
                {
                    alreadySeen = true;
                }
                if (!hidereload)
                {
                    agent.isStopped = true;
                }
                goingToHide = false;
                ReadyToShoot();
            }
            else
            {
                Hide();
                if (Vector3.Distance(transform.position, agent.destination) <= 1f)
                {
                    if (!alreadySeen)
                    {
                        alreadySeen = true;
                    }
                    if (!hidereload)
                    {
                        agent.isStopped = true;
                    }
                    goingToHide = false;
                    ReadyToShoot();
                }
                else
                {
                    Hide();
                }
            }
        }
        else if(alreadySeen && !goingToHide)
        {
            Hide();
        }
    }

    private void Hide()
    {
        goingToHide = true;
        float[] distance = new float[hideplaces.Length];
        
        float checker;
        checker = float.MaxValue;
        for (int i = 0; i < hideplaces.Length; i++)
        {
            distance[i] = Vector3.Distance(gameObject.transform.position, hideplaces[i].position);
            if(checker > distance[i] && hidingSpotsScript[i].PossibleToclaim(gameObject))
            {
                checker = distance[i];
                highest = i;
            }
        }
        hidingSpotsScript[highest].claim(gameObject);
        if (hidingSpotsScript[highest].claimedByMe(gameObject))
        {
            agent = GetComponent<NavMeshAgent>();
            hidingSpotsScript[highest].claimedByMe(gameObject);
            agent.destination = hideplaces[highest].position;
            agent.isStopped = false;
        }
        if(reload)
        {
            StartCoroutine(reloadTimer());
        }
        
       
    }

    private IEnumerator reloadTimer()
    {
        yield return new WaitForSeconds(4);
        ammoClip = maxAmmo;
        hidereload = false;
        reload = false;
        StopCoroutine(reloadTimer());
    }

    private void ReadyToShoot()
    {
        if (!reload)
        {
            agent.isStopped = true;
            LookToPlayer();
            if (allowedToShoot)
            {
                StartCoroutine(isAllowedToShoot());
            }
        }
        else if(!hidereload && reload)
        {
            Hide();
            hidereload = true;
            LookToPlayer();
        }
    }

    private IEnumerator isAllowedToShoot()
    {
        allowedToShoot = false;
        yield return new WaitForSeconds(0.5f);
        ammoClip--;
        if (ammoClip <= 0)
        {
            reload = true;
        }
        allowedToShoot = true;
        shooting.Play();
        print("ik schiet maar laat niks zien");
    }

    private void LookToPlayer()
    {
        Vector3 targetPostition = new Vector3(player.transform.position.x,
                                      this.transform.position.y,
                                      player.transform.position.z);
        this.transform.LookAt(targetPostition);
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
