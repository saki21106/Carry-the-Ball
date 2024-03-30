using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public Text scoreText;
    int point;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameOver()
    {
        point = Shot.Getpoint();
        scoreText.text = "Your score: " + point + " pt"; 

        // GameOverテキストを呼び出す
        gameoverText.SetActive(true);
        // GameRestartを呼び出して5秒間待つ
        Invoke("GameRestart", 5);
    }

    public void GameRestart()
    {
        Shot.Resetpoint();
        // 現在のシーンを取得してロードする
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
