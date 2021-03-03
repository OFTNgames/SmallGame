﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICanTakeDamage
{
    public static event System.Action PlayerDeath = delegate { };
    public static event System.Action<float, float> GravityAmount = delegate { };

    //[SerializeField] private float _bounceMax = 1f;
    //[SerializeField] private float _bounceMin = 0.5f;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _maxGravityControlTime;
    private float _gravityControlTime;
    private Rigidbody2D _rigidBody;
    private Vector2 _inputValues;
    private bool _canJump;
    private bool _shouldJump;
    private float _bounciness = 0.5f;
    private PhysicsMaterial2D _physicsMaterial;
    private bool _gravityOn;
    

    void Start()
    {
        _gravityOn = true;
        _rigidBody = GetComponent<Rigidbody2D>();
        _gravityControlTime = _maxGravityControlTime;
        //_physicsMaterial = _rigidBody.sharedMaterial;
        //_physicsMaterial.bounciness = _bounciness;
    }

    void Update()
    {
        _inputValues = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            _shouldJump = true;
            _canJump = false;
        }

        /*
        if(Input.GetKeyDown(KeyCode.Equals))
        {
            _bounciness = Mathf.Clamp(_bounciness + 0.1f, _bounceMin, _bounceMax);
            Debug.Log(_bounciness);
            _physicsMaterial.bounciness = _bounciness;
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            _bounciness = Mathf.Clamp(_bounciness - 0.1f, _bounceMin, _bounceMax);
            Debug.Log(_bounciness);
            _physicsMaterial.bounciness = _bounciness;
        }
        */

        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (_gravityOn)
            {
                
                _gravityOn = false;
            }
            else
            {
                _gravityOn = true;
            }
        }

        GravityTime();
        GravityAmount?.Invoke(_gravityControlTime,_maxGravityControlTime);
    }

    private void GravityTime()
    {
        if(_gravityOn)
        {
            _gravityControlTime = Mathf.Clamp(_gravityControlTime + Time.deltaTime, 0, _maxGravityControlTime);
            _rigidBody.gravityScale = 1;
        }
        else
        {
            _gravityControlTime -= Time.deltaTime;
            _rigidBody.gravityScale = 0;
            if (_gravityControlTime <= 0)
                _gravityOn = true;
        }
    }


    private void FixedUpdate()
    {
        _rigidBody.AddForce(_inputValues * Time.fixedDeltaTime * _speed);
        if(_shouldJump)
        {
            _shouldJump = false;
            _rigidBody.AddForce(Vector2.up * _jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var platform = collision.collider.gameObject.GetComponent<Platforms>();
        if(platform)
        {
            _canJump = true;
        }
    }

    public void TakeDamage()
    {
        PlayerDeath?.Invoke();
        Destroy(gameObject);
    }
}