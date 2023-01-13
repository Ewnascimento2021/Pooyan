using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameController>();
            return instance;
        }
    }


    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform spawn;
    [SerializeField]
    private TMP_Text killRest;
    public AudioSource audioTiro;

    public GameObject gameOver;
    public GameObject youWin;


    private int quantEnemy = 50;
    private int enemyCounter = 0;
    public int EnemyCounter { get => enemyCounter; set => enemyCounter = value; }

    private void Start()
    {
        gameOver.SetActive(false);
        youWin.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        KillsText();
        Mensagens();


    }

    public void CriarNovoInimigo()
    {
        if (quantEnemy != 0)
        {
            GameObject nascido = Instantiate(enemy, spawn.position, spawn.rotation);

            nascido.GetComponent<Enemy>().Down = RandomDown();

            quantEnemy--;
        }
        else
        {
            youWin.SetActive(true);
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private float RandomDown()
    {
        int randomized = UnityEngine.Random.Range(0, 4);

        switch (randomized)
        {
            case 0:
                return -5f;
            case 1:
                return -3f;

            case 2:
                return -1f;
            case 3:
                return 1f;

            default:
                Debug.Log(randomized);
                return 6.50498014f;
        }
    }
    private void KillsText()
    {
        killRest.text = "Quantd.\nRestante:\n" + quantEnemy.ToString();
    }
    private void Mensagens()
    {
        if ((youWin.activeSelf) || (gameOver.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}