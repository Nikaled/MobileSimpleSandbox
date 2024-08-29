using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenObjectVariants : ObjectVariants
{
    [SerializeField] protected  SkinnedMeshRenderer CitizenToChange;

    private void Awake()
    {
        if(CitizenToChange == null)
        {
            CitizenToChange = this?.GetComponent<SkinnedMeshRenderer>();
        }
    }
    public override void ChangeTextures(Texture textures)
    {

        for (int i = 0; i < CitizenToChange.materials.Length; i++)
        {
            CitizenToChange.materials[i].mainTexture = textures;
        }
    }

    public override int FindTextureIndex()
    {
        for (int i = 0; i < Variants.Length; i++)
        {
            if (CitizenToChange.sharedMaterials[0].mainTexture == Variants[i])
            {
                Debug.Log("currentVariantIndex:" + i);
                currentVariantIndex = i;
            }
        }
            return currentVariantIndex;
    }
    public override void ChangeTextures(int PlusOrMinusOne)
    {
        currentVariantIndex += PlusOrMinusOne;
        if (currentVariantIndex < 0)
        {
            currentVariantIndex = Variants.Length - 1;
        }
        if (currentVariantIndex >= Variants.Length)
        {
            currentVariantIndex = 0;
        }
        ChangeTextures(Variants[currentVariantIndex]);
        ChangeTextureManager.instance.ChangeVariantText(currentVariantIndex);
    }
}
