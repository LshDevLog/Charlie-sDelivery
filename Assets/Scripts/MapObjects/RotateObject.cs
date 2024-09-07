using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Rotation Options")]
    [Tooltip("False = Left Rotation / True = Right Rotation"), SerializeField]
    private bool _rotateLeft = false;

    [SerializeField]
    private float _rotateSpeed;

    private void Update()
    {
        float rotDir = _rotateLeft ? _rotateSpeed : -_rotateSpeed;
        transform.Rotate(0, 0, rotDir * Time.deltaTime);
    }
}
