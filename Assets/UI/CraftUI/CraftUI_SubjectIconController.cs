using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum UpgradeSubject
{
    SUBJECT_DIEZELENGINE = 0,
    SUBJECT_SHIPBODY,
    SUBJECT_WHALEMOUSE,
    SUBJECT_CRANE,
    SUBJECT_RADER
};

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


public class CraftUI_SubjectIconController : MonoBehaviour
{
    Player player;

    // �X�e�[�^�X�A�b�v�O���[�h�p�p�����[�^���X�g 
    [SerializeField] List<Paramater> UpgradePalamater;
    [SerializeField] List<SeaResource> UseResourceList;
    [SerializeField] UpgradeSubject IconNum;

    CraftUI craftUI;
    CraftUI_MainSubjectController MSC;

    int ContenaNum;

    bool CursolNotice;// �J�[�\�����������Ă��邩�̃t���O

    // Start is called before the first frame update
    void Start()
    {
        // CraftUI��CraftCanvas����擾
        craftUI = GameObject.Find("CraftCanvas").gameObject.GetComponent<CraftUI>();
        CursolNotice = false;

        // Player��CraftUI_MainSubjectController����擾
        GameObject SubjectPanel = this.gameObject.transform.parent.gameObject;
        MSC = SubjectPanel.GetComponent<CraftUI_MainSubjectController>();
        player = MSC.GetPlayer();

        // �e�X�g�p�v���C���[���\�[�X
        player.seaResource.ePlastic = 50000;
        player.seaResource.plastic = 50000;
        player.seaResource.seaFood = 50000;
        player.seaResource.steel = 50000;
        player.seaResource.wood = 50000;

        ContenaNum = 0;

        // IconNum��SUBJECT_WHALEMOUSE�������ꍇ����ʂ̋����p�����[�^�𑗂�
        if (IconNum == UpgradeSubject.SUBJECT_WHALEMOUSE)
        {
            MSC.SetMouseGetPower(1.0f);
        }
        // IconNum��SUBJECT_CRANE�������ꍇ����ʂ̋����p�����[�^�𑗂�
        if (IconNum == UpgradeSubject.SUBJECT_CRANE)
        {
            MSC.SetCraneGetPower(1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �J�[�\�����������Ă��鎞��Enter�ŋ���
        if (CursolNotice == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // �f�ޑ���Ă邩����
                if (UpgradeDecision())
                {
                    if (IconNum == UpgradeSubject.SUBJECT_DIEZELENGINE)
                    {
                        craftUI.DieselEngineUpgrade(UseResourceList[ContenaNum],UpgradePalamater[ContenaNum], UpgradePalamater.Count);                        
                    }
                    else if (IconNum == UpgradeSubject.SUBJECT_SHIPBODY)
                    {
                        craftUI.ShipBodyUpgrade(UseResourceList[ContenaNum], UpgradePalamater[ContenaNum], UpgradePalamater.Count);                        
                    }
                    else if (IconNum == UpgradeSubject.SUBJECT_WHALEMOUSE)
                    {
                        MSC.AddRate(UpgradePalamater[ContenaNum].Rate);

                        craftUI.WhaleMouseUpgrade(UseResourceList[ContenaNum], UpgradePalamater[ContenaNum], UpgradePalamater.Count);
                        MSC.SetMouseGetPower(UpgradePalamater[ContenaNum].GetPower);
                    }
                    else if (IconNum == UpgradeSubject.SUBJECT_CRANE)
                    {
                        craftUI.CraneUpgrade(UseResourceList[ContenaNum], UpgradePalamater[ContenaNum], UpgradePalamater.Count);
                        MSC.SetCraneGetPower(UpgradePalamater[ContenaNum].GetPower);
                    }
                    else if (IconNum == UpgradeSubject.SUBJECT_RADER)
                    {
                        craftUI.SonarUpgrade(UseResourceList[ContenaNum], UpgradePalamater[ContenaNum], UpgradePalamater.Count);                        
                    }

                    ContenaNumGet();
                    MSC.SetPlayerText(UseResourceList[ContenaNum], UpgradePalamater[ContenaNum], IconNum);
                }
            }           
        }
    }

    /* =========�X�e�[�^�X�̋������x������List�̗v�f�ԍ����擾========== */
    void ContenaNumGet()
    {
        if (IconNum == UpgradeSubject.SUBJECT_DIEZELENGINE)//�f�B�[�[���̋������x�����擾
        {
            ContenaNum = player.dieselEngine;
        }
        else if (IconNum == UpgradeSubject.SUBJECT_SHIPBODY)// �D�̂̋������x�����擾
        {
            ContenaNum = player.shipBody;
        }
        else if (IconNum == UpgradeSubject.SUBJECT_WHALEMOUSE)// WhaleMouse�̋������x�����擾
        {
            ContenaNum = player.whaleMouse;
        }
        else if (IconNum == UpgradeSubject.SUBJECT_CRANE)// �N���[���̋������x�����擾
        {
            ContenaNum = player.crane;
        }
        else if (IconNum == UpgradeSubject.SUBJECT_RADER)// ���[�_�[�̋������x�����擾
        {
            ContenaNum = player.sonar;
        }

        if (ContenaNum>= UpgradePalamater.Count)
        {
            ContenaNum = 0;
        }
    }

    /* ==========�A�b�v�O���[�h�o���邩���肷��========== */
    bool UpgradeDecision()
    {
        // �v�f�ԍ����擾����
        ContenaNumGet();

        // �v���C���[�������Ă���f�ނƕK�v�f�ސ����r����
        // �v���X�`�b�N
        if (player.seaResource.plastic < UseResourceList[ContenaNum].plastic)
        {
            return false;
        }

        // �G���v��
        if(player.seaResource.ePlastic< UseResourceList[ContenaNum].ePlastic)
        {
            return false;
        }

        // �؍�
        if(player.seaResource.wood< UseResourceList[ContenaNum].wood)
        {
            return false;
        }

        // �|��
        if(player.seaResource.steel< UseResourceList[ContenaNum].steel)
        {
            return false;
        }

        // �C�N
        if(player.seaResource.seaFood < UseResourceList[ContenaNum].seaFood)
        {
            return false;
        }

        return true;
    }

    // �㏸����p�����[�^���擾����
    public Paramater GetParam()
    {
        ContenaNumGet();

        return UpgradePalamater[ContenaNum];
    }

    // �����Ɏg�����\�[�X�ʂ̃f�[�^���擾����
    public SeaResource GetUseResource()
    {
        ContenaNumGet();

        return UseResourceList[ContenaNum];
    }

    // �J�[�\��������������CursolNotice��true�ɂ���
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SubjectCursol")
        {
            // �e�L�X�g�\����ύX
            ContenaNumGet();
            MSC.SetPlayerText(UseResourceList[ContenaNum], UpgradePalamater[ContenaNum],IconNum);            

            CursolNotice = true;
        }
    }

    // �J�[�\�������ꂽ����CursolNotice��false�ɂ���
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SubjectCursol")
        {
            CursolNotice = false;
        }
    }
}
