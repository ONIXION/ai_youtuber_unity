using System.Collections.Generic;

public class ReceiveMessageFormat
{
    /// <summary>
    /// [comment, message, agent1, agent2]
    /// </summary>
    public string name;
    /// <summary>
    /// [Nothing, Think, WebSearch]
    /// </summary>
    public string action;
    /// <summary>
    /// メッセージの内容
    /// </summary>
    public string content;
    /// <summary>
    /// [normal, happy, angry, sad, surprised, shy, excited, smug, calm]
    /// </summary>
    public string emotion;
    /// <summary>
    /// [””, “debate”, “conversation”]
    /// </summary>
    public string scene;
}

public static class GlobalVariables
{
    /// <summary>
    /// 現在のシーンのインデックス
    /// 0: conversation, 1: debate
    /// </summary>
    public static int sceneIdx = 0;
    // ReceiveMessageFormatの配列を保持する変数
    /// <summary>
    /// agent1のメッセージキュー
    /// </summary>
    public static List<ReceiveMessageFormat> Agent1Queue = new List<ReceiveMessageFormat>();
    /// <summary>
    /// agent2のメッセージキュー
    /// </summary>
    public static List<ReceiveMessageFormat> Agent2Queue = new List<ReceiveMessageFormat>();
    /// <summary>
    /// Youtubeのコメントキュー
    /// </summary>
    public static List<ReceiveMessageFormat> CommentQueue = new List<ReceiveMessageFormat>();
    /// <summary>
    /// Booyomiで読み上げるメッセージキュー
    /// </summary>
    public static List<ReceiveMessageFormat> MessageQueue = new List<ReceiveMessageFormat>();
    /// <summary>
    /// 棒読みちゃんの状態 0:停止 1:音声合成中 2:音声出力中
    /// </summary>
    public static int BooyomiState = 0;
    /// <summary>
    /// Aivisの状態 0:停止 1:音声合成中 2:音声出力中
    /// </summary>
    public static int AivisState = 0;
}

public enum Emotion{
    normal, // 0
    happy, // 1
    angry, // 2
    sad, // 3
    surprised, // 4
    shy, // 5
    excited, // 6
    smug, // 7
    calm, // 8
    waiting // 9
}

/// <summary>
/// QuQuのモーフ:
///  komaru, hohozome, koukakuage, bikkuri, okori, nikori, mayu_ue, mayu_sita,
/// mabataki, zitome, niramu, hitomi_small, hitomi_large, nagomi, ee, pero,
/// warai, niyari, wink_left, wink_right, heart, star, high_light_off
/// </summary>
public enum QuQuMorph
{
    komaru = 4,
    hohozome = 5,
    koukakuage = 7,
    bikkuri = 8,
    okori = 9,
    nikori = 10,
    mayu_ue = 11,
    mayu_sita = 12,
    mabataki = 13,
    zitome = 14,
    niramu = 15,
    hitomi_small = 16,
    hitomi_large = 17,
    nagomi = 19,
    ee = 21,
    pero = 22,
    warai = 43,
    niyari = 44,
    wink_left = 45,
    wink_right = 46,
    heart = 49,
    star = 50,
    high_light_off = 52,
}

/// <summary>
/// Anonのモーフ:
///  mouth_ω, mouth_v, mouth_yama, mouth_sankaku, mouth_shikaku, mouth_smile, mouth_sad, mouth_awawa, mouth_puku,
/// eye_close, eye_smile, eye_nagomi, eye_jito, eye_niya, eye_happy, eye_angly, eye_sad, eye_pleasure, eye_naki,
/// eye_small, eye_up, eye_down, highlight_hide, eye_shirome, eye_hoshi, eye_heart, eye_guruguru, mayu_tare,
/// mayu_joy, mayu_anger, mayu_serious, mayu_trouble, cheek_tere, namida, manpu_bikkuri, manpu_hatena
/// manpu_waiwai, manpu_gaan, manpu_heart, manpu_anger
/// </summary>
public enum AnonMorph
{
    mouth_ω = 28,
    mouth_v = 29,
    mouth_yama = 30,
    mouth_sankaku = 31,
    mouth_shikaku = 32,
    mouth_smile = 33,
    mouth_sad = 36,
    mouth_awawa = 39,
    mouth_puku = 42,
    eye_close = 61,
    eye_smile = 64,
    eye_nagomi = 67,
    eye_open = 70,
    eye_jito = 73,
    eye_niya = 76,
    eye_happy = 79,
    eye_angly = 82,
    eye_sad = 85,
    eye_pleasure = 88,
    eye_tare = 110,
    eye_naki = 112,
    eye_small = 115,
    eye_up = 117,
    eye_down = 118,
    highlight_hide = 128,
    eye_shirome = 132,
    eye_hoshi = 133,
    eye_heart = 134,
    eye_guruguru = 135,
    mayu_tare = 137,
    mayu_joy = 140,
    mayu_anger = 143,
    mayu_serious = 146,
    mayu_trouble = 149,
    cheek_tere = 163,
    namida = 165,
    manpu_bikkuri = 167,
    manpu_hatena = 168,
    manpu_waiwai = 169,
    manpu_gaan = 171,
    manpu_heart = 172,
    manpu_anger = 174,
}