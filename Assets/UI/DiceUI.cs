using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum MoveSelect
{
    WAIT,
    OK,
    NO
}

public class DiceUI : MonoBehaviour
{
    [SerializeField] Text rollDice;
    [SerializeField] Text diceVol;
    [SerializeField] GameObject stopUI;
    [SerializeField] GameObject rollButton;

    MoveSelect isMoveOK = MoveSelect.WAIT;

    int moveVol;

    int minD, maxD;

    [SerializeField] GameObject firstButton;
    [SerializeField] GameObject nextButton;

    public void SetFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    // Start is called before the first frame update
    void Start()
    {
        moveVol = -1;

    }

    // Update is called once per frame
    void Update()
    {
        if (moveVol < 0)
        {
            rollDice.text = Random.Range(minD, maxD + 1).ToString();
            diceVol.text = "";

            stopUI.SetActive(false);
            rollButton.active = true;
        }
        else if (moveVol > 0)
        {
            diceVol.text = "����" + moveVol.ToString()�@+�@"�}�X" ;
            stopUI.SetActive(false);
            rollButton.active = false;
            EventSystem.current.SetSelectedGameObject(nextButton);
        }
        else if(moveVol == 0)
        {
            switch (isMoveOK)
            {
                case MoveSelect.WAIT:
                    stopUI.SetActive(true);
                    break;
                case MoveSelect.OK:
                    stopUI.SetActive(false);
                    break;
                case MoveSelect.NO:
                    stopUI.SetActive(false);
                    break;
            }
        }
    }

    public void SetMoveVol(int vol)
    {
        moveVol = vol;
    }

    public int GetMoveVol() { return moveVol; }

    public void StopDice() {
        moveVol = Random.Range(minD, maxD+1);
        rollDice.text = "";
    }
    public void SetMinMax(int min,int max) { minD = min; maxD = max; }

    public void SetMoveOK() { isMoveOK = MoveSelect.OK; }
    public void SetMoveNO() { isMoveOK = MoveSelect.NO; }
    public void SetMoveWAIT() { isMoveOK = MoveSelect.WAIT; }

    public MoveSelect GetMoveOK() { return isMoveOK; }
}
