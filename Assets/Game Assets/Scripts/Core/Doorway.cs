using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hertzole.Github2017
{
    public class Doorway : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private SceneObject m_SceneToLoad;
        public SceneObject SceneToLoad { get { return m_SceneToLoad; } set { m_SceneToLoad = value; } }

        public void OnInteract()
        {
            SceneManager.LoadScene("Doorway");
            PlayerPrefs.SetString("SceneToLoad", SceneToLoad);
        }
    }
}
