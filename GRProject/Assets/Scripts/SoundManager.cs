using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
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
    public void SoundPlay(string sound, AudioClip clip)
    {
        GameObject soundStart = new GameObject(sound + "Sound");
        AudioSource audioSource = soundStart.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("EffectSound")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundStart, clip.length);
    }
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SetEffectVolume(float volume)
    {
        effectSource.volume = volume;
    }
    public void Save()
    {
        PlayerPrefs.SetFloat("BGM", musicSource.volume);
        PlayerPrefs.SetFloat("Effect", effectSource.volume);
    }
    public void Load()
    {
        musicSource.volume = PlayerPrefs.GetFloat("BGM");
        effectSource.volume = PlayerPrefs.GetFloat("Effect");
    }
}
