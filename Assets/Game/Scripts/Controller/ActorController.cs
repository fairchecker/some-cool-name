using Interfaces;
using UnityEngine;

namespace Controller
{
    public class ActorController : MonoBehaviour
    {
        public static InputSystem_Actions Actions;
        private IMovable _actor;

        private void Awake()
        {
            Actions = new InputSystem_Actions();
            Actions.Enable();
            _actor = GetComponent<IMovable>();
        }

        private void FixedUpdate()
        {
            ReadMovement();
        }

        private void ReadMovement()
        {
            var direction = Actions.Player.Move.ReadValue<Vector2>();
            _actor.Move(direction);
        }
    }
}