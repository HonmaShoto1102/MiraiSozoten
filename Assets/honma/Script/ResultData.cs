using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
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

    [Header("���U���g�J�n����GameScene�̕���True�ɂ���")]
    public bool ResultStart;

    [Header("�y�f�o�b�O�`�F�b�N�z")]
    [Header("�X�g���N�XOFF �� �f�o�b�O���Ƀ`�F�b�N������")]
    public bool LocalTestDebug;
    [Header("Game�V�[�����ڑ���Ԃ̂Ƃ��Ƀ`�F�b�N������(�V�[���J�ڂ��Ȃ�)")]
    public bool OnlyResultPlay;

    [Header("�y�V�[�����̓��́z")]
    [Header("���C�����j���[�V�[���������")]
    [SerializeField]
    private string MainMenuScene;
    [Header("���r�[�}�b�`���O�V�[���������")]
    [SerializeField]
    private string LobbyMatchingScene;

    [Header("=====GameScene��������=====")]
    [SerializeField]
    GameObject gameScene;
    [SerializeField]
    [Header("Player��script")]
    private List<Player> _playerScript;
    //private Player _playerScript;

    [SerializeField]
    [Header("GameScene��Player")]
    private List<GameObject> _playerObject;
    //private GameObject _playerObject;

    [SerializeField]
    [Header("GameScene��GameScene�I�u�W�F�N�g")]
    private GameObject _gameSceneObject;

    [Header("==============================\n")]

    [SerializeField]
    [Header("Phase01ClosingEventCanvas��script")]
    private TextController _textControllerScript;

    [SerializeField]
    [Header("ClickLoadScene��script")]
    private NextSceneLoad _nextSceneLoadScript;

    [SerializeField][Header("***���U���g��ԂɂȂ�܂�false***\n")]
    private GameObject AnimationObject;

    [SerializeField]
    private GameObject ResultSceneGameOblect;
    [SerializeField]
    private GameObject UiCanvas;

    [SerializeField] [Header("01�`04�܂ł̃t�F�[�Y������")]
    private List<GameObject> _phaseUiList;

    //���W���킹�p
    [SerializeField] [Header("ResultPlayer01��GameObject")]
    private List<GameObject> _resultPlayer01;
    //private GameObject _resultPlayer01;

    //���U���g���̓���p���W
    [SerializeField] [Header("PlayersPosition��GameObject")]
    private GameObject _playersPosition;

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
    [SerializeField]
    private List<GameObject> _debugShipList;


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
    private List<Text> _phase02OrderTextList;
    [SerializeField]
    private List<GameObject> _phase02CameraList;

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

    private List<int> _medalList;

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
        AnimationObject.SetActive(false);
        ResultSceneGameOblect.SetActive(false);
        UiCanvas.SetActive(false);
        _phase04InformationUi.SetActive(false);
        foreach (var ListObject in _phaseUiList)
        {
            ListObject.SetActive(false);
        }

        //�e�t�F�[�Y�̃A�N�e�B�u������
        _phase01ClosingEventCanvas.SetActive(false);

        //Phase02�J�����̏�����
        foreach(var camera in _phase02CameraList)
        {
            camera.SetActive(false);
        }

        //�f�o�b�O�p�̑D�̃I�u�W�F�N�g
        foreach (var debugShip in _debugShipList)
        {
            debugShip.SetActive(false);
            debugShip.transform.GetChild(0).gameObject.SetActive(false);
        }

        foreach(var ResultPlayer in _resultPlayer01)
        {
            ResultPlayer.transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!LocalTestDebug)
        {
            if (!isLocal) return;

            if (ResultStart == false) return;
        }

        switch (_nowPhase)
        {
            case NowPhase.Start:
                StartStep();
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
        int count = 3;
        if (!LocalTestDebug)
        {
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
        }
        return count;
    }
    //�����̏��ʎ擾
    private int MyPlayerRank(int number)//***************************************************************************************************     GameScene����
    {
        _medalList[0] = _playerScript[0].medal;
        _medalList[1] = _playerScript[1].medal;
        _medalList[2] = _playerScript[2].medal;
        _medalList[3] = _playerScript[3].medal;

        //�~���Ƀ\�[�g
        _medalList.Sort((a, b) => b - a);


        int rank = _medalList.IndexOf(number-1);

        return rank;
    }

    private void RankImageChange()
    {
        _phase02RankCrownImage.sprite = RankCrownImageLists[MyPlayerRank(1) - 1];
        _phase02RankNumberImage.sprite = RankNumberImageLists[MyPlayerRank(1) - 1];
        _phase03CrownImage.sprite = RankCrownImageLists[MyPlayerRank(1) - 1];
        _phase03NumberImage.sprite = RankNumberImageLists[MyPlayerRank(1) - 1];
    }

    //�����̖��O���擾����
    private string strixMyPlayerName(int rank)
    {
        string _myPlayerName = _playerScript[rank].playerName;// = "Test���[�h";

        if (!LocalTestDebug)
        {
            _myPlayerName = StrixNetwork.instance.selfRoomMember.GetName();
        }
            return _myPlayerName;
    }

    //Result�V�[����PlayerName���ꊇ�ύX
    private void MyPlayerNameChange()
    {
        //_phase01PlayerNameTextLists[strixMyEntryNumber() - 1].text = strixMyPlayerName();
        _phase01PlayerNameTextLists[MyPlayerRank(1)].text = _playerScript[MyPlayerRank(1)].playerName;//
        _phase01PlayerNameTextLists[MyPlayerRank(2)].text = _playerScript[MyPlayerRank(2)].playerName;
        _phase01PlayerNameTextLists[MyPlayerRank(3)].text = _playerScript[MyPlayerRank(3)].playerName;
        _phase01PlayerNameTextLists[MyPlayerRank(4)].text = _playerScript[MyPlayerRank(4)].playerName;


        _phase02PlayerName.text = strixMyPlayerName(1);
        _phase03PlayerName.text = strixMyPlayerName(1);

        //_phase04PlayerNameList[MyPlayerRank() - 1].text = strixMyPlayerName();
        _phase04PlayerNameList[MyPlayerRank(1)].text = _playerScript[MyPlayerRank(1)].playerName;
        _phase04PlayerNameList[MyPlayerRank(2)].text = _playerScript[MyPlayerRank(2)].playerName;
        _phase04PlayerNameList[MyPlayerRank(3)].text = _playerScript[MyPlayerRank(3)].playerName;
        _phase04PlayerNameList[MyPlayerRank(4)].text = _playerScript[MyPlayerRank(4)].playerName;


    }

    private int MyPlayerOrderCount(int number)//***************************************************************************************************     GameScene����
    {
        _medalList[0] = _playerScript[0].medal;
        _medalList[1] = _playerScript[1].medal;
        _medalList[2] = _playerScript[2].medal;
        _medalList[3] = _playerScript[3].medal;

        //�~���Ƀ\�[�g
        _medalList.Sort((a, b) => b - a);


        int rank = _medalList[number - 1];

        return rank;
    }

    private void OrderCountChange()
    {
        // Player��List����Ȃ��ꍇ
        //_phase01OrderCountTextLists[strixMyEntryNumber() - 1].text = $"{MyPlayerOrderCount()}";
        //_phase02OrderCount.text = $"{MyPlayerOrderCount()}";
        //_phase03OrderCount.text = $"{MyPlayerOrderCount()}";
        //_phase04OrderCountList[MyPlayerRank() - 1].text = $"{MyPlayerOrderCount()}";

        _phase01OrderCountTextLists[0].text = $"{MyPlayerOrderCount(0)}";
        _phase01OrderCountTextLists[1].text = $"{MyPlayerOrderCount(1)}";
        _phase01OrderCountTextLists[2].text = $"{MyPlayerOrderCount(2)}";
        _phase01OrderCountTextLists[3].text = $"{MyPlayerOrderCount(3)}";

        _phase02OrderCount.text = $"{MyPlayerOrderCount(0)}";
        _phase03OrderCount.text = $"{MyPlayerOrderCount(0)}";

        _phase04OrderCountList[MyPlayerRank(0)].text = $"{MyPlayerOrderCount(0)}";
        _phase04OrderCountList[MyPlayerRank(1)].text = $"{MyPlayerOrderCount(1)}";
        _phase04OrderCountList[MyPlayerRank(2)].text = $"{MyPlayerOrderCount(2)}";
        _phase04OrderCountList[MyPlayerRank(3)].text = $"{MyPlayerOrderCount(3)}";

    }

    private int MyPlayerMoneyCount(int number)//***************************************************************************************************     GameScene����
    {
        int myMoney = 1234;
        
        if (!OnlyResultPlay)
        {
            myMoney = _playerScript[number].money;
        }
        return myMoney;
    }

    private void MoneyCountChange()
    {
        _phase04MoneyCountList[MyPlayerRank(1)].text = $"{MyPlayerMoneyCount(MyPlayerRank(1))}";

        _phase04MoneyCountList[MyPlayerRank(2)].text = $"{MyPlayerMoneyCount(2)}";
        _phase04MoneyCountList[MyPlayerRank(3)].text = $"{MyPlayerMoneyCount(3)}";
        _phase04MoneyCountList[MyPlayerRank(4)].text = $"{MyPlayerMoneyCount(4)}";

    }

    //���̃v���C���[�̖��O�A�M�͂̐��A�R�C���̐����擾���Ă���
    private void OtherPlayers()
    {
        int rank = _playerScript[0].medal;
    }

    //����̈ʒu�ɒu�����߂ɃT�C�Y�Ȃǂ̏��������s��
    private void PlayerGameObjectInitialization()
    {
        if (!LocalTestDebug)
        {
            //debug�p�̃v���C���[���p
        }
       

       
        for(int i=0;i<_playerObject.Count;i++)
        {
            _playerObject[i].transform.localScale = _resultPlayer01[i].transform.localScale;
            _playerObject[i].transform.eulerAngles = _resultPlayer01[i].transform.eulerAngles;
            _playerObject[i].transform.position = _resultPlayer01[i].transform.position;

            //Transform myTrans = _playerObject[i].transform;
            //Vector3 myVector3 = myTrans.position;
            //myVector3.x = myVector3.x - (i * (-1.0f));
            //myTrans.position = myVector3;
        }

        //if(strixMyEntryNumber()>1)
        //{
        //    Transform myTransform = _resultPlayer01.transform;
        //    Vector3 myVector3 = myTransform.position;
        //    myVector3.x = myVector3.x - ((float)strixMyEntryNumber() - 1.0f);
            
        //    myTransform.position = myVector3;
        //}
    }

    //Player�̊K�w��GameScene����O��
    private void PlayerParentDetachChildren()
    {
        //Transform _playerParent = _playerObject.transform.parent;


        for(int i=0;i<_playerObject.Count;i++)
        {
            GameObject gameObjectPaerent = _playerObject[i].transform.parent.gameObject;//player�̈��̃I�u�W�F�N�g���擾
            gameObjectPaerent.transform.DetachChildren();
        }
    }


    //�ŏ��̈ʒu����   ��x�����Ăяo��
    private void StartStep()
    {
        if (!LocalTestDebug)
        {
            
        }

        if (!OnlyResultPlay)
        {
            PlayerParentDetachChildren();// ���炭GameSceneObject��ResultSceneObject�̊Ԃ�Player���o������
            
        }
        else
        {
            foreach (var debugShip in _debugShipList)
            {
                debugShip.SetActive(true);
                debugShip.transform.GetChild(0).gameObject.SetActive(true);
            }
            
            for (int i = 0; i < _resultPlayer01.Count; i++)
            {
               _resultPlayer01[i].transform.GetChild(0).gameObject.SetActive(true);

            }
        }

        AnimationObject.SetActive(true);
        UiCanvas.SetActive(true);
        gameSceneFalse();//���̊֐���Game�V�[�����I�t�ɂ���  **********************************************************************************************:
        ResultSceneGameOblect.SetActive(true);
        PlayerGameObjectInitialization();
        RankImageChange();
        MyPlayerNameChange();
        OrderCountChange();
        MoneyCountChange();
    }

    private void stepPhase01()
    {
        _phaseUiList[0].SetActive(true);//    animation��true�ɂ���
        
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
            StartCoroutine(WaitCoroutine(1, () =>// 1�b�҂�
            {
                _phase01ClosingEventCanvas.SetActive(true);
            }));

            if (_textControllerScript.GetTextEnd())//text�ǂݏグ�I����
            {
                _nowPhase = NowPhase.Phase02;
            }
        }
    }
    private void stepPhase02()
    {
        _phaseUiList[0].SetActive(false);
        //Camera��ǉ�
        _phase02CameraList[strixMyEntryNumber() - 1].SetActive(true);
        //CameraFocus��ɃC�x���g��_phaseUiList[1].SetActive(true);���s��

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


    //*****     Button   *****
    public void phase03_ResultDetailsButton()
    {
        _nowPhase = NowPhase.Phase04;
    }

    public void phase04_RoomExitButton()
    {
        _phase04InformationUi.SetActive(true);
        //Information���ɓ��͂���Ȃ��悤�Ƀ{�^��������
        _exitButton.enabled = false;
        _stayButton.enabled = false;
    }

    public void phase04_RoomStayButton()
    {
        if (!OnlyResultPlay)
        {
            //���[���}�b�`���O�V�[���ɑJ��
            //���[���}�b�`���O�V�[���Ƀ��v���J�̂����I�u�W�F�N�g���������܂Ȃ��悤�ɔj������ or �����̏�񂾂����������ނ悤�ɂ���
            _nextSceneLoadScript.LoadSceneStart(LobbyMatchingScene);//�V�[��������
        }
    }

    public void phase04_Information_YES_Button()
    {
        if (!LocalTestDebug)
        {
            //���[�����甲���āA���C�����j���[�V�[���ɑJ��
            StrixNetwork.instance.LeaveRoom(handler: deleteRoomResult => Debug.Log("�ގ����܂����B�F" + (StrixNetwork.instance.room == null)),
                            failureHandler: deleteRoomError => Debug.LogError("Could not delete room.Reason: " + deleteRoomError.cause));
        }
        if(!OnlyResultPlay)
        {
            _nextSceneLoadScript.LoadSceneStart(MainMenuScene);
        }
        
    }

    public void phase04_Information_NO_Button()
    {
        _phase04InformationUi.SetActive(false);
        //Information������phase04�̃{�^���L����
        _exitButton.enabled = true;
        _stayButton.enabled = true;
    }


    //*****     �A�j���[�V����     *****
    public void phase01Start()
    {
        _nowPhase = NowPhase.Phase01;
    }

    //�A�j���[�V�����̃C�x���g�Œ��x�ǂ��^�C�~���O��Game�V�[�����I�t�ɂ���
    public void gameSceneFalse()
    {
        if (!OnlyResultPlay)
        {
            _gameSceneObject.SetActive(false);
        }   
    }

    //Camera�Y�[�����Phase02��i�s������
    public void phase02CameraFocusEnd()
    {
        _phaseUiList[1].SetActive(true);
    }

    //*****     �R���[�`��       *****
    private IEnumerator WaitCoroutine(float waitSeconds, UnityAction callback)
    {
        yield return new WaitForSeconds(waitSeconds);
        callback?.Invoke();
    }

}
