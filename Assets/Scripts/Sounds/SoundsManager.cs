
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;
    public static SoundsManager Instance
    {
        get
        {
            if(instance == null)
            {
                var go = new GameObject("SoundsManager");
                go.AddComponent<SoundsManager>();
                instance = go.GetComponent<SoundsManager>(); ;
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private Transform _soundComponent;
    private AudioSource _jump;
    private AudioSource _fee;
    private AudioSource _changeCharacter;
    private AudioSource _babyScratch;
    private AudioSource _babyShoot;
    private AudioSource _menu;
    private AudioSource _ambient;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        _soundComponent = gameObject.transform;
        _jump = _soundComponent.Find("Jump")?.GetComponent<AudioSource>();
        _fee = _soundComponent.Find("Fee")?.GetComponent<AudioSource>();
        _changeCharacter = _soundComponent.Find("ChangeCharacter")?.GetComponent<AudioSource>();
        _babyScratch = _soundComponent.Find("BabyScratch")?.GetComponent<AudioSource>();
        _babyShoot = _soundComponent.Find("BabyShoot")?.GetComponent<AudioSource>();
        _menu = _soundComponent.Find("Menu")?.GetComponent<AudioSource>();
        _ambient = _soundComponent.Find("Ambient")?.GetComponent<AudioSource>();

    }

    private void Singleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AmbientPlay()
    {
        _menu.Stop();
        _ambient?.Play();
    }

    public void JumpPlay()
    {
        _jump?.Play();
    }

    public void FeePlay()
    {
        _fee?.Play();
    }

    public void ChangeCharacterPlay()
    {
        _changeCharacter?.Play();
    }


    public void BabyScratchPlay()
    {
        _babyScratch?.Play();
    }

    public void BabyShootPlay()
    {
        _babyShoot?.Play();
    }
}