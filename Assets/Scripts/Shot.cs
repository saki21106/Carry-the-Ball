using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    public float min_power = 700; //���ˑ��x�̍ŏ��l
    public float max_power = 1000; //���ˑ��x�̍ő�l
    Vector3 ang;
    float x, y = 3.9f, z = 24.8f;
    int num = 0;
    public static int point = 0;
    Rigidbody rb;
    public static bool swpressed = false; //�X�C�b�`�������ꂽ��
    public static bool blinkled = false; //LED�����点�邩
    bool hasshot = false; //�{�[�������˂��ꂽ��
    public Text scoreText;

    public AudioClip getptSE; // �v���X�_����������̉�
    public AudioClip lossptSE; // �}�C�i�X�_����������̉�
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        x = Random.Range(-11.5f, 11.5f); //�{�[���̈ʒu�������_���Ɍ��߂�
        transform.position = new Vector3(x, y, z);
        ang = transform.eulerAngles;
        num = Random.Range(0, 3); //�{�[���̎�ނ����߂�(0,1:�ԁ@2:��)
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var back = -transform.forward;
        if (num == 0 || num == 1) //��
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (num == 2) //��
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (swpressed && !hasshot)
        {
            rb.useGravity = true;
            rb.AddForce(back * Random.Range(min_power, max_power)); //����
            hasshot = true;
        }
        if(transform.position.y < -10.0f) //������
        {
            Gostart();
        }
        if (Input.GetKeyDown(KeyCode.R)) //R�L�[�Ń{�[�����Ĕ���(�n�}������)
        {
            Gostart();
        }
        scoreText.text = "score: " + point + "pt"; //�X�R�A�\��
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            int val = int.Parse(other.name);
            if (val>0) //�v���X�_���������
            {
                audioSource.PlayOneShot(getptSE);
                blinkled = true; //LED�_��
            }
            else if(val<0) //�}�C�i�X�_���������
            {
                audioSource.PlayOneShot(lossptSE);
            }
            if (num == 0 || num == 1) //��
            {
                point += val;
            }
            else if (num == 2) //��
            {
                point += val*2;
            }
            Debug.Log(point);
        }
        Gostart();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //�Փˎ�y�������̑��x��0�ɂ���
        }
    }

    public void Gostart()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        x = Random.Range(-11.5f, 11.5f); //�{�[���̈ʒu�������_���Ɍ��߂�
        transform.position = new Vector3(x, y, z);
        transform.eulerAngles = ang;
        num = Random.Range(0, 3); //�{�[���̎�ނ����߂�
        swpressed = false;
        hasshot = false;
    }

    public static int Getpoint()
    {
        return point;
    }

    public static void Resetpoint()
    {
        point = 0;
    }

    public static void Setswpressed()
    {
        swpressed = true;
    }   

    public static bool Getblinkled()
    {
        return blinkled;
    }

    public static void Resetled()
    {
        blinkled = false;
    }
}
