using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private FMOD.Studio.Bus master;
    private FMOD.Studio.Bus music;
    private FMOD.Studio.Bus sfx;

    private string masterKey = "Master";
    private string musicKey = "Music";
    private string sfxKey = "SFX";

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        sfx = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
    }

    void Start()
    {
        masterVolume = PlayerPrefs.GetFloat(masterKey, 1f);
        musicVolume = PlayerPrefs.GetFloat(musicKey, 1f);
        sfxVolume = PlayerPrefs.GetFloat(sfxKey, 1f);

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    public void MasterVolumeLevel(float newMasterVolume)
    {
        masterVolume = newMasterVolume;
        PlayerPrefs.SetFloat(masterKey, masterVolume);
        master.setVolume(masterVolume);
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
        PlayerPrefs.SetFloat(musicKey, musicVolume);
        music.setVolume(musicVolume);
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        sfxVolume = newSFXVolume;
        PlayerPrefs.SetFloat(sfxKey, sfxVolume);
        sfx.setVolume(sfxVolume);
    }
}
