using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHp : MonoBehaviour
{
    Camera mainCam;
    Slider slider;
    Turret turret;
    [SerializeField] GameObject Turret;
    private float curHp = 5;
    private float maxHp = 5;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        GetTurret();
        slider.value = curHp / maxHp;
        slider.maxValue = maxHp;
        mainCam = Camera.main;
    }

    private void Update()
    {
        GetTurret();
        GetHp();
        SetHp();
        CheckTurret();
    }

    private void GetTurret()
    {
        if(turret == null)
        {
            turret = GameManager.Instance.GetTurret();
            return;
        }
    }


    private void GetHp()
    {
        if(turret == null && slider == null)
        {
            return;
        }
        (float _cur, float _max) turretHp = turret.GetTurretHp();
        curHp = turretHp._cur;
        maxHp = turretHp._max;

    }

    private void SetHp()
    {
        slider.value = Mathf.Lerp(0, maxHp, curHp / maxHp);
    }

    private void CheckTurret()
    {
        Vector3 pos = mainCam.WorldToScreenPoint(Turret.transform.position);

        transform.position = pos - new Vector3(0, -45f, 0);

    }




}
