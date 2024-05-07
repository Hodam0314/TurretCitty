using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("�޴�")]
    [SerializeField] Button Main;
    [SerializeField] Button MainStart;
    [SerializeField] Button MainSave;
    [SerializeField] Button MainStop;
    [SerializeField] GameObject MenuMain;
    private bool menuOn = false;

    [Header("����ȭ�� üũ")]
    [SerializeField] GameObject checkExit;
    [SerializeField] Button ExitYes;
    [SerializeField] Button ExitNo;

    [Header("���� ����")]
    [SerializeField] List<GameObject> Enemylist;
    [SerializeField] List<GameObject> SpawnPoint;
    [SerializeField] private float spawnCool = 3f;
    [SerializeField] bool enemySpawn = false;
    private float spawnTime = 0f;

    [Header("�� ���� ����")]
    [SerializeField] private float SpawnLevel = 0f;
    [SerializeField] private float SpawnCount = 0f;
    [SerializeField] private float TotalSpawn = 0f;

    [Header("������ ����")]
    [SerializeField, Range(0f, 100f)] float DropRate = 0f;
    [SerializeField] private List<GameObject> ItemList;

    [Header("���̾� ó��")]
    [SerializeField] Transform layerEnemySpawn;

    public static GameManager Instance;
    private Player player;//�÷��̾ �˰� ���߿� �����Ǵ� ��� ������Ʈ�� �÷��̾ �ʿ��ϴٸ� �����ü� �ְ� ����
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

        #region ��ư���
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
            //Json �� �̿��� ������ ��������

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

    #region �̱��� Ȱ��
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
