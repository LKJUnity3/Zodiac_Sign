using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    

    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject Level;

    private HealthSystem playerHealthSystem;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Slider hpGaugeSlider;

    [SerializeField] private GameObject gameOverUI;

    private int currentSpawnCount = 0;

    public float spawnInterval = .5f;

    public List<GameObject> enemyPrefebs = new List<GameObject>();

    [SerializeField] private Transform spawnPositionsRoot;
    private List<Transform> spawnPositions = new List<Transform>();

    public List<GameObject> rewards = new List<GameObject>();

    [SerializeField] private CharacterStats defaultStats;
    [SerializeField] private CharacterStats rangedStats;

    float timer = 100.0f;

    [SerializeField] GameObject stage_1;
    [SerializeField] GameObject stage_2;
    [SerializeField] GameObject boss;

    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;     //tag가 Player인 sprite를 찾아 위치값을 Player 변수에 입력
        //hieracy 상의 모든 object를 검사하기 때문에 update 보단 awake 에서 한 번만 찾아주는게 좋음

        playerHealthSystem = Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += UpdateHealthUI;
        playerHealthSystem.OnHeal += UpdateHealthUI;
        playerHealthSystem.OnDeath += GameOver;

        gameOverUI.SetActive(false);

        int stage = PlayerPrefs.GetInt("Stage", 1);
        Level.transform.Find($"Stage_{stage}").gameObject.SetActive(true);
        
        

        for(int i = 0; i < spawnPositionsRoot.childCount; i++)
        {
            spawnPositions.Add(spawnPositionsRoot.GetChild(i));
        }

    }

    private void Start()
    {
        UpgradeStatInit();
        StartCoroutine("StartNextWave");
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            timer = 0f;

            if (stage_1.activeSelf == true)
            {
                SceneManager.LoadScene("Ep_1");
            }
        }

        if (timer <= 50f && stage_2.activeSelf == true)
        {
            boss.SetActive(true);
        }

        timer -= Time.deltaTime;
        timeText.text = timer.ToString("N1");
    }

    IEnumerator StartNextWave()
    {
        while(true)
        {
            for(int i = 0; i < 10; i++)
            {
                int PosIdx = Random.Range(0, spawnPositions.Count);

                int prefabIdx = Random.Range(0, enemyPrefebs.Count);
                GameObject enemy = Instantiate(enemyPrefebs[prefabIdx], spawnPositions[PosIdx].position, Quaternion.identity);
                enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
                enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(defaultStats);
                enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(rangedStats);
                currentSpawnCount++;

                yield return new WaitForSeconds(1f);
            }

            CreateReward();


            
            yield return null;
        }
    }

    private void OnEnemyDeath()
    {
        currentSpawnCount--;
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        StopAllCoroutines();
    }

    private void UpdateHealthUI()
    {
        hpGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void CreateReward()
    {
        int idx = Random.Range(0, rewards.Count);
        int posIdx = Random.Range(0, spawnPositions.Count);

        GameObject obj = rewards[idx];
        Instantiate(obj, spawnPositions[posIdx].position, Quaternion.identity);
    }

    void UpgradeStatInit()
    {
        defaultStats.statsChangeType = StatsChangeType.Add;
        defaultStats.attackSO = Instantiate(defaultStats.attackSO);

        rangedStats.statsChangeType = StatsChangeType.Add;
        rangedStats.attackSO = Instantiate(rangedStats.attackSO);
    }

   public void StageClear()
    {
        if(timer <= 0f) 
        {
            GameOver();
        }
        else
        {
            SceneManager.LoadScene("Ep_2");
        }
    }
}

