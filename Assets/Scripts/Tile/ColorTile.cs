using UnityEngine;

public class ColorTile : Tile
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material deactivatedMaterial;
    [SerializeField] private Renderer meshRenderer;

    protected override void Awake()
    {
        base.Awake();
        boxCollider.enabled = false;
    }

    public void EnableTile()
    {
        boxCollider.enabled = true;
    }

    public override void Activate()
    {
        meshRenderer.material = activatedMaterial;
    }

    public override void Deactivate()
    {
        meshRenderer.material = deactivatedMaterial;
    }
}
