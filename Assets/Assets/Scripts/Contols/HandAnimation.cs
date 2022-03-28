using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts{
    public class HandAnimation : MonoBehaviour{
        public InputActionReference grip;
        public InputActionReference trigger;
        private Animator _handAnimator;
        void Start(){
            _handAnimator = GetComponent<Animator>();
        }
        void UpdateHandAnimation(){
            _handAnimator.SetFloat("Grip",grip.action.ReadValue<float>());
            _handAnimator.SetFloat("Trigger",trigger.action.ReadValue<float>());
        }
        void Update(){
            UpdateHandAnimation();
        }
    }
}