using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FovManager : MonoBehaviour
{
    [SerializeField]
    private Material fov;

    [SerializeField]
    private GameObject player;

    private SecurityCamera camera;

    public void SetCamera(SecurityCamera cam)
    {
        if (camera != null)
            Destroy(camera.gameObject);
        camera = cam;
    }

    private void Start()
    {
        camera = null;
    }

    private void OnPostRender()
    {
        GL.PushMatrix();
        fov.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.TRIANGLE_STRIP);
        if (camera != null)
        {
            Vector2? last2 = null;
            for (int i = 0; i < 360; i++)
            {
                float fI = i * (2f * Mathf.PI) / 360f;
                RaycastHit2D hit = Physics2D.Raycast(camera.transform.position, camera.transform.position + new Vector3(Mathf.Cos(fI), Mathf.Sin(fI)) * 10f, float.MaxValue, (1 << 8) | (1 << 11));
                if (hit.collider.CompareTag("Enemy"))
                    hit.collider.GetComponent<Enemy>().IsEnable = true;
                if (last2 == null)
                {
                    last2 = hit.point;
                    continue;
                }
                DrawTriangle(camera.transform.position, last2.Value, hit.point);
                last2 = hit.point;
            }
        }
        GL.End();
        GL.Begin(GL.TRIANGLE_STRIP);
        Vector2? last = null;
        for (int i = 0; i < 360; i++)
        {
            float fI = i * (2f * Mathf.PI) / 360f;
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, player.transform.position + new Vector3(Mathf.Cos(fI), Mathf.Sin(fI)) * 10f, float.MaxValue, (1 << 8) | (1 << 11));
            if (hit.collider.CompareTag("Enemy"))
                hit.collider.GetComponent<Enemy>().IsEnable = true;
            if (last == null)
            {
                last = hit.point;
                continue;
            }
            DrawTriangle(player.transform.position, last.Value, hit.point);
            last = hit.point;
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
