using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public static ScoreManager Instance;

    public TMP_Text scoreText; // Ссылка на UI-элемент для отображения счёта

    private int leftScore = 0;
    private int rightScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Чтобы объект не исчезал при смене сцен
        }
        else
        {
            Destroy(gameObject); // Уничтожаем лишний экземпляр
        }
    }

    void Start()
    {
        if (scoreText != null) // Проверка, чтобы избежать ошибки если не инициализировано
        {
            UpdateScoreText();
        }
        else
        {
            Debug.LogError("ScoreText is not assigned in the inspector!");
        }
    }

    [PunRPC]
    public void AddScore(bool isLeftGoal)
    {
        if (isLeftGoal)
            rightScore++; // Правый игрок получает очко
        else
            leftScore++;  // Левый игрок получает очко

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null) // Проверка на null, чтобы избежать ошибок в случае неинициализированного компонента
        {
            scoreText.text = $"{leftScore} : {rightScore}";
        }
    }
}
