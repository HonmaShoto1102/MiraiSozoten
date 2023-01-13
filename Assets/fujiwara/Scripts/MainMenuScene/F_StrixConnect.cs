using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System.Linq;

using SoftGear.Strix.Client.Core.Auth.Message;
using SoftGear.Strix.Client.Core.Error;
using SoftGear.Strix.Client.Core.Model.Manager.Filter.Builder;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Net.Logging;
using SoftGear.Strix.Unity.Runtime.Event;


public class F_StrixConnect : MonoBehaviour
{
    public string host = "127.0.0.1";
    public int port = 9122;
    public string applicationId = "00000000-0000-0000-0000-000000000000";
    public Level logLevel = Level.INFO;
    public UnityEvent OnConnect;
    public string playerName = "�v���C���[";

    private bool isRoomCreate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�T�[�o�[�ڑ��J�n
    public void Connect(bool mode)
    {
        LogManager.Instance.Filter = logLevel;

        StrixNetwork.instance.applicationId = applicationId;
        StrixNetwork.instance.playerName = playerName;
        StrixNetwork.instance.ConnectMasterServer(host, port, OnConnectCallback, OnConnectFailedCallback);

        isRoomCreate = mode;
    }

    //�����n���h���[
    private void OnConnectCallback(StrixNetworkConnectEventArgs args)
    {

        OnConnect.Invoke();

        //�A�����ĉ������Ȃ��悤��UI���\��
        //gameObject.SetActive(false);
        Debug.Log("�ڑ��ɐ������܂���");



        //�����̍쐬�A�����ŏ������������
        if (isRoomCreate)
        {
            Debug.Log("�������쐬���܂�");
            GetComponent<F_StrixEntreRoom>().CreateRoom();
        }
        else
        {

            Debug.Log("�������������܂�");
            //�����ŕ����������Ă΂Ȃ���΂Ȃ�Ȃ�

            double a = GetComponent<MainMenuManager>().GetRoomID();
            RoomAccessWithID(a);
        }
    }

    //���s�n���h���[
    private void OnConnectFailedCallback(StrixNetworkConnectFailedEventArgs args)
    {
        string error = "�ڑ��ł��܂���ł���";

        if (args.cause != null)
        {
            error = args.cause.Message;
        }
    }



    //ID�������ĕ����ɃA�N�Z�X����
    public void RoomAccessWithID(double roomid)
    {
        var strixNetwork = StrixNetwork.instance;

        Debug.Log("���������ԍ�" + roomid);


        //�����[���̃J�X�^���v���p�e�B�ikey1�`8�j��double�^�Ȃ̂ŁA�����Ώۂ̕ϐ���double�^�łȂ��Ƃ����Ȃ�
        //�����̌���
        strixNetwork.SearchJoinableRoom(
                           condition: ConditionBuilder.Builder().Field("key3").EqualTo(roomid).Build(),  //key3(���[���ԍ�)��roomid�ƈ�v���镔���̂ݏo��
                            order: null,
                           limit: 1,                                                                            // ���ʂ�10���̂ݎ擾���܂�
                           offset: 0,                                                                            // ���ʂ�擪����擾���܂�
                           handler: searchResults => {


                               //�q�b�g�������[����񂪃��X�g�Ƃ��ĕԂ����
                               //���ǂ̕������q�b�g���Ȃ������Ƃ��Ă����̐����n���h���[���Ă΂�邪�AroomInfoCollection�͋�ł���

                               
                               var foundRooms = searchResults.roomInfoCollection;

                               if(foundRooms.Count>0)
                               {
                                   var roomInfo = foundRooms.First();

                                   GetComponent<F_StrixEntreRoom>().IDHitRoom(roomInfo);
                               }
                               else
                               {
                                   Debug.Log("�������T�[�`�ł��܂���ł���");
                                   GameObject.Find("UI_Canvas").GetComponent<MenuSceneUI>().RoomAccessError();

                               }

                              
                               
                           },
                           failureHandler: searchError => Debug.LogError("�������T�[�`�ł��܂���ł���"));
    }
}
