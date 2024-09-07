using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    private float _moveSpeed = 12.0f;
    private float _endXvalue = 800f;
    private float _playerXvalue = 150f;

    private Vector3 _originPos;
    private void Start()
    {
        _originPos = transform.position;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Move();
            BackToOriginPos();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
    }

    private void BackToOriginPos()
    {
        if(transform.position.x > _endXvalue || _player.position.x < _playerXvalue)
        {
            gameObject.SetActive(false);
            transform.position = _originPos;
        }
    }
}
