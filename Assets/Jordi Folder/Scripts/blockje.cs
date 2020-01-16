using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockje : MonoBehaviour
{
    public Rigidbody rb;
    public long force = -10000000000000000;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0, force, 0)); 
    }
}
