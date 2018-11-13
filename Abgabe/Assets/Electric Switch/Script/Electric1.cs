using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric1 : MonoBehaviour
{
    public delegate void OnComplete();
    public OnComplete onComplete;

    public delegate void OnFail();
    public OnFail onFail;

    [HideInInspector]
    public int[] buttonPressed = new int[5] { 0, 0, 0, 0, 0};

    public int[] buttonPower = new int[5];

    public Material lightOn_Material;
    public Material lightOff_Material;


    private int lightScore = 0;
    public GameObject[] lights;

    private AudioSource audioSource;
    public AudioClip tick_sound;
    public AudioClip button_Sound;
    public AudioClip complete_sound;

    [HideInInspector]
    public bool gamePause = false;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        Setup();
    }

    public void Setup()
    {
        lightScore = 0;
        buttonPressed = new int[5] { 0, 0, 0, 0, 0 };
        List<int> list = new List<int>() { 0, 1, 2, 3, 4 };

        int index = 0;
        int lastNum = 0;

        while (list.Count > 0)
        {
            int r = Random.Range(0, list.Count);

            if (index == 0)
            {
                buttonPower[list[r]] = Random.Range(1, 8);
                lastNum = buttonPower[list[r]];
            }
            if (index == 1)
            {
                buttonPower[list[r]] = Random.Range(2, (10 - lastNum) + 1);
                lastNum += buttonPower[list[r]];
            }
            if (index == 2)
            {
                if (lastNum < 10)
                {
                    buttonPower[list[r]] = Random.Range(1, (10 - lastNum) + 1);
                    lastNum += buttonPower[list[r]];
                }
                else
                {
                    buttonPower[list[r]] = Random.Range(2, 8);
                }
            }
            if (index == 3)
            {
                if (lastNum < 10)
                {
                    buttonPower[list[r]] = 10 - lastNum;
                    lastNum += buttonPower[list[r]];
                }
                else
                {
                    buttonPower[list[r]] = Random.Range(3, 8);
                }
            }
            if (index == 4)
            {
                buttonPower[list[r]] = Random.Range(3, 6);
            }
            index++;
            list.Remove(list[r]);
        }
    }

    public void ClickButton(int index)
    {
        if (!gamePause)
        {
            if (buttonPressed[index] == 0)
            {
                lightScore += buttonPower[index];
                buttonPressed[index] = 1;
            }
            else
            {
                lightScore -= buttonPower[index];
                buttonPressed[index] = 0;
            }
            if (lightScore > 10)
            {
                ResetGame();
            }
            audioSource.PlayOneShot(button_Sound);
        }
    }

    public void ResetGame()
    {
        gamePause = false;

		lightScore = 0;
        buttonPressed = new int[5] { 0, 0, 0, 0, 0 };
        onFail();
    }

    private IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 10; i++)
        {
            lights[i].GetComponent<MeshRenderer>().material = lightOff_Material;
            Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
            halo.enabled = false;
        }
        yield return new WaitForSeconds(0.35f);
        for (int i = 0; i < 10; i++)
        {
            lights[i].GetComponent<MeshRenderer>().material = lightOn_Material;
            Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
            halo.enabled = true;
        }
        yield return new WaitForSeconds(0.35f);
        for (int i = 0; i < 10; i++)
        {
            lights[i].GetComponent<MeshRenderer>().material = lightOff_Material;
            Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
            halo.enabled = false;
        }
        yield return new WaitForSeconds(0.35f);
        for (int i = 0; i < 10; i++)
        {
            lights[i].GetComponent<MeshRenderer>().material = lightOn_Material;
            Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
            halo.enabled = true;
        }
        yield return new WaitForSeconds(0.35f);
        for (int i = 0; i < 10; i++)
        {
            lights[i].GetComponent<MeshRenderer>().material = lightOff_Material;
            Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
            halo.enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            lights[i].GetComponent<MeshRenderer>().material = lightOn_Material;
            Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
            halo.enabled = true;
        }
        audioSource.PlayOneShot(complete_sound);

        onComplete();
    }

    private float timeLight = 0;
    private int lastScore = 0;

    void Update ()
    {
        timeLight += ((float)lightScore - timeLight) / 10;
        if (Mathf.RoundToInt(timeLight) != 10 && !gamePause)
        {
            if (lastScore != Mathf.RoundToInt(timeLight))
            {
                if (timeLight > lastScore)
                {
                    audioSource.PlayOneShot(tick_sound);
                }
                lastScore = Mathf.RoundToInt(timeLight);
            }
            for (int i = 0; i < 10; i++)
            {
                if (i < Mathf.RoundToInt(timeLight))
                {
                    lights[i].GetComponent<MeshRenderer>().material = lightOn_Material;
                    Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
                    halo.enabled = true;
                }
                else
                {
                    lights[i].GetComponent<MeshRenderer>().material = lightOff_Material;
                    Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
                    halo.enabled = false;
                }
            }
        }
        else if (!gamePause)
        {
            audioSource.PlayOneShot(tick_sound);

            for (int i = 0; i < 10; i++)
            {
                lights[i].GetComponent<MeshRenderer>().material = lightOn_Material;
                Behaviour halo = (Behaviour)lights[i].GetComponent("Halo");
                halo.enabled = true;
            }
            gamePause = true;
            StartCoroutine(WaitAndPrint());
        }
    }
}