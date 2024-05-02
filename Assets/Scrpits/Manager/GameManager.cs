using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player player;//플레이어를 알고 나중에 생성되는 모든 오브젝트가 플레이어가 필요하다면 가져올수 있게 해줌
    private Turret turret;
    private Camera maincam;

    float playerdeathtimecheck = 3f;
    float playerdeathtime;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        maincam = GetComponent<Camera>();
    }

    private void Update()
    {

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




}
