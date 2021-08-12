using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static event System.Action ExitDoorReached = delegate { };
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Character/LevelComplete");
            ExitDoorReached?.Invoke();
            var main = _particleSystem.main;
            main.useUnscaledTime = true;
            _particleSystem.Play();
        }
    }

}
