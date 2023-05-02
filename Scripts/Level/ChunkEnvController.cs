using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkEnvController : MonoBehaviour
{
    [SerializeField] private Renderer[] geometryRenderers;
    [SerializeField] private Material[] materials;
    [SerializeField] private int levelsPerEnv;
    private int _level;

    private void Awake()
    {
        _level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level); 
    }
    private void Start()
    {
        foreach (var rrr in geometryRenderers)
        {
            rrr.material = materials[_level / levelsPerEnv % materials.Length];
        }
    }
}
