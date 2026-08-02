using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Animation;
using System;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        //nas aulas esse colliderDamage é collider
        public Collider colliderDamage;
        public FlashColor flashColor;
        public ParticleSystem _particleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;

        public static Action<EnemyBase> OnEnemyKilled;

        [SerializeField]public float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool starWithBornAnimation = true;

        [Header("Events")]
        public UnityEvent OnKillEvent;


        private Player _player;
        private void Awake()
        {
            Init();
        }
        public void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }
        protected void ResetLife() 
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if(starWithBornAnimation)
            BornAnimation();
        }
        protected void Kill()
        {
            OnKill();
        }
        protected virtual void OnKill()
        {
            if (colliderDamage != null)
                colliderDamage.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();
        }

        public void OnDamage(float f)
        {
            if(flashColor != null)flashColor.Flash();
            if (_particleSystem != null) _particleSystem.Emit(7);
            transform.position -= transform.forward;
            _currentLife -= f;
            if(_currentLife <= 0) 
            {
                Kill();
            }
        }
        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        public void PlayAnimationByTrigger(AnimationType animationType)
        {
                _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion
        public void Damage(float damage)
        {
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }
        private void OnCollisionEnter(Collision collision)
        {
            Player p =  collision .transform.GetComponent<Player>();
            if(p != null) 
            {
                p.healthBase.Damage(1);
            }
        }
        public virtual void Update()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }

    }
}

