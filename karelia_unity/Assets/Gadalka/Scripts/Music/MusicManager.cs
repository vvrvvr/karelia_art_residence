using FMODUnity;
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


    [SerializeField] private EventReference _music;
    [SerializeField] private EventReference _noise;
    [SerializeField] private EventReference _turn;
    [SerializeField] private EventReference _pulse;


    protected FMOD.Studio.EventInstance _musicEvent;
    protected FMOD.Studio.EventInstance _noiseEvent;
    protected FMOD.Studio.EventInstance _turnEvent;
    protected FMOD.Studio.EventInstance _pulseEvent;

    protected FMOD.Studio.EventInstance _changeFormEvent;
    protected FMOD.Studio.EventInstance _ambienceEvent;
    protected FMOD.Studio.EventInstance _hummingMelodyEvent;

    private void Start()
    {
        PlayMusic();
        SetupNoiseEvent();
        SetupPulseEvent();
    }

    private void OnDestroy()
    {
        _musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _turnEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _noiseEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayMusic()
    {
        if (_music.IsNull)
            return;

        _musicEvent = FMODUnity.RuntimeManager.CreateInstance(_music);
        _musicEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        _musicEvent.setVolume(_masterVolume);
        _musicEvent.start();
    }
    public void SetupNoiseEvent()
    {
        if (_noise.IsNull)
            return;

        _noiseEvent = FMODUnity.RuntimeManager.CreateInstance(_noise);
        _noiseEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        _noiseEvent.setVolume(_masterVolume);
        _noiseEvent.start();
        NoiseSetValue(180);
    }

    public void SetupPulseEvent()
    {
        if (_pulse.IsNull)
            return;

        _pulseEvent = FMODUnity.RuntimeManager.CreateInstance(_pulse);
        _pulseEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        _pulseEvent.setVolume(_masterVolume);
        _pulseEvent.start();
        PulseSetValue(17);
    }

    public void PlayTurn()
    {
        RuntimeManager.PlayOneShot(_turn, transform.position);
    }

    public void NoiseSetValue(float val)
    {
        _noiseEvent.setParameterByName("MaterialAmplitude", val);
    }

    public void PulseSetValue(float val)
    {
        _pulseEvent.setParameterByName("distance", val);
    }
}

