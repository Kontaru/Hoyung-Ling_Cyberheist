using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    //private bool BL_SwitchScenes;

    public int max_enemies = 20;
    public int enemycount = 0;

    public int player_Health;

    [Header("Entities")]

    public GameObject[] GO_Player = new GameObject[2];

    [Header("Lights")]
    public Light DirectionalLight;
    [Range(0, 1)]
    public float FL_DirLight;

    [Header("PlayerUI")]

    #region --- Event Params ---

    [Header("Event Params")]
    public int DeathCounter = 0;
    public bool BL_Die = false;


    #endregion

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region ~ Scene Related ~

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    IEnumerator PauseGame(float delay)
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(delay);
        Time.timeScale = 1;
    }
    #endregion
}
