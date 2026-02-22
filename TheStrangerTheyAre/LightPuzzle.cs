using UnityEngine;

namespace TheStrangerTheyAre
{
    public class LightPuzzle : MonoBehaviour
    {
        [SerializeField]
        public MeshRenderer lightbulbRenderer; // get lightbulb game object
        [SerializeField]
        public Light lightbulbLight; //get reference to lightbulb's light
        [SerializeField]
        public EclipseDoorController door; // get hidden door

        public readonly Color maxColor = new Color(1.489617f, 1.513387f, 1.228142f);
        public readonly Color minColor = new Color(0.9354541f, 0.9530618f, 0.7417698f);
        
        private float _openValue; //calculate distance to open state, it'll need to be between 0 (farthest) and 1 (closest).
        private bool _isLit;

        public void Start()
        {
            DoLights();
        }

        public void Update()
        {
            for (int i = 0; i < door._lightSensors.Length; i++)
            {
                if (door._lightSensors[i].IsIlluminated())
                {
                    _isLit = true;
                    break;
                } else
                {
                    _isLit = false;
                }
            }

            if (_isLit)
            {
                DoLights();
            }
        }

        public void DoLights()
        {
            Material[] mats = lightbulbRenderer.materials;
            Material lightbulbMat = mats[1]; //get reference to lightbulb, then check its Meshrenderer, then get the material (not sharedMaterial) in the appropriate slot)

            float angle = door._rotatingElements[1].localRotation.eulerAngles.z;
            if (angle > 180) angle = 180 - (angle - 180);
            _openValue = Mathf.InverseLerp(0, 180, angle);

            Color currentColor = Color.Lerp(minColor, maxColor, _openValue);
            lightbulbMat.SetColor("_EmissionColor", currentColor);
            lightbulbLight.intensity = _openValue;
            lightbulbRenderer.materials = mats;
        }
    }
}
