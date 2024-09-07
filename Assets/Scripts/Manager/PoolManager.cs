using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public ObjectPool<PlayerMissile> _playerMissilePool {  get; private set; }
    public ObjectPool<Bomb> _bombPool {  get; private set; }
    public ObjectPool<Fish> _fishPool {  get; private set; }
    public ObjectPool<Meteor> _meteorPool {  get; private set; }
    public ObjectPool<UFO> _ufoPool { get; private set; }
    public ObjectPool<UFOMissile> _ufoMissilePool { get; private set; }
    public ObjectPool<BossBullet> _bossBulletPool {  get; private set; }

    [SerializeField]
    PlayerMissile _playerMissilePrefab;
    [SerializeField]
    Bomb _bombPrefab;
    [SerializeField]
    Fish _fishPrefab;
    [SerializeField]
    Meteor _meteorPrefab;
    [SerializeField]
    UFO _ufoPrefab;
    [SerializeField]
    UFOMissile _ufoMissilePrefab;
    [SerializeField]
    BossBullet _bossBulletPrefab;

    private const int PLAYER_MISSILE_INIT_NUM = 10;
    private const int FISH_INIT_NUM = 15;
    private const int BOMB_INIT_NUM = 3;
    private const int MEMEOR_INIT_NUM = 10;
    private const int UFO_INIT_NUM = 4;
    private const int UFO_MISSILE_INIT_NUM = 8;
    private const int BOSS_BULLET_INIT_NUM = 40;

    private void Awake()
    {
        Instance = this;

        InitPools();
    }

    private void InitPools()
    {
        _playerMissilePool = new ObjectPool<PlayerMissile>(_playerMissilePrefab, PLAYER_MISSILE_INIT_NUM, this);
        _fishPool = new ObjectPool<Fish>(_fishPrefab, FISH_INIT_NUM, this);
        _bombPool = new ObjectPool<Bomb>(_bombPrefab, BOMB_INIT_NUM, this);
        _meteorPool = new ObjectPool<Meteor>(_meteorPrefab, MEMEOR_INIT_NUM, this);
        _ufoPool = new ObjectPool<UFO>(_ufoPrefab, UFO_INIT_NUM, this);
        _ufoMissilePool = new ObjectPool<UFOMissile>(_ufoMissilePrefab, UFO_MISSILE_INIT_NUM, this);
        _bossBulletPool = new ObjectPool<BossBullet>(_bossBulletPrefab, BOSS_BULLET_INIT_NUM, this);
    }
}
