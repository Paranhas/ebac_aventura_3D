using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public TextMeshProUGUI checkpointMessageText;
    public int key = 01;
    public string checkpointKey = "checkpointKey";
    private bool checkpointActived = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!checkpointActived && other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
        
    }
    private void CheckCheckpoint()
    {
        TurnItOn();
        SaveCheckpoint();
    }
    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        if (checkpointMessageText != null)
        {
            checkpointMessageText.text = "Checkpoint Ativado!";
            Invoke("ClearMessage", 3f);
        }
    }
    private void ClearMessage()
    {
        TextMeshProUGUI uiText = FindObjectOfType<TextMeshProUGUI>();
        if (uiText != null)
            uiText.text = "";
    }
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }
    private void SaveCheckpoint()
    {
      /*  if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
        {
            PlayerPrefs.SetInt(checkpointKey, key);
        }*/
        CheckpointManager.Instance.SaveCheckPoint(key);
        checkpointActived = true;
    }

}
