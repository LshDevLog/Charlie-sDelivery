using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    [SerializeField]
    private AudioClip _damageSound;

    [SerializeField]
    protected int _HP;

    public void TakeDamage()
    {
        SoundManager.Instance.PlaySfx(_damageSound);

        if (_HP > 0)
        {
            --_HP;
        }
    }
}
