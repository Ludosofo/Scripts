using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using System;
// using System.DateTime;

public class TimeDevCounter : MonoBehaviour
{
    private TMP_Text TMPro;
    private int days = 0;
    // public DateTime initDev = new DateTime(2022, 11, 26);
    public DateTime initDev = new DateTime(2022, 12, 03);

    // Start is called before the first frame update
    void Awake()
    {
        TMPro = GetComponent<TMP_Text>();
        TMPro.text = "Dia de desarrollo: "+CalcDaysSince()+"!!!";
    }

    int CalcDaysSince(){
        return (int)(DateTime.Now - initDev).TotalDays;
    }
}
