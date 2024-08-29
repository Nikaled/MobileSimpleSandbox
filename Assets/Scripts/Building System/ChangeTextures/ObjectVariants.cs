using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVariants : MonoBehaviour
{
    [SerializeField] protected Texture[] Variants;
    [SerializeField] protected MeshRenderer ObjectToChange;
    [HideInInspector] public int currentVariantIndex;
    [SerializeField] protected MeshRenderer SecondObjectToChange;
    public virtual void ChangeTextures(Texture textures)
    {

        for (int i = 0; i < ObjectToChange.materials.Length; i++)
        {
            ObjectToChange.materials[i].mainTexture = textures;
            if (SecondObjectToChange != null) SecondObjectToChange.materials[i].mainTexture = textures;

        }
    }

    public virtual int FindTextureIndex()
    {
        for (int i = 0; i < Variants.Length; i++)
        {
            if(ObjectToChange.materials[0].mainTexture == null)
            {
                return 0;
            }
            if (ObjectToChange.materials[0].mainTexture == Variants[i])
            {
                Debug.Log("currentVariantIndex:" + i);
                currentVariantIndex = i;
            }
        }
        return currentVariantIndex;
    }
    public virtual void ChangeTextures(int PlusOrMinusOne)
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
        if (ChangeTextureManager.instance != null)
        {
            ChangeTextureManager.instance.ChangeVariantText(currentVariantIndex);
        }
    }
    public void ChangeTexturesOnLoad(int Index)
    {
        if (Index < Variants.Length)
        {
            ChangeTextures(Variants[Index]);
            currentVariantIndex = Index;
        }

    }
}
