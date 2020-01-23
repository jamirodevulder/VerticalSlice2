using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gethit : MonoBehaviour
{
    [SerializeField]private Image image;
    private float trans;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (hit)
        {
            Color c = image.color;
            c.a = trans;
            c.a -= 0.01f;
            trans -= 0.01f;
            image.color = c;
            if(image.color.a <= 0)
            {
                hit = false;
            }
        }
    }
    public void gothit()
    {
        trans = 0.75f;
        hit = true;

    }
  
}
