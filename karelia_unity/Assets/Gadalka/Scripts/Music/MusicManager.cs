using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [Space]
    [SerializeField] protected bool _musicOn;

    [Space]
    [SerializeField] protected float _masterVolume = 1;

    [Space]
    [SerializeField] private GameObject player;

    [Space]

    [FMODUnity.EventRef][SerializeField] protected string _music;
    [FMODUnity.EventRef][SerializeField] protected string _scream;
    [FMODUnity.EventRef][SerializeField] protected string _moving;
    [FMODUnity.EventRef][SerializeField] protected string _oneShotEngineLeft;
    [FMODUnity.EventRef][SerializeField] protected string _oneShotEngineRight;


    protected FMOD.Studio.EventInstance _ambienceEvent;
    protected FMOD.Studio.EventInstance _musicEvent;
    protected FMOD.Studio.EventInstance _voiceEvent;
    protected FMOD.Studio.EventInstance _screamEvent;
    protected FMOD.Studio.EventInstance _engineEvent;
    protected FMOD.Studio.EventInstance _hummingMelodyEvent;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
        }
    }
    private void Start()
    {
        if (_musicOn)
            PlayMusic();
    }

    private void OnDestroy()
    {
        _ambienceEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _voiceEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _screamEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _engineEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _hummingMelodyEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

    }

    public void PlayMusic()
    {
        if (_music == "") return;

        _musicEvent = FMODUnity.RuntimeManager.CreateInstance(_music);
        _musicEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        _musicEvent.setVolume(_masterVolume);
        _musicEvent.start();
    }

    public void OffroadSetParameters(float distance)
    {
        _voiceEvent.setParameterByName("distance", distance);
        _engineEvent.setParameterByName("offroad", distance);
        //Debug.Log("distance " + distance);
    }

    public void PlayScream(string scream, float volume)
    {
        if (_scream == "") return;

        _screamEvent = FMODUnity.RuntimeManager.CreateInstance(scream);

        if (player != null)
            _screamEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(player.transform));
        else
            _screamEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        _screamEvent.setVolume(_masterVolume * volume);
        _screamEvent.start();

    }
 
    public void PlayEngineSound(bool isPlay)
    {
        if (!_engineEvent.isValid())
        {
           // _engineEvent = FMODUnity.RuntimeManager.CreateInstance();
            if (player != null)
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(_engineEvent, player.transform);
            else
                _engineEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            _engineEvent.setVolume(_masterVolume);
            _engineEvent.start();
        }
        else
        {
            if (isPlay)
            {
                _engineEvent.setParameterByName("rpm", 0.11f);
                _engineEvent.setParameterByName("offroadrpm", 0.11f);
            }
            else
            {
                _engineEvent.setParameterByName("rpm", 0f);
                _engineEvent.setParameterByName("offroadrpm", 0f);
            }
        }

    }
    public float GetEngineEvenTRpm()
    {
        float val;
        float finalval;
        _engineEvent.getParameterByName("rpm", out val, out finalval);
        return finalval;
    }

    public void StopAmbient()
    {
        if (_ambienceEvent.isValid())
            StartCoroutine(FadeOutSound(_ambienceEvent, 2));
    }

    private IEnumerator FadeOutSound(FMOD.Studio.EventInstance sound, float time)
    {
        var timeLeft = time;
        sound.getVolume(out var volume);

        do
        {
            sound.setVolume(volume * (timeLeft / time));

            timeLeft -= Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();

        } while (timeLeft > 0);

        sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}

