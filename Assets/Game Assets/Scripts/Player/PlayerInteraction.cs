using UnityEngine;

namespace Hertzole.Github2017
{
    public interface IInteractable
    {
        void OnInteract();
    }

    public class PlayerInteraction : MonoBehaviour, IUpdate
    {
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, 1.5f))
                {
                    IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                    if (interactable != null)
                        interactable.OnInteract();
                }
            }
        }
    }
}
