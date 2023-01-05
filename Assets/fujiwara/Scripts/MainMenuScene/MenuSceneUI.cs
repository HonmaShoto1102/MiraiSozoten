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
        //�v���C���[�h�R�}���h���\��
        transform.Find("PlayModeGroup").gameObject.SetActive(false);

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
        //�v���C���[�h�R�}���h���\��
        transform.Find("MultiPlayGroup").gameObject.SetActive(false);

        //�}���`�v���C�R�}���h��\��
        transform.Find("RoomIDInputGroup").gameObject.SetActive(true);
    }

    //�u�L�����Z���v����
    public void Press_MultiPlayCancell()
    {
        //�v���C���[�h�R�}���h���\��
        transform.Find("MultiPlayGroup").gameObject.SetActive(false);

        //�}���`�v���C�R�}���h��\��
        transform.Find("PlayModeGroup").gameObject.SetActive(true);
    }

    //�����ԍ����͌�A�u�Q���v����
    public void Press_AccessRoom()
    {
        //�ڑ��J�n�i�Q���j
        Manager.GetComponent<F_StrixConnect>().Connect(false);

    }



}
