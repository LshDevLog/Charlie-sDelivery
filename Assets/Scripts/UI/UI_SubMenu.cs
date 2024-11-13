using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_SubMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _subMenu, _defaultMenu, _soundMenu;

    [SerializeField]
    private Button _quitBtn;

    private const string MAIN_MENU_SCENE_NAME = "MainMenuScene";
    private void Start()
    {
        _quitBtn.onClick.AddListener(() => SceneManager.LoadScene(MAIN_MENU_SCENE_NAME));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSubMenu();
        }
    }

    private void ToggleSubMenu()
    {
        _defaultMenu.SetActive(true);
        _soundMenu.SetActive(false);
        _subMenu.SetActive(!_subMenu.activeSelf);
    }
}
