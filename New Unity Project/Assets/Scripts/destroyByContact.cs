using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private gameController GameController;

    private void Start()
    {
        GameController = GameObject.FindWithTag("GameController").GetComponent<gameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Boundary" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "AIbound")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        
        if(other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameController.GameOver();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);

        GameController.UpdateScore();
    }
}
