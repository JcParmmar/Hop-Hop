using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public bool isGameRunning = false;//bool to check is game is running or not
    public DragManager dragManager;// drage manager ref

    //Total jump ball has mead in property 
    public int TotalJumps
    {
        get => _jumpCount;
        set
        {
            _jumpCount = value;
            jumpCountText.text = "Jump Count : " + _jumpCount.ToString();
        }
    }
    
    [Space(15), Header("UI")]
    public GameObject mainMenu;// main menu page ui ref
    public GameObject resultPage;// result page ui ref
    public TMP_Text jumpCountText;// jump count text for display
    
    private int _jumpCount = 0;//private jump count for mail calculation 

    private void Awake()
    {
        if(Instance) Destroy(this);
        else Instance = this;
    }

    public void StartGame()
    {
        TotalJumps = 0;//restart jump count
        isGameRunning = true;//start game
        mainMenu.SetActive(false);//hide main menu page
    }

    public void GameOver()
    {
        //make game over 
        isGameRunning = false;
        resultPage.SetActive(true);
    }

    public void RestartGame()
    {
        //open same scene again
        SceneManager.LoadScene(0);
    }
}
