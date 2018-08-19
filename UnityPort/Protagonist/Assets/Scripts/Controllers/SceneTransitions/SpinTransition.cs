using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpinTransition : SceneTransition
{
    Vector2 center;
    float angle = 0f;
    float dist = 0f;
    // number of triangles to use when creating the spiral
    int precision = 40;
    // speed of spiral growth
    float spd = 3.5f;
    float spinSpd = 480f;
    // it took this long to fill the screen with the speeds above.
    // when we scale due to the passed in duration, we scale proportional to this value
    // so that we fill the screen at the correct moment.
    float baselineTime = 2f;
    float inSpdScale => baselineTime / inDuration;

    MeshRenderer mr;
    MeshFilter mf;
    void Start()
    {
        center = ResolutionHandler.MapViewToWorldPoint(new Vector2(0.5f, 0.5f));
        mr = GetComponent<MeshRenderer>();
        mr.sortingLayerName = "SceneTransition";
        mf = GetComponent<MeshFilter>();
    }

    // update the mesh renderer to create a spiral
    protected override void Update()
    {
        base.Update();
        mf.mesh = null;
        if (state == State.IN)
        {
            UpdateSpiral();
        }
    }
    // draw the fade-out
    protected override void OnGUI()
    {
        if (state != State.IN)
        {
            base.OnGUI();
        }
    }

    // draw the spiral
    private void UpdateSpiral()
    {
        dist += GameTime.deltaTime * spd * inSpdScale;
        angle += GameTime.deltaTime * spinSpd * inSpdScale;
        float minDist = dist - (360f / spinSpd) * spd;
        // generate spiral polygon
        List<Vector2> vertices = new List<Vector2>();
        for (int i = 0; i <= precision; i++)
        {
            float length = Mathf.Max(0, Mathf.Lerp(minDist, dist, i / (float)precision));
            float dir = Mathf.Lerp(angle, angle + 360f, i / (float)precision);
            vertices.Add(center + PolarToXY(length, dir));
        }
        UpdateMesh(vertices);
    }
    // takes polygon and converts it to a mesh, then gives it to the MeshFilter. Used by UpdateSpiral()
    private void UpdateMesh(List<Vector2> polygon)
    {
        var vertices2D = polygon.Distinct().ToArray();
        var vertices3D = Array.ConvertAll<Vector2, Vector3>(vertices2D, v => v);

        // Use the triangulator to get indices for creating triangles
        var triangulator = new Triangulator(vertices2D);
        var indices = triangulator.Triangulate();

        // entire thing is black
        var colors = Enumerable.Range(0, vertices3D.Length)
            .Select(i => Color.black).ToArray();

        // Create the mesh and set to display
        var mesh = new Mesh
        {
            vertices = vertices3D,
            triangles = indices,
            colors = colors
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mf.mesh = mesh;
    }

    private Vector2 PolarToXY(float magnitude, float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.forward) * new Vector2(magnitude, 0f);
    }
}