using System;
using Unity.Cinemachine;
using UnityEngine;
using Random = System.Random;

namespace View
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject actor;
        [SerializeField] private CinemachineCamera cineCamera;
        [SerializeField] private GameObject enemy2;
        private float _timeElapsed;

        private void Awake()
        {
            var actorObject = Instantiate(actor);
            actorObject.transform.position = transform.position;
            actorObject.GetComponent<ActorView>().Initialize(10f);
            cineCamera.Follow = actorObject.transform;
            cineCamera.LookAt = actorObject.transform;
        }

        private void FixedUpdate()
        {
            if (_timeElapsed > 2f)
            {
                _timeElapsed -= 2f;
                Random rand = new();

                Vector3 spawnPosition;
                float minDistance = 10f; 
                do
                {
                    spawnPosition = new Vector3(rand.Next(-40, 40), 2, rand.Next(-40, 40));
                } 
                while (Vector3.Distance(Vector3.zero, spawnPosition) < minDistance); 

                var obj2 = Instantiate(enemy2);
                obj2.transform.position = spawnPosition;
                obj2.GetComponent<EnemyView>().Initialize(100);
            }
            _timeElapsed += Time.fixedDeltaTime;
        }
    }
}