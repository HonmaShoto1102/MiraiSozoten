using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Paramater
{
    public static Paramater operator +(Paramater a, Paramater b)
    {
        b.Speed += a.Speed;
        b.Lading += a.Lading;
        b.Armer += a.Armer;
        b.GetPower += a.GetPower;
        b.Rate += a.Rate;
        b.SearchPower += a.SearchPower;

        return b;
    }

    public static Paramater operator -(Paramater a, Paramater b)
    {
        b.Speed -= a.Speed;
        b.Lading -= a.Lading;
        b.Armer -= a.Armer;
        b.GetPower -= a.GetPower;
        b.Rate -= a.Rate;
        b.SearchPower -= a.SearchPower;

        return b;
    }


    public int Speed;
    public int Lading;
    public int Armer;
    public float GetPower;
    public float Rate;
    public int SearchPower;
}


public class CraftUI : MonoBehaviour
{
    [SerializeField] Player player;

    // �㏸����p�����[�^���X�g
    [SerializeField] List<Paramater> DeaselUpParamater;
    [SerializeField] List<Paramater> BodyUpParamater;
    [SerializeField] List<Paramater> MouseUpParamater;
    [SerializeField] List<Paramater> CraneUpParamater;
    [SerializeField] List<Paramater> RaderUpParamater;

    // �K�v�f�ރ��X�g
    [SerializeField] List<SeaResource> DeaselUseResource;
    [SerializeField] List<SeaResource> BodyUseResource;
    [SerializeField] List<SeaResource> MouseUseResource;
    [SerializeField] List<SeaResource> CraneUseResource;
    [SerializeField] List<SeaResource> RaderUseResource;

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


