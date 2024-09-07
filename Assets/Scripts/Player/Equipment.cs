using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private GameObject _divingHelmet, _spaceHelmet, _spaceShip;

    private void Update()
    {
        SetEquipment();
    }

    private void SetEquipment()
    {
        switch (PlayerCore.Instance._eMode)
        {
            case PlayerCore.ePLAYER_MODE.NORMAL:
                _divingHelmet.SetActive(false);
                _spaceHelmet.SetActive(false);
                _spaceShip.SetActive(false);
                break;
            case PlayerCore.ePLAYER_MODE.DIVING:
                _divingHelmet.SetActive(true);
                _spaceHelmet.SetActive(false);
                _spaceShip.SetActive(false);
                break;
            case PlayerCore.ePLAYER_MODE.SPACESHIP:
                _divingHelmet.SetActive(false);
                _spaceHelmet.SetActive(true);
                _spaceShip.SetActive(true);
                break;
            default:
                break;
        }
    }
}
