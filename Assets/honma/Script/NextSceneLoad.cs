using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class NextSceneLoad : MonoBehaviour
{
    [SerializeField][Header("���[�h��ʒ��ɕ\������UI")]
    private GameObject BlackBoardPanel;
    
    [SerializeField][Header("���[�h��ʒ��ɕ\������Slider")]
    private Slider _nowLodingSlider;

    [SerializeField][Header("�e�L�X�g���o")]
    private Text _loadingText;

    [SerializeField][Header("�s���I�h�`��J�E���^(�傫���ƒx���Ȃ�)")]
    private int _textCnt;

    [SerializeField]
    [Header("�摜��POP���o�p")]
    private Image _imageTexture;

    [SerializeField][Header("�摜��POP����")]
    private int _imagePopHeight;

    private AsyncOperation _asyncOperation;

    private int _cnt = 0;

    private string _nextSceneName;
    private string _period2 = "";
    private string _period3 = "";

    private Vector3 _startPos;
    private Vector3 _endPos;

    public void LoadSceneStart(string SceneName)
    {
        _nextSceneName = SceneName;

        //���[�h�p��UI�\��
        BlackBoardPanel.SetActive(true);

        //�V�[���ǂݍ��݂��J�n����R���[�`��
        StartCoroutine(NowLoadScene());
    }

    IEnumerator NowLoadScene()
    {
        // �V�[���̓ǂݍ��݂�����  LoadSceneAsync�Ō���̓������Ȃ��s���i�����ł̓��[�h���Ȃ���X���C�_�[�𓮂����Ă���j
        _asyncOperation = SceneManager.LoadSceneAsync(_nextSceneName);

        //�@�ǂݍ��݂��I���܂Ői���󋵂��X���C�_�[�̒l�ɔ��f������
        while (!_asyncOperation.isDone)
        {
            //  _asyncOperation.progress �ŃV�[���̓ǂݍ��ݏ󋵂��擾����
            _nowLodingSlider.value = _asyncOperation.progress;

            //  �s���I�h��2��3�ڂ̓_�ŗp�̌v�Z
            if (_cnt % (_textCnt * 3) == 0) _period2 = "";
            if (_cnt % (_textCnt * 3) == 0) _period3 = "";

            if ((_cnt - _textCnt) % (_textCnt * 3) == 0) _period2 = ".";
            if (((_cnt - _textCnt * 2) % (_textCnt * 3)) == 0) _period3 = ".";

            _loadingText.text = $"Now Loding.{_period2}{_period3}";
            _cnt++;

            //  Flag��POP UP���o
            Transform _transform = _imageTexture.transform;
            float _lerp = _nowLodingSlider.value * _nowLodingSlider.value * _nowLodingSlider.value;//    3���Ȑ���ɓ�����

            //���W�X�V
            _transform.position = Vector3.Lerp(_startPos, _endPos, _lerp);

            
            yield return null;
        }
    }

    private void Start()
    {
        //���W�̏�����
        _startPos = _imageTexture.transform.position;
        _endPos = _imageTexture.transform.position;
        _endPos.y = _endPos.y + _imagePopHeight;

        BlackBoardPanel.SetActive(false);
    }

    //  �m�F�p
    /*private void Update()
    {
        //  �s���I�h��2��3�ڂ̓_�ŗp�̌v�Z
        if (_cnt % (_textCnt * 3) == 0) _period2 = "";
        if (_cnt % (_textCnt * 3) == 0) _period3 = "";//

        if ((_cnt - _textCnt) % (_textCnt * 3) == 0) _period2 = ".";
        if (((_cnt - _textCnt * 2) % (_textCnt * 3)) == 0) _period3 = ".";

        _loadingText.text = $"Now Loding.{_period2}{_period3}";
        _cnt++;


        //  Flag��POP UP���o
        Transform _transform = _imageTexture.transform;
        float _lerp = _nowLodingSlider.value * _nowLodingSlider.value * _nowLodingSlider.value;//    3���Ȑ���ɓ�����

        //���W�X�V
        _transform.position = Vector3.Lerp(_startPos, _endPos, _lerp);
    }*/
}
