using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField] private int currentWaveIndex = 0;

    private int currentSpawnCount = 0;
    private int waveSpawnCount = 0;
    private int waveSpawnPosCount = 0;

    public float spawnInterval = .5f;

    public List<GameObject> enemyPrefebs = new List<GameObject>();

    [SerializeField] private Transform spawnPositionsRoot;
    private List<Transform> spawnPositions = new List<Transform>();

    public List<GameObject> rewards = new List<GameObject>();

    [SerializeField] private CharacterStats defaultStats;
    [SerializeField] private CharacterStats rangedStats;

    float timer = 10.0f;

    [SerializeField] GameObject stage_1;
    [SerializeField] GameObject stage_2;


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
            if(stage_1.activeSelf == true && stage_2.activeSelf == false)
            {
                SceneManager.LoadScene("Ep_1");
            }
            else if(stage_1.activeSelf == true && stage_1.activeSelf == false) 
            {
                SceneManager.LoadScene("Ep_2");
            }
            
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


            if(currentSpawnCount == 0)
            {
                yield return new WaitForSeconds(2f);

                if(currentWaveIndex % 20 == 0)
                {
                    RandomUpgrade();
                }

                if(currentWaveIndex % 10 == 0)
                {
                    waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPositions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
                    waveSpawnCount = 0;
                }

                if(currentWaveIndex % 5 == 0)
                {
                    CreateReward();
                }

                if(currentWaveIndex % 3 == 0)
                {
                    waveSpawnCount++;
                }

                for(int i = 0; i < waveSpawnPosCount; i++)
                {
                    int PosIdx = Random.Range(0, spawnPositions.Count);

                    for (int j = 0; j < waveSpawnCount; j++)
                    {
                        int prefabIdx = Random.Range(0, enemyPrefebs.Count);
                        GameObject enemy = Instantiate(enemyPrefebs[prefabIdx], spawnPositions[PosIdx].position, Quaternion.identity);
                        enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
                        enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(defaultStats);
                        enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(rangedStats);
                        currentSpawnCount++;
                        yield return new WaitForSeconds(spawnInterval);

                    }
                }

                currentWaveIndex++;
            }
            yield return null;
        }
    }

    private void OnEnemyDeath()
    {
        currentSpawnCount--;
    }

    private void GameOver()
    {
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

    void RandomUpgrade()
    {
        switch(Random.Range(0, 6))
        {
            case 0:
                defaultStats.maxHealth += 2;
                break;

            case 1:
                defaultStats.attackSO.power += 1;
                break;

            case 2:
                defaultStats.attackSO.isOnKnockback = true;
                defaultStats.attackSO.knockbackPower += 1;
                defaultStats.attackSO.knockbackTime = 0.1f;
                break;

            case 3:
                defaultStats.speed += 0.1f;
                break;

            case 4:
                defaultStats.attackSO.delay -= 0.05f;
                break;

            case 5:
                RangedAttackData rangedAttackData = rangedStats.attackSO as RangedAttackData;
                rangedAttackData.numberofProjectilesPerShot += 1;
                break;

            default:
                break;
        }

    }
}

