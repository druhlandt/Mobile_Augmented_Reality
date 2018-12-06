using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    public delegate void OnComplete();
    public OnComplete onComplete;

    public int roll = 4;
    public int column = 4;

    private int[][] boardArray;
    public GameObject[] buttonObject;

    public Material material1;
    public Material material2;

    public MeshRenderer resetButton;

    [HideInInspector]
    private bool canReset = false;

    [HideInInspector]
    public bool gamePause = false;

    private AudioSource audioSource;

    public AudioClip button_Sound;
    public AudioClip complete_sound;

    void Start ()
    {
        boardArray = new int[roll][];

        audioSource = GetComponent<AudioSource>();

        Setup();
    }

    public void Setup()
    {
        for (int i = 0; i < (roll * column); i++)
        {
            int y = Mathf.FloorToInt(i / column);
            int x = i - (y * column);


            if (boardArray[y] == null)
            {
                boardArray[y] = new int[column];
            }

            boardArray[y][x] = 1;
            changeMaterial(i, 1);
            canReset = false;
            resetButton.material = material2;
        }

        audioSource.PlayOneShot(button_Sound);
    }

    public void ClickButton (int index)
    {
        if (!gamePause)
        {
            int y = Mathf.FloorToInt(index / column);
            int x = index - (y * column);

            canReset = true;
            resetButton.material = material1;

            if (boardArray[y][x] == 1)
            {
                boardArray[y][x] = changeValue(boardArray[y][x]);
                buttonObject[index].GetComponent<MeshRenderer>().material = material2;

                // +
                int changeIndex = 0;
                if (checkCanPress(y, x - 1))
                {
                    boardArray[y][x - 1] = changeValue(boardArray[y][x - 1]);
                    changeIndex = (y * column) + (x - 1);
                    changeMaterial(changeIndex, boardArray[y][x - 1]);
                }
                if (checkCanPress(y, x + 1))
                {
                    boardArray[y][x + 1] = changeValue(boardArray[y][x + 1]);
                    changeIndex = (y * column) + (x + 1);
                    changeMaterial(changeIndex, boardArray[y][x + 1]);
                }
                if (checkCanPress(y - 1, x))
                {
                    boardArray[y - 1][x] = changeValue(boardArray[y - 1][x]);
                    changeIndex = ((y - 1) * column) + x;
                    changeMaterial(changeIndex, boardArray[y - 1][x]);
                }
                if (checkCanPress(y + 1, x))
                {
                    boardArray[y + 1][x] = changeValue(boardArray[y + 1][x]);
                    changeIndex = ((y + 1) * column) + x;
                    changeMaterial(changeIndex, boardArray[y + 1][x]);
                }
            }
            else
            {
                boardArray[y][x] = 1;
                buttonObject[index].GetComponent<MeshRenderer>().material = material1;

                // *
                int changeIndex = 0;
                if (checkCanPress(y - 1, x - 1))
                {
                    boardArray[y - 1][x - 1] = changeValue(boardArray[y - 1][x - 1]);
                    changeIndex = ((y - 1) * column) + (x - 1);
                    changeMaterial(changeIndex, boardArray[y - 1][x - 1]);
                }
                if (checkCanPress(y - 1, x + 1))
                {
                    boardArray[y - 1][x + 1] = changeValue(boardArray[y - 1][x + 1]);
                    changeIndex = ((y - 1) * column) + (x + 1);
                    changeMaterial(changeIndex, boardArray[y - 1][x + 1]);
                }
                if (checkCanPress(y + 1, x - 1))
                {
                    boardArray[y + 1][x - 1] = changeValue(boardArray[y + 1][x - 1]);
                    changeIndex = ((y + 1) * column) + (x - 1);
                    changeMaterial(changeIndex, boardArray[y + 1][x - 1]);
                }
                if (checkCanPress(y + 1, x + 1))
                {
                    boardArray[y + 1][x + 1] = changeValue(boardArray[y + 1][x + 1]);
                    changeIndex = ((y + 1) * column) + (x + 1);
                    changeMaterial(changeIndex, boardArray[y + 1][x + 1]);
                }
            }
            audioSource.PlayOneShot(button_Sound);

            CheckWin();
        }
    }

    private bool checkCanPress(int y, int x)
    {
        if (x >= 0 && y >= 0 && x <= column - 1 && y <= roll - 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int changeValue (int value)
    {
        if (value == 1)
        {
            value = 0;
        }
        else
        {
            value = 1;
        }
        return value;
    }

    private void changeMaterial(int index, int value)
    {
        if (value == 1)
        {
            buttonObject[index].GetComponent<MeshRenderer>().material = material1;
        }
        else
        {
            buttonObject[index].GetComponent<MeshRenderer>().material = material2;
        }
    }

    private bool CheckWin()
    {
        int score = 0;
        for (int i = 0; i < (roll * column); i++)
        {
            int y = Mathf.FloorToInt(i / column);
            int x = i - (y * column);

            if (boardArray[y][x] == 0)
            {
                score++;
            }
        }
        if (score == roll * column)
        {
            audioSource.PlayOneShot(complete_sound);

            onComplete();

            return true;
        }
        return false;
    }

    void Update ()
    {
        for (int i = 0; i < buttonObject.Length; i++)
        {
            int y = Mathf.FloorToInt(i / column);
            int x = i - (y * column);

            Vector3 moveToPosition = buttonObject[i].transform.localPosition;
            moveToPosition.y = (boardArray[y][x] * 0.12f) + 0.03f;

            buttonObject[i].transform.localPosition = Vector3.MoveTowards(buttonObject[i].transform.localPosition, moveToPosition, 0.02f);
        }

        if (canReset)
        {
            Vector3 moveToPosition = resetButton.transform.localPosition;
            moveToPosition.y = 0.15f;
            resetButton.transform.localPosition = Vector3.MoveTowards(resetButton.transform.localPosition, moveToPosition, 0.02f);
        }
        else
        {
            Vector3 moveToPosition = resetButton.transform.localPosition;
            moveToPosition.y = 0.03f;
            resetButton.transform.localPosition = Vector3.MoveTowards(resetButton.transform.localPosition, moveToPosition, 0.02f);
        }
    }
}
