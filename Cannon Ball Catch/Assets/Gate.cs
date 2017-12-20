using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject mcamera;
    public GameObject destination;
    
    void OnTriggerEnter(Collider mcamera)
    {
        mcamera.transform.position = destination.transform.position;
    }
}
