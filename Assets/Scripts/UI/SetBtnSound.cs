using UnityEngine;
using UnityEngine.UI;

public class SetBtnSound : MonoBehaviour
{
    private Button _btn;

    [SerializeField]
    AudioClip _btnClip;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    private void Start()
    {
        if (_btn == null)
        {
            return;
        }

        _btn.onClick.AddListener(PlayBtnSound);
    }

    private void PlayBtnSound()
    {
        if(SoundManager.Instance != null && _btnClip != null)
        {
            SoundManager.Instance.PlaySfx(_btnClip);
        }
    }
}
