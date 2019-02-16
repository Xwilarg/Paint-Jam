using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FovManager : MonoBehaviour
{
    [SerializeField]
    private Material fov;

    private IEnumerable<SecurityCamera> cameras;

    private void Start()
    {
        cameras = GameObject.FindGameObjectsWithTag("SecurityCamera").ToList().Select(x => x.GetComponent<SecurityCamera>());
    }

    private void OnPostRender()
    {
        GL.PushMatrix();
        fov.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.TRIANGLE_STRIP);
        foreach (SecurityCamera cam in cameras)
        {
            Vector2? last = null;
            for (int i = 0; i < 360; i++)
            {
                float fI = i * (2f * Mathf.PI) / 360f;
                RaycastHit2D hit = Physics2D.Raycast(cam.transform.position, cam.transform.position + new Vector3(Mathf.Cos(fI), Mathf.Sin(fI)) * 10f, float.MaxValue, (1 << 8) | (1 << 11));
                if (last == null)
                {
                    last = hit.point;
                    continue;
                }
                DrawTriangle(cam.transform.position, last.Value, hit.point);
                last = hit.point;
            }
        }
        GL.End();
        GL.PopMatrix();
    }

    private void DrawTriangle(Vector2 point1, Vector2 point2, Vector2 point3)
    {
        GL.Vertex(WorldToViewport(point1));
        GL.Vertex(WorldToViewport(point2));
        GL.Vertex(WorldToViewport(point3));
        GL.Vertex(WorldToViewport(point1));
    }

    private Vector3 WorldToViewport(Vector2 pos)
    {
        Vector3 newPos = Camera.main.WorldToViewportPoint(pos);
        newPos.z = 0f;
        return (newPos);
    }
}
