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
    [SerializeField] CraftUI craftUI;
    [SerializeField] Player player;

    // �X�e�[�^�X�e�L�X�g�\���I�u�W�F�N�g
    [SerializeField] GameObject SpeedTextUnit;
    [SerializeField] GameObject LasingTextUnit;
    [SerializeField] GameObject ArmerTextUnit;
    [SerializeField] GameObject SalvageTextUnit;
    [SerializeField] GameObject RateTextUnit;
    [SerializeField] GameObject RaderTextUnit;

    // ���݂̃X�e�[�^�X�\���e�L�X�g
    Text NowSpeedText;
    Text NowLadingText;
    Text NowArmerText;
    Text NowSalvageText;
    Text NowRateText;
    Text NowRaderText;

    // ������̃X�e�[�^�X�\���e�L�X�g
    Text AfterSpeedText;
    Text AfterLadingText;
    Text AfterArmerText;
    Text AfterSalvageText;
    Text AfterRateText;
    Text AfterRaderText;

    // �f�ރe�L�X�g�\���I�u�W�F�N�g
    [SerializeField] GameObject PlasticTextUnit;
    [SerializeField] GameObject EnplaTextUnit;
    [SerializeField] GameObject WoodTextUnit;
    [SerializeField] GameObject SteelTextUnit;
    [SerializeField] GameObject SeaFoodTextUnit;

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

    CraftUI_SubjectIconController NowSubject;

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

        // �X�e�[�^�X�\���I�u�W�F�N�g����e��Text���擾
        NowSpeedText = SpeedTextUnit.transform.Find("Speed_NowStatus").gameObject.GetComponent<Text>();
        AfterSpeedText = SpeedTextUnit.transform.Find("Speed_NextStatus").gameObject.GetComponent<Text>();

        NowLadingText = LasingTextUnit.transform.Find("Lading_NowStatus").gameObject.GetComponent<Text>();
        AfterLadingText = LasingTextUnit.transform.Find("Lading_NextStatus").gameObject.GetComponent<Text>();

        NowArmerText = ArmerTextUnit.transform.Find("Armer_NowStatus").gameObject.GetComponent<Text>();
        AfterArmerText = ArmerTextUnit.transform.Find("Armer_NextStatus").gameObject.GetComponent<Text>();

        NowSalvageText = SalvageTextUnit.transform.Find("Salvage_NowStatus").gameObject.GetComponent<Text>();
        AfterSalvageText = SalvageTextUnit.transform.Find("Salvage_NextStatus").gameObject.GetComponent<Text>();

        // �Ҍ����e�L�X�g
        NowRateText = RateTextUnit.transform.Find("Rate_NowStatus").gameObject.GetComponent<Text>();
        AfterRateText = RateTextUnit.transform.Find("Rate_NextStatus").gameObject.GetComponent<Text>();

        NowRaderText = RaderTextUnit.transform.Find("Rader_NowStatus").gameObject.GetComponent<Text>();
        AfterRaderText = RaderTextUnit.transform.Find("Rader_NextStatus").gameObject.GetComponent<Text>();

        // �f�ރe�L�X�g�\���I�u�W�F�N�g����e��Text���擾
        UsePlasticText = PlasticTextUnit.transform.Find("Plastic_NeedNum").gameObject.GetComponent<Text>();
        HavePlasticText = PlasticTextUnit.transform.Find("Plastic_HaveNum").gameObject.GetComponent<Text>();

        UseEnplaText = EnplaTextUnit.transform.Find("EnPla_NeedNum").gameObject.GetComponent<Text>();
        HaveEnplaText = EnplaTextUnit.transform.Find("EnPla_HaveNum").gameObject.GetComponent<Text>();

        UseWoodText = WoodTextUnit.transform.Find("Wood_NeedNum").gameObject.GetComponent<Text>();
        HaveWoodText = WoodTextUnit.transform.Find("Wood_HaveNum").gameObject.GetComponent<Text>();

        UseSteelText = SteelTextUnit.transform.Find("Steel_NeedNum").gameObject.GetComponent<Text>();
        HaveSteelText = SteelTextUnit.transform.Find("Steel_HaveNum").gameObject.GetComponent<Text>();

        UseSeafoodText = SeaFoodTextUnit.transform.Find("Seafood_NeedNum").gameObject.GetComponent<Text>();
        HaveSeafoodText = SeaFoodTextUnit.transform.Find("Seafood_HaveNum").gameObject.GetComponent<Text>();

        // �����f�ޕ\���e�L�X�g�Ƀe�L�X�g�����
        HavePlasticText.text = player.seaResource.plastic.ToString();
        HaveEnplaText.text = player.seaResource.ePlastic.ToString();
        HaveWoodText.text = player.seaResource.wood.ToString();
        HaveSteelText.text = player.seaResource.steel.ToString();
        HaveSeafoodText.text = player.seaResource.seaFood.ToString();

        returnabilityRate = 1.0f;
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

    public Player GetPlayer()
    {
        return player;
    }

    /* ===========CraftUI_SubjectIconController����p�����[�^�Ƒf�ސ����擾=========== */
    public void SetPlayerText(SeaResource resource, Paramater upParamater, UpgradeSubject subject)
    {
        // �X�e�[�^�X�\���e�L�X�g���X�V
        NowSpeedText.text = player.speed.ToString();
        int speed = upParamater.Speed + player.speed;
        AfterSpeedText.text = speed.ToString();

        NowLadingText.text = player.resourceStack.ToString();
        int lading = upParamater.Lading + player.resourceStack;
        AfterLadingText.text = lading.ToString();

        NowArmerText.text = player.shipArmer.ToString();
        //int lading = upParamater.Lading + player.resourceStack;
        AfterArmerText.text = (player.shipArmer + upParamater.Armer).ToString(); //lading.ToString();

        // ����ʃe�L�X�g(�N���[��&�}�E�X������ȊO���ŏ����𕪂���)
        NowSalvageText.text = player.getPower.ToString();
        if (subject== UpgradeSubject.SUBJECT_WHALEMOUSE)
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
        else if(subject == UpgradeSubject.SUBJECT_CRANE)
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
        UsePlasticText.text = resource.plastic.ToString();
        UseEnplaText.text = resource.ePlastic.ToString();
        UseWoodText.text = resource.wood.ToString();
        UseSteelText.text = resource.steel.ToString();
        UseSeafoodText.text = resource.seaFood.ToString();
    }

    // �}�E�X���N���[���̋����̎������ǉ��ŌĂ�
    public void SetGetPowerText(float state, UpgradeSubject subject)
    {
        if (subject == UpgradeSubject.SUBJECT_WHALEMOUSE)
        {
            mouseGetPower = state;
        }
        else if(subject == UpgradeSubject.SUBJECT_CRANE)
        {            
            craneGetPower = state;
        }
    }

    // �}�E�X�������ɌĂ�
    public void AddRate(float rate)
    {
        returnabilityRate += rate;
    }

    public void SetCraneGetPower(float power)
    {
        if (power > 0.0f)
        {
            craneGetPower = power;
            craftUI.SetCraneGetower(power);
        }
    }

    public void SetMouseGetPower(float power)
    {
        if (power > 0.0f)
        {
            mouseGetPower = power;
            craftUI.SetMouseGetower(power);
        }
    }
}
