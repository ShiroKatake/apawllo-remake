using UnityEngine;

/// <summary>
/// Timer Utility.
/// </summary>
public class Timer : MonoBehaviour
{
    #region Serialized Fields
    [ReadOnly]
    [SerializeField] private string timerName;
	#endregion

	#region Private Fields
	private float timeEnd;
    private float time;
    private bool isEndless;
    private bool isTiming;
    private bool timerFinished;
	#endregion

    #region Public Properties
	public bool TimerFinished { get => timerFinished; }
    public float TimePassed { get => time; }
    #endregion

    /// <summary>
    /// Creates, attaches and initializes this as a component to a game object with a timer duration.
    /// </summary>
    /// <param name="gameObject">Game object to attach to as component.</param>
    /// <param name="timeEnd">How long to time for.</param>
    /// <returns></returns>
    public static Timer CreateComponent(GameObject gameObject, string timerName, float timeEnd)
	{
        Timer timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        timer.timerName = timerName;
        timer.timeEnd = timeEnd;
        return timer;
	}

    /// <summary>
    /// Creates, attaches and initializes this as a component to a game object as an endless timer.
    /// </summary>
    /// <param name="gameObject">Game object to attach to as component.</param>
    /// <param name="isEndless">Whether this timer is endless.</param>
    /// <returns></returns>
    public static Timer CreateComponent(GameObject gameObject, string timerName, bool isEndless)
    {
        Timer timer = gameObject.AddComponent(typeof(Timer)) as Timer;
        timer.timerName = timerName;
        timer.isEndless = isEndless;
        return timer;
    }

    /// <summary>
    /// Handles timing.
    /// </summary>
    void Update()
    {
        if (!isTiming)
            return;

        time += Time.deltaTime;
            
        if (isEndless)
            return;

        if (time > timeEnd)
		{
            timerFinished = true;
            ResetTimer();
        }
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    public void StartTimer()
	{
        ResetTimer();

        if (!isTiming)
            isTiming = true;
        
        if (timerFinished)
        timerFinished = false;
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void ResetTimer()
	{
        if (isTiming)
            isTiming = false;
        time = 0;
    }
}
