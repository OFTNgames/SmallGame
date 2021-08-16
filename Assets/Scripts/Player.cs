using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICanTakeDamage
{
    public static event System.Action PlayerDeath = delegate { };
    public static event System.Action<float, float> GravityAmount = delegate { };
   

    [SerializeField] private ScriptableEventChannel _scriptableEventChannel;

    [SerializeField] private float tweenTime;
    [SerializeField] private GameObject _onDeathEffects;
    [SerializeField] private float _cameraShakeDur = 0.15f;
    [SerializeField] private float _cameraShakeAmount = 0.025f;

    private void OnEnable()
    {
        LevelController.LevelEnd += EndPlayerEffects;
    }

    private void OnDisable()
    {
        LevelController.LevelEnd -= EndPlayerEffects;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var platform = collision.collider.gameObject.GetComponent<Platforms>();
        if(platform)
        {
            _scriptableEventChannel.ShakeTheCamera?.Invoke(_cameraShakeDur, _cameraShakeAmount);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Character/Bounce");
        }
    }

    public void TakeDamage()
    {
        PlayerDeath?.Invoke();
        _scriptableEventChannel.ShakeTheCamera?.Invoke(0.5f,0.2f);
        Instantiate(_onDeathEffects, transform.position, Quaternion.identity);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Character/Death");
        Destroy(gameObject);
    }
    private void EndPlayerEffects(bool complete)
    {
        if(complete)
        {
            Tween();
        }
    }

    private void Tween()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * .1f, tweenTime).setIgnoreTimeScale(true);
    }
}
