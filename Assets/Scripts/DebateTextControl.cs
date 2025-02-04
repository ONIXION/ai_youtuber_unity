using UnityEngine;

public class DebateTextControl : MonoBehaviour
{
    void Update()
    {
        if (GlobalVariables.sceneIdx == 0)
        {
            gameObject.SetActive(false);
        }
        else if (GlobalVariables.sceneIdx == 1)
        {
            gameObject.SetActive(true);
        }
    }
}
