using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ManagerSounds : MonoBehaviour
{
    public static ManagerSounds Instance;
    [System.Serializable]
    public class Sound {
        public int _name;                     //nombre del sonido (identificador)
        public AudioClip _clip;                  //Clip de audio asociado
        [Range(0f, 1f)] public float _volume = 1;//volumen del sonido
        public bool _loop;                       //si el sonido debe repetirse(para musica de fondo,ejemplo)
        public UnityEngine.Audio.AudioMixerGroup _mixerGroup;
    }
    [Header("Lista de sonidos")]
    public List<Sound> _sounds;
    private Dictionary<int, AudioSource> _audioSource;
    public UnityEngine.Audio.AudioMixer _audioMixer;
    [SerializeField] Slider _sliderMusic;
    [SerializeField] Slider _sliderEfects;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else Destroy(gameObject);
        //crear diccionario para los sonidos
        _audioSource = new Dictionary<int, AudioSource>();
        foreach (var sound in _sounds) {
            AudioSource _source = gameObject.AddComponent<AudioSource>();
            _source.clip = sound._clip;
            _source.volume = sound._volume;
            _source.loop = sound._loop;
            _source.outputAudioMixerGroup = sound._mixerGroup;
            _audioSource.Add(sound._name, _source);
        }
    }

    public void PlaySound(int name) {
        if(_audioSource.TryGetValue(name, out AudioSource source)) {
            source.Play();
        }
    }
    public void StopSound(int name) {
        if(_audioSource.TryGetValue(name, out AudioSource source)) {
            source.Stop();
        }
    }

    //metodo para el background
    public void PlayBackgroudnMusic(int name) {
        foreach (var sourse in _audioSource.Values) {
            if (sourse.loop) sourse.Stop();
        }
        PlaySound(name);
    }
    public void SetMusicVolume(float volume) {
        _sliderMusic.value = volume;
        _audioMixer.SetFloat("MUSIC", Mathf.Log10(volume) * 20);
    }
    public void SetEffectsVolume(float volume) {
        _sliderEfects.value = volume;
        _audioMixer.SetFloat("EFECTS", Mathf.Log10(volume) * 20);
    }
}
