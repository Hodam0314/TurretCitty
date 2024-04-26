using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{

  [Header("터렛 설정")]
  [SerializeField] GameObject bullet;
  [SerializeField] float turretHp = 5f;
  [SerializeField] GameObject explosion;
  [SerializeField] Transform layerDynamic;
  [SerializeField] float scanRange;
  [SerializeField] float attackCoolTime = 2;
  [SerializeField] Transform shootPos;
  public LayerMask targetLayer;
  public RaycastHit2D[] targets;
  public Transform closetarget;
  private float attacktime;
  Vector3 passDir;

  Rigidbody2D rigid;
  SpriteRenderer spr;

  private void Awake()
  {
    rigid = GetComponent<Rigidbody2D>();
    spr = GetComponent<SpriteRenderer>();
  }

  private void Start()
  {
    GameManager.Instance.SetTurret(this);
    closetarget = Getclosetarget();
  }

  private void Update()
  {
    closetarget = Getclosetarget();
    Attack();
    checkEnemy();
  }

  Transform Getclosetarget()
  {
    targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
    Transform result = null;
    Vector3 myPos = transform.position;
    float diff = 100; //탐색하는 적 거리
    int count = targets.Length;
    for (int i = 0; i < count; i++)
    {
      Vector3 targetPos = targets[i].transform.position;
      float direction = Vector3.Distance(myPos, targetPos);

      if (direction < diff)
      {
        diff = direction;
        result = targets[i].transform;
      }

    }
    return result;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == GameTag.Enemy.ToString())
    {
      Hit(1f);
    }
  }

  private void Hit(float _damage)
  {
    turretHp -= _damage;
    if (turretHp <= 0f)
    {
      Destroy(gameObject);
      GameObject obj = Instantiate(explosion, transform.position, Quaternion.identity, layerDynamic);
      Explosion objSc = obj.GetComponent<Explosion>();
      float sizeWidth = spr.sprite.rect.width;
      objSc.SetAnimationSize(sizeWidth);
    }
  }

  private void Attack()
  {
    if (closetarget == null)
    {
      return;
    }

    attacktime += Time.deltaTime;
    if (attacktime > attackCoolTime)
    {
      attacktime = 0f;
      Vector3 enemyPos = closetarget.transform.position;
      Vector3 dir = enemyPos - transform.position;

      float angle = Quaternion.FromToRotation(Vector3.right, dir).eulerAngles.z;
      Vector3 eulerAngle = new Vector3(0, 0, angle);

      createBullet(bullet, shootPos.position, new Vector3(0f, 0f, angle));
    }
  }

  private void createBullet(GameObject _obj, Vector3 _pos, Vector3 _rot)
  {
    GameObject obj = Instantiate(_obj, _pos, Quaternion.Euler(_rot), layerDynamic);
  }

  private void checkEnemy()
  {
    if (closetarget == null)
    {
      return;
    }
    Vector3 enemyPos = closetarget.transform.position;
    Vector3 dir = enemyPos - transform.position;

    float angle = Quaternion.FromToRotation(Vector3.right, dir).eulerAngles.z;
    Vector3 eulerAngle = new Vector3(0, 0, angle);
    transform.eulerAngles = eulerAngle;
  }

  //private void passdir()
  //{
  //  Vector3 enemyPos = closetarget.transform.position;
  //  Vector3 dir = enemyPos - transform.position;

  //  float angle = Quaternion.FromToRotation(Vector3.right, dir).eulerAngles.z;
  //  Vector3 eulerAngle = new Vector3(0, 0, angle);
  //  transform.eulerAngles = eulerAngle;

  //  TurretBullet bulletsc = bullet.GetComponent<TurretBullet>();
  //  bulletsc.GetPos(passDir);
  //}

}
