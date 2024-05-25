using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    Player player;
    [SerializeField] private float bulletSpeed; // �Ѿ� ���ǵ�
    [SerializeField] private float damage = 0.0f; // �Ѿ� ������
    private bool playerBullet = false; //�÷��̾ �� �Ѿ����� üũ

    private void Start()
    {
        Invoke("Destroybullet", 2); //�Ѿ��� �����ǰ� 2�ʵڿ� "Destroybullet"��ũ��Ʈ ����
    }

    private void Awake()
    {
        player = GameManager.Instance.GetPlayer(); //���ӸŴ����� ��ϵ� �÷��̾� �����͸� ������
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GameTag.Enemy.ToString()) //���� collision�� ���� ���ӿ�����Ʈ�� Enemy �����±׸� ����������� ����
        {
            Enemy enemySc = collision.GetComponent<Enemy>(); //���� ��ũ��Ʈ ������ ������
            Destroy(gameObject); //�Ѿ��� ������
            enemySc.checkBullet(); //�� ��ũ��Ʈ ���� checkBullet �Լ� �ڵ带 ���������
            enemySc.Hit(damage); //enemySc�� Hit�� �Ѿ� ���������� ���� �� Hit �Լ��� ���������
        }
    }

    void Update()
    {
        moving();
    }

    private void moving()
    {
        if(transform.eulerAngles.y == 0) //x y z = eulerAngles�� �̿��ؼ� �������� ����.
        {
            transform.Translate(transform.right  * bulletSpeed * Time.deltaTime); //�Ѿ��� ������ ���������� �Ѿ��� �ӵ� * Time.deltaTime �ð���ŭ �̵�������
        }
        else if(transform.eulerAngles.y == 180) //������ ������Ʈ Ʈ�������� eulerAngle y���� 180�ϰ�� ����
        {
            transform.Translate(transform.right * -1 * bulletSpeed * Time.deltaTime);//�Ѿ��� ������ ���������� �����ϳ� , -1�� ���������ν� �������� �ƴ� �ݴ�������� �Ѿ��� �̵������ִ� �ڵ�
        }
        //else if(transform.rotation.z == )
           
    }

    private void OnBecameInvisible() //�ش��Լ��� ���ӿ�����Ʈ�� ����ī�޶� ���� ������ ��������� ��������ִ� �ڵ�
    {
        Destroy(gameObject);
    }

    private void Destroybullet() // �ش��Լ��� �ҷ�������� ���ӿ�����Ʈ�� ��������
    {
        Destroy(gameObject);
    }
}
