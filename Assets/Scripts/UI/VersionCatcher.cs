using TMPro;
using UnityEngine;

namespace UI
{
    public class VersionCatcher : MonoBehaviour
    {
    
        void Start()
        {
            GetComponent<TextMeshProUGUI>().text = $"v.{Application.version}";
        }

    
    }
}
