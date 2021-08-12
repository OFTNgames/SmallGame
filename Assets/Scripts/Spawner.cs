using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private ObjectPool _objectPool;

    private void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
    }

    public void Spawn()
    {
        GameObject newParticle = _objectPool.GetObject(_prefab);
        newParticle.transform.position = this.transform.position;
    }

    public void Spawn(Collision2D collision)
    {
        GameObject newParticle = _objectPool.GetObject(_prefab);
        newParticle.transform.position = collision.GetContact(0).point;
        newParticle.transform.rotation = Quaternion.Normalize(Quaternion.identity);
    }
}
