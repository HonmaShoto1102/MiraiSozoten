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

    public bool ResultStart;

    [Header("=====GameScene��������=====")]
    [SerializeField]
    [Header("Player��script")]
    private Player _playerScript;

    [SerializeField]
    [Header("GameScene��Player")]
    private GameObject _playerObject;

    [Header("==============================\n")]

    [SerializeField]
    [Header("ClickLoadScene��script")]
    private NextSceneLoad _nextSceneLoadScript;

    [SerializeField] [Header("***���U���g��ԂɂȂ�܂�false***\n")]
    private GameObject ResultSceneGameOblect;
    [SerializeField]
    private GameObject UiCanvas;

    [SerializeField] [Header("01�`04�܂ł̃t�F�[�Y������")]
    private List<GameObject> _phaseUiList;

    //���W���킹�p
    [SerializeField] [Header("ResultPlayer01��GameObject")]
    private GameObject _resultPlayer01;

    //���U���g���̓���p���W
    [SerializeField] [Header("PlayersPosition��GameObject")]
    private GameObject _playersPosition;

    //Phase02�ȍ~�Ɏg��
    [SerializeField] [Header("shipsPosition2��GameObject")]
    private GameObject _shipsPosition2;

    [Header("***************************\n")]

    //�e�t�F�[�Y�ŏ��X�ɃA�N�e�B�u���������Ă���
    [Header("Phase01")]
    [SerializeField]
    private GameObject _phase01ClosingEventCanvas;//TextWindow
    [SerializeField]
    private GameObject _phase01PlayersEndPosition;
    [SerializeField] [Header("Phase01�̑D�̑��x")]
    private float ShipsMovingSpeed;
    [SerializeField]
    private List<Text> _phase01OrderCountTextLists;
    [SerializeField]
    private List<Text> _phase01PlayerNameTextLists;


    [Header("Phase02")]
    [SerializeField]
    private Image _phase02RankCrownImage;
    [SerializeField]
    private Image _phase02RankNumberImage;
    [SerializeField]
    private Text _phase02PlayerName;
    [SerializeField]
    private Text _phase02OrderCount;
    [SerializeField]
    private List<Text> _phase02OrderTextLists;
    [SerializeField]
    private GameObject _phase02PlayerPosition;

    [Header("Phase03")]
    [SerializeField]
    private Image _phase03CrownImage;
    [SerializeField]
    private Image _phase03NumberImage;
    [SerializeField]
    private Text _phase03PlayerName;
    [SerializeField]
    private Text _phase03OrderCount;

    [Header("Phase04")]
    [SerializeField]
    private List<Text> _phase04PlayerNameList;
    [SerializeField]
    private List<Text> _phase04OrderCountList;
    [SerializeField]
    private List<Text> _phase04MoneyCountList;
    [SerializeField]
    [Header("�{�^�����������ƕ\��")]
    private GameObject _phase04InformationUi;
    [SerializeField]
    private Button _stayButton;
    [SerializeField]
    private Button _exitButton;

    [Header("\n���ʂ̉摜(Asset����I��)")]
    [SerializeField]
    private List<Sprite> RankCrownImageLists;
    [SerializeField]
    private List<Sprite> RankNumberImageLists;

    Keyboard _keyboard;

     //Start is called before the first frame update
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
        if (!isLocal) return;

        if (ResultStart == false) return;

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
    //�����̏��ʎ擾
    private int MyPlayerRank()//***************************************************************************************************     GameScene����
    {
        int rank = 1;

        return rank;
    }

    private void RankImageChange()
    {
        _phase02RankCrownImage.sprite = RankCrownImageLists[MyPlayerRank() - 1];
        _phase02RankNumberImage.sprite = RankNumberImageLists[MyPlayerRank() - 1];
        _phase03CrownImage.sprite = RankCrownImageLists[MyPlayerRank() - 1];
        _phase03NumberImage.sprite = RankNumberImageLists[MyPlayerRank() - 1];
    }

    //�����̖��O���擾����
    private string strixMyPlayerName()
    {
        string _myPlayerName = StrixNetwork.instance.selfRoomMember.GetName();

        return _myPlayerName;
    }

    //Result�V�[����PlayerName���ꊇ�ύX
    private void MyPlayerNameChange()
    {
        _phase01PlayerNameTextLists[strixMyEntryNumber() - 1].text = strixMyPlayerName();
        _phase02PlayerName.text = strixMyPlayerName();
        _phase03PlayerName.text = strixMyPlayerName();
        _phase04PlayerNameList[MyPlayerRank()-1].text = strixMyPlayerName();
    }

    private int MyPlayerOrderCount()//***************************************************************************************************     GameScene����
    {
        int myOrder = _playerScript.medal;//�v�m�F
        return myOrder;
    }

    private void OrderCountChange()
    {
        _phase01OrderCountTextLists[strixMyEntryNumber() - 1].text = $"{MyPlayerOrderCount()}";
        _phase02OrderCount.text = $"{MyPlayerOrderCount()}";
        _phase03OrderCount.text = $"{MyPlayerOrderCount()}";
        _phase04OrderCountList[MyPlayerRank() - 1].text = $"{MyPlayerOrderCount()}";
    }

    private int MyPlayerMoneyCount()//***************************************************************************************************     GameScene����
    {
        int myMoney = _playerScript.money;
        return myMoney;
    }

    private void MoneyCountChange()
    {
        _phase04MoneyCountList[MyPlayerRank() - 1].text = $"{MyPlayerMoneyCount()}";
    }

    //���̃v���C���[�̖��O�A�M�͂̐��A�R�C���̐����擾���Ă���
    private void OtherPlayers()
    {

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

    //Player�̊K�w��GameScene����O��
    private void PlayerParentDetachChildren()
    {
        Transform _playerParent = _playerObject.transform.parent;//player�̈��̃I�u�W�F�N�g���擾
        _playerParent.DetachChildren();
    }

    //�ŏ��̈ʒu����   ��x�����Ăяo��
    private void StartStep()
    {
        PlayerParentDetachChildren();
        UiCanvas.SetActive(true);
        PlayerGameObjectInitialization();
        RankImageChange();
        MyPlayerNameChange();
        OrderCountChange();
        MoneyCountChange();
    }

    private void stepPhase01()
    {
        _phaseUiList[0].SetActive(true);
        
        //�D�̓�����W�擾
        Transform myTransform = _playersPosition.transform;
        Vector3 StartPosition = myTransform.position;
        Vector3 EndPosition = _phase01PlayersEndPosition.transform.position;

        float _distance = StartPosition.z - EndPosition.z;
        float speed = Time.deltaTime * ShipsMovingSpeed / Mathf.Abs(_distance);

        //�D�̓�����W�X�V
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
        _shipsPosition2.SetActive(true);

        //�D�̍��W�擾
        Transform myTransform = _playersPosition.transform;
        Vector3 myVector3 = _phase02PlayerPosition.transform.position;
        //���W�X�V
        myTransform.position = myVector3;
        
        //�v���C���[���
        

        var key_B = _keyboard.bKey;

        if (key_B.wasPressedThisFrame)
        {
            _nowPhase = NowPhase.Phase03;
        }
    }
    private void stepPhase03()
    {
        _phaseUiList[1].SetActive(false);
        _phaseUiList[2].SetActive(true);

        //�{�^�����͏���
    }
    private void stepPhase04()
    {
        _phaseUiList[2].SetActive(false);
        _phaseUiList[3].SetActive(true);

        //�{�^�����͏���
    }

    public void phase03_ResultDetailsButton()
    {
        _nowPhase = NowPhase.Phase04;
    }

    public void phase04_RoomExitButton()
    {
        _phase04InformationUi.SetActive(true);
        //Information���ɓ��͂���Ȃ��悤�Ƀ{�^��������
        _exitButton.interactable = false;
        _stayButton.interactable = false;
    }

    public void phase04_RoomStayButton()
    {
        //���[���}�b�`���O�V�[���ɑJ��
        //���[���}�b�`���O�V�[���Ƀ��v���J�̂����I�u�W�F�N�g���������܂Ȃ��悤�ɔj������ or �����̏�񂾂����������ނ悤�ɂ���
        _nextSceneLoadScript.LoadSceneStart("���[���}�b�`���O�HScene");//�V�[��������
    }

    public void phase04_Information_YES_Button()
    {
        //���[�����甲���āA���C�����j���[�V�[���ɑJ��
        _nextSceneLoadScript.LoadSceneStart("���C�����j���[Scene");//�V�[��������
    }

    public void phase04_Information_NO_Button()
    {
        _phase04InformationUi.SetActive(false);
        //Information������phase04�̃{�^���L����
        _exitButton.interactable = true;
        _stayButton.interactable = true;
    }
}
