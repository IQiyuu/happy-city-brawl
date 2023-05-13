using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public List<Vector2> values;
    public List<BuildingType> type;

    public int  search(BuildingType searched) {
        for(int i = 0; i < type.Count; i++)
            if (type[i] == searched)
                return i;
        return -1;
    }
}
