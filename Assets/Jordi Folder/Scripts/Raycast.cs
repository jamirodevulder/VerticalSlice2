using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private int Force = 1000;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private ParticleSystem flames;
    [SerializeField] private ParticleSystem sparkle;
    [SerializeField] private ParticleSystem deepFlame;

    void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, ray.direction * 30, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.AddForceAtPosition(ray.direction * Force, hit.point);
                }
                Instantiate(particle, hit.point, Quaternion.identity);
            }
        }
        if (Input.GetMouseButton(1))
        {
            flames.Play();
            sparkle.Play();
            deepFlame.Play();
        }
        else
        {
            flames.Stop();
            sparkle.Stop();
            deepFlame.Stop();
        }
    }
}
