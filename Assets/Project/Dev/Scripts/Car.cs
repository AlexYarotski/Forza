using UnityEngine;

namespace Project.Dev.Scripts
{
    public class Car : MonoBehaviour
    {
        private const float ZeroPoint = 0.8f;
        
        [SerializeField]
        private float _speedTurn = 0;

        private void OnEnable()
        {
            SwipeController.Turn += SwipeController_Turn;
        }

        private void OnDisable()
        {
            SwipeController.Turn += SwipeController_Turn;
        }

        private void SwipeController_Turn(SwipeController controller)
        {
             Vector3 turn = new Vector3(1, 0, 0) * _speedTurn;
             float clamp = 0;
             
             if (controller.PointClickAxisX < controller.CurrentPositionAxisX)
             {
                 float turnRightAxisX = transform.position.x + turn.x;
                 clamp = Mathf.Clamp(turnRightAxisX, -7, 7);
             }
             else
             {
                 float turnLeftAxisX = transform.position.x - turn.x;
                 clamp = Mathf.Clamp(turnLeftAxisX, -7, 7);
             }

             transform.position = new Vector3(clamp, ZeroPoint, transform.position.z);
        }

    }
}