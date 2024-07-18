using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Transform sunlight;
    [SerializeField] private int startHour;
    [SerializeField] private int endHour;
    [SerializeField] private float cycleRate;
    [SerializeField] private TextMeshProUGUI timeText;
    private int hours;
    private float minutes;
    private float cycle;
    // Start is called before the first frame update
    private void Start()
    {
        Setup();
    }

    // Update is called once per frame
    private void Update()
    {
        minutes += Time.deltaTime * cycleRate;
        cycle += Time.deltaTime * cycleRate;
        timeText.text = String.Format("{0:00}:{1:00}", hours, (int) minutes);

        if (minutes > 59)
        {
            hours++;
            minutes = 0;
        }

        if (hours >= endHour)
        {
            Setup();
        }
        SunCycle(cycle/1440);

    }

    private void Setup() {
        hours = startHour;
        minutes = 0;
        cycle = startHour * 60;
        SunCycle(cycle/1440);
    }

    private void SunCycle(float timePercent) {
        float newRotationX = (timePercent * 360) - 90;
        sunlight.rotation = Quaternion.Euler(newRotationX, sunlight.rotation.eulerAngles.y, sunlight.rotation.eulerAngles.z);
    }
}
