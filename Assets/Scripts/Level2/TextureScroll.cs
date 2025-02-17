using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public bool scroll=true;
    Material backgroundMaterial;

    private void Awake()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }


    private void FixedUpdate()
    {
        if (scroll)
        {
            float offset = Time.time * scrollSpeed;
            backgroundMaterial.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}
