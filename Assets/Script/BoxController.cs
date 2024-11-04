using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 boxTarget;
    public float boxSpeed;

    void Start()
    {
        boxTarget = transform.position;
        if(boxSpeed <= 0) {
            boxSpeed = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, boxTarget, boxSpeed * Time.deltaTime);
    }
}
