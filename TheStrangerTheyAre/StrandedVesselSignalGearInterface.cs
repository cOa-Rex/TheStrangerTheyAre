using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class StrandedVesselSignalGearInterface : AbstractGearInterface
    {
        [SerializeField]
        public GameObject monitorOn;

        [SerializeField]
        public GameObject monitorOff;

        private GameObject _vesselSignal;

        private bool _isOn = false;

        public override void Start()
        {
            base.Start();
            _vesselSignal = SearchUtilities.Find("STRANDED_VESSEL_SIGNAL");
            UpdateObjects();
        }

        public override bool CanInteract => true;

        public override void OnGearRotated()
        {
            _isOn = !_isOn;
            UpdateObjects();
        }

        public void UpdateObjects()
        {
            _vesselSignal.SetActive(_isOn);
            monitorOff.SetActive(!_isOn);
            monitorOn.SetActive(_isOn);
        }
    }
}