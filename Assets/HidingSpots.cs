using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpots : MonoBehaviour
{

    private GameObject placeHolder;
   
    public void claim(GameObject ai)
    {
        if (PossibleToclaim(ai))
        {
            placeHolder = ai;
        }
    }
    
    public bool PossibleToclaim(GameObject ai)
    {
        return (placeHolder == null || placeHolder == ai);
        /*
        if (placeHolder == null || placeHolder == ai)
            return true;
        else
            return false;
        */
    }

    public bool claimedByMe(GameObject ai)
    {
        return placeHolder == ai;    
    }
  
}
