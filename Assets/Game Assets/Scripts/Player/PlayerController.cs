using UnityEngine;

namespace Hertzole.Github2017
{
    [RequireComponent(typeof(CharacterController), typeof(AudioSource))]
    public class PlayerController : MonoBehaviour, IUpdate
    {
        [Header("Movement")]
        [SerializeField]
        private float m_WalkSpeed = 2;
        public float WalkSpeed { get { return m_WalkSpeed; } set { m_WalkSpeed = value; } }
        [SerializeField]
        private float m_RunSpeed = 4;
        public float RunSpeed { get { return m_RunSpeed; } set { m_RunSpeed = value; } }
        [SerializeField]
        private float m_RotateSpeed = 90;
        public float RotateSpeed { get { return m_RotateSpeed; } set { m_RotateSpeed = value; } }

        [Header("Audio")]
        [SerializeField]
        private float m_StepTime = 0.8f;
        public float StepTime { get { return m_StepTime; } set { m_StepTime = value; } }
        [SerializeField]
        private AudioClip[] m_Footsteps;
        public AudioClip[] Footsteps { get { return m_Footsteps; } set { m_Footsteps = value; } }

        private bool m_IsRunning = false;

        private float m_StepCycle;
        private float m_NextStepTime = 0.5f;

        private Vector2 m_Input;

        private Vector3 m_MoveDirection;

        private CharacterController m_Controller;
        public CharacterController Controller { get { if (!m_Controller) m_Controller = GetComponent<CharacterController>(); return m_Controller; } }
        private AudioSource m_Audio;
        public AudioSource Audio { get { if (!m_Audio) m_Audio = GetComponent<AudioSource>(); return m_Audio; } }

        private void Start()
        {
            transform.position = transform.position + new Vector3(0, 0.08f, 0);
        }

        private void OnEnable()
        {
            UpdateManager.AddUpdate(this);
        }

        private void OnDisable()
        {
            UpdateManager.RemoveUpdate(this);
        }

        public void GameUpdate()
        {
            m_Input = GetInput();
            m_IsRunning = Input.GetKey(KeyCode.LeftShift);

            if (m_Input.x != 0)
            {
                transform.Rotate(Vector3.up * m_Input.x * m_RotateSpeed * Time.deltaTime);
            }

            m_MoveDirection = new Vector3(0, 0, m_Input.y);
            m_MoveDirection = transform.TransformDirection(m_MoveDirection);
            m_MoveDirection *= m_IsRunning ? m_RunSpeed : m_WalkSpeed;

            Controller.Move(m_MoveDirection * Time.deltaTime);

            FootstepsManager();
        }

        private void FootstepsManager()
        {
            float flatVelocity = new Vector3(Controller.velocity.x, 0, Controller.velocity.z).magnitude;
            float strideLengthen = 1 + (flatVelocity * 0.3f);
            m_StepCycle += (flatVelocity / strideLengthen) * (Time.deltaTime / m_StepTime);

            if (m_StepCycle > m_NextStepTime)
            {
                m_StepCycle = 0;
                if (Footsteps.Length > 1)
                {
                    int n = Random.Range(1, Footsteps.Length);
                    Audio.clip = Footsteps[n];
                    Footsteps[n] = Footsteps[0];
                    Footsteps[0] = Audio.clip;
                }
                else
                    Audio.clip = Footsteps[0];

                Audio.pitch = Random.Range(0.9f, 1.1f);
                Audio.Play();
            }
        }

        private Vector2 GetInput()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
