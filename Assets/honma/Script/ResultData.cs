using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Client.Core;

public class ResultData : StrixBehaviour
{
    public enum NowPhase
    {
        Start,
        Phase01,
        Phase02,
        Phase03,
        Phase04
    }
    private NowPhase _nowPhase;

    public bool ResultStart = false;


    [SerializeField]
    [Header("Player��script")]
    private Player _playerScript;

    [SerializeField]
    [Header("GameScene��Player")]
    private GameObject _playerObject;

    [SerializeField][Header("***���U���g��ԂɂȂ�܂�false***\n")]
    private GameObject ResultSceneGameOblect;
    [SerializeField]
    private GameObject UiCanvas;

    [SerializeField][Header("01�`04�܂ł̃t�F�[�Y������")]
    private List<GameObject> _phaseUiList;

    [SerializeField][Header("�{�^�����������ƕ\��")]
    private GameObject _phase04InformationUi;

    //���W���킹�p
    [SerializeField][Header("ResultPlayer01��GameObject")]
    private GameObject _resultPlayer01;

    //���U���g���̓���p���W
    [SerializeField][Header("PlayersPosition��GameObject")]
    private GameObject _playersPosition;

    //Phase02�ȍ~�Ɏg��
    [SerializeField][Header("shipsPosition2��GameObject")]
    private GameObject _shipsPosition2;

    [Header("***************************\n")]

    //�e�t�F�[�Y�ŏ��X�ɃA�N�e�B�u���������Ă���
    [Header("Phase01")]
    [SerializeField]
    private GameObject _phase01ClosingEventCanvas;//TextWindow
    [SerializeField]
    private GameObject _phase01PlayersEndPosition;
    [SerializeField][Header("Phase01�̑D�̑��x")]
    private float ShipsMovingSpeed;

    [Header("Phase02")]
    [SerializeField]
    private GameObject _phase02FullSizePanel;
    [SerializeField]
    private Image _rankCrownImage;
    [SerializeField]
    private Image _rankNumberImage;
    [SerializeField]
    private Text _phase02PlayerName;
    [SerializeField]
    private Text _phase02OrderCount;
    [SerializeField]
    private List<Text> _phase02OrderTextLists;
   

    Keyboard _keyboard;

    // Start is called before the first frame update
    void Start()
    {
        // ���݂̃L�[�{�[�h���
        _keyboard = Keyboard.current;

        // �L�[�{�[�h�ڑ��`�F�b�N
        if (_keyboard == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ���
            // Keyboard.current��null�ɂȂ�
            return;
        }

        ResultStart = false;
        _nowPhase = NowPhase.Start;

        //Game�V�[�����Ɏg��Ȃ��I�u�W�F�N�g��false�ɂ���
        ResultSceneGameOblect.SetActive(false);
        UiCanvas.SetActive(false);
        _phase04InformationUi.SetActive(false);
        foreach (var ListObject in _phaseUiList)
        {
            ListObject.SetActive(false);
        }
        _shipsPosition2.SetActive(false);

        //�e�t�F�[�Y�̃A�N�e�B�u������
        _phase01ClosingEventCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (ResultStart == false)
            //return;
        switch (_nowPhase)
        {
            case NowPhase.Start:
                ResultSceneGameOblect.SetActive(true);
                StartStep();
                _nowPhase = NowPhase.Phase01;
                break;
            case NowPhase.Phase01:
                stepPhase01();
                break;
            case NowPhase.Phase02:
                stepPhase02();
                break;
            case NowPhase.Phase03:
                stepPhase03();
                break;
            case NowPhase.Phase04:
                stepPhase04();
                break;
        }
        Debug.Log("�Ȃ��X�e�b�v:" + _nowPhase.ToString());
    }

    //���������Ԗڂɓ��������v���C���[����������
    private int strixMyEntryNumber()
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

    //�����̖��O���擾����
    private string strixMyPlayerName()
    {
        string _myPlayerName = StrixNetwork.instance.selfRoomMember.GetName();

        return _myPlayerName;
    }

    //����̈ʒu�ɒu�����߂ɃT�C�Y�Ȃǂ̏��������s��
    private void PlayerGameObjectInitialization()
    {
        _playerObject.transform.localScale = _resultPlayer01.transform.localScale;
        _playerObject.transform.eulerAngles = _resultPlayer01.transform.eulerAngles;
        _playerObject.transform.position = _resultPlayer01.transform.position;

        if(strixMyEntryNumber()>1)
        {
            Transform myTransform = _resultPlayer01.transform;
            Vector3 myVector3 = myTransform.position;
            myVector3.x = myVector3.x - ((float)strixMyEntryNumber() - 1.0f);
            
            myTransform.position = myVector3;
        }
    }

    //�ŏ��̈ʒu����   ��x�����Ăяo��
    private void StartStep()
    {
        if (!isLocal) return;

        UiCanvas.SetActive(true);
        PlayerGameObjectInitialization();
    }

    private void stepPhase01()
    {
        _phaseUiList[0].SetActive(true);

        Transform myTransform = _playersPosition.transform;
        Vector3 StartPosition = myTransform.position;
        Vector3 EndPosition = _phase01PlayersEndPosition.transform.position;

        float _distance = StartPosition.z - EndPosition.z;
        float speed = Time.deltaTime * ShipsMovingSpeed / Mathf.Abs(_distance);//  40.0f��z���W�̑��΋���

        myTransform.position = Vector3.Lerp(StartPosition, EndPosition, speed);

        if(speed>0.99f)//����speed�͊���    �����͊��ƓK���ȏ���
        {
            _phase01ClosingEventCanvas.SetActive(true);
            var key_B = _keyboard.bKey;

            if (key_B.wasPressedThisFrame)
            {
                _nowPhase = NowPhase.Phase02;
            }
        }
    }
    private void stepPhase02()
    {
        _phaseUiList[0].SetActive(false);
        _phaseUiList[1].SetActive(true);
    }
    private void stepPhase03()
    {

    }
    private void stepPhase04()
    {

    }

}
