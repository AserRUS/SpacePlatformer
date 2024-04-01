using UnityEngine;

public class EndGameComic : MonoBehaviour
{
    private UIComicController comicController;

    private void Start()
    {
        comicController = GetComponent<UIComicController>();
        LevelProgress.OnLevelFinished += OpenEndGameComic;
    }

    private void OpenEndGameComic()
    {
        comicController.OpenComic();
    }
}
