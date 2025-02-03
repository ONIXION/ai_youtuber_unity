using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Telop : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmpText;

    private CancellationTokenSource cancellationTokenSource;

    private void Awake()
    {
        if (tmpText == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned to Telop script!");
        }
        Clean();
    }

    private void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
    }

    public async UniTask Display(string text, Color color, float characterInterval = 0.1f, float displayDuration = 2f)
    {
        Clean();

        cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;

        tmpText.fontMaterial.SetColor("_OutlineColor", color);
        tmpText.text = "";

        // Display text character by character
        for (int i = 0; i < text.Length; i++)
        {
            if (token.IsCancellationRequested) return;

            tmpText.text += text[i];
            await UniTask.Delay((int)(characterInterval * 1000), cancellationToken: token);
        }

        // Keep the text displayed for the specified duration
        if (!token.IsCancellationRequested)
        {
            await UniTask.Delay((int)(displayDuration * 1000), cancellationToken: token);
        }

        if (!token.IsCancellationRequested)
        {
            Clean();
        }
    }

    public void Clean()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
        cancellationTokenSource = null;

        if (tmpText != null)
        {
            tmpText.text = "";
        }
    }
}
