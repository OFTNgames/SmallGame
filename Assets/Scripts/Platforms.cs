using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private GameObject _particlePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            Instantiate(_particlePrefab, collision.GetContact(0).point, Quaternion.Normalize(Quaternion.identity));
        }
    }
}
