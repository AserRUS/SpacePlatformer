using UnityEngine;

public class EndGameComic : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private UIComicController comicController;

    private void Start()
    {
        comicController = GetComponent<UIComicController>();
        LevelProgress.OnLevelFinished += OpenEndGameComic;
    }

    private void OpenEndGameComic()
    {
        comicController.OpenComic();
        MusicPlayer.Instance.SetLevelMusic(music);
    }
}
