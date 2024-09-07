using UnityEngine;

public class Meteor : EnemyCore
{
    [SerializeField]
    private float _moveSpeed = 25f;
    [SerializeField]
    private float _rotSpeed = 400f;
    [SerializeField]
    private float _returnX = 1530f;

    private void Update()
    {
        MoveAndRot();
        Return();
    }

    private void MoveAndRot()
    {
        transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * _rotSpeed * Time.deltaTime);
    }

    private void Return()
    {
        if (transform.position.x < _returnX)
        {
            PoolManager.Instance._meteorPool.ReturnObj(this);
        }
    }
}