        craneGetPower = CraneUpParamater[1].GetPower;
        mouseGetPower = MouseUpParamater[1].GetPower;

        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* ==========�f�B�[�[���G���W���̋���========== */
    public void DieselEngineUpgrade()
    {
        if (player.dieselEngine < DeaselUseResource.Count)
        {
            // �f�ނ�����Ă��邩���肷��
            if (UpgradeDecision(DeaselUseResource[player.dieselEngine]))
            {
                Debug.Log("player.seaResource:" + player.seaResource.plastic + ", " + player.seaResource.ePlastic + ", " + player.seaResource.wood + ", " + player.seaResource.steel + ", " + player.seaResource.seaFood);
                //��������
                player.seaResource = DeaselUseResource[player.dieselEngine] - player.seaResource;

                Debug.Log("DeaselUseResource[player.dieselEngine] :" + DeaselUseResource[player.dieselEngine].plastic + ", " + DeaselUseResource[player.dieselEngine].ePlastic 
                    + ", " + DeaselUseResource[player.dieselEngine].wood + ", " + DeaselUseResource[player.dieselEngine].steel + ", " + DeaselUseResource[player.dieselEngine].seaFood);
                Debug.Log("player.seaResource:" + player.seaResource.plastic + ", " + player.seaResource.ePlastic + ", " + player.seaResource.wood + ", " + player.seaResource.steel + ", " + player.seaResource.seaFood);

                //�p�����[�^����
                player.speed += DeaselUpParamater[player.dieselEngine].Speed;

                // �������x���A�b�v
                player.dieselEngine++;

                Debug.Log("�G���W������");
            }            
        }
    }

    /* ==========�D�̂̋���========== */
    public void ShipBodyUpgrade()
    {
        if (player.shipBody < BodyUseResource.Count)
        {
            // �f�ނ�����Ă��邩���肷��
            if (UpgradeDecision(BodyUseResource[player.shipBody]))
            {
                //��������
                player.seaResource = BodyUseResource[player.shipBody] - player.seaResource;

                //�p�����[�^����
                // �X�s�[�h
                player.speed += BodyUpParamater[player.shipBody].Speed;

                // �ύڗ�
                player.resourceStack += BodyUpParamater[player.shipBody].Lading;

                // ���b��
                player.shipArmer += BodyUpParamater[player.shipBody].Armer;

                // �������x���A�b�v
                player.shipBody++;


                Debug.Log("�D�̋���");
            }           
        }
    }

    /* ==========SWW�}�E�X�̋���========== */
    public void WhaleMouseUpgrade()
    {

        if (player.whaleMouse < MouseUseResource.Count)
        {
            // �f�ނ�����Ă��邩���肷��
            if (UpgradeDecision(MouseUseResource[player.whaleMouse]))
            {               
                //��������
                player.seaResource = MouseUseResource[player.dieselEngine] - player.seaResource;

                //�p�����[�^����
                // �X�s�[�h
                player.speed += MouseUpParamater[player.whaleMouse].Speed;

                // �����(100*(�N���[���̉����*�}�E�X�̉����))       
                if (MouseUpParamater[player.whaleMouse].GetPower > 0.0f)
                {
                    player.getPower = (int)(100.0f * (craneGetPower * MouseUpParamater[player.whaleMouse].GetPower));
                    mouseGetPower = MouseUpParamater[player.whaleMouse].GetPower;
                }

                // �������x���A�b�v
                player.whaleMouse++;

                Debug.Log("�����g���ʋ���");
            }            
        }
    }

    /* ==========�N���[���̋���========== */
    public void CraneUpgrade()
    {
        // �ő勭�����ǂ����𔻒肷��
        if (player.crane < CraneUseResource.Count)
        {
            // �f�ނ�����Ă��邩���肷��
            if (UpgradeDecision(CraneUseResource[player.crane]))
            {               
                //��������
                player.seaResource = CraneUseResource[player.crane] - player.seaResource;

                //�p�����[�^����
                if (CraneUpParamater[player.crane].GetPower > 0.0f)
                {
                    player.getPower = (int)(100.0f * (CraneUpParamater[player.crane].GetPower * mouseGetPower));

                    craneGetPower = CraneUpParamater[player.crane].GetPower;
                }

                // �������x���A�b�v
                player.crane++;

                Debug.Log("�N���[������");
            }
        }
    }

    /* ==========���[�_�[�̋���========== */
    public void SonarUpgrade()
    {

        if (player.sonar < RaderUseResource.Count)
        {
            // �f�ނ�����Ă��邩���肷��
            if (UpgradeDecision(RaderUseResource[player.sonar]))
            {                
                //��������
                player.seaResource = RaderUseResource[player.sonar] - player.seaResource;

                //�p�����[�^����
                player.searchPower += RaderUpParamater[player.sonar].SearchPower;

                // �������x���A�b�v
                player.sonar++;

                Debug.Log("�\�i�[����");
            }
        }
    }


    /* ==========�A�b�v�O���[�h�o���邩���肷��========== */
    bool UpgradeDecision(SeaResource UseResource)
    {      
        // �v���C���[�������Ă���f�ނƕK�v�f�ސ����r����
        // �v���X�`�b�N
        if (player.seaResource.plastic < UseResource.plastic)
        {
            return false;
        }

        // �G���v��
        if (player.seaResource.ePlastic < UseResource.ePlastic)
        {
            return false;
        }

        // �؍�
        if (player.seaResource.wood < UseResource.wood)
        {
            return false;
        }

        // �|��
        if (player.seaResource.steel < UseResource.steel)
        {
            return false;
        }

        // �C�N
        if (player.seaResource.seaFood < UseResource.seaFood)
        {
            return false;
        }

        return true;
    }

    /* ==========�p�����[�^�̉��Z�ʂ��擾========== */
    public Paramater GetNextParamater(int cursolNum)
    {
        // �J�[�\�����ǂ��ɂ��邩�𔻒肷��
        if (cursolNum == 0)
        {
            int ContenaNum = player.dieselEngine;
            if(DeaselUpParamater.Count<= player.dieselEngine)
            {
                ContenaNum = 0;
            }
            return DeaselUpParamater[ContenaNum];
        }
        else if(cursolNum == 1)
        {
            int ContenaNum = player.shipBody;
            if (BodyUpParamater.Count <= player.shipBody)
            {
                ContenaNum = 0;
            }

            return BodyUpParamater[ContenaNum];
        }
        else if(cursolNum == 2)
        {
            int ContenaNum = player.whaleMouse;
            if (MouseUpParamater.Count <= player.whaleMouse)
            {
                ContenaNum = 0;
            }

            return MouseUpParamater[ContenaNum];
        }
        else if (cursolNum == 3)
        {
            int ContenaNum = player.crane;
            if (CraneUpParamater.Count <= player.crane)
            {
                ContenaNum = 0;
            }

            return CraneUpParamater[ContenaNum];
        }
        else if(cursolNum == 4)
        {
            int ContenaNum = player.sonar;
            if (RaderUpParamater.Count <= player.sonar)
            {
                ContenaNum = 0;
            }

            return RaderUpParamater[ContenaNum];
        }

        // �J�[�\�����O�`�S�ȊO�������ꍇ
        Paramater param;
        param.Speed = -1;
        param.Lading = -1;
        param.Armer = -1;
        param.GetPower = -1.0f;
        param.Rate = -1.0f;
        param.SearchPower = -1;

        return param;
    }

    public SeaResource GetNextUseResource(int cursolNum)
    {        
        // �J�[�\�����ǂ��ɂ��邩�𔻒肷��
        if (cursolNum == 0)
        {
            int ContenaNum = player.dieselEngine;
            if (DeaselUpParamater.Count <= player.dieselEngine)
            {
                ContenaNum = 0;
            }
            return DeaselUseResource[ContenaNum];
        }
        else if (cursolNum == 1)
        {
            int ContenaNum = player.shipBody;
            if (BodyUpParamater.Count <= player.shipBody)
            {
                ContenaNum = 0;
            }

            return BodyUseResource[ContenaNum];
        }
        else if (cursolNum == 2)
        {
            int ContenaNum = player.whaleMouse;
            if (MouseUpParamater.Count <= player.whaleMouse)
            {
                ContenaNum = 0;
            }

            return MouseUseResource[ContenaNum];
        }
        else if (cursolNum == 3)
        {
            int ContenaNum = player.crane;
            if (CraneUpParamater.Count <= player.crane)
            {
                ContenaNum = 0;
            }

            return CraneUseResource[ContenaNum];
        }
        else if (cursolNum == 4)
        {
            int ContenaNum = player.sonar;
            if (RaderUpParamater.Count <= player.sonar)
            {
                ContenaNum = 0;
            }

            return RaderUseResource[ContenaNum];
        }

        // �J�[�\�����O�`�S�ȊO�������ꍇ
        SeaResource resource;
        resource.plastic = -1;
        resource.ePlastic = -1;
        resource.wood = -1;
        resource.steel = -1;
        resource.seaFood = -1;

        return resource;
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

    // �N���[���̂̉���͂��擾����
    public float GetCraneGetPower()
    {
        return craneGetPower;
    }

    // �}�E�X�̉���͂��擾
    public float GetMouseGetPower()
    {
        return mouseGetPower;
    }

    public Player GetPlayer()
    {
        if (player != null)
        {
            return player;
        }
        else
        {
            return null;
        }
    }
}
