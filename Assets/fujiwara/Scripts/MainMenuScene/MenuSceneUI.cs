using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject Manager;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //�u�}���`�v���C�v����
    public void Press_MultiPlay()
    {
        //�}���`�v���C�R�}���h��\��
        transform.Find("MultiPlayGroup").gameObject.SetActive(true);
    }

    //�u�������쐬�v����
    public void Press_CreateRoom()
    {
        //�ڑ��J�n�i�쐬�j
        Manager.GetComponent<F_StrixConnect>().Connect(true);
        
    }

    //�u�����������v����
    public void Press_JoinRoom()
    {

        //�}���`�v���C�R�}���h��\��
        transform.Find("RoomIDInputGroup").gameObject.SetActive(true);
    }

    //�����E�B���h�E�ŁA�u�L�����Z���v����
    public void Press_MultiPlayCancell()
    {
        //�v���C���[�h�R�}���h���\��
        transform.Find("MultiPlayGroup").gameObject.SetActive(false);
    }

    //�����ԍ����͌�A�u�Q���v����
    public void Press_AccessRoom()
    {
        //�ڑ��J�n�i�Q���j
        Manager.GetComponent<F_StrixConnect>().Connect(false);

    }


    //�����ԍ����͌�A�u�Q���v����
    public void Press_RoomFoundCancel()
    {

        //�}���`�v���C�R�}���h��\��
        transform.Find("RoomIDInputGroup").gameObject.SetActive(false);
    }

    /// <summary>
    /// ���[���������s������
    /// </summary>
    public void RoomAccessError()
    {
        transform.Find("RoomIDInputGroup").gameObject.SetActive(false);
        transform.Find("RoomAccessErrorGroup").gameObject.SetActive(true);

        GameObject.Find("MainMenuManager").GetComponent<PadControll_MenuScene>().StateChange_Back();
    }

    /// <summary>
    /// ���[���������s�E�B���h�E�Łu�߂�v���������Ƃ�
    /// </summary>
    public void Press_ErrorBackButton()
    {
        transform.Find("RoomAccessErrorGroup").gameObject.SetActive(false);
        transform.Find("RoomIDInputGroup").gameObject.SetActive(true);
    }

    /// <summary>
    /// �����E�B���h�E����}���`�v���C�E�B���h�E�ɖ߂�
    /// </summary>
    public void Back_AccessToMulti()
    {
        transform.Find("RoomIDInputGroup").gameObject.SetActive(false);
    }

    /// <summary>
    /// �}���`�v���C�E�B���h�E���烂�[�h�I���E�B���h�E�ɖ߂�
    /// </summary>
    public void Back_MultiToMode()
    {

        transform.Find("MultiPlayGroup").gameObject.SetActive(false);
    }

}
