using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;
using SoftGear.Strix.Client.Core;

public class RoomNumberChecker : StrixBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var Number = StrixNetwork.instance.sortedRoomMembers;

        Debug.Log("Number(���Ԃ�S)�F"+ Number.Count);
       
        
        int count = 1;
        foreach (var RoomMember in StrixNetwork.instance.sortedRoomMembers)
        {//                                           �����o�[

            if (StrixNetwork.instance.selfRoomMember.GetUid() != RoomMember.GetUid())
            {//                     �����̔ԍ�                 Room���̃����o�[�̔ԍ��i�����j

                Debug.Log(count+"�l�ڂ�����");
                count++;
            }
            else//  selfRoomMember.GetUid() = RoomMember.GetUid()�̂Ƃ�
            {
                Debug.Log("���Ȃ���" + count + "�l�ڂł�");
            }
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
