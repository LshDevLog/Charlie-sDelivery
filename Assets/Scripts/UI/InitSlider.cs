using UnityEngine;
using UnityEngine.UI;

public class InitSlider : MonoBehaviour
{
    private Slider _slider;

    private const string BGM_SLIDER = "BgmSlider";
    private const string BGM_VOL = "BgmVol";
    private const string SFX_VOL = "SfxVol";


    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (gameObject.name.Equals(BGM_SLIDER))
        {
            _slider.onValueChanged.AddListener(SoundManager.Instance.SetBgmSound);
            _slider.value = SoundManager.Instance.InitVol(BGM_VOL);
        }
        else
        {
            _slider.onValueChanged.AddListener(SoundManager.Instance.SetSfxSound);
            _slider.value = SoundManager.Instance.InitVol(SFX_VOL);
        }
    }
}
