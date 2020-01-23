using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gethit : MonoBehaviour
{
    [SerializeField]private Image image;
    private float trans;
    // Start is called before the first frame update
    void Start()
    {
        gothit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gothit()
    {
        trans = 100f;
        while (image.color.a != 0)
        {
            trans -= 0.00001f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, trans);
        }
    }
}
