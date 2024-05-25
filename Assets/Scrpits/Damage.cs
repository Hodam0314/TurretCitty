using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Damage : MonoBehaviour
{
    TMP_Text textmesh; //�������� �߱����� Text
    private float moveSpeed = 50f; //�������� �����Ǿ������ ���� �÷������� ���� �ӵ�
    float damege = 0f; // ������ �����͸� ��Ƴ��� float ������
    Camera maincam; //���� ������ �� ��ũ�� �������� Ȱ���ϱ� ���� ����ī�޶� ����
    Vector3 createWorldPos;//�����Ǿ��� ��� ���� ������

    private void Awake()
    {
        maincam = Camera.main; //maincam �Լ��� Camera�� �ִ� maincam ����� �־���
    }


    private void Start()
    {
        Invoke("dmgdestroy", 1.5f); //�ش� ������Ʈ�� �����Ƿ� 1.5�� �� "dmgdestroy"�Լ��� ���������

    }

    private void Update()
    {
        float x = maincam.WorldToScreenPoint(createWorldPos).x; //float x �� maincam����� Ȱ���� WorldToScreenPoint �� ��ũ������ �������� �������������� �������ִµ�
        //createWorldPos�� x���� �����ؼ� float x�� �־��ش�.

        //transform.Translate(new Vector3(x, moveSpeed * Time.deltaTime, 0));

        transform.position = new Vector3(x, transform.position.y + moveSpeed * Time.deltaTime, 0); //������ �־���  x���� Ȱ���Ͽ� �������� �ߴ°��� ��ũ���� �ƴ� ���� �����ǿ��� �������� ���� �� ���� ���ش�.

        //transform.Translate(new Vector3(mypos.x,mypos.y*moveSpeed *Time.deltaTime,mypos.z));
    }

    private void dmgdestroy() //������ �����ڵ�
    {
        Destroy(gameObject);
    }


    public void SetDamage(float _damege, Vector3 _worldPos) //������ �� �����Ǵ� ��ġ�� �޾ƿ��� �ڵ�
    {
        damege = _damege;
        createWorldPos = _worldPos;
        createWorldPos.z = 0f;
        Damege();
    }


    private void Damege() //�޾ƿ� �������� �ؽ�Ʈȭ ���ִ� �ڵ�
    {
        textmesh = GetComponent<TMP_Text>();
        textmesh.text = $"{(int)damege}";
    }

}
