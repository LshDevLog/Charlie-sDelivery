using UnityEngine;

public class Entrance : MonoBehaviour
{
    public Transform _trs;

    [SerializeField]
    private PlayerCore.ePLAYER_MODE _mode;
    public void Move()
    {
        if(PlayerCore.Instance != null && _trs != null)
        {
            PlayerCore.Instance.transform.position = _trs.position;
            PlayerCore.Instance._eMode = _mode;
        }
    }

}
