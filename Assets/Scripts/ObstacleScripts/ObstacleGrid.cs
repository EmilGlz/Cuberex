using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleGrid : MonoBehaviour
{
    [SerializeField] GameObject[] itemsToPickFrom;

    List<int> intlist = new List<int>();

    int min = 0;
    [SerializeField] int max;

    [SerializeField] int removedObsCount;

    private void Start()
    {
        for (int i = min; i < max; i++)
        {
            intlist.Add(i);
        }

        intlist = intlist.OrderBy(tvz => System.Guid.NewGuid()).ToList();

        Remove();
    }


    private void Remove()
    {
        for (int i = 0; i < removedObsCount; i++)
        {
            itemsToPickFrom[intlist[i]].SetActive(false); ;
        }
    }
}
