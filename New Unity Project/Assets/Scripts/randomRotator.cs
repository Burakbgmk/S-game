using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotator : MonoBehaviour
{
    Rigidbody physic;
    [SerializeField] int speed;
    void Start()
    {
        physic = GetComponent<Rigidbody>();
        speed = Random.Range(2, 15);
        physic.angularVelocity = (Random.insideUnitSphere)*speed;
    }

    
}
