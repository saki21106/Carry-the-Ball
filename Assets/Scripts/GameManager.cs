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
        point = Shot.Getpoint(); //���݂̃|�C���g���擾
        scoreText.text = "Your score: " + point + " pt"; 

        // GameOver�e�L�X�g���Ăяo��
        gameoverText.SetActive(true);
        // GameRestart���Ăяo����5�b�ԑ҂�
        Invoke("GameRestart", 5);
    }

    public void GameRestart()
    {
        Shot.Resetpoint(); //�|�C���g��0�Ƀ��Z�b�g
        // ���݂̃V�[�����擾���ă��[�h����
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
