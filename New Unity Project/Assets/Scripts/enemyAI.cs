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
    public float attackRate;
    public GameObject enemyShot;
    public GameObject enemyShotSpawn;
    public GameObject targetShip;


    AudioSource audioPlayer;
    Rigidbody physic;

    private bool mustPatrol;
    private bool mustAttack;
    private bool mustShoot;
    private bool mustReturn;
    
    
    private Vector3 target;
    private int shotForTurn;
    private int attackForTurn;
    private int bossForTurn;

    private void OnEnable()
    {
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
        if (mustAttack)
        {
            Attack();
        }
        if (mustReturn)
        {
            Return();
        }
        
    }

    void Patrol()
    {
        physic.velocity = new Vector3(-patrolSpeed * Time.fixedDeltaTime, 0, 0);
    }
    void Attack()
    {
        target = new Vector3(targetShip.transform.position.x, 0, 6);
        physic.transform.position = Vector3.Lerp(transform.position, target, moveRate);
    }
    void Shoot()
    {
        Instantiate(enemyShot, enemyShotSpawn.transform.position, enemyShotSpawn.transform.rotation);
        audioPlayer.Play();
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
            yield return new WaitForSecondsRealtime(patrolDuration);
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
                    yield return new WaitForSecondsRealtime(attackRate);
                    shotForTurn -= 1;
                }
                mustShoot = false;
                yield return new WaitForSecondsRealtime(1f);
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
