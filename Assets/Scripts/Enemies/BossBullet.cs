using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Boss _boss;

    private float _speed = 30f;

    public Vector2 _dir, spaceCenter;

    private const string PLAYER_TAG = "Player";
    private const string SPACE_LINE_TAG = "SpaceLine";
    private const string RETURN_BULLET = "ReturnBullet";

    private void Start()
    {
        spaceCenter = new Vector2(1555, 942);
    }

    private void OnEnable()
    {
        if(_boss == null)
        {
            _boss = FindAnyObjectByType<Boss>();
        }

        Invoke(RETURN_BULLET, 3.0f);
    }

    private void Update()
    {
        if(_boss == null || !_boss.gameObject.activeSelf)
        {
            PoolManager.Instance._bossBulletPool.ReturnObj(this);
        }
        else
        {
            Move(_dir);
        }
    }

    public void Move(Vector2 dir)
    {
        transform.Translate(dir * _speed * Time.deltaTime);
    }

    private void ReturnBullet()
    {
        PoolManager.Instance._bossBulletPool.ReturnObj(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(PLAYER_TAG) || collision.collider.CompareTag(SPACE_LINE_TAG))
        {
            PoolManager.Instance._bossBulletPool.ReturnObj(this);
        }
    }
}
