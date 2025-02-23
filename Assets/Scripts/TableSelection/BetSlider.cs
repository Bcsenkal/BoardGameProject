using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BetSlider : MonoBehaviour
{
    private int minimumBet;
    private int maximumBet;
    private TextMeshProUGUI minimumBetText;
    private TextMeshProUGUI maximumBetText;
    private TextMeshProUGUI currentBetText;
    private Slider slider;

    private CreateTablePanel createTablePanel;

    void Start()
    {
        createTablePanel = GetComponentInParent<CreateTablePanel>();
        slider = GetComponent<Slider>();
        minimumBetText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        maximumBetText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        currentBetText = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        slider.onValueChanged.AddListener(ChangeValue);

    }

    public void SetBetRange(TableInfo betRange)
    {
        minimumBet = betRange.minimumBet;
        maximumBet = betRange.maximumBet;
        minimumBetText.text = XearUtils.ThousandFormat(minimumBet);
        maximumBetText.text = XearUtils.ThousandFormat(maximumBet);
        slider.value = slider.minValue;
        slider.maxValue = maximumBet/ minimumBet;
        currentBetText.text = XearUtils.ThousandFormat(minimumBet);
    }

    public int GetCurrentBet()
    {
        return minimumBet * (int)slider.value;
    }

    private void ChangeValue(float value)
    {
        currentBetText.text = XearUtils.ThousandFormat(minimumBet * (int)value);
    }

    
}
