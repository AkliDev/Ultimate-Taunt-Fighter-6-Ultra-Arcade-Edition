using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    [SerializeField] Character m_Characters1, m_Characters2;
    [SerializeField] PhysicsWorld m_PhysicsWorld;
    [SerializeField] Stage m_Stage;
    public Stage Stage { get { return m_Stage; } }
    int m_Score1, m_Score2;
    [SerializeField] TMP_Text m_Score1Text, m_Score2Text;
    [SerializeField] int m_Rounds = 3;
    float m_ResetWaitSeconds = 1;
    float m_StartingPosition = 40;

    [SerializeField] GameObject m_WinPanel;
    [SerializeField]TMP_Text m_WinText;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        m_Characters1.transform.position = new Vector2(-m_StartingPosition, 0);
        m_Characters2.transform.position = new Vector2(m_StartingPosition, 0);
    }

    private void Update()
    {
        m_Characters1.UpdateCharacter();
        m_Characters2.UpdateCharacter();

        m_PhysicsWorld.UpdateWorld();
    }

    public void AddScore(Character scorer)
    {
        if (scorer == m_Characters1)
        {
            m_Score1++;
            m_Score1Text.text = m_Score1.ToString();
            if (m_Score1 == m_Rounds)
            {
                win(m_Characters1.name);
                return;
            }
        }

        if (scorer == m_Characters2)
        {
            m_Score2++;
            m_Score2Text.text = m_Score2.ToString();

            if (m_Score2 == m_Rounds)
            {
                win(m_Characters2.name);
                return;
            }
        }

        StartCoroutine("WaitForReset");
    }

    void ResetRound()
    {
        m_Characters1.SwitchState(m_Characters1.StateDictionary[CharacterStates.Idle]);
        m_Characters2.SwitchState(m_Characters1.StateDictionary[CharacterStates.Idle]);
        m_Characters1.transform.position = new Vector2(-m_StartingPosition,0);
        m_Characters2.transform.position = new Vector2(m_StartingPosition, 0);
    }

    void win(string winner)
    {
        m_WinText.text = winner + "wins";
        m_WinPanel.SetActive(true);
    }

    IEnumerator WaitForReset()
    {
        int time = 0;

        while (time < 60 * m_ResetWaitSeconds)
        {
            time++;
            yield return new WaitForEndOfFrame();
        }

        ResetRound();

    }
}
