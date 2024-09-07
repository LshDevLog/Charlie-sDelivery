using UnityEngine;

public class Boss : EnemyCore
{
    private BossCombat _combat;

    public GameObject _moon;

    [SerializeField]
    private ParticleSystem _deathParticlePrefab;

    [SerializeField]
    private AudioClip _deathSound, _appearanceSound;

    private float _moveSpeed = 3.0f;
    private float _patternAttackCoolTime = 0f;
    private float _patternAttackCoolTimeMax = 10f;
    private float _normalAttackCoolTime = 0f;
    private float _normalAttackCoolTimeMax = 3f;

    public bool _positionCenter = false;

    bool _IsDead = false;

    Vector2 _centerPos, _originPos;

    public int HP() => _HP;
    private int _maxHP = 150;
    public enum _eBOSS_STATE
    {
        NORMAL,
        PATTERN_Attack,
    }

    public _eBOSS_STATE _state = _eBOSS_STATE.NORMAL;

    private void Awake()
    {
        _combat = GetComponent<BossCombat>();
    }

    private void Start()
    {
        _centerPos = new Vector2(1555.5f, 942f);
        _originPos = transform.position;
    }

    private void OnEnable()
    {
        SoundManager.Instance.PlaySfx(_appearanceSound);
        InitBoss();
    }

    private void OnDisable()
    {
        InitBoss();
    }

    private void Update()
    {
        if (_IsDead)
        {
            return;
        }

        _normalAttackCoolTime += Time.deltaTime;

        if(_HP <= 0)
        {
            Death();
        }
        else
        {
            Move(_positionCenter);
            SetAttack();
        }
    }

    private void Move(bool toCenter)
    {
        Vector3 targetPos = toCenter ? _centerPos : _originPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, _moveSpeed * Time.deltaTime);
    }

    public void InitBoss()
    {
        _HP = _maxHP;
        _positionCenter = false;
        _IsDead = false;
        _state = _eBOSS_STATE.NORMAL;
        _normalAttackCoolTime = 0;
        _patternAttackCoolTime = 0;
    }

    private void SetAttack()
    {
        if (_state == _eBOSS_STATE.NORMAL)
        {
            _patternAttackCoolTime += Time.deltaTime;

            if (_patternAttackCoolTime >= _patternAttackCoolTimeMax)
            {
                int ran = Random.Range(1, 4);
                _patternAttackCoolTime = 0f;

                switch (ran)
                {
                    case 1:
                        StartCoroutine(_combat.CRT_CenterAttack());
                        break;
                    case 2:
                        StartCoroutine(_combat.CRT_MovingAttack());
                        break;
                    case 3:
                        StartCoroutine(_combat.CRT_LineAttack());
                        break;
                    default:
                        break;
                }
            }
            else if(_normalAttackCoolTime >= _normalAttackCoolTimeMax)
            {
                _combat.NormalAttack();
                _normalAttackCoolTime = 0f;
            }
        }
        else
        {
            _normalAttackCoolTime = 0f;
        }
    }

    private void Death()
    {
        SoundManager.Instance.PlaySfx(_deathSound);
        StopAllCoroutines();
        _IsDead = true;
        Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
        _moon.transform.position = transform.position;
        _moon.SetActive(true);
        gameObject.SetActive(false);
    }
}
