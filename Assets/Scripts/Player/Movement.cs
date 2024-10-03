using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerInteraction _playerInteraction;
    private Rigidbody2D _rb;
    private Animator _anim;

    [SerializeField]
    AudioClip _jumpSound;

    private const string IS_RUNNING = "isRunning";
    private const string IS_JUMPING = "isJumping";

    [SerializeField]
    private float _maxMoveSpeed = 15f;
    [SerializeField]
    private float _spaceShipSpeed = 25f;
    [SerializeField]
    private float _jumpPower = 15f;

    private float _inputMoveX, _inputMoveY;
    private bool _isGrounded, _inputJump;

    private float _moveBreakValue = 0.5f;
    private float _isRunningAnimStopValue = 0.3f;

    [SerializeField]
    private Vector2 _boxCastSize = new Vector2(0.9f, 1.5f);

    private float _boxCastAngle = 0f;
    private float _boxCastDist = 0.1f;

    private LayerMask _walkableLayer;

    private void Awake()
    {
        _playerInteraction = GetComponent<PlayerInteraction>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        _walkableLayer = LayerMask.GetMask("Walkable");

        _playerInteraction._modeEvent.AddListener(SetMoveEvent);
        var moveEvent = PlayerCore.Instance._moveEvent;
        moveEvent.AddListener(CheckGrounded);
        moveEvent.AddListener(NormalMove);
        moveEvent.AddListener(NormalJump);
    }

    private void Update()
    {
        _inputMoveX = Input.GetAxis("Horizontal");
        _inputMoveY = Input.GetAxis("Vertical");
        _inputJump = Input.GetButtonDown("Jump");
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics2D.BoxCast(transform.position, _boxCastSize, _boxCastAngle, Vector3.down, _boxCastDist, _walkableLayer);
    }

    private void NormalMove()
    {
        float speed = _inputMoveX;

        _rb.velocity = new Vector2(speed * _maxMoveSpeed, _rb.velocity.y);

        if (Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxMoveSpeed, _rb.velocity.y);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x * _moveBreakValue, _rb.velocity.y);
        }

        if(speed != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(speed), 1, 1);
        }

        _anim.SetBool(IS_RUNNING, Mathf.Abs(_rb.velocity.x) >= _isRunningAnimStopValue);
    }

    private void SpaceShipMove()
    {
        Vector2 dir = new Vector2(_inputMoveX, _inputMoveY).normalized;
        _rb.velocity = dir * _spaceShipSpeed;

        _anim.SetBool(IS_RUNNING, false);
        _anim.SetBool(IS_RUNNING, false);
    }

    private void NormalJump()
    {
        if (_inputJump && !_anim.GetBool(IS_JUMPING) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _anim.SetBool(IS_JUMPING, true);
            RecordManager.Instance.AddRecordNum("TotalJump");
            SoundManager.Instance.PlaySfx(_jumpSound);
        }

        if (_rb.velocity.y <= 0)
        {
            _anim.SetBool(IS_JUMPING, false);
        }
    }

    private void WaterJump()
    {
        if (_inputJump && !_anim.GetBool(IS_JUMPING))
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _anim.SetBool(IS_JUMPING, true);
            RecordManager.Instance.AddRecordNum("TotalJump");
        }

        if (_rb.velocity.y <= 0)
        {
            _anim.SetBool(IS_JUMPING, false);
        }
    }

    private void ResetConstraints()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void SetMoveEvent()
    {
        ResetConstraints();

        var moveEvent = PlayerCore.Instance._moveEvent;
        moveEvent.RemoveAllListeners();

        var mode = PlayerCore.Instance._eMode;
        if (mode == PlayerCore.ePLAYER_MODE.NORMAL)
        {
            moveEvent.AddListener(CheckGrounded);
            moveEvent.AddListener(NormalMove);
            moveEvent.AddListener(NormalJump);
        }
        else if(mode == PlayerCore.ePLAYER_MODE.DIVING)
        {
            moveEvent.AddListener(NormalMove);
            moveEvent.AddListener(WaterJump);
        }
        else
        {
            moveEvent.AddListener(SpaceShipMove);
        }
    }
}
