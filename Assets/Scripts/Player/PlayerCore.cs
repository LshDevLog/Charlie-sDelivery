using UnityEngine;
using UnityEngine.Events;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;

    public UnityEvent _moveEvent = new UnityEvent();

    [HideInInspector]
    public Rigidbody2D _rb;

    public enum ePLAYER_MODE
    {
        NORMAL,
        DIVING,
        SPACESHIP
    }

    public ePLAYER_MODE _eMode;
    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _eMode = ePLAYER_MODE.NORMAL;
    }

    private void Update()
    {
        _moveEvent?.Invoke();
    }
}
