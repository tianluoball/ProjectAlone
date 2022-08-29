using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProgress : MonoBehaviour
{

    public MoreMountains.Tools.MMProgressBar progressBar;

    public void UpdateProgressBar(float progress)
    {
        progressBar.UpdateBar01(progress);
    }

}
