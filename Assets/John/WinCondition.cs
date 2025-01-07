using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WinCondition : MonoBehaviour
{
    public string playerTag = "Player";
    public static bool reachGoal = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            reachGoal = true;
        }

    }
}
