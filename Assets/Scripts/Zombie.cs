using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    Girl Girl;
    [SerializeField]
    NavMeshAgent ZombieAgent, GirlAgent;
    
    public float Damage;

    [SerializeField]
    Animator Animator;

    [SerializeField]
    float AttackRange;

    [SerializeField]
    float DropChance;

    [SerializeField]
    GameObject Drop;
    void Start()
    {
        ZombieAgent = GetComponent<NavMeshAgent>();
        
        Girl = FindObjectOfType<Girl>();
        if (Girl != null)
            GirlAgent = Girl.GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        if(GirlAgent == null)
        {
            Girl = FindObjectOfType<Girl>();
            if(Girl != null)
                GirlAgent = Girl.GetComponent<NavMeshAgent>();
            else
            {
                ZombieAgent.SetDestination(transform.position);
                Animator.SetInteger("State", 0);
                return;
            }  
        }
        NavMeshHit hit;
        NavMesh.SamplePosition(GirlAgent.transform.position, out hit, 0.1f, NavMesh.AllAreas);
        if (hit.mask == 8)
        {
            transform.LookAt(Girl.transform);
            if(Vector3.Distance(transform.position, Girl.transform.position) <= AttackRange)
            {
                ZombieAgent.SetDestination(transform.position);
                Animator.SetInteger("State", 2);
            }
            else
            {
                ZombieAgent.SetDestination(Girl.transform.position);
                Animator.SetInteger("State", 1);
            }
            
        }
        else
        {
            ZombieAgent.SetDestination(transform.position);
            Animator.SetInteger("State", 0);
        }
            
    }

    public void Die()
    {
        if(Random.value < DropChance && Drop != null)
            Instantiate(Drop, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
