using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAudio : MonoBehaviour
{
    public float minRPM = 0f;
    public float maxRPM = 5000f;
    private Rigidbody2D _rigidbody2D;
    private FMODUnity.StudioEventEmitter _emitter;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _emitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    
    void Update()
    {
        float playerSpeed = _rigidbody2D != null ? _rigidbody2D.velocity.magnitude : 0;

        float effectiveRpm = Mathf.Lerp(minRPM, maxRPM, playerSpeed);
        _emitter.SetParameter("RPM", effectiveRpm);
    }
}
