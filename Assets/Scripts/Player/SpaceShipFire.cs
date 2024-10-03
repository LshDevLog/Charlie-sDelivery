using System.Collections;
using UnityEngine;

public class SpaceShipFire : MonoBehaviour
{
    [SerializeField]
    private Transform _firePoint;
    [SerializeField]
    private float _coolTime = 0;
    private float _coolTimeMax = 0.2f;

    private void Update()
    {
        Fire();
    }

    private void OnEnable()
    {
        StartCoroutine(CRT_UpdateCoolTime());
    }

    private void OnDisable()
    {
        StopCoroutine(CRT_UpdateCoolTime());
        _coolTime = 0;
    }

    IEnumerator CRT_UpdateCoolTime()
    {
        while (enabled)
        {
            _coolTime += Time.deltaTime;
            yield return null;
        }
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
