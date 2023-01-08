using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PickObject", order = 1)]
public class PickObject : ScriptableObject
{
    public ObjectType objectType;

    public Sprite icon;
}
