using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color  baseColor, secondColor;
    [SerializeField] private SpriteRenderer _renderer;

    private GameObject content;

    public void    Init(bool isOffset) {
        _renderer.color = isOffset ? secondColor : baseColor;
        content = null;
    }

    public void    setContent(GameObject newContent) { content = newContent; }

    public GameObject getContent() { return content; }
}
