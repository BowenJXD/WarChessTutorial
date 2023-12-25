using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace WarChess
{
    /// <summary>
    /// Battle unit base class
    /// </summary>
    public class ModelBase : MonoBehaviour
    {
        /// <summary>
        /// Object id
        /// </summary>
        public int id;
        
        /// <summary>
        /// Config data
        /// </summary>
        public Dictionary<string, string> data;

        public int step;

        public int attack;

        public EModelType type;
        
        public int maxHp;

        public int curHp;

        public int rowIndex;

        public int colIndex;

        [HideInInspector] public SpriteRenderer bodySp;
        
        /// <summary>
        /// target object when stopped moving
        /// </summary>
        [HideInInspector] public GameObject stopObj;
        
        [HideInInspector] public Transform tf;

        /// <summary>
        /// Is the object stopped moving
        /// </summary>
        protected bool isStop;
        
        public bool IsStop
        {
            get => isStop;
            set
            {
                stopObj.SetActive(isStop);
                bodySp.color = value ? Color.gray : Color.white;
                isStop = value;
            }
        }

        [HideInInspector] public Animator ani;

        void Awake()
        {
            tf = transform;
            bodySp = tf.Find("body").GetComponent<SpriteRenderer>();
            stopObj = tf.Find("stop").gameObject;
            ani = tf.Find("body").GetComponent<Animator>();
        }

        protected virtual void OnEnable()
        {
            AddEvents();
        }

        protected virtual void AddEvents()
        {
            GameApp.MessageCenter.AddEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
            GameApp.MessageCenter.AddEvent(Defines.OnUnselectEvent, OnUnselectCallback);
        }

        protected virtual void OnDisable()
        {
            RemoveEvents();
        }

        protected virtual void RemoveEvents()
        {
            GameApp.MessageCenter.RemoveEvent(gameObject, Defines.OnSelectEvent, OnSelectCallback);
            GameApp.MessageCenter.RemoveEvent(Defines.OnUnselectEvent, OnUnselectCallback);
        }

        protected virtual void OnSelectCallback(object arg)
        {
            GameApp.MessageCenter.PostEvent(Defines.OnUnselectEvent);
            
            GameApp.MapManager.ShowStepGrid(this, step, Color.green);
        }
        
        protected virtual void OnUnselectCallback(object arg)
        {
            bodySp.color = IsStop ? Color.gray : Color.white;
            
            GameApp.MapManager.HideStepGrid(this, step);
        }

        public void TryFlip(Vector3 pos)
        {
            if (pos.x > tf.position.x && tf.localScale.x > 0)
            {
                Flip();
            }
            else if (pos.x < tf.position.x && tf.localScale.x < 0)
            {
                Flip();
            }
        }
        
        public void Flip()
        {
            Vector3 scale = tf.localScale;
            scale.x *= -1;
            tf.localScale = scale;
        }

        /// <summary>
        /// Move to target position
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="targetCol"></param>
        /// <param name="dt"></param>
        public virtual bool Move(int targetRow, int targetCol, float dt)
        {
            Vector3 pos = GameApp.MapManager.GetBlockPos(targetRow, targetCol);
            pos.z = tf.position.z;
            
            TryFlip(pos);

            if (Vector3.Distance(transform.position, pos) <= 0.02f)
            {
                rowIndex = targetRow;
                colIndex = targetCol;
                tf.position = pos;
                return true;
            }
            
            tf.position = Vector3.MoveTowards(tf.position, pos, dt);
            return false;
        }
        
        /// <summary>
        /// Play animation
        /// </summary>
        /// <param name="aniName"></param>
        public void PlayAni(string aniName)
        {
            ani.Play(aniName);
        }

        /// <summary>
        /// Take damage
        /// </summary>
        /// <param name="skillUser"></param>
        public virtual void GetHit(ISkillUser skillUser)
        {
            
        }

        /// <summary>
        /// Play effect (effect object)
        /// </summary>
        /// <param name="effectName"></param>
        public virtual void PlayEffect(string effectName)
        {
            GameObject obj = Instantiate(Resources.Load($"Effect/{name}")) as GameObject;
            obj.transform.position = tf.position;
            
            DestroyObj destroyObj = obj.AddComponent<DestroyObj>();
            Animator objAni = obj.GetComponent<Animator>();
            if (objAni)
            {
                destroyObj.duration = Mathf.Max(objAni.GetCurrentAnimatorStateInfo(0).length, destroyObj.duration);
            }
            ParticleSystem objPs = obj.GetComponent<ParticleSystem>();
            if (objPs)
            {
                destroyObj.duration = Mathf.Max(objPs.main.duration, destroyObj.duration);
            }
            
            destroyObj.StartDestroy();
        }

        /// <summary>
        /// Calculate manhattan distance between two objects
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public float GetDis(ModelBase model)
        {
            return Mathf.Abs(model.rowIndex - rowIndex) + Mathf.Abs(model.colIndex - colIndex);
        }

        public void PlaySound(string soundName)
        {
            GameApp.SoundManager.PlayEffect(soundName, tf.position);
        }

        public void LookAtModel(ModelBase model)
        {
            TryFlip(model.tf.position);
        }
    }
}