using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWave_GameSample : MonoBehaviour
{
    public GameObject showUI;
    public ElectricWavePuzzle electricWavePuzzle;

    public AudioClip completeSound;
    private AudioSource audioSource;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        electricWavePuzzle.onComplete += OnComplete;
    }

    private void OnComplete()
    {
        showUI.SetActive(true);

        audioSource.PlayOneShot(completeSound);
    }

    public void CloseUI()
    {
        showUI.SetActive(false);
        electricWavePuzzle.SetUp();
    }
}
