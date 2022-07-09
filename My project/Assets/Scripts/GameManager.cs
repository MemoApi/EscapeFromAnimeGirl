using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject animeGirl;
    public static GameManager instance;

    [SerializeField] int AnimeGirlWave=10;
    [SerializeField] int animeGirlcounter;

    [SerializeField] GameObject menuPanel;
    [SerializeField] TMP_Text GirlCounterTxt;

    public bool isDead = false;


    private void Awake()
    {
        instance = this;
        menuPanel.SetActive(false);
        isDead = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        animeGirlcounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(animeGirlcounter<=0)
        {
            AnimeGirlWave += 10;
            spawnWave(AnimeGirlWave);
            return;
        }
        GirlCounterTxt.text = "Anime Girl Left : " + animeGirlcounter;
    }


    void spawnWave(int Wave)
    {
        for (int i = 0; i < Wave; i++)
        {
        Instantiate(animeGirl, spawnPositions[UnityEngine.Random.Range(0,spawnPositions.Length)].position, Quaternion.identity);
        }
    }

    private void Start()
    {
        animeGirlcounter = AnimeGirlWave;
        spawnWave(AnimeGirlWave);
        
    }
    public void restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayerDead()
    {
        isDead = true;
        menuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = .1f;
    }


}
