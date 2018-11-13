using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPuzzle : Puzzle {
    public GameObject paperToPrint;

    private void Start()
    {
        StartCoroutine(OpenPrinter());
    }

    private void OnSayKeyword()
    {
        PuzzleSolved();
    }

    IEnumerator OpenPrinter()
    {
        Box.instance.GetComponent<Animation>().Play("OpenPrinter");
        Box.instance.GetComponent<AudioSource>().clip = openSound;
        Box.instance.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        PrintPaper();
    }

    public void PrintPaper()
    {
        paperToPrint.SetActive(true);
    }
}
