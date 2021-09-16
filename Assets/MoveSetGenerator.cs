using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetGenerator : MonoBehaviour
{
    public int MoveSetCount;
    //amount of moves, amount of arrow per move, list of directions
    List<ArrowSequence> arrowSequences = new List<ArrowSequence>();

    [Header("Move Set Assets")]
    public GameObject arrowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
