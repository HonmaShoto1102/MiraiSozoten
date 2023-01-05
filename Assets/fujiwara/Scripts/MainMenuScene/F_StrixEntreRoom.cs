using System.Collections;
using System.Collections.Generic;
using SoftGear.Strix.Unity.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class F_StrixEntreRoom : MonoBehaviour
{
    /// <summary>
    /// ���[���ɎQ���\�ȍő�l��
    /// </summary>
    public int capacity = 4;

    /// <summary>
    /// ���[����
    /// </summary>
    public string roomName = "New Room";

    /// <summary>
    /// ���[�������������C�x���g
    /// </summary>
    public UnityEvent onRoomEntered;

    /// <summary>
    /// ���[���������s���C�x���g
    /// </summary>
    public UnityEvent onRoomEnterFailed;


    //�����̍쐬
    public void CreateRoom()
    {
        //�����v���p�e�B�ݒ�
        RoomProperties roomProperties = new RoomProperties
        {
            name = roomName,  //������
            capacity = 4,     //���
            key1 = 0,         //
            key2 = 0,         //
            key3 = 0,         //���[��ID
        };

        //�����o�[�v���p�e�B
        RoomMemberProperties memberProperties = new RoomMemberProperties
        {
            name = StrixNetwork.instance.playerName,
            properties = new Dictionary<string, object>(){
                    {"state",0 },   //�������
                    {"nowScene",0 }, //���݂���V�[��

                }
        };


        StrixNetwork.instance.CreateRoom(
          roomProperties,
           memberProperties,
            args =>
            {
                onRoomEntered.Invoke();
                Debug.Log("�����̍쐬�ɐ������܂����B");

                RoomStatusInit();
            },
            args =>
            {
                Debug.Log("�����̍쐬�Ɏ��s���܂����Berror=" + args.cause);
                onRoomEnterFailed.Invoke();
            }
            );
    }


    //�Q����A�v���p�e�B�̏����ݒ�
    public void RoomStatusInit()
    {
        //����ID������
        StrixNetwork.instance.SetRoom(
            roomId: StrixNetwork.instance.roomSession.room.GetPrimaryKey(),   // The ID of the current room
                    roomProperties: new RoomProperties
                    {
                        name = roomName,
                        capacity = 4,
                        key1 = 0,
                        key2 = 0,
                        key3 = StrixNetwork.instance.selfRoomMember.GetRoomId()


                    },
                    handler: null,  // Printing the new capacity
                    failureHandler: null
                );
        Debug.Log("���[��ID�F" + StrixNetwork.instance.roomSession.room.GetPrimaryKey());

        //�N���C�A���g�̕�������ݒ肷��
        StrixNetwork.instance.SetRoomMember(
          StrixNetwork.instance.selfRoomMember.GetPrimaryKey(),
          new Dictionary<string, object>(){
                {"properties",new Dictionary<string,object>(){
                    {"nowScene",0 },
                    {"state",0 }
                } }
              },
          args =>
          {
          },
          args =>
          {
          }
          );
    }


    public void IDHitRoom(RoomInfo hitroom)
    {
        //�����ɓ���
        StrixNetwork.instance.JoinRoom(
     host: hitroom.host,
     port: hitroom.port,
     protocol: hitroom.protocol,
     roomId: hitroom.roomId,
     playerName: "My Player Name",
     handler: __ => onRoomEntered.Invoke(),



     failureHandler: joinError => Debug.LogError("Join failed.Reason: " + joinError.cause)
 );
    }
}
