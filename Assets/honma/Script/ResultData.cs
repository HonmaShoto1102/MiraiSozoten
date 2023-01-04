using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Client.Core;

public class ResultData : StrixBehaviour
{
    public enum nowPhase
    {
        Phase01,
        Phase02,
        Phase03,
        Phase04
    }

    [SerializeField][Header("***���U���g��ԂɂȂ�܂�false***\n")]
    private GameObject ResultSceneGameOblect;
    [SerializeField]
    private GameObject UiCanvas;

    [SerializeField][Header("01�`04�܂ł̃t�F�[�Y������")]
    private List<GameObject> _phaseUiList;

    [SerializeField][Header("�{�^�����������ƕ\��")]
    private GameObject _phase04InformationUi;

    [Header("***************************\n")]


    [SerializeField][Header("Player��script")]
    private Player _playerScript;

    [SerializeField][Header("GameScene��Player")]
    private Player _playerObject;

    //���W���킹�p
    [SerializeField][Header("kari oki�̃Q�[���I�u�W�F�N�g")]
    private GameObject _playerNo01;

    private GameObject _playerNo02;
    private GameObject _playerNo03;
    private GameObject _playerNo04;


    private int count = -1;
    Keyboard _keyboard;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�ŏ��̈ʒu����
    private void StartPosition()
    {
        if (!isLocal) return;

        PlayerGameObjectInitialization();

    }

    private int MyEntryNumber()
    {
        int count = 1;

        foreach (var RoomMember in StrixNetwork.instance.sortedRoomMembers)
        {
            if (StrixNetwork.instance.selfRoomMember.GetUid() != RoomMember.GetUid())
            {
                count++;
            }
            else//  selfRoomMember.GetUid() = RoomMember.GetUid()�̂Ƃ�
            {
                Debug.Log("���Ȃ���" + count + "�l�ڂł�");
                return count;
            }
        }

        return -1;
    }

    //����̈ʒu�ɒu�����߂ɃT�C�Y�Ȃǂ̏��������s��
    private void PlayerGameObjectInitialization()
    {
        _playerObject.transform.localScale = _playerNo01.transform.localScale;
        _playerObject.transform.eulerAngles = _playerNo01.transform.eulerAngles;
        _playerObject.transform.position = _playerNo01.transform.position;

        if(MyEntryNumber()>1)
        {
            Transform myTransform = _playerNo01.transform;
            Vector3 myVector3 = myTransform.position;
            myVector3.x = myVector3.x - ((float)MyEntryNumber() - 1.0f);
            
            myTransform.position = myVector3;
        }
    }

    private void stepPhase01()
    {

    }
    private void stepPhase02()
    {

    }
    private void stepPhase03()
    {

    }
    private void stepPhase04()
    {

    }

}
