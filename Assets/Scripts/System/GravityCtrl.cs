using UnityEngine;
using UnityEngine.UI;

public class GravityCtrl : MonoBehaviour
{
    private PlayerInteraction _playerInteraction;

    [SerializeField]
    private Image _loadingImg;

    float[] _gravity = new float[] {5f, 1f, 0f};

    private void Awake()
    {
        _playerInteraction = FindAnyObjectByType<PlayerInteraction>();
        _loadingImg.gameObject.SetActive(false);
    }

    private void Start()
    {
        _playerInteraction._modeEvent.AddListener(SetGravity);
    }

    private void SetGravity()
    {
        if(PlayerCore.Instance != null)
        {
            for (int i = 0; i < _gravity.Length; ++i)
            {
                if ((int)PlayerCore.Instance._eMode == i)
                    PlayerCore.Instance._rb.gravityScale = _gravity[i];
            }
        }
    }
}
