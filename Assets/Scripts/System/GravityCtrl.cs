using UnityEngine;
using UnityEngine.UI;

public class GravityCtrl : MonoBehaviour
{
    [SerializeField]
    private Image _loadingImg;

    float[] _gravity = new float[] {5f, 1f, 0f};

    private void Awake()
    {
        _loadingImg.gameObject.SetActive(false);
    }

    private void Update()
    {
        SetGravity();
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
