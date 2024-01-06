using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Player;
    public LayerMask WhatIsGround, whatIsPlayer;
    public GameObject AtkSomthing;
    public float Health;
    //Patroling
    public Vector3 WalkPoint;
    bool WalkPointSet;
    public float walkpointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool PlayerInSightRange, playerInAttackRange;

    private void Awake()
    {
        Player = GameObject.Find("PlaterObj").transform;
        agent = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        //Check for attack Range
        PlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!PlayerInSightRange && !playerInAttackRange) Patroling();
        if (PlayerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInAttackRange) AttackPlayer();
    }
    private void Patroling()
    {
        if (!WalkPointSet) SearchWalkPoint();

        if (WalkPointSet) agent.SetDestination(WalkPoint);
        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f) WalkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkpointRange, walkpointRange);
        float randomX = Random.Range(-walkpointRange, walkpointRange);

        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
            WalkPointSet = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(Player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(Player);
        if (!alreadyAttacked)
        {

            ///Attack code here
            Rigidbody rb = Instantiate(AtkSomthing,transform.position,Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void TakeDamage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
