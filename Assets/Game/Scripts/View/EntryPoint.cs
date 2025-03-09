using Unity.Cinemachine;
using UnityEngine;

namespace View
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject actor;
        [SerializeField] private CinemachineCamera cineCamera;

        private void Awake()
        {
            var actorObject = Instantiate(actor);
            actorObject.transform.position = transform.position;
            actorObject.GetComponent<ActorView>().Initialize(10f);
            cineCamera.Follow = actorObject.transform;
            cineCamera.LookAt = actorObject.transform;
        }
    }
}