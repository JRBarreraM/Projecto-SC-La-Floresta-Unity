using UnityEngine;

public class MiniMapWorldObject : MonoBehaviour
{
    // make sure this is true for the player object, this is what is used to center the map
    [SerializeField]
    private bool isPlayer = false;

    public Sprite Icon;
    public Color IconColor = Color.white;
    public string Text;
    public int TextSize = 10;

    private void Start()
    {
        MiniMap.Instance.RegisterMiniMapWorldObject(this, isPlayer);
    }

    private void OnDestroy()
    {
        MiniMap.Instance.DestroyCorrespondingMiniMapIcon(this);
    }
}