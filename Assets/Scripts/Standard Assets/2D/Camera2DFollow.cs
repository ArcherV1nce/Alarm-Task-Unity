using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        private float _offsetZ;
        private Vector3 _lastTargetPosition;
        private Vector3 _currentVelocity;
        private Vector3 _lookAheadPos;

        public Transform Target;
        public float Damping = 1;
        public float LookAheadFactor = 3;
        public float LookAheadReturnSpeed = 0.5f;
        public float LookAheadMoveThreshold = 0.1f;

        private void Start()
        {
            _lastTargetPosition = Target.position;
            _offsetZ = (transform.position - Target.position).z;
            transform.parent = null;
        }


        private void Update()
        {
            float xMoveDelta = (Target.position - _lastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > LookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                _lookAheadPos = LookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                _lookAheadPos = Vector3.MoveTowards(_lookAheadPos, Vector3.zero, Time.deltaTime*LookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = Target.position + _lookAheadPos + Vector3.forward*_offsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref _currentVelocity, Damping);

            transform.position = newPos;

            _lastTargetPosition = Target.position;
        }
    }
}
