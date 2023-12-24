using UnityEngine;

namespace WarChess
{
    public class CameraManager
    {
        protected Transform camTf;
        
        /// <summary>
        /// Previous pos
        /// </summary>
        protected Vector3 prePos;
        
        public CameraManager()
        {
            camTf = Camera.main.transform;
            prePos = camTf.position;
        }
        
        public void SetPos(Vector3 pos)
        {
            pos.z = camTf.position.z;
            camTf.position = pos;
        }
        
        public void ResetPos()
        {
            camTf.position = prePos;
        }
    }
}