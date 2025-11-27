using UnityEngine;

public class UpradeIconDisplay : MonoBehaviour
{
    public UpgradeSO[] upgrades;

    public UpgradeHandler UH;

    void Start()
    {
    }

    void Update()
    {
        if (UH.UpgradesOwned.Count > 0)
        {
            for (int i = 0; i < UH.UpgradesOwned.Count; i++)
            {
                if (i < transform.childCount)
                {
                    Transform iconTransform = transform.GetChild(i);
                    Renderer iconRenderer = iconTransform.GetComponent<Renderer>();
                    if (iconRenderer != null)
                    {
                        iconRenderer.material.mainTexture = UH.UpgradesOwned[i].Icon.texture;
                    }
                }
            }
        }
    }



}
