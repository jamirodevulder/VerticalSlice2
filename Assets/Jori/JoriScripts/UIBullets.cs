using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBullets : MonoBehaviour
{
    private int currentBullet;
    private Sprite activeBullet;
    private Sprite inactiveBullet;
    [SerializeField] private Image[] bullets = new Image[7];
    [SerializeField] private GameObject[] bulletGameObjects = new GameObject[7];

    private void Awake()
    {
        for (int i = 0; i < bulletGameObjects.Length; i++)
        {
            bulletGameObjects[i] = GameObject.Find("BulletGameObject " + i); //Pakt parent gameobjects van bullets
            bullets[i] = GameObject.Find("BulletGameObject " + i).GetComponentInChildren<Image>(); //Pakt bullet images
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentBullet > 7) // Checkt als bullets boven de 7 is
            {
                currentBullet = 0;
                for (int i = 0; i < bullets.Length; i++)
                {
                    bullets[i].sprite = activeBullet;
                }
            }
            else
            {
                currentBullet += 1;
                bullets[currentBullet].sprite = inactiveBullet;
            }
        }
    }
}