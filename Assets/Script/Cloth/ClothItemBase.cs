using Itens;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public float duration= 600f;
        public string compareTag = "Player";

        public SFXType sfxType;
        public ClothType clothTypeSFX;
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }
        public void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }
        public virtual void Collect() 
        {
            //teste
            PlaySFX();
            ClothManager.Instance.currentCloth = clothType;
            var setup = ClothManager.Instance.GetSetupByType(clothType);
            Player.Instance.ChangeTexture(setup, duration);
            HideObject();
        }
        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }

}
