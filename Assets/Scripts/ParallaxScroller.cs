using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public Vector2 ParallaxStrength;
    public bool InfiniteHorizontal = true;
    public bool InfiniteVertical = false;
    private Transform camTrans;
    private Vector3 lastCamPos;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    private void Awake() {
        camTrans = Camera.main.transform;
        lastCamPos = camTrans.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    private void LateUpdate() {
        Vector3 movedThisFrame = camTrans.position - lastCamPos;
        transform.position += new Vector3(movedThisFrame.x * ParallaxStrength.x, movedThisFrame.y * ParallaxStrength.y, 0f);
        lastCamPos = camTrans.position;

        if(InfiniteHorizontal && Mathf.Abs(camTrans.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPosX = (camTrans.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(camTrans.position.x + offsetPosX, transform.position.y);
        }

        if(InfiniteVertical && Mathf.Abs(camTrans.position.y - transform.position.y) >= textureUnitSizeY)
        {
            float offsetPosY = (camTrans.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.y, camTrans.position.y + offsetPosY);
        }
    }
}
