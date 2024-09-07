using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SetButton : MonoBehaviour
{
    private UI_SetRecord _setRecord;

    [SerializeField]
    private Button _startBtn, _deleteBtn, _quitBtn;

    private const string PLAY_SCENE_NAME = "PlayScene";
    private void Awake()
    {
        _setRecord = GetComponent<UI_SetRecord>();
    }

    private void Start()
    {
        if(_startBtn != null)
        {
            _startBtn.onClick.AddListener(StartBtn);
        }

        if(_deleteBtn != null)
        {
            _deleteBtn.onClick.AddListener(DeleteRecordBtn);
        }

        if(_quitBtn != null)
        {
            _quitBtn.onClick.AddListener(QuitBtn);
        }
    }

    private void StartBtn()
    {
        StartCoroutine(StartGameCo());
    }

    private IEnumerator StartGameCo()
    {
        Cursor.visible = false;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(PLAY_SCENE_NAME);
    }

    private void QuitBtn()
    {
        Application.Quit();
    }

    private void DeleteRecordBtn()
    {
        _setRecord.ResetRecords();
    }
}