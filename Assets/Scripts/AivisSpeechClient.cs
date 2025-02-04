using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

[System.Serializable]
public class Mora
{
    public string text;
    public string consonant;
    public float consonant_length;
    public string vowel;
    public float vowel_length;
    public float pitch;
}

[System.Serializable]
public class AccentPhrase
{
    public List<Mora> moras;
    public int accent;
    public Mora pause_mora;
    public bool is_interrogative;
}

[System.Serializable]
public class AudioQueryResponse
{
    public List<AccentPhrase> accent_phrases;
    public float speedScale;
    public float intonationScale;
    public float tempoDynamicsScale;
    public float pitchScale;
    public float volumeScale;
    public float prePhonemeLength;
    public float postPhonemeLength;
    public float pauseLength;
    public float pauseLengthScale;
    public int outputSamplingRate;
    public bool outputStereo;
    public string kana;
}

public class AivisSpeechClient
{
    private const string HOST = "localhost";
    private const int PORT = 10101;

    private static AivisSpeechClient instance;
    private VoiceModel currentVoiceModel;
    public static AivisSpeechClient Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AivisSpeechClient();
            }
            return instance;
        }
    }

    private AivisSpeechClient() { }

    public void SetVoiceModel(VoiceModel model)
    {
        currentVoiceModel = model;
    }

    public async UniTask<byte[]> Text2VoiceAsync(string text, string emotion,
        float speedScale = 1.0f,
        float intonationScale = 1.0f,
        float tempoDynamicsScale = 1.0f,
        float pitchScale = 0.0f,
        float volumeScale = 1.0f)
    {
        int speaker = (int)currentVoiceModel;

        try
        {
            // audio_queryリクエスト
            var queryUrl = $"http://{HOST}:{PORT}/audio_query?text={UnityWebRequest.EscapeURL(text)}&speaker={speaker}";
            var queryRequest = UnityWebRequest.Post(queryUrl, new WWWForm());
            await queryRequest.SendWebRequest();

            if (queryRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Audio query failed: {queryRequest.error}");
                return null;
            }

            var queryResponse = queryRequest.downloadHandler.text;
            // Add voice control parameters to the query response
            // Parse and modify the JSON response
            var originalJson = JsonUtility.FromJson<AudioQueryResponse>(queryResponse);
            originalJson.speedScale = speedScale;
            originalJson.intonationScale = intonationScale;
            originalJson.tempoDynamicsScale = tempoDynamicsScale;
            originalJson.pitchScale = pitchScale;
            originalJson.volumeScale = volumeScale;
            var modifiedQueryResponse = JsonUtility.ToJson(originalJson);

            // synthesisリクエスト
            var synthesisUrl = $"http://{HOST}:{PORT}/synthesis?speaker={speaker}";
            var synthesisRequest = new UnityWebRequest(synthesisUrl, "POST");
            synthesisRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(modifiedQueryResponse));
            synthesisRequest.downloadHandler = new DownloadHandlerBuffer();
            synthesisRequest.SetRequestHeader("Content-Type", "application/json");
            synthesisRequest.SetRequestHeader("accept", "audio/wav");

            await synthesisRequest.SendWebRequest();

            if (synthesisRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Synthesis failed: {synthesisRequest.error}");
                return null;
            }

            return synthesisRequest.downloadHandler.data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in Text2Voice: {e.Message}");
            return null;
        }
    }

    public static AudioClip CreateAudioClipFromWAV(byte[] wavData)
    {
        if (wavData == null) return null;

        // WAVヘッダーをスキップ (44バイト)
        const int headerSize = 44;
        // AudioClipを作成
        var audioClip = AudioClip.Create("voice",
            (wavData.Length - headerSize) / 2, // 16bitなので2で割る
            1, // モノラル
            44100, // サンプリングレート
            false);

        // 音声データをfloat配列に変換
        var audioData = new float[(wavData.Length - headerSize) / 2];
        for (int i = 0; i < audioData.Length; i++)
        {
            // 16bitデータをfloatに変換 (-1.0f to 1.0f)
            short sample = BitConverter.ToInt16(wavData, headerSize + i * 2);
            audioData[i] = sample / 32768f;
        }

        audioClip.SetData(audioData, 0);
        return audioClip;
    }
}
