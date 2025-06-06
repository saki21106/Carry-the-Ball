using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    public float min_power = 700; //発射速度の最小値
    public float max_power = 1000; //発射速度の最大値
    Vector3 ang;
    float x, y = 3.9f, z = 24.8f;
    int num = 0;
    public static int point = 0;
    Rigidbody rb;
    public static bool swpressed = false; //スイッチが押されたか
    public static bool blinkled = false; //LEDを光らせるか
    bool hasshot = false; //ボールが発射されたか
    public Text scoreText;

    public AudioClip getptSE; // プラス点を取った時の音
    public AudioClip lossptSE; // マイナス点を取った時の音
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        x = Random.Range(-11.5f, 11.5f); //ボールの位置をランダムに決める
        transform.position = new Vector3(x, y, z);
        ang = transform.eulerAngles;
        num = Random.Range(0, 3); //ボールの種類を決める(0,1:赤　2:黄)
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var back = -transform.forward;
        if (num == 0 || num == 1) //赤
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (num == 2) //黄
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (swpressed && !hasshot)
        {
            rb.useGravity = true;
            rb.AddForce(back * Random.Range(min_power, max_power)); //発射
            hasshot = true;
        }
        if(transform.position.y < -10.0f) //落下時
        {
            Gostart();
        }
        if (Input.GetKeyDown(KeyCode.R)) //Rキーでボールを再発射(ハマった時)
        {
            Gostart();
        }
        scoreText.text = "score: " + point + "pt"; //スコア表示
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Point")
        {
            int val = int.Parse(other.name);
            if (val>0) //プラス点を取った時
            {
                audioSource.PlayOneShot(getptSE);
                blinkled = true; //LED点灯
            }
            else if(val<0) //マイナス点を取った時
            {
                audioSource.PlayOneShot(lossptSE);
            }
            if (num == 0 || num == 1) //赤
            {
                point += val;
            }
            else if (num == 2) //黄
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
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //衝突時y軸方向の速度を0にする
        }
    }

    public void Gostart()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        x = Random.Range(-11.5f, 11.5f); //ボールの位置をランダムに決める
        transform.position = new Vector3(x, y, z);
        transform.eulerAngles = ang;
        num = Random.Range(0, 3); //ボールの種類を決める
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
