using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    gameController GameController;
    Rigidbody physic;
    [SerializeField] float speed;
    void Start()
    {
        GameController = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        physic = GetComponent<Rigidbody>();
        if (physic.gameObject.tag == "Asteroids")
        {
            speed = Random.Range(-4, -7) * (1+(GameController.score+1)/100);
        }
        physic.velocity = transform.forward * speed;
        
    }
}

