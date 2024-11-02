using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTest : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // 충돌하는 스프라이트의 SpriteRenderer
    private Texture2D texture;

    private int[] _dx = new int[4] { 0, 1, 0, -1 };
    private int[] _dy = new int[4] { 1, 0, -1, 0 };
    private Color[,] _colors;
    private bool[,] _visited;
    void Start()
    {
        texture = spriteRenderer.sprite.texture;

        // 텍스처가 읽기 가능해야 합니다.
        if (!texture.isReadable)
        {
            Debug.LogError("Texture is not readable. Check 'Read/Write Enabled' in the Texture Import Settings.");
            return;
        }
        _colors = new Color[texture.width, texture.height];

        // 픽셀별로 색상 변경
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                _colors[x, y] = texture.GetPixel(x, y);
                if (texture.GetPixel(x, y).a == 0) continue;
                Color newColor = Color.red; // 원하는 색으로 변경
                texture.SetPixel(x, y, newColor);
            }
        }

        // 변경 사항을 텍스처에 적용
        texture.Apply();
    }
    private void OnApplicationQuit()
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, _colors[x, y]);
            }
        }
        texture.Apply();
    }
}
