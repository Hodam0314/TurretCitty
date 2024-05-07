using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("메뉴")]
    [SerializeField] Button Main;
    [SerializeField] Button MainStart;
    [SerializeField] Button MainSave;
    [SerializeField] Button MainStop;
    [SerializeField] GameObject MenuMain;
    private bool menuOn = false;

    [Header("메인화면 체크")]
    [SerializeField] GameObject checkExit;
    [SerializeField] Button ExitYes;
    [SerializeField] Button ExitNo;

    [Header("적기 생성")]
    [SerializeField] List<GameObject> Enemylist;
    [SerializeField] List<GameObject> SpawnPoint;
    [SerializeField] private float spawnCool = 3f;
    [SerializeField] bool enemySpawn = false;
    private float spawnTime = 0f;

    [Header("적 생성 레벨")]
    [SerializeField] private float SpawnLevel = 0f;
    [SerializeField] private float SpawnCount = 0f;
    [SerializeField] private float TotalSpawn = 0f;

    [Header("아이템 생성")]
    [SerializeField, Range(0f, 100f)] float DropRate = 0f;
    [SerializeField] private List<GameObject> ItemList;

    [Header("레이어 처리")]
    [SerializeField] Transform layerEnemySpawn;

    public static GameManager Instance;
    private Player player;//플레이어를 알고 나중에 생성되는 모든 오브젝트가 플레이어가 필요하다면 가져올수 있게 해줌
    private Turret turret;
    private Camera maincam;

    float playerdeathtimecheck = 3f;
    float playerdeathtime;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        maincam = GetComponent<Camera>();

        #region 버튼기능
        Main.onClick.AddListener(() =>
        {
            MenuMain.SetActive(true);
        });

        MainStart.onClick.AddListener(() =>
        {
            MenuMain.SetActive(false);
        });

        MainSave.onClick.AddListener(() =>
        {
            //Json 을 이용한 저장기능 구현예정

        });

        MainStop.onClick.AddListener(() =>
        {
            checkExit.SetActive(true);
        });

        ExitYes.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync((int)enumScene.StartScene);
        });
        ExitNo.onClick.AddListener(() =>
        {
            checkExit.SetActive(false);
        });
        #endregion

    }

    private void Update()
    {
        GetMainButton();
        CheckEnemySpawn();


    }

    #region 싱글턴 활용
    public Player GetPlayer()
    {
        return player;
    }


    public void SetPlayer(Player _value)
    {
        player = _value;
    }

    public Turret GetTurret()
    {
        return turret;
    }
    public void SetTurret(Turret _value)
    {
        turret = _value;
    }
    #endregion

    public void GameOver()
    {
        SceneManager.LoadSceneAsync((int)enumScene.EndScene);
    }

    public void GameClear()
    {
        SceneManager.LoadSceneAsync((int)enumScene.ClearScene);
    }


    private void GetMainButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuMain.activeSelf == true)
            {
                MenuMain.SetActive(false);
            }
            else
            {
                MenuMain.SetActive(true);
            }
        }
    }

    private void CheckEnemySpawn()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= spawnCool)
        {
            SpawnEnemy();
            CheckSpawn();
            spawnTime = 0.0f;
        }



    }

    private void SpawnEnemy()
    {
        if (enemySpawn == true)
        {
            int spawnE = Random.Range(0, Enemylist.Count);
            GameObject objEnemy = Enemylist[spawnE];

            int spawnP = Random.Range(0, SpawnPoint.Count);
            GameObject spawnPoint = SpawnPoint[spawnP];


            GameObject obj = Instantiate(objEnemy, spawnPoint.transform.position, Quaternion.identity, layerEnemySpawn);
            Enemy objSc = obj.GetComponent<Enemy>();

            float range = Random.Range(0f, 100f);
            if (range <= DropRate)
            {
                objSc.GetItem();
            }

        }
    }

    private void CheckSpawn()
    {
        SpawnCount++;
        TotalSpawn++;
        if (SpawnCount == 20)
        {
            SpawnLevel++;
            SpawnCount = 0;
        }
        else
        {
            return;
        }


    }

}
