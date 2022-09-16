using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 SpawnPosition;
    [SerializeField]
    float Distance;
    [SerializeField]
    float Speed;
    void Awake()
    {
        var Girl = FindObjectOfType<Girl>();
        SpawnPosition = Girl.transform.position;
        Distance = Girl.AttackDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * Speed, Space.Self);
        if(Vector3.Distance(SpawnPosition, transform.position) > Distance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Girl>() == null)
        {
            var Zombie = other.GetComponent<Zombie>();
            if (Zombie != null)
            {
                Zombie.Die();
            }
            Destroy(gameObject);
        }
        
    }
}
