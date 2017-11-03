using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Hertzole.Github2017
{
    public class DoorOpeningScene : MonoBehaviour, IUpdate
    {
        [SerializeField]
        private PlayableDirector m_Director;
        public PlayableDirector Director { get { return m_Director; } set { m_Director = value; } }

        private string m_SceneToLoad;
        private bool m_LoadDone = false;

        private AsyncOperation m_AsyncLoad;

        // Use this for initialization
        void Start()
        {
            m_SceneToLoad = PlayerPrefs.GetString("SceneToLoad");
            if (string.IsNullOrEmpty(m_SceneToLoad))
            {
                Debug.LogError("Something horrible has gone wrong! There's no scene to load but the doorway scene got entered!");
                return;
            }

            StartCoroutine(LoadScene(m_SceneToLoad));
        }

        void OnEnable()
        {
            UpdateManager.AddUpdate(this);
        }

        void OnDisable()
        {
            UpdateManager.RemoveUpdate(this);
        }

        IEnumerator LoadScene(string sceneName)
        {
            m_AsyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            m_AsyncLoad.allowSceneActivation = false;
            while (m_AsyncLoad.progress < 0.9f)
            {
                yield return null;
            }

            m_LoadDone = true;
        }

        public void GameUpdate()
        {
            if (m_Director.state == PlayState.Paused && m_LoadDone)
            {
                m_AsyncLoad.allowSceneActivation = true;
            }
        }
    }
}
