using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _clearTime, _clearDeath;

    [SerializeField]
    private GameObject _parcel, _pressEscImg, _newRecordImg;

    [SerializeField]
    private Image _charlieTalk, _astronautTalk;

    [SerializeField]
    private Sprite[] _talkImgs;

    private const string MAIN_MENU_SCENE_NAME = "MainMenuScene";
    private const string THIS_CLEAR_DEATH = "ThisClearDeath";
    private const string THIS_CLEAR_TIME = "ThisClearTime";
    private const string TOTAL_CLEAR = "TotalClear";
    private const string BEST_CLEAR_TIME = "BestClearTime";

    private bool _isDone = false;
    private bool _newRecord = false;

    private void Start()
    {
        StartCoroutine(CRT_Ending());
    }

    private void Update()
    {
        if(_isDone && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        }
    }

    private IEnumerator CRT_Ending()
    {
        int clearDeath = PlayerPrefs.GetInt(THIS_CLEAR_DEATH);
        _clearDeath.text = clearDeath.ToString();


        float clearTime = PlayerPrefs.GetFloat(THIS_CLEAR_TIME);
        TimeSpan timespan = TimeSpan.FromSeconds(clearTime);
        string timer = string.Format("{0:00}:{1:00},{2:00},{3:00}", timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
        _clearTime.text = timer;

        yield return new WaitForSeconds(1);
        _charlieTalk.gameObject.SetActive(true);
        _parcel.SetActive(true);
        yield return new WaitForSeconds(2);
        _charlieTalk.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        _parcel.SetActive(false);
        _astronautTalk.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        _astronautTalk.sprite = _talkImgs[0];
        yield return new WaitForSeconds(2);
        _astronautTalk.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        _charlieTalk.sprite = _talkImgs[1];
        _astronautTalk.sprite = _talkImgs[2];
        _charlieTalk.gameObject.SetActive(true);
        _astronautTalk.gameObject.SetActive(true);

        float recordTime = 0;

        int totalClearNum = PlayerPrefs.GetInt(TOTAL_CLEAR) + 1;
        PlayerPrefs.SetInt(TOTAL_CLEAR, totalClearNum);

        if (PlayerPrefs.HasKey(BEST_CLEAR_TIME))
        {
            recordTime = PlayerPrefs.GetFloat(BEST_CLEAR_TIME);

            if (clearTime < recordTime)
            {
                PlayerPrefs.SetFloat(BEST_CLEAR_TIME, clearTime);
                _newRecord = true;
            }
        }
        else
        {
            PlayerPrefs.SetFloat(BEST_CLEAR_TIME, clearTime);
            _newRecord = true;
        }

        _pressEscImg.gameObject.SetActive(true);
        _isDone = true;

        while (_newRecord)
        {
            _newRecordImg.gameObject.SetActive(!_newRecordImg.gameObject.activeSelf);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
