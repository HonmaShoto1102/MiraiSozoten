using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadControll_LobbyScene : MonoBehaviour
{

    //���j���[�̏��
    [SerializeField] private bool isExitWindow;

    [SerializeField] bool ExitWindowCursorState;

    // Start is called before the first frame update
    void Start()
    {
        isExitWindow = false;
        ExitWindowCursorState = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Enter()
    {
        //�ގ�����
        if (isExitWindow&& ExitWindowCursorState)
        {
            GameObject.Find("LobbyManager").GetComponent<LobbyManager>().RoomExitAct();
        }

        //�L�����Z��
        else if(isExitWindow && !ExitWindowCursorState)
        {
            GameObject.Find("LobbyManager").GetComponent<LobbyManager>().RoomExitWindowClose();
        }
    }

    //�߂�{�^������
    public void Back()
    {
        isExitWindow = !isExitWindow;

        //�E�B���h�E�\��        
        if (isExitWindow)
        {
            //���C���E�B���h�E����o�b�N�m�F�E�B���h�E��
            GameObject.Find("LobbyManager").GetComponent<LobbyManager>().RoomExitWindowPopUp();
        }
        else
        {
            GameObject.Find("LobbyManager").GetComponent<LobbyManager>().RoomExitWindowClose();
        }

        
    }

    public void PadControll_Left()
    {
        if(isExitWindow)
        {
            ExitWindowCursorState = !ExitWindowCursorState;
        }

        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().ExitWindowCursorUpdate(ExitWindowCursorState);
    }

    public void PadControll_Right()
    {
        if (isExitWindow)
        {
            ExitWindowCursorState = !ExitWindowCursorState;
        }

        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().ExitWindowCursorUpdate(ExitWindowCursorState);

    }
}