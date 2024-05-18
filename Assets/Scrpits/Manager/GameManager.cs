using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] Transform layerItemList;

    [Header("�÷��̾� ����")]
    [SerializeField] TMP_Text moneytext;

    public static GameManager Instance;
    private Player player;//�÷��̾ �˰� ���߿� �����Ǵ� ��� ������Ʈ�� �÷��̾ �ʿ��ϴٸ� �����ü� �ְ� ����
    private Turret turret;
    private Camera maincam;
    private float playermoney;
    private Shop shop;
    private Inventory inventory;
    private bool checkInven = false;

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
            if (checkInven == false)
            {
                MenuMain.SetActive(true);
                Inventory.Instance.ActiveMenu();
                Time.timeScale = 0.0f;
            }
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

            Fade.instance.FadeOut(() =>
            {
                Time.timeScale = 1f;
                SceneManager.LoadSceneAsync((int)enumScene.StartScene);
            });
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
        updatemoney();

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

    public Shop GetShop()
    {
        return shop;
    }

    public void SetShop(Shop _value)
    {
        shop = _value;
    }

    public Inventory GetInven()
    {
        return inventory;
    }

    public void SetInven(Inventory _value)
    {
        inventory = _value;
    }

    #endregion

    public void GameOver()
    {
        Fade.instance.FadeOut(() =>
        {
            SceneManager.LoadSceneAsync((int)enumScene.EndScene);
        });
    }

    public void GameClear()
    {
        Fade.instance.FadeOut(() =>
        {
            SceneManager.LoadSceneAsync((int)enumScene.ClearScene);
        });
    }


    private void GetMainButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && checkInven == false)
        {
            if (MenuMain.activeSelf == true)
            {
                MenuMain.SetActive(false);
                Inventory.Instance.DisableMenu();
                Time.timeScale = 1.0f;
            }
            else
            {
                MenuMain.SetActive(true);
                Inventory.Instance.ActiveMenu();
                Time.timeScale = 0.0f;
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

    public void CreateGameItem(Vector3 Pos)
    {
        int iRand = Random.Range(0, ItemList.Count);
        GameObject itemrange = ItemList[iRand];

        Instantiate(itemrange, Pos, Quaternion.identity, layerItemList);
    }

    public void checkmoney(float _money)
    {
        playermoney = _money;
    }

    private void updatemoney()
    {
        moneytext.text = $"X {(int)playermoney}";
    }

    public void ActiveInven()
    {
        checkInven = true;
    }

    public void DisableInven()
    {
        checkInven = false;
    }



}
