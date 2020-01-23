using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookToWardsPlayer : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(player.transform.position.x,
                                      this.transform.position.y + 1,
                                      player.transform.position.z);
        this.transform.LookAt(targetPostition);
    }
}
