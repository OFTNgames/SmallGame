using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    [SerializeField] private float _Force;
    private Rigidbody2D _rigidBody;
   
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(new Vector2(Random.value, Random.value) * _Force);
    }
}
