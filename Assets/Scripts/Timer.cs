using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //�@�g�[�^����������
    private float totalTime;
    //�@�J�E���g�_�E���̉���炷�p�̃^�C�}�[
    private float cntTime;
    private int cntNum = 11;
    //�@�������ԁi���j
    [SerializeField]
    public int minute = 3;
    //�@�������ԁi�b�j
    [SerializeField]
    public float seconds = 0f;
    //�@�O��Update���̕b��
    private float oldSeconds;
    public Text timerText;
    public GameManager gamemanager;

    public AudioClip countSE; // �J�E���g�_�E���̉�
    public AudioClip timeoutSE; // ���U���g��ʕ\�����̉�
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        timerText = GetComponentInChildren<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�@�������Ԃ�0�b�ȉ��Ȃ牽�����Ȃ�
        if (totalTime <= 0f)
        {
            return;
        }
        //�@��U�g�[�^���̐������Ԃ��v���G
        totalTime = minute * 60 + seconds;
        cntTime = totalTime;
        totalTime -= Time.deltaTime;

        //�@�Đݒ�
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        //�@�^�C�}�[�\���pUI�e�L�X�g�Ɏ��Ԃ�\������
        if ((int)seconds != (int)oldSeconds)
        {
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            if (totalTime < 11)
            {
                timerText.color = Color.red;
            }
        }
        oldSeconds = seconds;

        if(cntTime>=cntNum && totalTime<cntNum && cntNum>0)
        {
            audioSource.PlayOneShot(countSE);
            cntNum--;
        }

        //�@�������Ԉȉ��ɂȂ�����R���\�[���Ɂw�������ԏI���x�Ƃ����������\������
        if (totalTime <= 0f)
        {
            audioSource.PlayOneShot(timeoutSE);
            // �Q�[���I�[�o�[���Ăяo��
            gamemanager.GameOver();
            Debug.Log("Time Out");
        }
    }
}
