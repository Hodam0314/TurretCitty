using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Damage : MonoBehaviour
{
    TMP_Text textmesh; //데미지가 뜨기위한 Text
    private float moveSpeed = 50f; //데미지가 생성되었을경우 위로 올려보내기 위한 속도
    float damege = 0f; // 데미지 데이터를 담아놓을 float 데이터
    Camera maincam; //월드 포지션 및 스크린 포지션을 활용하기 위한 메인카메라 선언
    Vector3 createWorldPos;//생성되었을 당시 월드 포지션

    private void Awake()
    {
        maincam = Camera.main; //maincam 함수에 Camera에 있는 maincam 기능을 넣어줌
    }


    private void Start()
    {
        Invoke("dmgdestroy", 1.5f); //해당 오브젝트가 생성되로 1.5초 뒤 "dmgdestroy"함수를 실행시켜줌

    }

    private void Update()
    {
        float x = maincam.WorldToScreenPoint(createWorldPos).x; //float x 에 maincam기능을 활용한 WorldToScreenPoint 즉 스크린상의 포지션을 월드포지션으로 변경해주는데
        //createWorldPos의 x값을 변경해서 float x에 넣어준다.

        //transform.Translate(new Vector3(x, moveSpeed * Time.deltaTime, 0));

        transform.position = new Vector3(x, transform.position.y + moveSpeed * Time.deltaTime, 0); //위에서 넣어준  x값을 활용하여 데미지가 뜨는곳을 스크린이 아닌 월드 포지션에서 나오도록 설정 및 생성 해준다.

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
