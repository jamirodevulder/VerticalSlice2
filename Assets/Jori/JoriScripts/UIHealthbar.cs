using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIHealthbar : MonoBehaviour
{
    private int health;
    private int shield;
    private Image healthBar;
    private Image shieldBar;
    [SerializeField] private Sprite[] healthImages;
    [SerializeField] private Sprite[] shieldImages;

    private void Awake()
    {
        health = 0;
        shield = 0;
        healthBar = GameObject.Find("Health").GetComponentInChildren<Image>();
        shieldBar = GameObject.Find("Shield").GetComponentInChildren<Image>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) /* health > 7 */) //De comments hier moeten de inputs vervangen, zodat de health/shield niet hoger gaat dan 7
        {
            //health = 7;
            HealPlayer(1);
        }
        if (Input.GetKeyDown(KeyCode.W) /* shield > 7*/) //Als shield boven de 7 gaat wordt het terug gedaan naar 7
        {
            //Shield = 7;
            DamagePlayer(1);
        }
    }

    void DamagePlayer(int damage) //Health erafhalen
    {
        if (shield < 7)
        {
            shield += damage;
            shieldBar.sprite = shieldImages[shield]; //De full shield  Sprite staat in shieldImages[0] en -1 damage staat in index 1
        }
        else if (shield == 7 && health < 7) //Checkt of shield 7 (geen shield meer) is en of health onder de 7 is (7 betekent geen health meer)
        {
            health += damage; //Zelfde als bij shield
            healthBar.sprite = healthImages[health];
        }
        /*else if(shield == 7 && health == 7) //Voor als je de dood animatie wilt activeren
         {
            death functie hier
         }
        */
    }

    void HealPlayer(int healthToAdd) //Health erbij doen
    {
        if (shield < 7 && shield >= 0)
        {
            shield -= healthToAdd;
            shieldBar.sprite = shieldImages[shield];
        }
        else if (shield == 7 && health < 7)
        {
            health -= healthToAdd;
            healthBar.sprite = healthImages[health];
        }
    }
}