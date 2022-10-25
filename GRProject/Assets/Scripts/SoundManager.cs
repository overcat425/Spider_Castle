using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SoundEffect;
    public AudioMixer audioMixer;
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
}
