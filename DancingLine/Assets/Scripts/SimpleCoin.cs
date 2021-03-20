using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleCoin : MonoBehaviour
{
    public AudioClip _coinSound;
    public AudioSource _audioSource;

    public static event Action OnGetCoin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_coinSound);
            OnGetCoin?.Invoke();
            Destroy(gameObject);
        }
    }
}
