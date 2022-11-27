using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private SaveManager saveManager;
    public Slider bgmVolume;
    public Slider effectVolume;
    public static SoundManager SoundEffect;
    public AudioMixer audioMixer;
    public AudioSource musicSource;
    public AudioSource effectSource;
    //public AudioSource bgm;
    private void Awake()
    {
        if(SoundEffect == null)
        {
            SoundEffect = this;
            //DontDestroyOnLoad(SoundEffect);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Debug.Log(SaveManager.bgmVolumeInstance);
        Debug.Log(SaveManager.effectVolumeInstance);
        if (SaveManager.savefile == true)
        {
            bgmVolume.value = SaveManager.bgmVolumeInstance;
            effectVolume.value = SaveManager.effectVolumeInstance;
        }else if (SaveManager.savefile == false)
        {
            bgmVolume.value = 0.6f;
            effectVolume.value = 0.6f;
        }
    }
    private void Update()
    {
        //Debug.Log("bgm"+bgmVolume.value);
        //Debug.Log("eft"+effectVolume.value);
        SaveManager.getBgmInstance = bgmVolume.value;
        SaveManager.getEffectInstance = effectVolume.value;
    }
    public void SoundPlay(string sound, AudioClip clip)
    {
        GameObject soundStart = new GameObject(sound + "Sound");
        AudioSource audioSource = soundStart.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("EffectSound")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundStart, clip.length);
    }
    public void SetMusicVolume()
    {
        musicSource.volume = bgmVolume.value;
        //musicSource.volume = volume; + 매개변수 float volume
    }
    public void SetEffectVolume()
    {
        effectSource.volume = effectVolume.value;
        //effectSource.volume = volume; + 매개변수 float volume
    }
    public void SaveVolume()
    {
        saveManager.SaveVolume();
    }
    public void Load()
    {
    }
}
