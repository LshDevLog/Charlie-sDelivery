using System.Collections;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static RecordManager Instance;

    private const string TOTAL_DEATH = "TotalDeath";
    private const string THIS_CLEAR_DEATH = "ThisClearDeath";
    private const string THIS_CLEAR_TIME = "ThisClearTime";

    private float _playTime = 0f;

    public float PlayTime
    {
        get => _playTime;
        set => _playTime = value;
    }
    
    private int _deathNum = 0;

    public int DeathNum
    {
        get => _deathNum;
        set => _deathNum = value;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(CRT_SetPlayTime());
    }

    private IEnumerator CRT_SetPlayTime()
    {
        while (true)
        {
            PlayTime += Time.deltaTime;
            yield return null;
        }
    }

    public void AddRecordNum(string record)
    {
        if (record.Equals(TOTAL_DEATH))
            ++DeathNum;

        int temp = PlayerPrefs.GetInt(record, 0);
        PlayerPrefs.SetInt(record, ++temp);
    }

    public void SetCurRecord()
    {
        PlayerPrefs.SetFloat(THIS_CLEAR_TIME, PlayTime);
        PlayerPrefs.SetInt(THIS_CLEAR_DEATH, DeathNum);
    }
}
