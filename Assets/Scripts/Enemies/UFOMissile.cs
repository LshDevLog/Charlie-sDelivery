using UnityEngine;

public class UFOMissile : MonoBehaviour
{
    public Vector3 _dir;

    public float _speed;
    private float _speedMin = 30f;
    private float _speedMax = 60f;

    private const string PLAYER_TAG = "Player";
    private const string SPACE_LINE_TAG = "SpaceLine";

    private void OnEnable()
    {
        _speed = Random.Range(_speedMin, _speedMax);
    }
    void Update()
    {
        Fire(_dir);
    }

    public void Fire(Vector3 dir)
    {
        transform.Translate(dir * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(PLAYER_TAG) || collision.collider.CompareTag(SPACE_LINE_TAG))
        {
            PoolManager.Instance._ufoMissilePool.ReturnObj(this);
        }
    }
}
