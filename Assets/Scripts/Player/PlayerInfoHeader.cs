using UnityEngine;
using UnityEngine.UI;
public class PlayerInfoHeader : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowInfoPanel);
    }

    private void ShowInfoPanel()
    {
        Managers.EventManager.Instance.ONOnShowPlayerInfoPanel();
    }
}
