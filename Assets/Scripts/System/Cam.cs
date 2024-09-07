using UnityEngine;

public class Cam : MonoBehaviour
{
    private PlayerCore _playerCore;

    Vector3 _spaceCamVec, _normalCamVec;
    private void Awake()
    {
        _playerCore = GetComponentInParent<PlayerCore>();
    }

    private void Start()
    {
        _spaceCamVec = new Vector3(1555f, 942, -10);
        _normalCamVec = new Vector3(0, 0, -10);
    }
    private void Update()
    {
        ChangeMode();
    }

    private void ChangeMode()
    {
        if(_playerCore._eMode == PlayerCore.ePLAYER_MODE.SPACESHIP)
        {
            transform.SetParent(null);
            transform.position = _spaceCamVec;
        }
        else
        {
            transform.SetParent(_playerCore.transform);
            transform.localPosition = _normalCamVec;
        }
    }
}
