using UnityEngine;

public class Cam : MonoBehaviour
{
    private PlayerInteraction _playerInteraction;

    private Vector3 _spaceCamVec, _normalCamVec;

    private void Awake()
    {
        _playerInteraction = GetComponentInParent<PlayerInteraction>();
    }

    private void Start()
    {
        _spaceCamVec = new Vector3(1555f, 942, -10);
        _normalCamVec = new Vector3(0, 0, -10);
        _playerInteraction._modeEvent.AddListener(ChangeMode);
    }

    private void ChangeMode()
    {
        if(PlayerCore.Instance._eMode == PlayerCore.ePLAYER_MODE.SPACESHIP)
        {
            transform.SetParent(null);
            transform.position = _spaceCamVec;
        }
        else
        {
            transform.SetParent(PlayerCore.Instance.transform);
            transform.localPosition = _normalCamVec;
        }
    }
}
