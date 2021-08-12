using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSetToInactive : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(!_particleSystem.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }

}
