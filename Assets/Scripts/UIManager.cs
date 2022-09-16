using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject RestartButton;
    public void ShowRestartButton()
    {
        RestartButton.SetActive(true);
    }
}
