using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Finish");
            other.GetComponent<Movement>().Speed = 0.1f;
            other.GetComponent<Animator>().SetBool("isIdle", true);
        }
    }
}
