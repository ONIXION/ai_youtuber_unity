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

public enum AnonMorph
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