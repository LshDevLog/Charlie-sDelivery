using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _timeTxt, _deathTxt;

    public GameObject _subMenu;

    private void Start()
    {
        StartCoroutine(CRT_SetCursorAndText());
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    IEnumerator CRT_SetCursorAndText()
    {
        while (true)
        {
            SetCursor(_subMenu.activeSelf);
            SetText();
            yield return null;
        }
    }

    private void SetCursor(bool visible)
    {
        Cursor.visible = visible;
    }

    private void SetText()
    {
        TimeSpan timespan = TimeSpan.FromSeconds(RecordManager.Instance.PlayTime);
        string timer = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
        _timeTxt.text = timer;

        _deathTxt.text = RecordManager.Instance.DeathNum.ToString();
    }
}
