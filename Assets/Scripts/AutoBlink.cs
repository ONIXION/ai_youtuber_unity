using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBlink : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer faceMR;
    [SerializeField] private bool isAnon; // キャラクターがAnonの場合はtrue, QuQuの場合はfalse
    [SerializeField]
    private float blinkSpanMin = 5.0f;
    [SerializeField]
    private float blinkSpanMax = 10.5f;
    private float countTime;
    private float blinkSpan;
    void Start()
    {
        countTime = 0.0f;
        blinkSpan = Random.Range(blinkSpanMin, blinkSpanMax);
    }
    void FixedUpdate()
    {
        CheckCountTime();
    }
    void CheckCountTime()
    {
        countTime += Time.deltaTime;
        if (countTime >= blinkSpan)
        {
            blinkSpan = Random.Range(blinkSpanMin, blinkSpanMax);
            countTime = 0.0f;
            StartCoroutine(Blink());
        }
    }
    IEnumerator Blink()
    {
        if (isAnon)
        {
            faceMR.SetBlendShapeWeight((int)AnonMorph.mabataki, 80);
            yield return new WaitForSeconds(0.01f);
            faceMR.SetBlendShapeWeight((int)AnonMorph.mabataki, 100);
            yield return new WaitForSeconds(0.04f);
            faceMR.SetBlendShapeWeight((int)AnonMorph.mabataki, 60);
            yield return new WaitForSeconds(0.015f);
            faceMR.SetBlendShapeWeight((int)AnonMorph.mabataki, 0);
        }
        else
        {
            faceMR.SetBlendShapeWeight((int)QuQuMorph.mabataki, 80);
            yield return new WaitForSeconds(0.01f);
            faceMR.SetBlendShapeWeight((int)QuQuMorph.mabataki, 100);
            yield return new WaitForSeconds(0.04f);
            faceMR.SetBlendShapeWeight((int)QuQuMorph.mabataki, 60);
            yield return new WaitForSeconds(0.015f);
            faceMR.SetBlendShapeWeight((int)QuQuMorph.mabataki, 0);
        }
    }
}
