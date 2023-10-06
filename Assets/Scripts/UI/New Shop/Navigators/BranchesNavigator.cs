using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchesNavigator : MonoBehaviour
{
    [SerializeField] GameObject[] treesObjects;
    
    [SerializeField] BranchNavigator[] trees;

    private void Awake()
    {
        trees = new BranchNavigator[treesObjects.Length];

        for(int i = 0; i < treesObjects.Length; i++)
        {
            BranchNavigator _temp = treesObjects[i].GetComponent<BranchNavigator>();

            if(_temp != null) trees[i] = _temp;
        }
    }

    public void ActivateBranch(int branch)
    {
        for(int i = 0; i < treesObjects.Length; i++)
        {
            if(i == branch)
            {
                treesObjects[i].SetActive(true);
                trees[i].Control = true;
            } else
            {
                treesObjects[i].SetActive(false);
                trees[i].Control = false;
            }
        }
    }

    public GameObject[] GetBranches()
    {
        return treesObjects;
    }
}
