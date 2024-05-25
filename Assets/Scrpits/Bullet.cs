using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    Player player;
    [SerializeField] private float bulletSpeed; // 총알 스피드
    [SerializeField] private float damage = 0.0f; // 총알 데미지
    private bool playerBullet = false; //플레이어가 쏜 총알인지 체크

    private void Start()
    {
        Invoke("Destroybullet", 2); //총알이 생성되고 2초뒤에 "Destroybullet"스크립트 실행
    }

    private void Awake()
    {
        player = GameManager.Instance.GetPlayer(); //게임매니저에 등록된 플레이어 데이터를 가져옴
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTag.Enemy.ToString()) //만약 collision에 닿은 게임오브젝트가 Enemy 게임태그를 갖고있을경우 실행
        {
            Enemy enemySc = collision.GetComponent<Enemy>(); //적의 스크립트 정보를 가져옴
            Destroy(gameObject); //총알을 지워줌
            enemySc.checkBullet(); //적 스크립트 내의 checkBullet 함수 코드를 실행시켜줌
            enemySc.Hit(damage); //enemySc의 Hit에 총알 데미지값을 전달 및 Hit 함수를 실행시켜줌
        }
    }

    void Update()
    {
        moving();
    }

    private void moving()
    {
        if(transform.eulerAngles.y == 0) //x y z = eulerAngles를 이용해서 각도설정 가능.
        {
            transform.Translate(transform.right  * bulletSpeed * Time.deltaTime); //총알의 방향을 오른쪽으로 총알의 속도 * Time.deltaTime 시간만큼 이동시켜줌
        }
        else if(transform.eulerAngles.y == 180) //생성된 오브젝트 트렌스폼의 eulerAngle y값이 180일경우 실행
        {
            transform.Translate(transform.right * -1 * bulletSpeed * Time.deltaTime);//총알의 방향을 오른쪽으로 가야하나 , -1을 곱해줌으로써 오른쪽이 아닌 반대방향으로 총알을 이동시켜주는 코드
        }
        //else if(transform.rotation.z == )
           
    }

    private void OnBecameInvisible() //해당함수는 게임오브젝트가 메인카메라 범위 내에서 나갔을경우 실행시켜주는 코드
    {
        Destroy(gameObject);
    }

    private void Destroybullet() // 해당함수가 불러졌을경우 게임오브젝트를 제거해줌
    {
        Destroy(gameObject);
    }
}
