using UnityEngine;

public class Equipment : MonoBehaviour
{
    private PlayerInteraction _playerInteraction;

    [SerializeField]
    private GameObject _divingHelmet, _spaceHelmet, _spaceShip;

    private void Awake()
    {
        _playerInteraction = GetComponent<PlayerInteraction>();
    }

    private void Start()
    {
        _playerInteraction._modeEvent.AddListener(SetEquipment);
    }

    private void SetEquipment()
    {
        var curMode = PlayerCore.Instance._eMode;

        if (curMode == PlayerCore.ePLAYER_MODE.NORMAL)
        {
            _divingHelmet.SetActive(false);
            _spaceHelmet.SetActive(false);
            _spaceShip.SetActive(false);
        }
        else if(curMode == PlayerCore.ePLAYER_MODE.DIVING)
        {
            _divingHelmet.SetActive(true);
            _spaceHelmet.SetActive(false);
            _spaceShip.SetActive(false);
        }
        else
        {
            _divingHelmet.SetActive(false);
            _spaceHelmet.SetActive(true);
            _spaceShip.SetActive(true);
        }
    }
}
