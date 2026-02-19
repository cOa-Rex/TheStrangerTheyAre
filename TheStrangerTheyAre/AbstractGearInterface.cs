using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public abstract class AbstractGearInterface : MonoBehaviour
    {
        [SerializeField]
        public InteractReceiver interactReceiver;
        [SerializeField]
        public GearInterfaceEffects gearEffects;

        public abstract bool CanInteract { get; }
        public abstract void OnGearRotated();

        public virtual void Start()
        {
            if (interactReceiver != null)
            {
                interactReceiver.OnPressInteract += OnPressInteract;
                interactReceiver.SetPromptText(UITextType.RotateGearPrompt);
            }
        }

        public virtual void OnDestroy()
        {
            if (interactReceiver != null)
            {
                interactReceiver.OnPressInteract -= OnPressInteract;
            }
        }

        private void OnPressInteract()
        {
            interactReceiver.ResetInteraction();
            bool success = CanInteract;
            if (gearEffects != null)
            {
                if (success)
                {
                    gearEffects.AddRotation(90);
                    OnGearRotated();
                }
                else
                {
                    gearEffects.PlayFailure();
                }
            }
        }
    }
}
