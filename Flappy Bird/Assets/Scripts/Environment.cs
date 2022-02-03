using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] float backgroundSpeed = 0.1f;

    private MeshRenderer meshRenderer;
    private bool isMoving = true;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if (!isMoving) { return; }
        meshRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * 0.02f, 0);
    }

    public void StopEnvironment()
    {
        isMoving = false;
    }
}
