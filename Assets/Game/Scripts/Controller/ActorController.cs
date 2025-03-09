using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class ActorController : MonoBehaviour
    {
        public static InputSystem_Actions Actions;
        private IMovable _actor;
        private GunScript _gunScript;
        private void Awake()
        {
            Actions = new InputSystem_Actions();
            Actions.Enable();
            _actor = GetComponent<IMovable>();
            _gunScript = GameObject.Find("Gun").GetComponent<GunScript>();
        }

        private void FixedUpdate()
        {
            ReadMovement();
            transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gunScript.Shoot();
            }
        }

        private void ReadMovement()
        {
            var direction = Actions.Player.Move.ReadValue<Vector2>();
            _actor.Move(direction);
        }

    }
}