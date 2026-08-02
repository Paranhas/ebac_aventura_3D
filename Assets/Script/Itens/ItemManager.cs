using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using RPStudio.Core.Singleton;
using TMPro;

namespace Itens
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
        
    }
    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itensSetups;
        public static Action<ItemType> OnItemCollected;

        private void Start()
        {
            Reset();
            LoadItensFromSave();
        }
        private void LoadItensFromSave() 
        {
            AddByType(ItemType.COIN, (int)SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK, (int)SaveManager.Instance.Setup.health);
        }


        private void Reset()
        {
            foreach (var i in itensSetups) 
            {
                i.soInt.value = 0;
            }
        }
        public ItemSetup GetItemByType(ItemType type)
        {
            return itensSetups.Find(i => i.itemType == type);
        }


        public void AddByType(ItemType itemType,int amount = 1)
        {
            if(amount <= 0) return; 
            itensSetups.Find(i => i.itemType == itemType).soInt.value += amount;
            OnItemCollected?.Invoke(itemType);  // ← adiciona esta linha
        }
        public void RemoveByType(ItemType itemType, int amount = 1) 
        {
          
            var item = itensSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if(item.soInt.value < 0)item.soInt.value = 0;
        }


        [NaughtyAttributes.Button]
        private void AddCoins()
        {
            AddByType(ItemType.COIN);
        }
        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }

    }

    [System.Serializable]
    public class ItemSetup 
    {
        public ItemType itemType;
        public SOInt soInt;
        public Sprite icon;
    }
}
