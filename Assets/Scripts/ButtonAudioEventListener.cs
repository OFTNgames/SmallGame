using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudioEventListener : MonoBehaviour
{
    [FMODUnity.EventRef] public string HooverEvent;
    [FMODUnity.EventRef] public string ClickEvent;

    private void OnEnable()
    {
        ClickSound.PlayClickSound += ClickSound_PlayClickSound;
        ClickSound.PlayHooverSound += ClickSound_PlayHooverSound;
    }

    private void OnDisable()
    {
        ClickSound.PlayClickSound -= ClickSound_PlayClickSound;
        ClickSound.PlayHooverSound -= ClickSound_PlayHooverSound;
    }

    private void ClickSound_PlayHooverSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(ClickEvent, gameObject);
    }

    private void ClickSound_PlayClickSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(HooverEvent, gameObject);
    }
}
