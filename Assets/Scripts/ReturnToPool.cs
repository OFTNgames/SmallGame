using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    private ObjectPool _objectPool;

    void Start()
    {
        _objectPool = FindObjectOfType<ObjectPool>();
    }

    private void OnDisable()
    {
        if(_objectPool != null)
        {
            _objectPool.ReturnGameObjectToPool(this.gameObject);
        }
    }
}
