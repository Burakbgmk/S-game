using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    [SerializeField] private float moveRate;
    [SerializeField] private int shotRemaining;
    [SerializeField] private int attackRemaining;
    [SerializeField] private float patrolDuration = 5f;
    public int bossTurn;
    public float patrolSpeed;
    public float shootRate;
    public float attackRate;
    public GameObject enemyShot;
    

    AudioSource audioPlayer;
    Rigidbody physic;

    private bool mustPatrol;
    private bool mustAttack;
    private bool mustShoot;
    private bool mustReturn;

    private GameObject[] enemyShotSpawn;
    private GameObject targetShip;
    private Vector3 target;
    private int shotForTurn;
    private int attackForTurn;
    private int bossForTurn;

    private void OnEnable()
    {
        targetShip = GameObject.FindGameObjectWithTag("Player");
        enemyShotSpawn = GameObject.FindGameObjectsWithTag("ShotSpawn");
        audioPlayer = GetComponent<AudioSource>();
        physic = GetComponent<Rigidbody>();
        mustPatrol = true;
        mustAttack = false;
        mustShoot = false;
        mustReturn = false;
        StartCoroutine(Attacks());
    }
    
   
    private void FixedUpdate()
    {
        
        if (mustPatrol)
        {
            Patrol();
        }
        if (mustReturn)
        {
            Return();
        }
        if (target == null || targetShip == null)
        {
            return;
        }
        if (mustAttack)
        {
            Attack();
        }

    }

    void Patrol()
    {
        physic.velocity = new Vector3(-patrolSpeed * Time.fixedDeltaTime, 0, 0);
    }
    void Attack()
    {
        target = new Vector3(targetShip.transform.position.x, 0, 6);
        physic.transform.position = (Vector3.Lerp(transform.position, target, moveRate) + Vector3.MoveTowards(transform.position, target, moveRate))/2;
    }
    void Shoot()
    {
        foreach (GameObject spawn in enemyShotSpawn)
        {
            Instantiate(enemyShot, spawn.transform.position, spawn.transform.rotation);
            audioPlayer.Play();
        }

    }
    void Return()
    {
        physic.transform.position = Vector3.MoveTowards(physic.transform.position, new Vector3(0, 0, 8), moveRate);
    }
    void Turn()
    {
        patrolSpeed *= -1;
    }
    
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "AIbound" && bossForTurn>0)
        {
            Turn();
        }
    }


    IEnumerator Attacks()
    {
        bossForTurn = bossTurn;
        while (bossForTurn>0)
        {
            yield return new WaitForSecondsRealtime(Random.Range(1,patrolDuration));
            mustPatrol = false;
            mustAttack = true;
            attackForTurn = attackRemaining;
            yield return new WaitForSecondsRealtime(1f);
            while (mustAttack == true && attackForTurn > 0)
            {
                mustShoot = true;
                shotForTurn = shotRemaining;
                while (mustShoot == true && shotForTurn > 0)
                {
                    Shoot();
                    yield return new WaitForSecondsRealtime(shootRate);
                    shotForTurn -= 1;
                }
                mustShoot = false;
                yield return new WaitForSecondsRealtime(attackRate);
                attackForTurn -= 1;
            }
            mustAttack = false;
            mustReturn = true;
            yield return new WaitUntil(() => transform.position.z == 8);
            mustReturn = false;
            mustPatrol = true;
            bossForTurn -= 1;
        }
    }
    
}
