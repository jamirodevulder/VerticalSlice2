using UnityEngine;
using UnityEngine.UI;

public class UIBullets : MonoBehaviour
{
    private int currentBullet;
    private int displayedBullet;
    private int bulletReserve; //Reservekogels
    private Text bulletText;
    private bool canShoot;
    [SerializeField] private Sprite activeBullet;
    [SerializeField] private Sprite inactiveBullet;
    [SerializeField] private Image[] bullets = new Image[6];
    [SerializeField] private GameObject[] bulletGameObjects = new GameObject[6];
    [SerializeField] private BaseGun baseGunScript;

    private void Start()
    {
        baseGunScript = GameObject.Find("Gun").GetComponent<BaseGun>();
    }
    private void Awake()
    {
        for (int i = 0; i < 7; i++)
        {
            bulletGameObjects[i] = GameObject.Find("BulletGameObject " + i); //Pakt parent gameobjects van bullets
            bullets[i] = GameObject.Find("BulletGameObject " + i).GetComponentInChildren<Image>(); //Pakt bullet images
        }
        bulletText = GameObject.Find("BulletTextGameObject").GetComponentInChildren<Text>();
        currentBullet = -1; // Begint bij -1, zodat de eerste kogel weggehaalt wordt, in plaats van de 2e
        bulletReserve = 41;
        displayedBullet = 7;
        bulletText.text = displayedBullet + " - " + bulletReserve;
        canShoot = true;
    }

    private void Update()
    {
        if (canShoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                baseGunScript.Shoot();
                print("Shot");//Laat de gun schieten
                displayedBullet--; //Haalt een kogel van de teller af
                currentBullet++; //Geeft aan bij welke van de 7 kogels je bent
                if (currentBullet >= 7 && bulletReserve >= 7 && bulletReserve > 0) // Checkt als bullets boven 7 is en of de reserve bullets 7 of meer bullets heeft
                {
                    currentBullet = -1;
                    bulletReserve -= 7;
                    displayedBullet = 7;
                    for (int i = 0; i < 7; i++)
                    {
                        bullets[i].sprite = activeBullet; //Alle sprites worden vervangen met de active sprite
                    }
                }
                else if (currentBullet >= 7 && bulletReserve < 7 && bulletReserve >= 0) // Checkt of de bullets boven 7 is en of er 0 of meer, maar minder dan 7, reservekogels zijn
                {
                    currentBullet = -1;
                    displayedBullet = bulletReserve;
                    bulletReserve = 0;
                    for (int i = 0; i < bulletReserve; i++)
                    {
                        bullets[i].sprite = activeBullet;
                    }
                }
                else
                {
                    bullets[currentBullet].sprite = inactiveBullet;
                    if (bulletReserve <= 0)
                    {
                        bulletReserve = 0;
                    }
                }
                if (displayedBullet <= 0 && bulletReserve <= 0) //Als de bullets op zijn dan wordt de kogel ook niet displayed en kan je niet meer schieten
                {
                    displayedBullet = 0;
                    canShoot = false;
                }
            }
        }
        bulletText.text = displayedBullet + " - " + bulletReserve;

    }
}