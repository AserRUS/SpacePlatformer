using UnityEngine;

public class UIComicController : MonoBehaviour
{
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject[] imagesList;

    private int imageIndex;

    private void Start()
    {
        imageIndex = 0;

        foreach (GameObject image in imagesList)
        {
            image.SetActive(false);
        }
        nextButton.SetActive(false);
    }

    public void OpenComic()
    {
        imagesList[imageIndex].SetActive(true);
        nextButton.SetActive(true);
    }

    public void NextButton()
    {
        if (imageIndex + 1 < imagesList.Length)
        {
            imagesList[imageIndex].SetActive(false);
            imageIndex++;
            imagesList[imageIndex].SetActive(true);
        }
        else
        {
            imagesList[imageIndex].SetActive(false);
            nextButton.SetActive(false);
        }
    }
}
