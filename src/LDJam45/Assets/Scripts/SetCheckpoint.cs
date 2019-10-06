using UnityEngine;

namespace Assets.Scripts
{
    public class SetCheckpoint : MonoBehaviour
    {
        [SerializeField] private GameState GameState;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
                GameState.LastCheckpoint = other.transform.position;
        }
    }
}
