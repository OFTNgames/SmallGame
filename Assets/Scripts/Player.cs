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

    private PlayerJump playerJump;
    private PlayerMovement playerMovement;
    private PlayerGravityControl playerGravity;
    private Rigidbody2D myRigidbody2D;


    private void OnEnable()
    {
        //LevelController.LevelEnd += EndPlayerEffects;
        Door.ExitDoorReached += EndofLevelPlayerFX;
    }

    private void OnDisable()
    {
        //LevelController.LevelEnd -= EndPlayerEffects;
        Door.ExitDoorReached -= EndofLevelPlayerFX;
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
    /*
    private void EndPlayerEffects(bool complete)
    {
        if(complete)
        {
            Tween();
        }
    }
    */

    private void EndofLevelPlayerFX(Vector3 endingPosition, float time)
    {
        StartCoroutine(EndLevelTween(endingPosition, time));
    }

    private IEnumerator EndLevelTween(Vector3 endingPosition, float time)
    {
        playerJump = GetComponent<PlayerJump>();
        playerMovement = GetComponent<PlayerMovement>();
        playerGravity = GetComponent<PlayerGravityControl>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playerJump.enabled = false;
        playerMovement.enabled = false;
        playerGravity.enabled = false;
        myRigidbody2D.simulated = false;


        LeanTween.cancel(gameObject);

        float waitTime = time / 2;

        LeanTween.move(gameObject, endingPosition, waitTime).setIgnoreTimeScale(true);
        yield return new WaitForSecondsRealtime(waitTime);

        transform.localScale = Vector3.one;

        LeanTween.scale(gameObject, Vector3.one * .1f, waitTime).setIgnoreTimeScale(true);
    }
}
