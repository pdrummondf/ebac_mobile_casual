using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        TYPE01,
        TYPE02,
        TYPE03
    }

    public List<ArtSetup> artSetups;

    public ArtSetup GetSetupByType(ArtType art)
    {
        return artSetups.Find(i => i.artType == art);
    }
}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject gameObject;
}
