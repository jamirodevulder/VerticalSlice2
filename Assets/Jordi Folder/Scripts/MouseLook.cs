﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]private float mouseSensitivity = 100f;
    [SerializeField]private Transform playerBody;
    [SerializeField]private Transform gun;
    [SerializeField]public bool cursorLocked = true;
    

    float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (cursorLocked)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -85f, 85f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }

    public void ToggleLockstate()
    {
        if (cursorLocked == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {

            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
