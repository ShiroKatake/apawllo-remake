using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeEnd;
    private float time;
    private bool isEndless;
    private bool isTiming;
    private bool timerFinished;

	public bool TimerFinished { get => timerFinished; }
    public float TimePassed { get => time; }
	public static Timer CreateComponent(GameObject gameObject, float timeEnd)
	{
        Timer timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        timer.timeEnd = timeEnd;
        return timer;
	}

    public static Timer CreateComponent(GameObject gameObject, bool isEndless)
    {
        Timer timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        timer.isEndless = isEndless;
        return timer;
    }

    void Update()
    {
        if (isTiming)
		{
            time += Time.deltaTime;
            
            if (isEndless)
                return;

            if (time > timeEnd)
			{
                timerFinished = true;
                ResetTimer();
            }
        }
    }

    public void StartTimer()
	{
        isTiming = true;
    }

    public void ResetTimer()
	{
        isTiming = false;
        time = 0;
	}
}
