using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{

    public static ProgressController instance;

    Image image;
    [SerializeField] Sprite[] sprites;

    public int currentIndex = 0;

    private void Awake()
    {
        instance = this;

        image = GetComponent<Image>();

    }
    // Start is called before the first frame update
    void Start()
    {
        SetProgress(currentIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetProgress(int progress)
    {
        image.sprite = sprites[progress];
    }

    public void IncreaseProgress()
    {
        currentIndex++;
        SetProgress(currentIndex);
    }

    public void DecreaseProgress()
    {
        currentIndex--;
        SetProgress(currentIndex);
    }

    public void CompleteProgress()
    {
        currentIndex = sprites.Length - 1;
        SetProgress(currentIndex);
    }


}
