using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    //�����̍쐬�E�������[�h
    private bool RoomMode;

    //���[��ID�C���v�b�g�t�B�[���h
    public InputField RoomIDInputField;

    //���[��ID�ۑ��ϐ�
    public double RoomID;


    // Start is called before the first frame update
    void Start()
    {
        RoomID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���̓t�B�[���h�X�V
    public void InputRoomID()
    {
        string input = RoomIDInputField.text;

        //�����ȊO���������ꍇ�I��
        int temp = 1;
        if (!int.TryParse(input, out temp))
        {
            return;
        }

        //�������l�ɕϊ�
        int ans = int.Parse(input);
        RoomID = ans;
    }

    public void SceneChangetoGame()
    {
        SceneManager.LoadScene("OnlineLobbyScene");
    }

    

    


    //���[��ID���擾�i�Q�b�^�j
    public double GetRoomID() { return RoomID; }
}
