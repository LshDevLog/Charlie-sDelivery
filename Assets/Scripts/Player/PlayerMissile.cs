using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    private const string TOTAL_HIT = "TotalHit";

    private float _speed = 50f;
    private float _returnX = 1700f;

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);

        ReturnOverXpos();
    }

    private void ReturnOverXpos()
    {
        if(transform.position.x > _returnX)
        {
            PoolManager.Instance._playerMissilePool.ReturnObj(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyCore enemyCore = collision.gameObject.GetComponent<EnemyCore>();
        if (enemyCore == null)
        {
            return;
        }

        enemyCore.TakeDamage();
        RecordManager.Instance.AddRecordNum(TOTAL_HIT);
        PoolManager.Instance._playerMissilePool.ReturnObj(this);
    }
}
