using UnityEngine;

namespace Helpers
{
    public static class SpaceHelper
    {
        private static GameObject space;

        public static GameObject GetSpace()
        {
            return space ? space : GameObject.Find("Space");
        }
    }
}