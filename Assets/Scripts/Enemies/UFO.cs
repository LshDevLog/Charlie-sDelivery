using UnityEngine;

public class UFO : EnemyCore
{
    private Transform _player;

    [SerializeField]
    private AudioClip _deathSound;

    private float _moveSpeed = 10f;
    private float _attackCoolTime;
    private float _attackCoolTimeMax;
    private float _stopX = 1570f;

    public int HP => _HP;
    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
    }
    private void Start()
    {
        ResetStats();
    }

    private void OnEnable()
    {
        ResetStats();
    }

    private void Update()
    {
        _attackCoolTime += Time.deltaTime;

        Move();
        Attack();
        Death();
    }
    
    private void ResetStats()
    {
        _HP = 15;
        _attackCoolTime = 0;
        _attackCoolTimeMax = Random.Range(1.0f, 3.0f);
    }
    private void Move()
    {
        if(transform.position.x > _stopX)
        {
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if(_attackCoolTime >= _attackCoolTimeMax)
        {
            UFOMissile missile = PoolManager.Instance._ufoMissilePool.GetObj();
            missile.transform.position = transform.position;

            Vector2 dir = (_player.transform.position - transform.position).normalized;

            missile._dir = dir;

            _attackCoolTime = 0;
            _attackCoolTimeMax = Random.Range(1.0f, 3.0f);
        }
    }

    private void Death()
    {
        if(_HP <= 0)
        {
            SoundManager.Instance.PlaySfx(_deathSound);
            PoolManager.Instance._ufoPool.ReturnObj(this);
        }
    }
}
