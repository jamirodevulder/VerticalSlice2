﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseGun : MonoBehaviour
{
    [SerializeField] private int force = 1000;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private MouseLook mouselookScript;
    [SerializeField] private PlayerMovment playerMovmentScript;
    [SerializeField] private Camera cam;
    [SerializeField] private UIBullets uiBullets;
    [SerializeField] private ParticleSystem shootParticle;
    [SerializeField] private Sprite vizier1;
    [SerializeField] private Sprite vizier2;
    [SerializeField] Animator animator;

    private void Start()
    {
        crossHair.GetComponent<Image>().sprite = vizier1;
        shootParticle.Stop();
        mouselookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        playerMovmentScript = GameObject.Find("Hands model 0.5").GetComponent<PlayerMovment>();
        
    }

    void Update()
    {

        gun.transform.LookAt(UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10000)));

        if (Input.GetMouseButton(1))
        {
            
            crossHair.GetComponent<Image>().sprite = vizier2;
            playerMovmentScript.canMove = false;
            mouselookScript.cursorLocked = false;
            crossHair.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
         
        }
        else
        {
            crossHair.GetComponent<Image>().sprite = vizier1;
            playerMovmentScript.canMove = true;
            crossHair.GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, Screen.height / 2);
            mouselookScript.cursorLocked = true;
        }
        mouselookScript.ToggleLockstate();
    }
    public void Shoot()
    {
        shootParticle.Play();
        RaycastHit hit;
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawLine(ray.origin, ray.direction * 30, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                print(hit.collider.tag);
                animator.SetTrigger("shoot");
                if (hit.collider.gameObject.tag == "ai")
                {
                    hit.collider.gameObject.GetComponent<Behavouir>().takeDamage(50);
                }

            }


        }

    }
}