using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask scorePadLayer;
    public bool inPlace = false;

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, .1f, scorePadLayer)) {
            //Debug.Log("in place!");
            inPlace = true;
        }
        else {
            inPlace = false;
        }
    }
}
