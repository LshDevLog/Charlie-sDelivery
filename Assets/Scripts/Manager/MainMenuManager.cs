using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private Camera _cam;

    [SerializeField]
    private Image _companyLogo, _introImg, _pressAnyKeyImg, _loadingImg;

    [SerializeField]
    private GameObject _defaultBox;


    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(SetMenuScene());
    }

    private void Update()
    {
        PressAnyKey();
    }

    private IEnumerator SetMenuScene()
    {
        _companyLogo.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.7f);
        _companyLogo.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.PlayBGM();
        _introImg.gameObject.SetActive(true);
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (_introImg.gameObject.activeSelf)
        {
            _pressAnyKeyImg.gameObject.SetActive(!_pressAnyKeyImg.gameObject.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void PressAnyKey()
    {
        if (Input.anyKeyDown && _introImg.gameObject.activeSelf)
        {
            StopCoroutine(Flicker());
            _cam.backgroundColor = new Color(119f/255f, 154f/255f, 1f, 1f);
            _introImg.gameObject.SetActive(false);
            _pressAnyKeyImg.gameObject.SetActive(false);
            _defaultBox.gameObject.SetActive(true);
        }
    }
}
