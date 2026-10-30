using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ColorCanLibrary")]
public class ColorCanLibrary : ScriptableObject
{

    private static ColorCanLibrary instance;

    public static ColorCanLibrary Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<ColorCanLibrary>("ColorCanLibrary");
            return instance;
        }
    }

    [System.Serializable]
    public struct ColorCan
    {
        public ColorClass color;
        public Sprite sprite;
    }

    [SerializeField] private ColorCan[] Library;

    public static Sprite Get(ColorClass color)
    {
        foreach (var colorCan in Instance.Library){
            if (colorCan.color == color){
                return colorCan.sprite;
            }
        }
        Debug.Log("Color {color} not found");
        return null;
    }
}