using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitDetector : MonoBehaviour
{
    public bool GirlBeat = false;
    public Zombie BeatOwner;
    public Collider Coll;
    private void OnEnable()
    {
        GirlBeat = false;
        Coll.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var Girl = other.GetComponent<Girl>();
        if(Girl != null && !GirlBeat)
        {
            GirlBeat = true;
            Girl.Damage(BeatOwner.Damage);
        }
    }
    private void OnDisable()
    {
        Coll.enabled = false;
    }
}
