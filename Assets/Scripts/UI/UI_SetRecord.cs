using System;
using TMPro;
using UnityEngine;

public class UI_SetRecord : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _bestClearTimeTxt, _totalClearTxt, _totalDeathTxt, _totalJumpTxt, _totalHitTxt;

    private const string BEST_CLEAR_TIME = "BestClearTime";
    private const string TOTAL_CLEAR = "TotalClear";
    private const string TOTAL_DEATH = "TotalDeath";
    private const string TOTAL_JUMP = "TotalJump";
    private const string TOTAL_HIT = "TotalHit";
    private const string DEFAULT_ZERO_CLEAR_TIME = "00:00:00:00";
    private const string ZERO = "0";
    private void Start()
    {
        InitTexts();
    }

    private void InitTexts()
    {
        if (PlayerPrefs.HasKey(BEST_CLEAR_TIME))
        {
            float bestTimeRecord = PlayerPrefs.GetFloat(BEST_CLEAR_TIME);
            TimeSpan timespan = TimeSpan.FromSeconds(bestTimeRecord);
            string time = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
            _bestClearTimeTxt.text = time;
        }
        else
        {
            _bestClearTimeTxt.text = DEFAULT_ZERO_CLEAR_TIME;
        }

        _totalClearTxt.text = SetRecordText(TOTAL_CLEAR);
        _totalDeathTxt.text = SetRecordText(TOTAL_DEATH);
        _totalJumpTxt.text = SetRecordText(TOTAL_JUMP);
        _totalHitTxt.text = SetRecordText(TOTAL_HIT);
    }

    private string SetRecordText(string text)
    {
        return PlayerPrefs.GetInt(text, 0).ToString();
    }

    public void ResetRecords()
    {
        PlayerPrefs.DeleteKey(BEST_CLEAR_TIME);
        PlayerPrefs.DeleteKey(TOTAL_CLEAR);
        PlayerPrefs.DeleteKey(TOTAL_DEATH);
        PlayerPrefs.DeleteKey(TOTAL_JUMP);
        PlayerPrefs.DeleteKey(TOTAL_HIT);

        _bestClearTimeTxt.text = DEFAULT_ZERO_CLEAR_TIME;
        _totalClearTxt.text = ZERO;
        _totalDeathTxt.text = ZERO;
        _totalJumpTxt.text = ZERO;
        _totalHitTxt.text = ZERO;
    }
}
