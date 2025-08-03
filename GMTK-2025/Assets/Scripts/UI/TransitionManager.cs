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

    public void transitionLeftToCenter()
    {
        canvas.enabled = true;
        panel.DOAnchorPosX(-2500f, 0f);
        panel.DOAnchorPosX(0, 0.3f).OnComplete(() =>
       {
           canvas.enabled = false;
       });
    }

    public void transitionCenterToRight()
    {
        SceneManager.LoadScene("Game"); // TODO I am not sure if this is the right way to do that, but at this point who cares
        canvas.enabled = true;
        panel.DOAnchorPosX(0f, 0f);
        panel.DOAnchorPosX(2500f, 0.3f).OnComplete(() =>
        {
            canvas.enabled = false;
        });
    }
}
