using UnityEngine;

namespace Helpers
{
    public static class CameraExtension
    {
        public static bool IsObjectVisible(this Camera camera, Renderer renderer)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
        }

        public static Vector2 RandomPoint(this Camera camera)
        {
            var y = Random.Range(
                camera.ScreenToWorldPoint(new Vector2(0, 0)).y,
                camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
            var x = Random.Range(
                camera.ScreenToWorldPoint(new Vector2(0, 0)).x,
                camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            return new Vector2(x, y);
        }
    }
}