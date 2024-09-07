using System.Collections;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    private Boss _boss;
    private GameObject _player;

    [SerializeField]
    private AudioClip _circleAttackSound;

    private const int CENTER_ATTACK_INITIAL_BULLET = 4;
    private const int CENTER_ATTACK_INCREASE_BULLET = 4;
    private const int CENTER_ATTACK_MAX_BULLET = 12;
    private const float CENTER_ATTACK_DELAY = 1f;


    private const int MOVING_ATTACK_INITIAL_BULLET = 10;
    private const int MOVING_ATTACK_INCREASE_BULLET = 10;
    private const int MOVING_ATTACK_MAX_BULLET = 30;
    private const float MOVING_ATTACK_SINGLE_DELAY = 0.5f;
    private const float MOVING_ATTACK_MULTY_DELAY = 1f;


    private const int LINE_ATTACK_MAX_BULLET = 19;
    private const float LINE_ATTACK_STEP_VALUE = 10;
    private const float LINE_ATTACK_DELAY = 0.2f;

    private void Awake()
    {
        _boss = GetComponent<Boss>();
        _player = GameObject.Find("Player");
    }

    public void NormalAttack()
    {
        FireSingleBullet();
    }

    public void FireSingleBullet()
    {
        Vector2 bulletDir = (_player.transform.position - transform.position).normalized;

        BossBullet bullet = PoolManager.Instance._bossBulletPool.GetObj();
        bullet.transform.position = transform.position;
        bullet._dir = bulletDir;
    }

    public void FireMultipleBullets(int count)
    {
        float angleStep = 360f / count;
        float angle = 0;
        for (int i = 0; i < count; ++i)
        {
            float bulletX = transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad);
            float bulletY = transform.position.y + Mathf.Cos(angle * Mathf.Deg2Rad);

            Vector3 bulletMoveVector = new Vector2(bulletX, bulletY);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            BossBullet bullet = PoolManager.Instance._bossBulletPool.GetObj();
            bullet.transform.position = transform.position;
            bullet._dir = bulletDir;

            angle += angleStep;
        }
    }

    public IEnumerator CRT_CenterAttack()
    {
        _boss._positionCenter = true;
        _boss._state = Boss._eBOSS_STATE.PATTERN_Attack;

        yield return new WaitForSeconds(1f);

        for (int i = CENTER_ATTACK_INITIAL_BULLET; i <= CENTER_ATTACK_MAX_BULLET; i += CENTER_ATTACK_INCREASE_BULLET)
        {
            FireMultipleBullets(i);
            SoundManager.Instance.PlaySfx(_circleAttackSound);
            yield return new WaitForSeconds(CENTER_ATTACK_DELAY);
        }

        _boss._positionCenter = false;
        _boss._state = Boss._eBOSS_STATE.NORMAL;
    }

    public IEnumerator CRT_MovingAttack()
    {
        _boss._positionCenter = true;
        _boss._state = Boss._eBOSS_STATE.PATTERN_Attack;

        for (int i = 0; i < 4; ++i)
        {
            FireSingleBullet();
            yield return new WaitForSeconds(MOVING_ATTACK_SINGLE_DELAY);
        }

        yield return new WaitForSeconds(1f);

        for (int i = MOVING_ATTACK_INITIAL_BULLET; i < MOVING_ATTACK_MAX_BULLET; i += MOVING_ATTACK_INCREASE_BULLET)
        {
            FireMultipleBullets(i);
            SoundManager.Instance.PlaySfx(_circleAttackSound);
            yield return new WaitForSeconds(MOVING_ATTACK_MULTY_DELAY);
        }

        _boss._positionCenter = false;
        _boss._state = Boss._eBOSS_STATE.NORMAL;
    }

    public IEnumerator CRT_LineAttack()
    {
        _boss._state = Boss._eBOSS_STATE.PATTERN_Attack;
        yield return new WaitForSeconds(1f);

        float angle = 360;

        for (int i = 0; i < LINE_ATTACK_MAX_BULLET; ++i)
        {
            float bulletX = transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad);
            float bulletY = transform.position.y + Mathf.Cos(angle * Mathf.Deg2Rad);

            Vector3 bulletMoveVector = new Vector2(bulletX, bulletY);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            BossBullet bullet = PoolManager.Instance._bossBulletPool.GetObj();
            bullet.transform.position = transform.position;
            bullet._dir = bulletDir;

            angle -= LINE_ATTACK_STEP_VALUE;
            yield return new WaitForSeconds(LINE_ATTACK_DELAY);
        }

        _boss._state = Boss._eBOSS_STATE.NORMAL;
    }
}
