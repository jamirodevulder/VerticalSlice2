using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class behavouir : MonoBehaviour
{
    private bool foundPlayer = false;
    private GameObject player;
    private NavMeshAgent agent;
    private bool alreadySeen = false;
    [SerializeField] private float maxRange;
    [SerializeField] private float range;
    [SerializeField] private float rotateSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
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
    }


    //first step check if player is inrange of enemie
    private void OnTriggerEnter(Collider other)
    {
        // checks for player in range if so it will make foundplayer true
        if(other.gameObject.tag == "Player")
        {
            foundPlayer = true;
            player = other.gameObject;
        }
    }


    // if ai didnt see the player with his "eyes" and it is out of range it will set foundplayer to false and all steps will be reset than. will do nothing if ai already seen the player with his "eyes"
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            foundPlayer = false;
        }
           
    }

    // third step it will check if the AI is possible to see the player when he is in range and will walk towards to the player.
    public void CheckIfISeePlayer()
    {
        NavMeshHit hit;
        if (!agent.Raycast(player.transform.position, out hit))
        {
            if(!alreadySeen)
            {
                alreadySeen = true;
            }
            walkTowardsPlayer(true);
        }
        else if(alreadySeen)
        {
            walkTowardsPlayer(false);
            
        }
    }


    // will walk to the player when it get called if the player is seen with the eye of the AI it will shoot if it is inrange
    private void walkTowardsPlayer(bool canShoot)
    {

        var heading = player.transform.position - transform.position;


        if (canShoot && heading.sqrMagnitude < maxRange)
        {
            ReadyToShoot();
        }
        else if (heading.sqrMagnitude > range)
        {
            agent.isStopped = false;

            agent.destination = player.transform.position;
            LookToPlayer();
        }





    }
    

    private void ReadyToShoot()
    {
        agent.isStopped = true;
        print("pew pew");

        LookToPlayer();
    }
    private void LookToPlayer()
    {
        Vector3 targetPostition = new Vector3(player.transform.position.x,
                                      this.transform.position.y,
                                      player.transform.position.z);
        this.transform.LookAt(targetPostition);
    }
}
