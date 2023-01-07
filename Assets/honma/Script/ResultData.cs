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
    [Header("�X�g���N�XOFF �Ƀ`�F�b�N������")]
    public bool LocalPlay;

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
    //[SerializeField]
    //GameObject gameScene;
    [SerializeField]
    [Header("Player��script")]
    private List<Player> _playerScript;
    //private Player _playerScript;

    [SerializeField]
    [Header("GameScene��Player")]
    private List<GameObject> _playerObject;
    //private GameObject _playerObject;

    [SerializeField]
    [Header("GameScene��PlayerParent")]
    private List<GameObject> _playerParentList;

    [SerializeField]
    [Header("GameScene��GameScene�I�u�W�F�N�g")]
    private GameObject _gameSceneObject;

    [SerializeField]
    [Header("GameScene��Camera�I�u�W�F�N�g")]
    private GameObject _gameSceneCamera;

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

    private Transform myTransform;
    private Vector3 StartPosition;
    Vector3 EndPosition;

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

        if (!LocalPlay)
        {
            //debug�p�̃v���C���[���p
            for (int i = 0; i < _playerObject.Count; i++)
            {
                _playerObject[i] = _resultPlayer01[i];
            }
        }

        //�e�t�F�[�Y�̃A�N�e�B�u������
        _phase01ClosingEventCanvas.SetActive(false);

        //Phase02�J�����̏�����
        foreach(var camera in _phase02CameraList)
        {
            camera.SetActive(false);
        }

    
        foreach(var ResultPlayer in _resultPlayer01)
        {
            ResultPlayer.transform.GetChild(0).gameObject.SetActive(false);
        }


        myTransform = _playersPosition.transform;
        StartPosition = myTransform.position;
        EndPosition = _phase01PlayersEndPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LocalPlay)
        {
            if (!isLocal) return;    
        }
        
        if (ResultStart == false) return;
        
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
        if (!LocalPlay)
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

    private int PlayerOrderCount(int number)//***************************************************************************************************     GameScene����
    {
        List<int> _medalList = new List<int>();
        if (!OnlyResultPlay)
        {
            _medalList.Add(_playerScript[0].medal);
            _medalList.Add(_playerScript[1].medal);
            _medalList.Add(_playerScript[2].medal);
            _medalList.Add(_playerScript[3].medal);
        }
        else
        {
            _medalList.Add(5);
            _medalList.Add(3);
            _medalList.Add(11);
            _medalList.Add(6);
        }
        //�~���Ƀ\�[�g
        _medalList.Sort((a, b) => b - a);


        int orderCount = _medalList[number];

        //number�ʂ̃��_���̐���Ԃ�0~3
        return orderCount;
    }

    //���ʎ擾
    private int MyPlayerRank(int number)//***************************************************************************************************     GameScene����
    {
        //  PlayerOrderCount(3)   =   5
        number = PlayerOrderCount(number);//number�ʂ̃��_���̐�

        List<int> _medalList = new List<int>(4); 
        
        if (!OnlyResultPlay)
        {
            _medalList.Add(_playerScript[0].medal);
            _medalList.Add(_playerScript[1].medal);
            _medalList.Add(_playerScript[2].medal);
            _medalList.Add(_playerScript[3].medal);
        }
        else
        {
            _medalList.Add(5);
            _medalList.Add(3);
            _medalList.Add(11);
            _medalList.Add(6);
        }

        //�~���Ƀ\�[�g
        _medalList.Sort((a, b) => b - a);

        int rank = _medalList.IndexOf(number);
        
        //number�ʂ���͂���Ƃ��̐l�����ʂɂ��邩������

        return rank;

        //MyPlayerRank(2)��2��Ԃ�
        //MyPlayerRank(1)��1��Ԃ�

    }

    private void RankImageChange()
    {
        _phase02RankCrownImage.sprite = RankCrownImageLists[MyPlayerRank(0)];//1��
        _phase02RankNumberImage.sprite = RankNumberImageLists[MyPlayerRank(0)];
        _phase03CrownImage.sprite = RankCrownImageLists[MyPlayerRank(0)];
        _phase03NumberImage.sprite = RankNumberImageLists[MyPlayerRank(0)];
    }

    //�����̖��O���擾����
    private string strixMyPlayerName(int number)
    {
        string _myPlayerName = "TestPlayer";// = "Test���[�h";

        if (!OnlyResultPlay)
        {
           _myPlayerName = _playerScript[number].playerName;// = "Test���[�h";

        }
        if (!LocalPlay)
        {
            _myPlayerName = StrixNetwork.instance.selfRoomMember.GetName();
        }
            return _myPlayerName;
    }

    //Result�V�[����PlayerName���ꊇ�ύX
    private void MyPlayerNameChange()
    {
        if (!OnlyResultPlay)
        {
           
            _phase01PlayerNameTextLists[0].text = _playerScript[0].playerName;
            _phase01PlayerNameTextLists[1].text = _playerScript[1].playerName;
            _phase01PlayerNameTextLists[2].text = _playerScript[2].playerName;
            _phase01PlayerNameTextLists[3].text = _playerScript[3].playerName;


            _phase02PlayerName.text = strixMyPlayerName(MyPlayerRank(0));
            _phase03PlayerName.text = strixMyPlayerName(MyPlayerRank(0));

           
            _phase04PlayerNameList[0].text = _playerScript[MyPlayerRank(0)].playerName;
            _phase04PlayerNameList[1].text = _playerScript[MyPlayerRank(1)].playerName;
            _phase04PlayerNameList[2].text = _playerScript[MyPlayerRank(2)].playerName;
            _phase04PlayerNameList[3].text = _playerScript[MyPlayerRank(3)].playerName;

        }
        else
        {
            _phase01PlayerNameTextLists[0].text = "Player01";
            _phase01PlayerNameTextLists[1].text = "Player02";
            _phase01PlayerNameTextLists[2].text = "Player03";
            _phase01PlayerNameTextLists[3].text = "Player04";


            _phase02PlayerName.text = strixMyPlayerName(MyPlayerRank(0));
            _phase03PlayerName.text = strixMyPlayerName(MyPlayerRank(0));


            _phase04PlayerNameList[0].text = _phase01PlayerNameTextLists[MyPlayerRank(0)].text;
            _phase04PlayerNameList[1].text = _phase01PlayerNameTextLists[MyPlayerRank(1)].text;
            _phase04PlayerNameList[2].text = _phase01PlayerNameTextLists[MyPlayerRank(2)].text;
            _phase04PlayerNameList[3].text = _phase01PlayerNameTextLists[MyPlayerRank(3)].text;
        }
    }



    private void OrderCountChange()
    {
        // Player��List����Ȃ��ꍇ
        //_phase01OrderCountTextLists[strixMyEntryNumber() - 1].text = $"{MyPlayerOrderCount()}";
        //_phase02OrderCount.text = $"{MyPlayerOrderCount()}";
        //_phase03OrderCount.text = $"{MyPlayerOrderCount()}";
        //_phase04OrderCountList[MyPlayerRank() - 1].text = $"{MyPlayerOrderCount()}";

        if (!OnlyResultPlay)
        {
            _phase01OrderCountTextLists[0].text = $"{_playerScript[0].medal}";
            _phase01OrderCountTextLists[1].text = $"{_playerScript[1].medal}";
            _phase01OrderCountTextLists[2].text = $"{_playerScript[2].medal}";
            _phase01OrderCountTextLists[3].text = $"{_playerScript[3].medal}";

            _phase02OrderCount.text = $"{PlayerOrderCount(0)}";
            _phase03OrderCount.text = $"{PlayerOrderCount(0)}";

            _phase04OrderCountList[MyPlayerRank(0)].text = $"{PlayerOrderCount(0)}";
            _phase04OrderCountList[MyPlayerRank(1)].text = $"{PlayerOrderCount(1)}";
            _phase04OrderCountList[MyPlayerRank(2)].text = $"{PlayerOrderCount(2)}";
            _phase04OrderCountList[MyPlayerRank(3)].text = $"{PlayerOrderCount(3)}";
        }
        else
        {
            _phase01OrderCountTextLists[0].text = $"5";
            _phase01OrderCountTextLists[1].text = $"3";
            _phase01OrderCountTextLists[2].text = $"11";
            _phase01OrderCountTextLists[3].text = $"6";

            _phase02OrderCount.text = $"{PlayerOrderCount(0)}";
            _phase03OrderCount.text = $"{PlayerOrderCount(0)}";

            _phase04OrderCountList[MyPlayerRank(0)].text = $"{PlayerOrderCount(0)}";
            _phase04OrderCountList[MyPlayerRank(1)].text = $"{PlayerOrderCount(1)}";
            _phase04OrderCountList[MyPlayerRank(2)].text = $"{PlayerOrderCount(2)}";
            _phase04OrderCountList[MyPlayerRank(3)].text = $"{PlayerOrderCount(3)}";
        }
        

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
        _phase04MoneyCountList[MyPlayerRank(0)].text = $"{MyPlayerMoneyCount(MyPlayerRank(0))}";
        _phase04MoneyCountList[MyPlayerRank(1)].text = $"{MyPlayerMoneyCount(MyPlayerRank(1))}";
        _phase04MoneyCountList[MyPlayerRank(2)].text = $"{MyPlayerMoneyCount(MyPlayerRank(2))}";
        _phase04MoneyCountList[MyPlayerRank(3)].text = $"{MyPlayerMoneyCount(MyPlayerRank(3))}";

    }

    //����̈ʒu�ɒu�����߂ɃT�C�Y�Ȃǂ̏��������s��
    private void PlayerGameObjectInitialization()
    {
        //if (!LocalPlay)
        //{
        //    //debug�p�̃v���C���[���p
        //    for(int i=0;i<_playerObject.Count;i++)
        //    {
        //        _playerObject[i] = _resultPlayer01[i];
        //    }
        //}
       

       
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

        foreach(var parent in _playerParentList)
        {
            parent.transform.DetachChildren();
        }


        for(int i=0;i< _playerObject.Count;i++)
        {
            _playerObject[i].transform.parent = _resultPlayer01[i].transform;
        }
    }


    //�ŏ��̈ʒu����   ��x�����Ăяo��
    private void StartStep()
    {
        if (ResultStart)
        {
            if (!OnlyResultPlay)
            {
                //���U���g�p�̃J�����ɐ؂�ւ��邽�߂ɃI�t�ɂ���
                _gameSceneCamera.SetActive(false);
            }
        }


        if (!OnlyResultPlay)
        {
            PlayerParentDetachChildren();// ���炭GameSceneObject��ResultSceneObject�̊Ԃ�Player���o������
            for (int i = 0; i < _resultPlayer01.Count; i++)
            {
                _resultPlayer01[i].SetActive(true);
            }
        }
        else
        {
            //debug�p��Player��\������
            for (int i = 0; i < _resultPlayer01.Count; i++)
            {
                _resultPlayer01[i].SetActive(true);
                _resultPlayer01[i].transform.GetChild(1).gameObject.SetActive(true);

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
       
        float _distance = StartPosition.z - EndPosition.z;
        float speed = Time.deltaTime * ShipsMovingSpeed / Mathf.Abs(_distance);

        //�D�̓�����W�X�V
        myTransform.position = Vector3.Lerp(StartPosition, EndPosition, speed);//��ֈ�

        Debug.Log("StartPosition" + StartPosition);
        Debug.Log("EndPosition" + EndPosition);
        Debug.Log("_distance" + _distance);
        //Debug.Log("speed" + speed);
        Debug.Log("myTransform.position" + myTransform.position);

        if (speed>0.99f)//����speed�͊���    �����͊��ƓK���ȏ���
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

       
        if (Input.GetKeyDown(KeyCode.Return) || Gamepad.current.bButton.wasPressedThisFrame)
        {
            _nowPhase = NowPhase.Phase03;
        }
    }
    private void stepPhase03()
    {
        _phaseUiList[1].SetActive(false);
        _phaseUiList[2].SetActive(true);

        //�{�^�����͏���
        if (Input.GetKeyDown(KeyCode.Return) || Gamepad.current.bButton.wasPressedThisFrame)
        {
            phase03_ResultDetailsButton();
        }
    }
    private void stepPhase04()
    {
        _phaseUiList[2].SetActive(false);
        _phaseUiList[3].SetActive(true);

        bool test = false;
        //�{�^�����͏���
        if ((Input.GetKeyDown(KeyCode.Return) || Gamepad.current.bButton.wasPressedThisFrame)&& !test)
        {
            phase04_RoomExitButton();
            test = true;
        }

        if (test)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Gamepad.current.aButton.wasPressedThisFrame)
            {
                phase04_Information_NO_Button();
                test = false;
            }
            
        }
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
        if (!LocalPlay)
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
