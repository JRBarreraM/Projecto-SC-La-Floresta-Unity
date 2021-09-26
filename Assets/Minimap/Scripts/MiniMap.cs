using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniMapMode
{
    Mini, Fullscreen
}

public class MiniMap : MonoBehaviour
{
    public static MiniMap Instance;

    private Vector2 miniSizeDimensions;

    private Vector2 fullScreenDimensions;

    [SerializeField]
    float scrollSpeed = 0.1f;

    [SerializeField]
    float maxZoom = 5f;

    [SerializeField]
    float minZoom = 1f;

    [SerializeField]
    RectTransform scrollViewRectTransform;

    [SerializeField]
    RectTransform contentRectTransform;

    [SerializeField]
    MiniMapIcon miniMapIconPrefab;

    Matrix4x4 transformationMatrix;
    private MiniMapIcon playerMiniMapIcon;
    private MiniMapMode currentMiniMapMode = MiniMapMode.Mini;
    Dictionary<MiniMapWorldObject, MiniMapIcon> miniMapWorldObjectsLookup = new Dictionary<MiniMapWorldObject, MiniMapIcon>();
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        float w = canvas.GetComponent<RectTransform>().rect.width;
        float h = canvas.GetComponent<RectTransform>().rect.height;
        miniSizeDimensions = new Vector2(150, 150);
        fullScreenDimensions = new Vector2(w+5.0f, h+5.0f);
        CalculateTransformationMatrix();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // toggle mode
            SetMiniMapMode(currentMiniMapMode == MiniMapMode.Mini ? MiniMapMode.Fullscreen : MiniMapMode.Mini);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ScrollMap(scroll);
        UpdateMiniMapIcons();
        CenterMiniMapOnPlayer();
    }

    private void SetFullMap() {
        MainEventSystem.current.FullMap();
    }

    private void SetMiniMap() {
        MainEventSystem.current.MiniMap();
    }

    public void SetMiniMapMode(MiniMapMode mode)
    {
        if (mode == currentMiniMapMode)
            return;

        switch (mode)
        {
            case MiniMapMode.Mini:
                scrollViewRectTransform.sizeDelta = miniSizeDimensions;
                // scrollViewRectTransform.transform.position = new Vector3(-2,2,0);
                // scrollViewRectTransform.anchorMin = new Vector2(0, 1);
                // scrollViewRectTransform.anchorMax = new Vector2(0, 1);
                // scrollViewRectTransform.pivot = new Vector2(0, 1);
                currentMiniMapMode = MiniMapMode.Mini;
                SetMiniMap();
                break;
            case MiniMapMode.Fullscreen:
                scrollViewRectTransform.sizeDelta = fullScreenDimensions;
                // scrollViewRectTransform.anchorMin = new Vector2(0, 0);
                // scrollViewRectTransform.anchorMax = new Vector2(1, 1);
                // scrollViewRectTransform.pivot = new Vector2(0.5f, 0.5f);
                // scrollViewRectTransform.offsetMin = new Vector2(-5, -5);
                // scrollViewRectTransform.offsetMax = new Vector2(-5, -5);
                currentMiniMapMode = MiniMapMode.Fullscreen;
                SetFullMap();
                break;
        }
        CalculateTransformationMatrix();
    }

    public void RegisterMiniMapWorldObject(MiniMapWorldObject miniMapWorldObject, bool isPlayer = false)
    {
        var miniMapIcon = Instantiate(miniMapIconPrefab);
        miniMapIcon.transform.SetParent(contentRectTransform);
        miniMapIcon.SetIcon(miniMapWorldObject.Icon);
        miniMapIcon.SetColor(miniMapWorldObject.IconColor);
        miniMapIcon.SetText(miniMapWorldObject.Text);
        miniMapIcon.SetTextSize(miniMapWorldObject.TextSize);
        miniMapWorldObjectsLookup[miniMapWorldObject] = miniMapIcon;

        if (isPlayer)
            playerMiniMapIcon = miniMapIcon;
    }

    public void DestroyCorrespondingMiniMapIcon(MiniMapWorldObject miniMapWorldObject)
    {
        if (miniMapWorldObjectsLookup.TryGetValue(miniMapWorldObject, out MiniMapIcon icon))
        {
            miniMapWorldObjectsLookup.Remove(miniMapWorldObject);
            Destroy(icon.gameObject);
        }
    }

    void ScrollMap(float scroll)
    {
        if (scroll == 0)
            return;

        float currentMapScale = contentRectTransform.localScale.x;
        // we need to scale the scroll speed by the current map scale to keep the zooming linear
        float scrollAmount = (scroll > 0 ? scrollSpeed : -scrollSpeed) * currentMapScale;
        float newScale = currentMapScale + scrollAmount;
        float clampedScale = Mathf.Clamp(newScale, minZoom, maxZoom);
        contentRectTransform.localScale = Vector3.one * clampedScale;
    }

    void CenterMiniMapOnPlayer()
    {
        if (playerMiniMapIcon != null)
        {
            float mapScale = contentRectTransform.transform.localScale.x;
            // we simply move the map in the opposite direction the player moved, scaled by the mapscale
            contentRectTransform.transform.localPosition = (-playerMiniMapIcon.transform.localPosition * mapScale);
        }
    }

    void UpdateMiniMapIcons()
    {
        // scale icons by the inverse of the mapscale to keep them a consitent size
        foreach (var kvp in miniMapWorldObjectsLookup)
        {
            var miniMapWorldObject = kvp.Key;
            var miniMapIcon = kvp.Value;
            var mapPosition = WorldPositionToMapPosition(miniMapWorldObject.transform.position);

            miniMapIcon.RectTransform.anchoredPosition = mapPosition;
            var rotation = miniMapWorldObject.transform.rotation.eulerAngles;
            miniMapIcon.IconRectTransform.localRotation = Quaternion.AngleAxis(-rotation.y, Vector3.forward);
        }
    }

    Vector2 WorldPositionToMapPosition(Vector3 worldPos)
    {
        var pos = new Vector2(worldPos.x, worldPos.z);
        return transformationMatrix.MultiplyPoint3x4(pos);
    }


    void CalculateTransformationMatrix()
    {
        var miniMapDimensions = contentRectTransform.rect.size;
        var terrainDimensions = new Vector2(30, 30);
        var scaleRatio = miniMapDimensions / terrainDimensions;
        var translation = new Vector2(0, 0);
        transformationMatrix = Matrix4x4.TRS(translation, Quaternion.identity, scaleRatio);

        //  {scaleRatio.x,   0,           0,   translation.x},
        //  {  0,        scaleRatio.y,    0,   translation.y},
        //  {  0,            0,           0,            0},
        //  {  0,            0,           0,            0}
    }
}