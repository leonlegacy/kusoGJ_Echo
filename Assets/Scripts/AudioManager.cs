using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace CliffLeeCL
{
    /// <summary>
    /// The class that manage all audio source in the scene.
    /// </summary>
    public class AudioManager : SingletonMono<AudioManager>
    {
        public enum AudioName
        {
            ButtonClicked,
            GameSong1,
            EnemyDead2,
            EnemyDead3,
            EnemySlash,
            Fail,
            Pass
        }

        [SerializeField] AudioMixerGroup audioGroup;
        [SerializeField] AudioClip[] audioClips;
        [SerializeField] AudioSource musicSource;
        [SerializeField] int pooledSoundSourceAmount = 10;
        [SerializeField] float lowPitchRange = 0.95f, highPitchRange = 1.05f;
        [SerializeField] bool canGrow = true;
        List<AudioSource> pooledSoundSources;

        // Use this for initialization
        void Start()
        {
            pooledSoundSources = new List<AudioSource>();

            for (int i = 0; i < pooledSoundSourceAmount; i++)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();

                source.outputAudioMixerGroup = audioGroup;
                source.playOnAwake = false;
                pooledSoundSources.Add(source);
            }
        }

        public AudioSource GetSoucre()
        {
            for (int i = 0; i < pooledSoundSources.Count; i++)
            {
                if (!pooledSoundSources[i].isPlaying)
                {
                    return pooledSoundSources[i];
                }
            }

            if (canGrow)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();

                source.outputAudioMixerGroup = audioGroup;
                source.playOnAwake = false;
                pooledSoundSources.Add(source);
                return source;
            }

            return null;
        }

        public void PlayMusic(AudioName clipName, float delayTime = 0.0f)
        {
            if (audioClips[(int) clipName])
            {
                musicSource.clip = audioClips[(int) clipName]; 
                musicSource.pitch = 1.0f;
                musicSource.PlayScheduled(AudioSettings.dspTime + delayTime);
            }
            else
            {
                print("AudioManager : AudioClip[" + name.ToString() + "] is not setted");
            }
        }
        
        public void PlayMusicReversed(AudioName clipName, float delayTime = 0.0f)
        {
            if (audioClips[(int) clipName])
            {
                musicSource.clip = audioClips[(int) clipName]; 
                musicSource.pitch = -1.0f;
                musicSource.timeSamples = musicSource.clip.samples - 1;
                musicSource.PlayScheduled(AudioSettings.dspTime + delayTime);
            }
            else
            {
                print("AudioManager : AudioClip[" + name.ToString() + "] is not setted");
            }
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }
        
        public bool IsMusicPlaying()
        {
            return musicSource.isPlaying;
        }

        public void PlaySound(params AudioName[] name)
        {
            foreach (AudioName clipName in name)
            {
                if (audioClips[(int)clipName])
                {
                    AudioSource source = GetSoucre();

                    source.pitch = 1.0f;
                    source.PlayOneShot(audioClips[(int)clipName]);
                }
                else
                {
                    print("AudioManager : AudioClip[" + name.ToString() + "] is not setted");
                }
            }
        }

        public void PlaySoundRandomPitch(params AudioName[] name)
        {
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            foreach (AudioName clipName in name)
            {
                if (audioClips[(int)clipName])
                {
                    AudioSource source = GetSoucre();

                    source.pitch = randomPitch;
                    source.PlayOneShot(audioClips[(int)clipName]);
                }
                else
                {
                    print("AudioManager : AudioClip[" + name.ToString() + "] is not setted");
                }
            }
        }

        public void PlaySoundRandomClip(params AudioName[] name)
        {
            int clipIndex = Random.Range((int)name[0], (int)name[name.Length - 1]);

            if (audioClips[(int)clipIndex])
            {
                AudioSource source = GetSoucre();

                source.pitch = 1.0f;
                source.PlayOneShot(audioClips[(int)clipIndex]);
            }
            else
            {
                print("AudioManager : AudioClip[" + name.ToString() + "] is not setted");
            }
        }

        public void PlaySoundRandomClipAndPitch(params AudioName[] name)
        {
            int clipIndex = Random.Range((int)name[0], (int)name[name.Length - 1] + 1);
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);

            if (audioClips[(int)clipIndex])
            {
                AudioSource source = GetSoucre();

                source.pitch = randomPitch;
                source.PlayOneShot(audioClips[(int)clipIndex]);
            }
            else
            {
                print("AudioManager : AudioClip[" + name.ToString() + "] is not setted");
            }
        }



    }
}
