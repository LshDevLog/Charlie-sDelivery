using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private const string BGM_VOL = "BgmVol";
    private const string SFX_VOL = "SfxVol";

    [SerializeField]
    private AudioSource _bgmAudioSrc, _sfxAudioSrc;

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        _bgmAudioSrc.volume = InitVol(BGM_VOL);
        _sfxAudioSrc.volume = InitVol(SFX_VOL);
    }

    public float InitVol(string key)
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetFloat(key) : 0.5f;
    }

    public void SetBgmSound(float vol)
    {
        _bgmAudioSrc.volume = vol;
        PlayerPrefs.SetFloat(BGM_VOL, vol);
    }

    public void SetSfxSound(float vol)
    {
        _sfxAudioSrc.volume = vol;
        PlayerPrefs.SetFloat(SFX_VOL, vol);
    }

    public void PlayBGM()
    {
        _bgmAudioSrc.loop = true;
        _bgmAudioSrc.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null)
            return;

        _sfxAudioSrc.PlayOneShot(clip);
    }
}
