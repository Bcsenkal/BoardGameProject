using UnityEngine;
using UnityEngine.UI;
public class SettingsButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        transform.localScale = Vector3.zero;
        Managers.EventManager.Instance.OnCreateTable += ShowButton;
        Managers.EventManager.Instance.OnShowSettingsPanel += HideButton;
        Managers.EventManager.Instance.OnHideSettingsPanel += ShowButton;
    }

    void ShowButton(TableCreationInfo arg)
    {
        transform.localScale = Vector3.one;
        button.onClick.AddListener(ShowSettingsPanel);
    }

    void ShowButton()
    {
        transform.localScale = Vector3.one;
        button.onClick.AddListener(ShowSettingsPanel);
    }


    void HideButton()
    {
        transform.localScale = Vector3.zero;
        button.onClick.RemoveListener(ShowSettingsPanel);
    }

    void ShowSettingsPanel()
    {
        Managers.EventManager.Instance.ONOnShowSettingsPanel();
        button.onClick.RemoveListener(ShowSettingsPanel);
    }
}
