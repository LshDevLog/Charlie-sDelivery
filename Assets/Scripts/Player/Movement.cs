using UnityEngine;

public class Movement : MonoBehaviour
{
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

    private void Start()
    {
        _walkableLayer = LayerMask.GetMask("Walkable");
    }

    private void Update()
    {
        _inputMoveX = Input.GetAxis("Horizontal");
        _inputMoveY = Input.GetAxis("Vertical");
        _inputJump = Input.GetButtonDown("Jump");
    }

    public void CheckGrounded()
    {
        _isGrounded = Physics2D.BoxCast(transform.position, _boxCastSize, _boxCastAngle, Vector3.down, _boxCastDist, _walkableLayer);
    }

    public void NormalMove(Rigidbody2D rb, Animator anim)
    {
        float speed = _inputMoveX;

        rb.velocity = new Vector2(speed * _maxMoveSpeed, rb.velocity.y);

        if (Mathf.Abs(rb.velocity.x) > _maxMoveSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * _maxMoveSpeed, rb.velocity.y);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rb.velocity = new Vector2(rb.velocity.x * _moveBreakValue, rb.velocity.y);
        }

        if(speed != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(speed), 1, 1);
        }

        anim.SetBool(IS_RUNNING, Mathf.Abs(rb.velocity.x) >= _isRunningAnimStopValue);
    }

    public void SpaceShipMove(Rigidbody2D rb, Animator anim)
    {
        Vector2 dir = new Vector2(_inputMoveX, _inputMoveY).normalized;
        rb.velocity = dir * _spaceShipSpeed;

        anim.SetBool(IS_RUNNING, false);
        anim.SetBool(IS_RUNNING, false);
    }

    public void Jump(Rigidbody2D rb, Animator anim, bool inWater)
    {
        if (_inputJump && !anim.GetBool(IS_JUMPING) && (_isGrounded || inWater))
        {
            rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            anim.SetBool(IS_JUMPING, true);
            RecordManager.Instance.AddRecordNum("TotalJump");

            if (_isGrounded && !inWater)
            {
                SoundManager.Instance.PlaySfx(_jumpSound);
            }
        }

        if (rb.velocity.y <= 0)
        {
            anim.SetBool(IS_JUMPING, false);
        }
    }
}
