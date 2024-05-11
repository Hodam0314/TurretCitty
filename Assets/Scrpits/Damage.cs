using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Damage : MonoBehaviour
{
    TMP_Text textmesh;
    private float moveSpeed = 50f;
    float damege = 0f;
    Camera maincam;
    Vector3 createWorldPos;//생성되었을 당시 월드 포지션

    private void Awake()
    {
        maincam = Camera.main;
    }


    private void Start()
    {
        Invoke("dmgdestroy", 1.5f);

    }

    private void Update()
    {
        float x = maincam.WorldToScreenPoint(createWorldPos).x;

        //transform.Translate(new Vector3(x, moveSpeed * Time.deltaTime, 0));

        transform.position = new Vector3(x, transform.position.y + moveSpeed * Time.deltaTime, 0);

        //transform.Translate(new Vector3(mypos.x,mypos.y*moveSpeed *Time.deltaTime,mypos.z));
    }

    private void dmgdestroy() //데미지 삭제코드
    {
        Destroy(gameObject);
    }


    public void SetDamage(float _damege, Vector3 _worldPos) //데미지 및 생성되는 위치를 받아오는 코드
    {
        damege = _damege;
        createWorldPos = _worldPos;
        createWorldPos.z = 0f;
        Damege();
    }


    private void Damege() //받아온 데미지를 텍스트화 해주는 코드
    {
        textmesh = GetComponent<TMP_Text>();
        textmesh.text = $"{(int)damege}";
    }

}
