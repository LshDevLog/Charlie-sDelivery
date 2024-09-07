using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private AudioClip _deathSound, _jumpingGroundSound;

    [SerializeField]
    private EnemySpawner _enemySpawner;

    [SerializeField]
    private AmazingBoat _boat;

    private const string TOTAL_DEATH = "TotalDeath";
    private const string DEAD_ZONE_TAG = "DeadZone";
    private const string JUMPING_GROUND_TAG = "JumpingGround";
    private const string ENTRANCE_TAG = "Entrance";
    private const string BOARD_SPACE_SHIP_TAG = "BoardSpaceShip";
    private const string FINISH_TAG = "Finish";
    private const string ENDING_SCENE_NAME = "EndingScene";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(DEAD_ZONE_TAG))
        {
            DeathZone();
        }
        else if (collision.collider.CompareTag(JUMPING_GROUND_TAG))
        {
            SoundManager.Instance.PlaySfx(_jumpingGroundSound);
        }
        else if (collision.collider.CompareTag(ENTRANCE_TAG))
        {
            Entrance(collision.collider);
        }
        else if (collision.collider.CompareTag(FINISH_TAG))
        {
            Finish();
        }
    }

    private void DeathZone()
    {
        if (PlayerCore.Instance._eMode == PlayerCore.ePLAYER_MODE.SPACESHIP)
            _enemySpawner.Init();

        PlayerCore.Instance._eMode = PlayerCore.ePLAYER_MODE.NORMAL;
        SoundManager.Instance.PlaySfx(_deathSound);
        RecordManager.Instance.AddRecordNum(TOTAL_DEATH);
        transform.position = new Vector2(15, 12);
        PlayerCore.Instance.ResetConstraints();
        _boat.ResetPos();
        _enemySpawner.enabled = false;
    }

    private void Entrance(Collider2D col)
    {
        col.GetComponent<Entrance>().Move();

        if (col.gameObject.name.Equals(BOARD_SPACE_SHIP_TAG))
        {
            _enemySpawner.enabled = true;
        }
    }

    private void Finish()
    {
        RecordManager.Instance.SetCurRecord();
        SceneManager.LoadScene(ENDING_SCENE_NAME);
    }
}
