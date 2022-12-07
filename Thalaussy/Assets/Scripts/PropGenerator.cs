using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : MonoBehaviour
{
    public List<GameObject> ruins = new List<GameObject>();
    public List<GameObject> coral = new List<GameObject>();
    [SerializeField]
    private float range = 200;

    [SerializeField]
    private int numRuins = 100;

    [SerializeField]
    private int numCoral = 100;

        // used to "bury" the ruins
    [SerializeField]
    private Vector3 ruinBuryOffset = new Vector3(0, -10, 0);

    [SerializeField]
    private float coralGrowthRange = 5;

    [SerializeField]
    private int coralMinGrowthCount = 5, coralMaxGrowthCount = 10;

    [SerializeField]
    private AnimationCurve coralSizeCurve;
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateRuins(numRuins);
        GrowCoral(numCoral);
    }

    private void GenerateRuins(int numGenerations)
    {
        for (int i = 0; i < numGenerations; i++)
        {
            Vector3 newPosition = new Vector3(Random.Range(-range, range), 300, Random.Range(-range, range));
            RaycastHit hit;
            if (Physics.Raycast(newPosition, -Vector3.up, out hit))
            {
                newPosition = hit.point + ruinBuryOffset;
                int ruinIndex = Random.Range(0, ruins.Count);
                Instantiate(ruins[ruinIndex], newPosition, ruins[ruinIndex].transform.rotation, this.transform);
            }
            else
            {
                i--;
            }
        }
        return;
    }

    private void GrowCoral(int numGenerations)
    {
        for (int i = 0; i < numGenerations; i++)
        {
            int growthCount = Random.Range(coralMinGrowthCount, coralMaxGrowthCount);
            Vector3 basePosition = new Vector3(Random.Range(-range, range), 300, Random.Range(-range, range));
            int coralIndex = Random.Range(0, coral.Count);
            for (int j = 0; j < growthCount; j++)
            {
                Vector3 offset = new Vector3(Random.Range(-coralGrowthRange, coralGrowthRange), 0, Random.Range(-coralGrowthRange, coralGrowthRange));
                Vector3 newPosition = basePosition + offset;
                RaycastHit hit;
                if (Physics.Raycast(newPosition, -Vector3.up, out hit))
                {
                    newPosition = hit.point;
                    ScaleObjectFromOffset(Instantiate(coral[coralIndex], newPosition, AddRandomRotation(Quaternion.FromToRotation(transform.up, hit.normal)), this.transform), offset.magnitude);
                }
            }
            
            
        }
        return;
    }

    private void ScaleObjectFromOffset(GameObject obj, float dist)
    {
        obj.transform.localScale = obj.transform.localScale * coralSizeCurve.Evaluate(Mathf.Clamp(dist / coralGrowthRange, 0, 1));
    }

    private Quaternion AddRandomRotation(Quaternion input)
    {
        float randomX = Random.Range(-0.1f, 0.1f);
        float randomY = Random.Range(-0.1f, 0.1f);
        float randomZ = Random.Range(-0.1f, 0.1f);
        float randomW = Random.Range(-0.1f, 0.1f);

        Quaternion oldRot = input;
        Quaternion newRot = new Quaternion(oldRot.x + randomX, oldRot.y + randomY, oldRot.z + randomZ, oldRot.w + randomW);
        return newRot;
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
