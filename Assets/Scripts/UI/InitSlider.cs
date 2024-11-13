using UnityEngine;
using UnityEngine.UI;

public class InitSlider : MonoBehaviour
{
    private Slider _slider;

    private const string BGM_SLIDER = "BgmSlider";
    private const string BGM_VOL = "BgmVol";
    private const string SFX_VOL = "SfxVol";

    enum eSLIDER_TYPE
    {
        BGM,
        SFX
    }

    [SerializeField]
    private eSLIDER_TYPE _type;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if(_type == eSLIDER_TYPE.BGM)
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
