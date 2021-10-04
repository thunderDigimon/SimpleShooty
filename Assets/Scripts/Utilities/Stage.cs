using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject m_StageExitGate;

    public bool GoalCompleted { get; private set; }

    void Awake()
    {
        m_StageExitGate.SetActive(true);
        GoalCompleted = false;
    }

    public void OnStageCompleted()
    {
        m_StageExitGate.SetActive(false);
        GoalCompleted = true;
    }
}
