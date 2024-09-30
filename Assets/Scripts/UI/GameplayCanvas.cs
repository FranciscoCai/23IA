using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField]
    private CinemachineCamera CinemachineCamera;
    [SerializeField]
    private TMP_Dropdown DropDownElement;

    private List<GameObject> m_NPCs;

    private void Start()
    {
        m_NPCs = new List<GameObject>();
        m_NPCs.AddRange(GameObject.FindGameObjectsWithTag("Thief"));
        m_NPCs.AddRange(GameObject.FindGameObjectsWithTag("Worker"));
        m_NPCs.AddRange(GameObject.FindGameObjectsWithTag("Guard"));
        DropDownElement.onValueChanged.AddListener(OnDropDownValueChanged);
        DropDownElement.ClearOptions();
        List<TMP_Dropdown.OptionData> optionsList = new List<TMP_Dropdown.OptionData>();
        foreach (GameObject npc in m_NPCs)
        {
            optionsList.Add(new TMP_Dropdown.OptionData(npc.name));
        }

        DropDownElement.AddOptions(optionsList);
        OnDropDownValueChanged(0);
    }

    public void OnDropDownValueChanged(int value)
    {
        if ((0 <= value) && (value < m_NPCs.Count))
        {
            CinemachineCamera.Follow = m_NPCs[value].transform;
        }
    }
}
