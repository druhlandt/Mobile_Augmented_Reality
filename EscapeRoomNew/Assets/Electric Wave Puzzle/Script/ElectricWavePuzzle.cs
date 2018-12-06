using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWavePuzzle : MonoBehaviour
{
    public delegate void OnComplete();
    public OnComplete onComplete;

    public Material monitor1;
    public Material monitor2;

    public bool gamePause = false;

    private AudioSource audioSource;
    private float m1_point1 = 0;
    private float m1_point2 = 0;
    private float m2_point1 = 0;
    private float m2_point2 = 0;
    private bool complete1 = false;
    private bool complete2 = false;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        SetUp();
    }

    public void SetUp ()
    {
        gamePause = false;

        m1_point1 = Random.Range(0, 10) / 10f;
        m1_point2 = Random.Range(1, 10);
        m2_point1 = Random.Range(0, 10) / 10f;
        m2_point2 = Random.Range(1, 10);

        m1_point1 = Mathf.Round(m1_point1 * 100f) / 100f;
        m1_point2 = Mathf.Round(m1_point2 * 100f) / 100f;
        m2_point1 = Mathf.Round(m2_point1 * 100f) / 100f;
        m2_point2 = Mathf.Round(m2_point2 * 100f) / 100f;

        CheckComplete();
    }
	
	void Update ()
    {
        monitor1.SetFloat("_Point1", m1_point1);
        monitor1.SetFloat("_Point2", m1_point2);

        monitor2.SetFloat("_Point1", m2_point1);
        monitor2.SetFloat("_Point2", m2_point2);
    }

    public void AMP_UP()
    {
        m1_point1 += 0.05f;
        m1_point1 = Mathf.Round(m1_point1 * 100f) / 100f;

        if (m1_point1 >= m2_point1 + 2.5f)
        {
            m1_point1 = m2_point1;
        }

        CheckComplete();
    }
    public void AMP_DOWN()
    {
        m1_point1 -= 0.05f;
        m1_point1 = Mathf.Round(m1_point1 * 100f) / 100f;

        if (m1_point1 <= m2_point1 - 2.5f)
        {
            m1_point1 = m2_point1;
        }

        CheckComplete();
    }

    public void FRE_UP()
    {
        if (m1_point2 < 10)
            m1_point2 += 0.5f;

        CheckComplete();
    }
    public void FRE_DOWN()
    {
        if (m1_point2 >= 0.5f)
            m1_point2 -= 0.5f;

        CheckComplete();
    }

    public void CheckComplete ()
    {
        float pp = m1_point1 % 2.5f;
        pp = Mathf.Round(pp * 100f) / 100f;

        if (m1_point1 == m2_point1 || pp == m2_point1)
        {
            complete1 = true;
        }
        else
        {
            complete1 = false;
        }

        if (m1_point2 == m2_point2)
        {
            complete2 = true;
        }
        else
        {
            complete2 = false;
        }

        //---- Sound Control ----//
        pp = 0.2f + ((m2_point2 - m1_point2) / 10);
        pp = (1 - pp) - 0.8f;
        pp = Mathf.Round(pp * 100f) / 100f;
        if (pp == 0)
        {
            pp = 0.05f;
        }
        audioSource.pitch = pp;

        if (m1_point1 <= m2_point1)
        {
            pp = m2_point1 - m1_point1;
        }

        if (m1_point1 > m2_point1)
        {
            pp = m1_point1 - m2_point1;

            float aa = 2.5f + m2_point1;
            aa = Mathf.Round(aa * 100f) / 100f;

            if (m1_point1 > m2_point1 + 1 && m1_point1 <= aa)
            {
                pp = (2.5f + m2_point1) - m1_point1;
                pp = Mathf.Round(pp * 100f) / 100f;
            }
        }
        audioSource.volume = pp + 0.15f;
        //////////////////////////

        if (complete1 && complete2)
        {
            gamePause = true;
            onComplete();
            print("Complete");
        }
    }
}
