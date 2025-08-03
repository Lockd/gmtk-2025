using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private Canvas canvas;
    public static TransitionManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void transitionMenu()
    {
        canvas.enabled = true;
        panel.DOAnchorPosX(-2500f, 0f);
        panel.DOAnchorPosX(0, 1f).OnComplete(() =>
       {
           SceneManager.LoadScene("Main Menu");
       });
    }

    public void transitionGame()
    {
        SceneManager.LoadScene("Game"); // TODO I am not sure if this is the right way to do that, but at this point who cares
        MusicManager.instance.SetMusicIntensity(1);
        canvas.enabled = true;
        panel.DOAnchorPosX(0f, 0f);
        panel.DOAnchorPosX(2500f, 1f).OnComplete(() =>
        {
            canvas.enabled = false;
        });
    }
}
