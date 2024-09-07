using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    [SerializeField]
    private Transform _outputPos;

    [SerializeField]
    private AudioClip _clip;

    private const string PLAYER_TAG = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(PLAYER_TAG))
        {
            SoundManager.Instance.PlaySfx(_clip);
            Bomb bomb = PoolManager.Instance._bombPool.GetObj();
            bomb.transform.position = _outputPos.position;
        }
    }
}
