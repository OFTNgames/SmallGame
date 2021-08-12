using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    private Spawner _particlePrefab;

    private void Start()
    {
        _particlePrefab = GetComponent<Spawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() || collision.gameObject.GetComponent<MenuPlayer>())
        {
            _particlePrefab.Spawn(collision);
        }
    }
}
