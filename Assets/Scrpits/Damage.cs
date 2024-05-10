using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Damage : MonoBehaviour
{
    TMP_Text textmesh;
    private float moveSpeed = 50f;
    float damege = 0f;
    Camera maincam;
    Vector3 mypos;

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
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        //transform.Translate(new Vector3(mypos.x,mypos.y*moveSpeed *Time.deltaTime,mypos.z));
    }

    private void dmgdestroy()
    {
        Destroy(gameObject);
    }


    public void SetDamage(float _damege)
    {
        damege = _damege;
        Damege();
    }

    public void SetPos(Vector3 Pos)
    {
        mypos = Pos;
    }

    private void Damege()
    {
        textmesh = GetComponent<TMP_Text>();
        textmesh.text = $"{(int)damege}";
    }

}
