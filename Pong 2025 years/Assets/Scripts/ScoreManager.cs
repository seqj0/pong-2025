using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public static ScoreManager Instance;

    public TMP_Text scoreText; // ������ �� UI-������� ��� ����������� �����

    private int leftScore = 0;
    private int rightScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ����� ������ �� ������� ��� ����� ����
        }
        else
        {
            Destroy(gameObject); // ���������� ������ ���������
        }
    }

    void Start()
    {
        if (scoreText != null) // ��������, ����� �������� ������ ���� �� ����������������
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
            rightScore++; // ������ ����� �������� ����
        else
            leftScore++;  // ����� ����� �������� ����

        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null) // �������� �� null, ����� �������� ������ � ������ ��������������������� ����������
        {
            scoreText.text = $"{leftScore} : {rightScore}";
        }
    }
}
