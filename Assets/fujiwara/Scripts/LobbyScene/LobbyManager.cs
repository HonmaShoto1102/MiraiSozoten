using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SoftGear.Strix.Unity.Runtime;

public class LobbyManager : StrixBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GameObject gameStartButton = GameObject.Find("PrivateUI_Canvas").gameObject.transform.Find("MatchConfirmButton").gameObject;
        //�z�X�g�ɂ̂݃Q�[���X�^�[�g�{�^����\��������
        if(StrixNetwork.instance.isRoomOwner)
        {
            gameStartButton.SetActive(true);
        }
        else
        {
            gameStartButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press_GameStart()
    {
        RpcToAll(nameof(SceneChangetoGame));
    }

    [StrixRpc]
    public void SceneChangetoGame()
    {
        SceneManager.LoadScene(2);
    }
}
