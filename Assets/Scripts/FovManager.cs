using UnityEngine;

public class FovManager : MonoBehaviour
{
    [SerializeField]
    private Material fov;

    [SerializeField]
    private Transform player;

    private void OnPostRender()
    {
        GL.PushMatrix();
        fov.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.TRIANGLE_STRIP);
        Vector3 mousePos = Input.mousePosition - new Vector3(25f, 25f);
        GL.Color(Color.yellow);
        Vector2? last = null;
        for (int i = 0; i < 50; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(player.position, Camera.main.ViewportToWorldPoint(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0)), float.MaxValue, 1 << 8);
            mousePos += new Vector3(1f, 1f);
            if (last == null)
            {
                last = hit.point;
                continue;
            }
            DrawTriangle(player.transform.position, last.Value, hit.point);
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
