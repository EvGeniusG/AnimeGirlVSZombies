using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Girl : MonoBehaviour
{
    public float MaxHP;
    float HP;
    public Transform HitPointBar;
    public GameObject Ragdoll;
    NavMeshAgent GirlAgent;
    Joystick Joystick;
    public float AttackDistance;
    bool Shooting = false;
    public GameObject Bullet;
    public Transform bulletSpawn;
    public Animator Animator;
    [HideInInspector]
    public int Drop = 0;
    DropCounter DC;
    void Start()
    {
        GirlAgent = GetComponent<NavMeshAgent>();  
        Joystick = FindObjectOfType<Joystick>();
        Joystick.ReStart();
        HP = MaxHP;
        DC = FindObjectOfType<DropCounter>();
        HitPointBar = GameObject.FindGameObjectWithTag("HitPoints").transform;
        HitPointBar.localScale = Vector3.one;
    }

    
    void FixedUpdate()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(GirlAgent.transform.position, out hit, 0.1f, NavMesh.AllAreas);
        GirlAgent.SetDestination(transform.position + new Vector3(Joystick.Horizontal, 0, Joystick.Vertical));
        if(Joystick.Direction.magnitude > 0)
        {
            transform.LookAt(transform.position + GirlAgent.velocity);
            Animator.SetFloat("Speed", new Vector3(Joystick.Horizontal, 0, Joystick.Vertical).magnitude);
            Animator.SetBool("Aim", false);
        }
        else
        {
           
            Transform Enemy = ClosestEnemy();
            if(Enemy != null && hit.mask == 8)
            {
                transform.LookAt(Enemy);
                StartCoroutine(MakeBullet());
                Animator.SetFloat("Speed", 0);
                Animator.SetBool("Aim", true);
            }
            else
            {
                Animator.SetFloat("Speed", 0);
                Animator.SetBool("Aim", false);
            }

        }
        if(hit.mask == 16 && Drop > 0)
        {
            DC.AddBonus(Drop);
            Drop = 0;
        }
    }
    IEnumerator MakeBullet()
    {
        if (!Shooting)
        {
            Shooting = true;
            yield return new WaitForSeconds(0.33f);
            if (Joystick.Direction.magnitude == 0)
            {
                Instantiate(Bullet, bulletSpawn.position, bulletSpawn.rotation);
                yield return new WaitForSeconds(0.67f);
            }
            Shooting = false;
        }
    }
    public void Damage(float dmg)
    {
        HP -= dmg;
        if(HP <= 0)
        {
            HitPointBar.localScale = Vector3.zero;
            Die();
            return;
        }
        HitPointBar.localScale = new Vector3(HP / MaxHP, 1, 1);
    }
    
    public void Die()
    {
        Instantiate(Ragdoll, transform.position, transform.rotation);
        FindObjectOfType<UIManager>().ShowRestartButton();
        Destroy(gameObject);
    }

    Transform ClosestEnemy()
    {
        var Objects = Physics.OverlapSphere(transform.position, AttackDistance);
        float dist = AttackDistance;
        Transform EnemyToReturn = null;
        for (int i = 0; i < Objects.Length; i++)
        {
            float newDist = Vector3.Distance(transform.position, Objects[i].transform.position);
            if (Objects[i].GetComponent<Zombie>() != null && newDist <= dist)
            {
                EnemyToReturn = Objects[i].transform;
                dist = newDist;
            }
        }
        return EnemyToReturn;
    }
}
