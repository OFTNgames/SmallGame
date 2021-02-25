using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class Monster : MonoBehaviour
{
    [SerializeField] private bool _faceingLeft;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        if (_faceingLeft)
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        else
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ICanTakeDamage _canTakeDamage = collision.collider.gameObject.GetComponent<ICanTakeDamage>();

        if(_canTakeDamage != null)
        {
            _canTakeDamage.TakeDamage();
        }
    }
}
