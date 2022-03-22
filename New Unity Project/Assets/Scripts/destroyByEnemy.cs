using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyByEnemy : MonoBehaviour
{
    private gameController GameController;
    public GameObject explosion;
    public GameObject playerExplosion;
    void Start()
    {
        GameController = GameObject.FindWithTag("GameController").GetComponent<gameController>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boundary" || other.gameObject.tag == "Asteroids" || other.gameObject.tag == "AIbound")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.gameObject.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameController.GameOver();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
