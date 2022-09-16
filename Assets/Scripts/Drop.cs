using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var Girl = other.GetComponent<Girl>();
        if (Girl != null)
        {
            Girl.Drop++;
            Destroy(gameObject);
        }
    }
}
