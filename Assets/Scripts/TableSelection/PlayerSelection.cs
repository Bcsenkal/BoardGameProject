using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    private ToggleGroup toggleGroup;
    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public int GetPlayerCount()
    {
        var selection = toggleGroup.GetFirstActiveToggle();
        return selection.transform.GetSiblingIndex() == 0 ? 2 : 4;
    }
}
