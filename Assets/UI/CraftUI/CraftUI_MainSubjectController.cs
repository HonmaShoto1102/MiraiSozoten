using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI_MainSubjectController : MonoBehaviour
{
    GameObject SubjectCursol;
    GameObject IconFragment;

    RectTransform CursolTransform;
    int SubjectNum;

    CraftUI craftUI;
    Player player;

    // ���݂̃X�e�[�^�X�\���e�L�X�g
    Text NowShipLevelText;

    Text NowSpeedText;
    Text NowLadingText;
    Text NowArmerText;
    Text NowSalvageText;
    Text NowRateText;
    Text NowRaderText;

    // ������̃X�e�[�^�X�\���e�L�X�g
    Text AfterShipLevelText;

    Text AfterSpeedText;
    Text AfterLadingText;
    Text AfterArmerText;
    Text AfterSalvageText;
    Text AfterRateText;
    Text AfterRaderText;

    // �K�v�f�ރe�L�X�g
    Text UsePlasticText;
    Text UseEnplaText;
    Text UseWoodText;
    Text UseSteelText;
    Text UseSeafoodText;

    // �����f�ޕ\���e�L�X�g
    Text HavePlasticText;
    Text HaveEnplaText;
    Text HaveWoodText;
    Text HaveSteelText;
    Text HaveSeafoodText;

    Paramater NextUpParamater;      // ������̃p�����[�^���ꎞ�I�ɕێ�����ϐ�
    SeaResource UpgradeUseResource; // �����ɕK�v�ȑf�ސ����ꎞ�I�ɕێ�����ϐ�

    // ����ʂ̌v�Z�̂��߂ɕK�v
    float craneGetPower;
    float mouseGetPower;

    float returnabilityRate;

    // Start is called before the first frame update
    void Start()
    {
        IconFragment = this.gameObject.transform.Find("IconFragment").gameObject;
        IconFragment.SetActive(false);

        SubjectCursol = this.gameObject.transform.Find("SubjectCursol").gameObject;
        CursolTransform = SubjectCursol.GetComponent<RectTransform>();
        SubjectNum = 0;

        /* ==========Text��e�I�u�W�F�N�g����擾==========*/
        GameObject CraftUIPanel= this.gameObject.transform.parent.transform.parent.transform.parent.gameObject;
        GameObject StatusBackground = CraftUIPanel.transform.Find("StatusBackground").gameObject;
        GameObject UpdateView = StatusBackground.transform.Find("UpdateView").gameObject;

        // �X�e�[�^�X�\���I�u�W�F�N�g����e��Text���擾
        GameObject UnderLine_ShipLevel = UpdateView.transform.Find("UnderLine_ShipLV").gameObject;
        NowShipLevelText = UnderLine_ShipLevel.transform.Find("Ship_NowLevel").gameObject.GetComponent<Text>();
        AfterShipLevelText = UnderLine_ShipLevel.transform.Find("Ship_NextLevel").gameObject.GetComponent<Text>();

        GameObject UnderLine_Speed = UpdateView.transform.Find("UnderLine_Speed").gameObject;
        NowSpeedText = UnderLine_Speed.transform.Find("Speed_NowStatus").gameObject.GetComponent<Text>();
        AfterSpeedText = UnderLine_Speed.transform.Find("Speed_NextStatus").gameObject.GetComponent<Text>();

        GameObject UnderLine_Lading = UpdateView.transform.Find("UnderLine_Lading").gameObject;
        NowLadingText = UnderLine_Lading.transform.Find("Lading_NowStatus").gameObject.GetComponent<Text>();
        AfterLadingText = UnderLine_Lading.transform.Find("Lading_NextStatus").gameObject.GetComponent<Text>();

        GameObject UnderLine_Armer = UpdateView.transform.Find("UnderLine_Armer").gameObject;
        NowArmerText = UnderLine_Armer.transform.Find("Armer_NowStatus").gameObject.GetComponent<Text>();
        AfterArmerText = UnderLine_Armer.transform.Find("Armer_NextStatus").gameObject.GetComponent<Text>();

        GameObject UnderLine_Salvage = UpdateView.transform.Find("UnderLine_Salvage").gameObject;
        NowSalvageText = UnderLine_Salvage.transform.Find("Salvage_NowStatus").gameObject.GetComponent<Text>();
        AfterSalvageText = UnderLine_Salvage.transform.Find("Salvage_NextStatus").gameObject.GetComponent<Text>();

        // �Ҍ����e�L�X�g
        GameObject UnderLine_Rate = UpdateView.transform.Find("UnderLine_Rate").gameObject;
        NowRateText = UnderLine_Rate.transform.Find("Rate_NowStatus").gameObject.GetComponent<Text>();
        AfterRateText = UnderLine_Rate.transform.Find("Rate_NextStatus").gameObject.GetComponent<Text>();

        GameObject UnderLine_Rader = UpdateView.transform.Find("UnderLine_Rader").gameObject;
        NowRaderText = UnderLine_Rader.transform.Find("Rader_NowStatus").gameObject.GetComponent<Text>();
        AfterRaderText = UnderLine_Rader.transform.Find("Rader_NextStatus").gameObject.GetComponent<Text>();

        // �f�ރe�L�X�g�\���I�u�W�F�N�g����e��Text���擾
        GameObject ItemBackground = StatusBackground.transform.Find("ItemBackground").gameObject;

        GameObject UnderLine_Plastic= ItemBackground.transform.Find("UnderLine_Plastic").gameObject;
        UsePlasticText = UnderLine_Plastic.transform.Find("Plastic_NeedNum").gameObject.GetComponent<Text>();
        HavePlasticText = UnderLine_Plastic.transform.Find("Plastic_HaveNum").gameObject.GetComponent<Text>();

        GameObject UnderLine_EnPla = ItemBackground.transform.Find("UnderLine_EnPla").gameObject;
        UseEnplaText = UnderLine_EnPla.transform.Find("EnPla_NeedNum").gameObject.GetComponent<Text>();
        HaveEnplaText = UnderLine_EnPla.transform.Find("EnPla_HaveNum").gameObject.GetComponent<Text>();

        GameObject UnderLine_Wood = ItemBackground.transform.Find("UnderLine_Wood").gameObject;
        UseWoodText = UnderLine_Wood.transform.Find("Wood_NeedNum").gameObject.GetComponent<Text>();
        HaveWoodText = UnderLine_Wood.transform.Find("Wood_HaveNum").gameObject.GetComponent<Text>();

        GameObject UnderLine_Steel = ItemBackground.transform.Find("UnderLine_Steel").gameObject;
        UseSteelText = UnderLine_Steel.transform.Find("Steel_NeedNum").gameObject.GetComponent<Text>();
        HaveSteelText = UnderLine_Steel.transform.Find("Steel_HaveNum").gameObject.GetComponent<Text>();

        GameObject UnderLine_Seefood = ItemBackground.transform.Find("UnderLine_Seefood").gameObject;
        UseSeafoodText = UnderLine_Seefood.transform.Find("Seafood_NeedNum").gameObject.GetComponent<Text>();
        HaveSeafoodText = UnderLine_Seefood.transform.Find("Seafood_HaveNum").gameObject.GetComponent<Text>();


        // craftUI��player���擾
        craftUI = CraftUIPanel.transform.parent.transform.parent.gameObject.GetComponent<CraftUI>();

        player = craftUI.GetPlayer();


        // �����f�ޕ\���e�L�X�g�Ƀe�L�X�g�����
        HavePlasticText.text = player.seaResource.plastic.ToString();
        HaveEnplaText.text = player.seaResource.ePlastic.ToString();
        HaveWoodText.text = player.seaResource.wood.ToString();
        HaveSteelText.text = player.seaResource.steel.ToString();
        HaveSeafoodText.text = player.seaResource.seaFood.ToString();

        returnabilityRate = 1.0f;

        craneGetPower = craftUI.GetCraneGetPower();
        mouseGetPower = craftUI.GetMouseGetPower();
        
        NextUpParamater = new Paramater();
        UpgradeUseResource = new SeaResource();
    }

    // Update is called once per frame
    void Update()
    {        
        // �㉺���͂ō��ڂ�I��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SubjectNum++;
            if (SubjectNum > 4)
            {
                SubjectNum = 4;
            }

            if (SubjectNum == 3)
            {
                IconFragment.SetActive(true);
            }
            else if (SubjectNum != 3)
            {
                IconFragment.SetActive(false);
            }
            
            CursolPositionCalculation();            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SubjectNum--;
            if (SubjectNum < 0)
            {
                SubjectNum = 0;
            }

            if (SubjectNum == 3)
            {
                IconFragment.SetActive(true);
            }
            else if (SubjectNum != 3)
            {
                IconFragment.SetActive(false);
            }
            
            CursolPositionCalculation();
        }

        // Enter�L�[�ŋ���
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // �f�ޑ���Ă邩����
            if (SubjectNum == 0)
            {
                craftUI.DieselEngineUpgrade();
            }
            else if (SubjectNum == 1)
            {
                craftUI.ShipBodyUpgrade();
            }
            else if (SubjectNum == 2)
            {
                //AddRate(UpgradePalamater[ContenaNum].Rate);

                craftUI.WhaleMouseUpgrade();
            }
            else if (SubjectNum == 3)
            {
                craftUI.CraneUpgrade();
            }
            else if (SubjectNum == 4)
            {
                craftUI.SonarUpgrade();
            }

            TextUpdate();
        }
    }
    
    // �J�[�\���̃|�W�V�������v�Z���ăZ�b�g����
    void CursolPositionCalculation()
    {
        // y=-5+160*x
        CursolTransform.anchoredPosition = new Vector2(-45.0f, -5.0f + 160.0f * (2 - SubjectNum));
    }

    public int GetSubjectNum()
    {
        return SubjectNum;
    }

    // �e�L�X�g���e���X�V����
    public void TextUpdate()
    {
        NextUpParamater = craftUI.GetNextParamater(SubjectNum);
        UpgradeUseResource = craftUI.GetNextUseResource(SubjectNum);

        craneGetPower = craftUI.GetCraneGetPower();
        mouseGetPower = craftUI.GetMouseGetPower();

        SetPlayerText(NextUpParamater);
    }

    /* ===========CraftUI_SubjectIconController����p�����[�^�Ƒf�ސ����擾=========== */
    public void SetPlayerText(Paramater upParamater)
    {
        // �X�e�[�^�X�\���e�L�X�g���X�V
        NowShipLevelText.text = player.shipLevel.ToString();
        AfterShipLevelText.text = (player.shipLevel + 1).ToString();

        NowSpeedText.text = player.speed.ToString();
        int speed = NextUpParamater.Speed + player.speed;
        AfterSpeedText.text = speed.ToString();

        NowLadingText.text = player.resourceStack.ToString();
        int lading = NextUpParamater.Lading + player.resourceStack;
        AfterLadingText.text = lading.ToString();

        NowArmerText.text = player.shipArmer.ToString();
        AfterArmerText.text = (player.shipArmer + upParamater.Armer).ToString();

        // ����ʃe�L�X�g(�N���[��&�}�E�X������ȊO���ŏ����𕪂���)
        NowSalvageText.text = player.getPower.ToString();
        if (SubjectNum== 2)
        {
            if (upParamater.GetPower > 0.0f)
            {
                AfterSalvageText.text = ((int)(100.0f * (upParamater.GetPower * craneGetPower))).ToString();
            }
            else
            {
                AfterSalvageText.text = player.getPower.ToString();
            }
        }
        else if(SubjectNum == 3)
        {
            if (upParamater.GetPower > 0.0f)
            {
                AfterSalvageText.text = ((int)(100.0f * (upParamater.GetPower * mouseGetPower))).ToString();
            }
            else
            {
                AfterSalvageText.text = player.getPower.ToString();
            }
        }
        else
        {
            AfterSalvageText.text= player.getPower.ToString();
        }

        // �Ҍ����e�L�X�g
        NowRateText.text = (returnabilityRate * 100.0f).ToString() + "%";
        AfterRateText.text = ((returnabilityRate + upParamater.Rate) * 100.0f).ToString() + "%";


        // ���[�_�[�X�e�[�^�X�e�L�X�g
        NowRaderText.text = player.searchPower.ToString();
        int rader = upParamater.SearchPower + player.searchPower;
        AfterRaderText.text = rader.ToString();

        // �����f�ރe�L�X�g���X�V
        HavePlasticText.text = player.seaResource.plastic.ToString();
        HaveEnplaText.text = player.seaResource.ePlastic.ToString();
        HaveWoodText.text = player.seaResource.wood.ToString();
        HaveSteelText.text = player.seaResource.steel.ToString();
        HaveSeafoodText.text = player.seaResource.seaFood.ToString();

        // �K�v�f�ރe�L�X�g�ɐ��l�����
        UsePlasticText.text = UpgradeUseResource.plastic.ToString();
        UseEnplaText.text = UpgradeUseResource.ePlastic.ToString();
        UseWoodText.text = UpgradeUseResource.wood.ToString();
        UseSteelText.text = UpgradeUseResource.steel.ToString();
        UseSeafoodText.text = UpgradeUseResource.seaFood.ToString();
    }
    
    // �}�E�X�������ɌĂ�
    public void AddRate(float rate)
    {
        returnabilityRate += rate;
    }
}
