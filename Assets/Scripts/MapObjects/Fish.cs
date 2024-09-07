using UnityEngine;

public class Fish : MonoBehaviour
{
    private float _speed, _upValue, _downValue;

    public enum eDIRECTION
    {
        UP,
        DOWN
    }

    public eDIRECTION _dir;

    private void Start()
    {
        _speed = 30.0f;
        _upValue = -219f;
        _downValue = -240f;
    }

    private void Update()
    {
        if(PoolManager.Instance == null || PoolManager.Instance._fishPool == null)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
        float moveDir = 0f;

        switch (_dir)
        {
            case eDIRECTION.UP:
                moveDir = _speed * Time.deltaTime;
                if (transform.position.y > _upValue)
                {
                    ReturnToPool();
                }
                break;
            case eDIRECTION.DOWN:
                moveDir = -_speed * Time.deltaTime;
                if (transform.position.y < _downValue)
                {
                    ReturnToPool();
                }
                break;
        }

        transform.Translate(Vector3.up * moveDir);
    }

    private void ReturnToPool()
    {
        PoolManager.Instance._fishPool.ReturnObj(this);
    }
}
