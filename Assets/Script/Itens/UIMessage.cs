using UnityEngine;
using TMPro;
using Itens;

    public class UIMessage : MonoBehaviour
    {
        public TextMeshProUGUI messageText;
        public float displayDuration = 2f;

        private void Start()
        {
            ItemManager.OnItemCollected += ShowCollectMessage;
            messageText.gameObject.SetActive(false);
        }

        private void ShowCollectMessage(ItemType type)
        {
            if (type == ItemType.LIFE_PACK)
            {
                messageText.text = "+1 Life Pack! Aperte o L para usar";
                messageText.gameObject.SetActive(true);
                CancelInvoke(nameof(HideMessage));
                Invoke(nameof(HideMessage), displayDuration);
            }
        }

        private void HideMessage()
        {
            messageText.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (ItemManager.Instance != null)
                ItemManager.OnItemCollected -= ShowCollectMessage;
        }
    }
