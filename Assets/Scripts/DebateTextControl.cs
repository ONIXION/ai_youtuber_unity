using UnityEngine;
using TMPro;

public class DebateTextControl : MonoBehaviour
{
    // TMPを取得
    [SerializeField] private TextMeshProUGUI textMeshPro;
    void Update()
    {
        if (GlobalVariables.sceneIdx == 0)
        {
            textMeshPro.text = "";
        }
        else if (GlobalVariables.sceneIdx == 1)
        {
            textMeshPro.text = "Debate Time!";
        }
    }
}
