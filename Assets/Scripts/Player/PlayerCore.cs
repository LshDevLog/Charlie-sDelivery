using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;

    [HideInInspector]
    public Rigidbody2D _rb;
    private Movement _movement;
    private SpaceShipFire _spaceShipFire;
    private Animator _anim;

    public enum ePLAYER_MODE
    {
        NORMAL,
        DIVING,
        SPACESHIP
    }

    public ePLAYER_MODE _eMode;
    private void Awake()
    {
        Instance = this;

        _movement = GetComponent<Movement>();
        _spaceShipFire = GetComponent<SpaceShipFire>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _eMode = ePLAYER_MODE.NORMAL;
    }

    private void Update()
    {
        _movement.CheckGrounded();

        switch (_eMode)
        {
            case ePLAYER_MODE.NORMAL:
                _movement.NormalMove(_rb, _anim);
                _movement.Jump(_rb, _anim, false);
                break;
            case ePLAYER_MODE.DIVING:
                _movement.NormalMove(_rb, _anim);
                _movement.Jump(_rb, _anim, true);
                break;
            case ePLAYER_MODE.SPACESHIP:
                _movement.SpaceShipMove(_rb, _anim);
                _spaceShipFire.Fire();
                break;
            default:
                break;
        }
    }

    public void ResetConstraints()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
