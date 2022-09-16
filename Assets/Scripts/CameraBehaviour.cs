using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform Center;
    Girl Girl;

    private void Awake()
    {
        Girl = FindObjectOfType<Girl>();
    }
    void LateUpdate()
    {
        if(Girl == null)
        {
            Girl = FindObjectOfType<Girl>();
            return;
        }
        Center.position = Girl.transform.position;
    }
}
