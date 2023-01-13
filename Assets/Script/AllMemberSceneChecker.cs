using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class AllMemberSceneChecker : MonoBehaviour
{
    [SerializeField] private int SceneNum;
    private bool isCheck;

    // Start is called before the first frame update
    void Start()
    {
        isCheck = false;

        //���݂̃V�[���ԍ���StrixNetwork.instance�̒ǉ��v���p�e�B�unowScene�v�ɕۑ��i�O��Ƃ��āA���[���쐬���ɒǉ��v���p�e�B��ݒ肵�Ă���j
        //���̏����́A�V�[���ړ����ォ�A�����ɐڑ�����Ă���󋵂ł���΁A���̃X�N���v�g����ł������\
        StrixNetwork.instance.SetRoomMember(
          StrixNetwork.instance.selfRoomMember.GetPrimaryKey(),
          new Dictionary<string, object>(){
                {"properties",new Dictionary<string,object>(){
                    {"nowScene",2 }
                } }
              },
          args =>
          {
              Debug.Log("�����o�[�v���p�e�B�F�V�[���ԍ���ύX���܂����B");
          },
          args =>
          {
              Debug.Log("�����o�[�v���p�e�B�F�V�[���ԍ��̕ύX�Ɏ��s���܂����Berror = " + args.cause);
          }
          );
    }

    // Update is called once per frame
    void Update()
    {        
        //�S�����V�[���W�����v�ł������`�F�b�N

        //���f�o�b�O����ہA�Q�[���V�[���ɔ��ł���A�S�N���C�A���g�̃E�B���h�E����u�ł��A�N�e�B�u�ɂ��ĂȂ��Ƃ��̃N���C�A���g��Start()���Ă΂�Ȃ��̂Œ���
        if (!isCheck)
        {
            if (CheckAllMemberinGameScene(SceneNum))
            {
                Debug.Log("�S�v���C���[���Q�[���V�[���Ɉړ����܂����B");
                isCheck = true;
            }
        }

        //�z�X�g�����Łi�܂��́A���[������1�񂾂��j�֐����Ăт������́A�ȉ���if�����g����
        //if(StrixNetwork.instance.isRoomOwner)
        //{

        //}
    }

    private bool CheckAllMemberinGameScene(int num)
    {
        //���݂̑S���[�������o�[���Q��
        var A = StrixNetwork.instance.roomMembers;

        int membercount = 0;

        foreach (var roomMember in A)
        {
            membercount++;
            //�����������̃v���p�e�B���Ȃ������玸�s
            if (!roomMember.Value.GetProperties().TryGetValue("nowScene", out object value))
            {
                Debug.Log("�v���p�e�B�unowScene�v������܂���ł����B");
                return false;
            }

            if ((int)value != num)
            {
                Debug.Log(membercount+"�ԃv���C���[�̒l��" + (int)value);
                return false;
            }
        }
        return true;
    }
}