using UnityEngine;

/// <summary>
/// Timer Utility.
/// </summary>
public class Timer : MonoBehaviour
{
    #region Serialized Fields
    [ReadOnly]
    [SerializeField] private string timerName;

    [ReadOnly]
    [SerializeField] private float timePassed;
    #endregion

    #region Private Fields
    private float timeEnd;

    private bool isEndless;
    private bool isTiming;
    private bool timerFinished;
	#endregion

    #region Public Properties
	public bool TimerFinished { get => timerFinished; }
    public float TimePassed { get => timePassed; }
    #endregion

    /// <summary>
    /// Creates, attaches and initializes this as a component to a game object with a timer duration.
    /// </summary>
    /// <param name="gameObject">Game object to attach the timer to.</param>
    /// <param name="timerName">Name of the timer (to be shown in the inspector).</param>
    /// <param name="timeEnd">How long the timer will run before resetting. Set to -1 to make the timer never resets</param>
    /// <returns></returns>
    public static Timer CreateComponent(GameObject gameObject, string timerName, float timeEnd)
	{
        Timer timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        timer.timerName = timerName;
        timer.timeEnd = timeEnd;
        return timer;
	}

    void Update()
    {
        if (!isTiming)
            return;

        timePassed += Time.deltaTime;
        
        if (timeEnd == -1)
            return;

        if (timePassed > timeEnd)
		{
            timerFinished = true;
            ResetTimer();
        }
    }

    public void StartTimer()
	{
        ResetTimer();

        if (!isTiming)
            isTiming = true;
        
        if (timerFinished)
        timerFinished = false;
    }

    public void ResetTimer()
	{
        if (isTiming)
            isTiming = false;
        timePassed = 0;
    }
}
