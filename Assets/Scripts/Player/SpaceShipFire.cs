using UnityEngine;

public class SpaceShipFire : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;

    private float _coolTime;
    private float _coolTimeMax = 0.2f;

    private void Start()
    {
        _coolTime = 0;
    }

    private void Update()
    {
        UpdateCoolTime();
    }

    private void UpdateCoolTime()
    {
        if (_coolTime >= _coolTimeMax)
            return;

        _coolTime += Time.deltaTime;
    }

    public void Fire()
    {
        if (Input.GetButton("Fire"))
        {
            if (_coolTime >= _coolTimeMax)
            {
                var missile = PoolManager.Instance._playerMissilePool.GetObj();
                missile.transform.position = _firePoint.position;
                _coolTime = 0f;
            }
        }
    }
}
