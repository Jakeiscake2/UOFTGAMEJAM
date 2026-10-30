using UnityEngine;

public class RandomStationScript : StationScript
{
    private void Awake(){
        storedColor = (ColorClass)Random.Range(1, 8);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ColorCanLibrary.Get(storedColor);
    }
}
