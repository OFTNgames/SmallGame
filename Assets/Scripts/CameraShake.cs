using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private ScriptableEventChannel _scriptableEvent;

    private void OnEnable()
    {
        _scriptableEvent.ShakeTheCamera += StartShake;
    }

    private void OnDisable()
    {
        _scriptableEvent.ShakeTheCamera -= StartShake;
    }

    private void StartShake(float duration, float magnitude) 
    {
        StopAllCoroutines();
        StartCoroutine(Shake(duration, magnitude));
    }

    IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;
        
        while(elapsed<duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
