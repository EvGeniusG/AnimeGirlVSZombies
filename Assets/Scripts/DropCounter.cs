using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropCounter : MonoBehaviour
{
    public Text Counter;
    int Count = 0;
    void Start()
    {
        Counter.text = Count.ToString();
    }

    public void AddBonus(int Addiction)
    {
        Count += Addiction;
        Counter.text = Count.ToString();
    }
}
