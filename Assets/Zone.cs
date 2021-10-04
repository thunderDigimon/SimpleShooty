using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField]
    private Stage[] m_ZoneStages;
    public Stage CurrentStage
    {
        get
        {
            Debug.Log("CurrentStage----> " + CurrentStageIndex);
            if (m_ZoneStages != null && CurrentStageIndex != -1 && CurrentStageIndex < m_ZoneStages.Length)
            {
                return m_ZoneStages[CurrentStageIndex];
            }

            return null;
        }
    }

    public int CurrentStageIndex { get; private set; }

    void Awake()
    {
        CurrentStageIndex = 0;
    }

    public void MoveToNextStage()
    {
        CurrentStageIndex = m_ZoneStages != null && (CurrentStageIndex + 1) < m_ZoneStages.Length ? CurrentStageIndex + 1 : -1;
    }
}
