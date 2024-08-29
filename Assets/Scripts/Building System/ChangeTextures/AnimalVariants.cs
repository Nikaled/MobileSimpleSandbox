using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalVariants : ObjectVariants
{
    public GameObject CurrentActiveSkin;
    public GameObject[] AnimalSkins;

    public override void ChangeTextures(int PlusOrMinusOne)
    {
        currentVariantIndex += PlusOrMinusOne;
        if (currentVariantIndex < 0)
        {
            currentVariantIndex = AnimalSkins.Length - 1;
        }
        if (currentVariantIndex >= AnimalSkins.Length)
        {
            currentVariantIndex = 0;
        }
        for (int i = 0; i < AnimalSkins.Length; i++)
        {
            AnimalSkins[i].SetActive(false);
        }
        AnimalSkins[currentVariantIndex].SetActive(true);
        ChangeTextureManager.instance.ChangeVariantText(currentVariantIndex);
    }
    public override int FindTextureIndex()
    {
        for (int i = 0; i < AnimalSkins.Length; i++)
        {
            if(AnimalSkins[i].activeInHierarchy == true)
            {
                currentVariantIndex = i;
            }
        }
        return currentVariantIndex;
    }
}
