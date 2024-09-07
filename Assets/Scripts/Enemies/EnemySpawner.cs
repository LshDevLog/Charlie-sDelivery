using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<UFO> _remainingUFO;

    [SerializeField]
    private Image[] _noticeImgs;

    [SerializeField]
    private GameObject _meteorPrefab, _ufoPrefab, _boss;

    [SerializeField]
    private Transform[] _spawnPos;

    private int _wave;

    private void Awake()
    {
        _remainingUFO = new List<UFO>();
    }
    private void OnEnable()
    {
        StartCoroutine(CRT_Spawn());
    }

    private void Update()
    {
        if(_remainingUFO.Count.Equals(0))
        {
            if (_wave.Equals(1))
            {
                UFO_Wave_2();
            }
            else if (_wave.Equals(2))
            {
                UFO_Wave_3();
            }
            else if (_wave.Equals(3))
            {
                StartCoroutine(CRT_Final_Boss());
            }
        }
        RemoveUFO();
    }

    private IEnumerator CRT_Spawn()
    {
        _noticeImgs[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _noticeImgs[0].gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        SpawnMeteor(3);
        yield return new WaitForSeconds(1.5f);
        SpawnMeteor(0);
        SpawnMeteor(2);
        SpawnMeteor(4);
        yield return new WaitForSeconds(1.5f);
        SpawnMeteor(1);
        SpawnMeteor(3);
        SpawnMeteor(5);
        yield return new WaitForSeconds(1);
        SpawnMeteor(0);
        SpawnMeteor(2);
        SpawnMeteor(4);
        yield return new WaitForSeconds(1);
        SpawnMeteor(1);
        SpawnMeteor(3);
        SpawnMeteor(5);
        yield return new WaitForSeconds(1.5f);
        SpawnMeteor(0);
        SpawnMeteor(1);
        SpawnMeteor(2);
        SpawnMeteor(3);
        SpawnMeteor(4);
        yield return new WaitForSeconds(3.0f);
        SpawnMeteor(1);
        SpawnMeteor(2);
        SpawnMeteor(3);
        SpawnMeteor(4);
        SpawnMeteor(5);
        yield return new WaitForSeconds(1.0f);
        SpawnMeteor(0);
        SpawnMeteor(3);
        SpawnMeteor(4);
        yield return new WaitForSeconds(0.5f);
        SpawnMeteor(0);
        SpawnMeteor(1);
        SpawnMeteor(2);
        SpawnMeteor(3);
        yield return new WaitForSeconds(0.5f);
        SpawnMeteor(0);
        SpawnMeteor(1);
        SpawnMeteor(2);
        SpawnMeteor(3);
        SpawnMeteor(4);
        yield return new WaitForSeconds(0.3f);
        SpawnMeteor(5);
        yield return new WaitForSeconds(1.0f);
        SpawnMeteor(0);
        SpawnMeteor(2);
        SpawnMeteor(3);
        SpawnMeteor(4);
        yield return new WaitForSeconds(0.7f);
        SpawnMeteor(0);
        SpawnMeteor(1);
        SpawnMeteor(3);
        yield return new WaitForSeconds(0.3f);
        SpawnMeteor(1);
        SpawnMeteor(2);
        SpawnMeteor(5);
        yield return new WaitForSeconds(0.3f);
        SpawnMeteor(0);
        SpawnMeteor(3);
        SpawnMeteor(4);
        yield return new WaitForSeconds(0.5f);
        SpawnMeteor(0);
        SpawnMeteor(1);
        SpawnMeteor(2);
        SpawnMeteor(3);
        SpawnMeteor(4);
        yield return new WaitForSeconds(0.3f);
        SpawnMeteor(5);
        yield return new WaitForSeconds(0.2f);
        SpawnMeteor(4);
        yield return new WaitForSeconds(0.2f);
        SpawnMeteor(3);
        yield return new WaitForSeconds(0.2f);
        SpawnMeteor(2);
        yield return new WaitForSeconds(0.2f);
        SpawnMeteor(1);
        yield return new WaitForSeconds(0.5f);
        SpawnMeteor(0);
        yield return new WaitForSeconds(0.2f);
        SpawnMeteor(1);
        yield return new WaitForSeconds(0.2f);
        SpawnMeteor(2);
        yield return new WaitForSeconds(0.2f);
        SpawnMeteor(3);
        yield return new WaitForSeconds(0.2f);
        SpawnMeteor(4);
        yield return new WaitForSeconds(0.5f);
        SpawnMeteor(5);
        yield return new WaitForSeconds(0.5f);
        SpawnMeteor(0);
        SpawnMeteor(2);
        SpawnMeteor(4);
        yield return new WaitForSeconds(2.5f);
        _noticeImgs[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _noticeImgs[1].gameObject.SetActive(false);
        SpawnUFO(2);
        _wave = 1;
    }

    public void SpawnMeteor(int pos)
    {
        Meteor meteor = PoolManager.Instance._meteorPool.GetObj();
        if(meteor != null)
        {
            meteor.transform.position = _spawnPos[pos].position;
        }
    }

    public void SpawnUFO(int pos)
    {
        UFO ufo = PoolManager.Instance._ufoPool.GetObj();
        if(ufo != null)
        {
            _remainingUFO.Add(ufo);
            ufo.transform.position = _spawnPos[pos].position;
        }
    }

    private void UFO_Wave_2()
    {
        SpawnUFO(0);
        SpawnUFO(2);
        SpawnUFO(4);
        _wave = 2;
    }

    private void UFO_Wave_3()
    {
        SpawnUFO(0);
        SpawnUFO(1);
        SpawnUFO(2);
        SpawnUFO(3);
        SpawnUFO(4);
        _wave = 3;
    }

    private IEnumerator CRT_Final_Boss()
    {
        _wave = 4;
        yield return new WaitForSeconds(1);
        _boss.SetActive(true);
    }

    private void RemoveUFO()
    {
        _remainingUFO.RemoveAll(ufo => ufo.HP <= 0);
    }

    public void Init()
    {
        StopAllCoroutines();

        if (_remainingUFO != null)
        {
            foreach (UFO ufo in _remainingUFO)
            {
                PoolManager.Instance._ufoPool.ReturnObj(ufo);
            }
            _remainingUFO.Clear();
        }

        foreach (Image noticeImg in _noticeImgs)
        {
            noticeImg.gameObject.SetActive(false);
        }

        _boss.SetActive(false);

        _wave = 0;
    }
}
