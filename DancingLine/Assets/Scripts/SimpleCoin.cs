using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleCoin : MonoBehaviour
{
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioSource _audioSource;

    public static event Action OnGetCoin;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnGetCoin?.Invoke();
            Destroy(gameObject);
            _audioSource.PlayOneShot(_coinSound);
        }
    }
}
