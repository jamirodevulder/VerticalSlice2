using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    [SerializeField] private int force = 1000;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private MouseLook mouselookScript;
    [SerializeField] private PlayerMovment playerMovmentScript;
    [SerializeField] private Camera cam;
    [SerializeField] private UIBullets uiBullets;

    private void Start()
    {
        mouselookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        playerMovmentScript = GameObject.Find("First-Person-Player").GetComponent<PlayerMovment>();
        cam = Camera.main;
    }

    void Update()
    {

        gun.transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10000)));

        if (Input.GetMouseButton(1))
        {

            playerMovmentScript.canMove = false;
            mouselookScript.cursorLocked = false;
            crossHair.GetComponent<RectTransform>().position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Debug.Log("Right mouse pressed");
        }
        else
        {
            playerMovmentScript.canMove = true;
            crossHair.GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, Screen.height / 2);
            mouselookScript.cursorLocked = true;
        }
        mouselookScript.ToggleLockstate();
    }
    public void Shoot()
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
                    rb.AddForceAtPosition(ray.direction * force, hit.point);
                }

            }


        }

    }
}