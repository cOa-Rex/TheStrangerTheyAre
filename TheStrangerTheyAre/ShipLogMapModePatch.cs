using HarmonyLib;
using NewHorizons.Handlers;
using System.Collections.Generic;
using System.Linq;

namespace TheStrangerTheyAre;

[HarmonyPatch(typeof(ShipLogMapMode))]
public static class ShipLogMapModePatch
{
    [HarmonyPostfix]
    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch(nameof(ShipLogMapMode.Initialize))]
    public static void ShipLogMapMode_Initialize(ShipLogMapMode __instance)
    {
        if (TheStrangerTheyAre.NewHorizonsAPI.GetCurrentStarSystem().Equals("AnonymousStrangerOW.StrangerSystem"))
        {
            if (!ShipLogHandler.KnowsFact("HOME_REVEAL"))
            {
                // Hide all bodies except for the star until the player learns about the home moon
                for (int y = 0; y < __instance._astroObjects.Length; y++)
                {
                    for (int x = 0; x < __instance._astroObjects[y].Length; x++)
                    {
                        var astroObject = __instance._astroObjects[y][x];
                        if (astroObject != null && astroObject.GetID() != "Nearest Neighbor")
                        {
                            astroObject._invisibleWhenHidden = true;
                            astroObject.UpdateState();
                        }
                    }
                }
            }
            else
            {
                __instance._startingAstroObjectID = "Homeworld";
                // Make Nearest Neighbor unselectable after the player learns about the home moon
                var nearestNeighborIndex = __instance.GetAstroObjectIndex("Nearest Neighbor");
                var nearestNeighbor = __instance._astroObjects[nearestNeighborIndex[0]][nearestNeighborIndex[1]];
                nearestNeighbor.UpdateState();
                __instance._astroObjects[nearestNeighborIndex[0]][nearestNeighborIndex[1]] = null;
                __instance._astroObjects = __instance._astroObjects.Where(a => a.Count(c => c != null) > 0).ToArray();
                for (var index = 0; index < __instance._astroObjects.Length; index++)
                {
                    __instance._astroObjects[index] = __instance._astroObjects[index].Where(a => a != null).ToArray();
                }
            }
        }
    }
}
