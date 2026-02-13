using NewHorizons.Utility;
using UnityEngine;

namespace TheStrangerTheyAre
{
    public class TornadoFix : MonoBehaviour
    {
        private GameObject[] _tornadoes = new GameObject[3]; // create new array of gameobjects to store all tornadoes
        private TornadoFluidVolume[] _tornadoVolumes = new TornadoFluidVolume[3]; // create new array of all tornado fluid volumes

        public void Start()
        {
            for (int i = 0; i < _tornadoes.Length; i++)
            {
                _tornadoVolumes[i] = SearchUtilities.Find("CRIMSONTORNADO_" + (i + 1)).GetComponentInChildren<TornadoFluidVolume>();
                _tornadoVolumes[i]._density = 1;
            }
        }
    }
}
