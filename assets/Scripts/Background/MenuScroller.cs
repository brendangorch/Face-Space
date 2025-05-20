using UnityEngine;

public class MenuScroller : MonoBehaviour
{
    // Stars (continuous tiling)
    public RectTransform[] starsTiles;
    public float starsSpeed = 0.5f;

    // Planets (restart when off-screen)
    public RectTransform ringPlanet;
    public RectTransform bigPlanet;
    public RectTransform[] smallPlanets;
    public float smallPlanetSpeed;
    public float bigPlanetSpeed;
    public float ringPlanetSpeed;

    private void Update()
    {
        // Scroll stars and small planets continuously
        ScrollTiles(starsTiles, starsSpeed);
        ScrollTiles(smallPlanets, smallPlanetSpeed);

        // Move the planets and reset them when they exit the screen
        ScrollPlanet(ringPlanet, ringPlanetSpeed);
        ScrollPlanet(bigPlanet, bigPlanetSpeed);
    }

    private void ScrollTiles(RectTransform[] tiles, float speed)
    {
        foreach (RectTransform tile in tiles)
        {
            // Move the tile
            tile.anchoredPosition += Vector2.left * speed * Time.deltaTime;

            // Check if the tile has moved off-screen and reset its position
            if (tile.anchoredPosition.x <= -GetLayerWidth(tile))
            {
                RectTransform rightmostTile = GetRightmostTile(tiles);
                tile.anchoredPosition = new Vector2(
                    rightmostTile.anchoredPosition.x + GetLayerWidth(rightmostTile),
                    tile.anchoredPosition.y
                );
            }
        }
    }

    private void ScrollPlanet(RectTransform planet, float speed)
    {
        // Move the planet
        planet.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        // If the planet exits the left edge of the screen, reset it
        float screenWidth = GetCanvasWidth();
        if (planet.anchoredPosition.x <= -screenWidth / 2 - GetLayerWidth(planet) / 2)
        {
            planet.anchoredPosition = new Vector2(
                screenWidth / 2 + GetLayerWidth(planet) / 2,
                planet.anchoredPosition.y
            );
        }
    }

    private float GetLayerWidth(RectTransform layer)
    {
        // Get the width of the RectTransform
        return layer.rect.width;
    }

    private RectTransform GetRightmostTile(RectTransform[] layerTiles)
    {
        // Find the tile farthest to the right
        RectTransform rightmost = layerTiles[0];
        foreach (RectTransform tile in layerTiles)
        {
            if (tile.anchoredPosition.x > rightmost.anchoredPosition.x)
            {
                rightmost = tile;
            }
        }
        return rightmost;
    }

    private float GetCanvasWidth()
    {
        // Get the width of the canvas in world units
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        return canvasRect.rect.width;
    }
}
