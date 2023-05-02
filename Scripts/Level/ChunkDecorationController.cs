using UnityEngine;

public class ChunkDecorationController : MonoBehaviour
{
    [SerializeField] private GameObject[] env1;
    [SerializeField] private GameObject[] env2;
    [SerializeField] private GameObject[] env3;

    [SerializeField] private int levelsPerEnv;
    [SerializeField] private float spawnChance;
    private int _level;

    private float[] rotationMas = { 0, 90, 180, -90 };
    private void Awake()
    {
        _level = PlayerPrefsStrings.GetIntValue(PlayerPrefsStrings.Level);
    }
    private void Start()
    {
        if (Random.value > spawnChance) return;
        switch (_level / levelsPerEnv % 3)
        {
            case 0:
                {
                    SpawnDecoration(env1);
                }
                break;
            case 1:
                {
                    SpawnDecoration(env2);
                }
                break;
            case 2:
                {
                    SpawnDecoration(env3);
                }
                break;
        }
    }
    private void SpawnDecoration(GameObject[] objects)
    {
        Transform decorTransform = Instantiate(objects[Random.Range(0, env1.Length)], transform).transform;
        decorTransform.localEulerAngles = Vector3.up * rotationMas[Random.Range(0, rotationMas.Length)];
    }
}
