using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public static Timer instance;

    [SerializeField] Text timerText;

    [SerializeField] Text addTimeText;

    public float timeLeft = 30.0f;

    [SerializeField] GameObject loseScreen;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip gainSound;

    public bool shouldCountdown = true;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        addTimeText.gameObject.SetActive(false);

        SetTimerText(timeLeft);
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        while (timeLeft > 0.0f && shouldCountdown)
        {
            yield return new WaitForSeconds(1.0f);
            timeLeft -= 1.0f;
            SetTimerText(timeLeft);

        }
        if (timeLeft <= 0.0f)
        {
            Lose();
        }
        Debug.Log("Time up");
    }

    public void SetTimerText(float timeLeft)
    {
        timerText.text = timeLeft.ToString() + "s";
    }

    public void AddTime(float time)
    {
        timeLeft += time;
        SetTimerText(timeLeft);

        StartCoroutine(AddTimeCoroutine(time));
    }

    IEnumerator AddTimeCoroutine(float time)
    {
        addTimeText.gameObject.SetActive(true);
        addTimeText.text = "+" + time.ToString() + "s";
        audioSource.PlayOneShot(gainSound);
        yield return new WaitForSeconds(1f);
        addTimeText.gameObject.SetActive(false);

    }

    public void Lose()
    {
        shouldCountdown = false;
        StartCoroutine(LoseCoroutine());
    }

    IEnumerator LoseCoroutine()
    {
        loseScreen.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Level1");
    }
}
