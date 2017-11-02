using System.Collections.Generic;
using UnityEngine;

namespace Hertzole.Github2017
{
    public interface IUpdate
    {
        void GameUpdate();
    }

    public interface IFixedUpdate
    {
        void FixedGameUpdate();
    }

    public interface ILateUpdate
    {
        void LateGameUpdate();
    }

    public class UpdateManager : MonoBehaviour
    {
        [SerializeField]
        private bool m_Persist = true;
        public bool Persist { get { return m_Persist; } set { m_Persist = value; } }

        private static UpdateManager instance;
        private static UpdateManager Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<UpdateManager>();

                    if (instance == null && Application.isPlaying)
                    {
                        GameObject go = new GameObject("Update Manager");
                        instance = go.AddComponent<UpdateManager>();
                        if (instance.Persist)
                            DontDestroyOnLoad(go);
                    }
                }

                return instance;
            }
        }

        private List<IFixedUpdate> m_FixedUpdateList = new List<IFixedUpdate>();
        private static List<IFixedUpdate> FixedUpdateList { get { return Instance.m_FixedUpdateList; } set { Instance.m_FixedUpdateList = value; } }
        private List<ILateUpdate> m_LateUpdateList = new List<ILateUpdate>();
        private static List<ILateUpdate> LateUpdateList { get { return Instance.m_LateUpdateList; } set { Instance.m_LateUpdateList = value; } }
        private List<IUpdate> m_UpdateList = new List<IUpdate>();
        private static List<IUpdate> UpdateList { get { return Instance.m_UpdateList; } set { Instance.m_UpdateList = value; } }

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            if (Persist)
                DontDestroyOnLoad(this.gameObject);
        }

        // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
        private void FixedUpdate()
        {
            if (m_FixedUpdateList.Count > 0)
            {
                for (int i = 0; i < m_FixedUpdateList.Count; i++)
                    m_FixedUpdateList[i].FixedGameUpdate();
            }
        }

        // LateUpdate is called every frame, if the Behaviour is enabled
        private void LateUpdate()
        {
            if (m_LateUpdateList.Count > 0)
            {
                for (int i = 0; i < m_LateUpdateList.Count; i++)
                    m_LateUpdateList[i].LateGameUpdate();
            }
        }

        // Update is called every frame, if the MonoBehaviour is enabled
        private void Update()
        {
            if (m_UpdateList.Count > 0)
            {
                for (int i = 0; i < m_UpdateList.Count; i++)
                    m_UpdateList[i].GameUpdate();
            }
        }

        /// <summary>
        /// Adds a new component that needs to be called every frame.
        /// </summary>
        /// <param name="update"></param>
        public static void AddUpdate(IUpdate update)
        {
            if (Application.isPlaying)
                UpdateList.Add(update);
        }

        /// <summary>
        /// Removes a new that needs to be called every frame.
        /// </summary>
        /// <param name="update"></param>
        public static void RemoveUpdate(IUpdate update)
        {
            if (Application.isPlaying)
                UpdateList.Remove(update);
        }

        /// <summary>
        /// Adds a new component that needs to be called every fixed framerate frame.
        /// </summary>
        /// <param name="update"></param>
        public static void AddFixedUpdate(IFixedUpdate update)
        {
            if (Application.isPlaying)
                FixedUpdateList.Add(update);
        }

        /// <summary>
        /// Removes a new that needs to be called every fixed framerate frame.
        /// </summary>
        /// <param name="update"></param>
        public static void RemoveFixedUpdate(IFixedUpdate update)
        {
            if (Application.isPlaying)
                FixedUpdateList.Remove(update);
        }

        /// <summary>
        /// Adds a new component that needs to be called every frame.
        /// </summary>
        /// <param name="update"></param>
        public static void AddLateUpdate(ILateUpdate update)
        {
            if (Application.isPlaying)
                LateUpdateList.Add(update);
        }

        /// <summary>
        /// Removes a new that needs to be called every frame.
        /// </summary>
        /// <param name="update"></param>
        public static void RemoveLateUpdate(ILateUpdate update)
        {
            if (Application.isPlaying)
                LateUpdateList.Remove(update);
        }
    }
}
