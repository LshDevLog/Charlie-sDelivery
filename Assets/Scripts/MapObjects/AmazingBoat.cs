using UnityEngine;

public class AmazingBoat : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Vector3 _originPos;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _originPos = transform.position;
        ResetPos();
    }

    public void ResetPos()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = 0;
        _rb.rotation = 0;
        transform.rotation = Quaternion.identity;
        transform.position = _originPos;
    }
}
