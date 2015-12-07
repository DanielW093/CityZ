using System;
using UnityEngine;
using UnityEngine.Networking;


 	[Serializable]
    public class MouseLook : NetworkBehaviour
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;

        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;

		private Transform charTransform;
		private Transform camTransform;
		
		float xRot;
		float yRot;

		void Start()
		{
			charTransform = gameObject.transform;
			camTransform = GetComponentInChildren<Camera>().transform;

			m_CharacterTargetRot = charTransform.localRotation;
			m_CameraTargetRot = camTransform.localRotation;
		}	

		void Update()
		{
			if(isLocalPlayer)
			{
				xRot = Input.GetAxis("Mouse Y") * YSensitivity;
				yRot = Input.GetAxis("Mouse X") * XSensitivity; 

				LookRotation ();
				CmdGunRotation();
			}
		}

        public void LookRotation()
        {
            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
           
            if(smooth)
            {
				charTransform.localRotation = Quaternion.Slerp (charTransform.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
				charTransform.localRotation = m_CharacterTargetRot;
            }
        }

		public void CmdGunRotation()
		{
			m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

			if(clampVerticalRotation)
				m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

			if(smooth)
			{
				camTransform.localRotation = Quaternion.Slerp (camTransform.localRotation, m_CameraTargetRot,
	                                               smoothTime * Time.deltaTime);
			}
			else
			{
				camTransform.localRotation = m_CameraTargetRot;
			}
		}

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
