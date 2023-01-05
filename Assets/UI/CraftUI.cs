using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    [SerializeField] Player player;

    // ����ʂ̌v�Z�̂��߂ɕK�v
    float craneGetPower;
    float mouseGetPower;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.Log("MainPlayer NULL");
        }
        
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* ==========�f�B�[�[���G���W���̋���========== */
    public void DieselEngineUpgrade(SeaResource resource, Paramater param,int maxLevel)
    {
        if (player.dieselEngine < maxLevel)
        {
            // �������x���A�b�v
            player.dieselEngine++;

            //��������
            player.seaResource = resource - player.seaResource;


            //�p�����[�^����
            player.speed += param.Speed;

            Debug.Log("�G���W������");
        }
    }

    /* ==========�D�̂̋���========== */
    public void ShipBodyUpgrade(SeaResource resource, Paramater param, int maxLevel)
    {
        if (player.shipBody < maxLevel)
        {
            // �������x���A�b�v
            player.shipBody++;

            //��������
            player.seaResource = resource - player.seaResource;

            //�p�����[�^����
            // �X�s�[�h
            player.speed += param.Speed;

            // �ύڗ�
            player.resourceStack += param.Lading;

            // ���b��
            player.shipArmer += param.Armer;

            Debug.Log("�D�̋���");
        }
    }

    /* ==========SWW�}�E�X�̋���========== */
    public void WhaleMouseUpgrade(SeaResource resource, Paramater param, int maxLevel)
    {

        if (player.whaleMouse < maxLevel)
        {
            // �������x���A�b�v
            player.whaleMouse++;

            //��������
            player.seaResource = resource - player.seaResource;

            //�p�����[�^����
            // �X�s�[�h
            player.speed += param.Speed;

            // �����(100*(�N���[���̉����*�}�E�X�̉����))       
            if (param.GetPower > 0.0f)
            {
                player.getPower = (int)(100.0f * (craneGetPower * param.GetPower));
                mouseGetPower = param.GetPower;
            }                

            Debug.Log("�����g���ʋ���");
        }
    }

    /* ==========�N���[���̋���========== */
    public void CraneUpgrade(SeaResource resource, Paramater param, int maxLevel)
    {

        if (player.crane < maxLevel)
        {
            // �������x���A�b�v
            player.crane++;

            //��������
            player.seaResource = resource - player.seaResource;

            //�p�����[�^����
            if (param.GetPower > 0.0f)
            {
                player.getPower = (int)(100.0f * (mouseGetPower * param.GetPower));
                craneGetPower = param.GetPower;
            }            

            Debug.Log("�N���[������");
        }
    }

    /* ==========���[�_�[�̋���========== */
    public void SonarUpgrade(SeaResource resource, Paramater param, int maxLevel)
    {

        if (player.sonar < maxLevel)
        {
            // �������x���A�b�v
            player.sonar++;

            //��������
            player.seaResource = resource - player.seaResource;

            //�p�����[�^����
            player.searchPower += param.SearchPower;

            Debug.Log("�\�i�[����");
        }
    }

    // �N���t�g���n�܂鎞�ɌĂԏ���������
    void Init()
    {
        // �X�s�[�h�̃X�e�[�^�X�\��
        //NowSpeedText.text = player.speed.ToString();

        //float afterSpeedStatus = player.speed + DieselPalamUpList[player.dieselEngine];
        //AfterSpeedText.text = afterSpeedStatus.ToString();

        // �ύڗʂ̃X�e�[�^�X�\��
        //NowLadingText.text = player.resourceStack.ToString();

        //float afterStackText = player.resourceStack + BodyPalamUpList[player.shipBody].y;
        //AfterLadingText.text = afterStackText.ToString();

        // ����ʂ̃X�e�[�^�X�\��
        //NowSalvageText.text = player.getPower.ToString();

        //float afterSalvageText = player.getPower + CranePalamUpList[player.crane];
        //AfterSalvageText.text = afterSalvageText.ToString();

        // �T�m�͂̃X�e�[�^�X�\��
        //NowRaderText.text = player.searchPower.ToString();

        //float AfterText = player.searchPower + SonarPalamUpList[player.sonar];
        //AfterRaderText.text = AfterText.ToString();
    }

    public void SetCraneGetower(float power)
    {
        craneGetPower = power;
    }

    public void SetMouseGetower(float power)
    {
        mouseGetPower = power;
    }
}
