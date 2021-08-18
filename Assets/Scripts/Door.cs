using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Door : MonoBehaviour
{
    public static event System.Action<Vector3, float> ExitDoorReached = delegate { };

    [SerializeField] private float delayTime = 4f;

    private ParticleSystem _particleSystem;
    //private PlayableDirector _playableDirector;

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
      //  _playableDirector = FindObjectOfType<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Character/LevelComplete");
            ExitDoorReached?.Invoke(transform.position, delayTime);
            var main = _particleSystem.main;
            main.useUnscaledTime = true;
            _particleSystem.Play();
        }
    }
}
