using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeGameUIController : MonoBehaviour
{
    public static SnakeGameUIController Instance;


    public System.Action OnHitPlayBut;


    [SerializeField] Transform UIPanel;
    [SerializeField] Button PlayButton;
    [SerializeField] Text ScoreText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(()=> { OnHitPlayBut.SafeAction(); });
        OnHitPlayBut += OnHitPlay;
    }
    private void OnDestroy()
    {
        OnHitPlayBut -= OnHitPlay;
    }

    public void OnHitPlay()
    {
        UIPanel.gameObject.SetActive(false);
    }
    public void UpdateScore(int score)
    {
        LeanTween.scale(ScoreText.rectTransform, Vector3.one, 0.3f).setEaseInOutBounce() ;

        ScoreText.text = "Score : " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
