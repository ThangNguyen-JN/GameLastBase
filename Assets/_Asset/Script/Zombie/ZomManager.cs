using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ZomManager : MonoBehaviour
{
    public ZombieMovement movement;
    private IZombieState currentState;
    public Transform target;
    public bool canSeePlayer;

    [Header("Patrol Settings")]
    public Vector3 patrolCenter;
    public float patrolWidth;
    public float patrolHeight;
    public float patrolWaitTime = 3f;

    public float stopDistance = 4f;
    public bool playerInZone = false;

    public Animator anim;
    public int damage;

    public bool isSpawning = false;
    public float detectRange= 13f;



    void Start()
    {
        movement = GetComponent<ZombieMovement>();
        ChangeState(new ZombieSpawnState()); //bat dau voi trang thai spawn
    }

    void Update()
    {
        currentState?.UpdateState(this); // goi update trang thai tiep theo
    }

    public void ChangeState(IZombieState newState)
    {
        currentState?.ExitState(this); // thoat trang thai cu
        currentState = newState;       // cap nhat trang thai moi
        currentState?.EnterState(this); // vao trang thai cu
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSpawning) return;
        if (other.CompareTag("PlayerChasing"))
        {
            target = other.transform;
            canSeePlayer = true;
            ChangeState(new ZombieChasingState());
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("PlayerChasing"))
        {
            //target = null;
            //Debug.Log("Player out of range");
            //canSeePlayer = false;
            //ChangeState(new ZombieIdleState());
            //Collider[] playersInZone = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Player"));
            //Debug.Log($"Players in zone count: {playersInZone.Length}");
            //if (playersInZone.Length == 0) // khong con player
            //{
            //    target = null;
            //    Debug.Log("Player out of range");
            //    canSeePlayer = false;
            //    ChangeState(new ZombieIdleState());
            //}
            Debug.Log($"Exited: {other.gameObject.name}, Layer: {LayerMask.LayerToName(other.gameObject.layer)}");
            StartCoroutine(CheckForPlayersAfterDelay());

        }
    }

    private IEnumerator CheckForPlayersAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Chờ một chút để Physics cập nhật

        Collider[] playersInZone = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Player"));

        Debug.Log($"Players in zone count: {playersInZone.Length}");

        if (playersInZone.Length == 1)
        {
            target = null;
            Debug.Log("Player out of range");
            canSeePlayer = false;
            ChangeState(new ZombiePatrolState());
        }
    }



    public Vector3 GetRandomPatrolPoint()
    {
        Vector3 randomPoint = patrolCenter + new Vector3(
           Random.Range(-patrolWidth / 2, patrolWidth / 2),
           0.2f,
           Random.Range(-patrolHeight / 2, patrolHeight / 2)
       );

        return randomPoint;
    }

    public void DealDamage()
    {
        if (target == null)
        {
            Debug.LogWarning("zombie khong co muc tieu");
            return;
        }

        HealthPlayer healthPlayer = target.GetComponentInParent<HealthPlayer>();


        if (healthPlayer != null)
        {
            healthPlayer.TakeDamage(damage);
            Debug.Log("Zombie gay sat thuong: " + damage);
        }
        else
        {
            Debug.LogWarning("muc tieu khong co healthplayer");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(patrolCenter, new Vector3(patrolWidth, 1, patrolHeight));
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, 13f);  

    //    Gizmos.color = Color.red;
    //    if (GetComponent<Collider>() != null)
    //    {
    //        Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);
    //    }
    //}

}
