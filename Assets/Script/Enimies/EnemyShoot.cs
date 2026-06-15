using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();
        }

        private void OnEnable()
        {
            if (gunBase != null)
                gunBase.StartShoot();
        }

        private void OnDisable()
        {
            if (gunBase != null)
                gunBase.StopShoot();
        }
        protected override void OnKill()
        {
            gunBase?.StopShoot();

            base.OnKill();
        }

    }
}
