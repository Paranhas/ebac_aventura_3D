using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPStudio.Core.Singleton;


namespace Cloth
{
    public enum ClothType
    {
        SPEED,
        STRONG,
        FIRE
    }
    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetup;
        //teste
        public ClothType currentCloth = ClothType.SPEED;

        public ClothSetup GetSetupByType(ClothType clothType)
        {
            return clothSetup.Find(i => i.clothType == clothType);
        }
    }
    [System.Serializable]
    public class ClothSetup
    {
        public ClothType clothType;
        public Texture2D texture;
    }
}
