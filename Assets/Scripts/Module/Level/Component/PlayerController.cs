using System;
using UnityEngine;

namespace WarChess
{
    /// <summary>
    /// A basic player controller in level select scene
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        [HideInInspector] public Animator ani;
        [HideInInspector] public Transform tf;
        
        void Start()
        {
            ani = GetComponent<Animator>();
            tf = transform;
            GameApp.CameraManager.SetPos(tf.position);
        }

        void Update()
        {
            float h = Input.GetAxisRaw("Horizontal");
            if (h == 0)
            {
                ani.Play("idle");
            }
            else
            {
                CheckFlip(h > 0);
                
                Vector3 pos = tf.position + Vector3.right * (h * moveSpeed * Time.deltaTime);
                pos.x = Mathf.Clamp(pos.x, -32, 24); // limit the player's movement range
                tf.position = pos;
                
                GameApp.CameraManager.SetPos(tf.position);
                ani.Play("move");
            }
        }

        public void CheckFlip(bool isRight)
        {
            Vector3 scale = tf.localScale;
            if (isRight != scale.x > 0)
            {
                scale.x *= -1;
                tf.localScale = scale;
            }
        }
    }
}