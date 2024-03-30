using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //　トータル制限時間
    private float totalTime;
    //　カウントダウンの音を鳴らす用のタイマー
    private float cntTime;
    private int cntNum = 11;
    //　制限時間（分）
    [SerializeField]
    public int minute = 3;
    //　制限時間（秒）
    [SerializeField]
    public float seconds = 0f;
    //　前回Update時の秒数
    private float oldSeconds;
    public Text timerText;
    public GameManager gamemanager;

    public AudioClip countSE; // カウントダウンの音
    public AudioClip timeoutSE; // リザルト画面表示時の音
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
        //　制限時間が0秒以下なら何もしない
        if (totalTime <= 0f)
        {
            return;
        }
        //　一旦トータルの制限時間を計測；
        totalTime = minute * 60 + seconds;
        cntTime = totalTime;
        totalTime -= Time.deltaTime;

        //　再設定
        minute = (int)totalTime / 60;
        seconds = totalTime - minute * 60;

        //　タイマー表示用UIテキストに時間を表示する
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

        //　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
        if (totalTime <= 0f)
        {
            audioSource.PlayOneShot(timeoutSE);
            // ゲームオーバーを呼び出す
            gamemanager.GameOver();
            Debug.Log("Time Out");
        }
    }
}
