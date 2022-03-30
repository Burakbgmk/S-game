using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyByBoundary : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = new Vector3(3.35f, 0, 8);
            other.gameObject.GetComponent<enemyAI>().patrolSpeed = 60f;
            return;
        }
        Destroy(other.gameObject);
    }
}
