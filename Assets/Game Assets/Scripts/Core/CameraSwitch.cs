using UnityEngine;

namespace Hertzole.Github2017
{
    [RequireComponent(typeof(BoxCollider))]
    public class CameraSwitch : MonoBehaviour
    {
        [SerializeField]
        private Vector3 m_SwitchToPosition;
        public Vector3 SwitchToPosition { get { return m_SwitchToPosition; } set { m_SwitchToPosition = value; } }
        [SerializeField]
        private Vector3 m_SwitchToRotation;
        public Vector3 SwitchToRotation { get { return m_SwitchToRotation; } set { m_SwitchToRotation = value; } }

        private BoxCollider m_BoxCol;
        public BoxCollider BoxCol { get { if (!m_BoxCol) m_BoxCol = GetComponent<BoxCollider>(); return m_BoxCol; } }

        private Transform m_CameraTransform;

        private void Start()
        {
            BoxCol.isTrigger = true;
            m_CameraTransform = Camera.main.transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                m_CameraTransform.position = SwitchToPosition;
                m_CameraTransform.eulerAngles = SwitchToRotation;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Gizmos.DrawCube(transform.position + BoxCol.center, BoxCol.size);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + BoxCol.center, BoxCol.size);
        }
    }
}
