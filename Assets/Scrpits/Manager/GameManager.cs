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

    }

    private void Update()
    {
        GetMainButton();
    }


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



}
