using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public GameObject Girl, GamePlayCanvas;

    private void OnEnable()
    {
        GamePlayCanvas.SetActive(false);
    }
    public void Restart()
    {
        GamePlayCanvas.SetActive(true);
        Instantiate(Girl);
        Destroy(GameObject.FindGameObjectWithTag("Ragdoll"));
        gameObject.SetActive(false);
    }
}
