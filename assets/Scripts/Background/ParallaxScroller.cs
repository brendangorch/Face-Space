using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    // Stars (continuous tiling)
    public Transform[] starsTiles;
    public float starsSpeed = 0.5f;

    // Planets (restart when off-screen)
    public Transform smallPlanet;
    public Transform bigPlanet;
    public float smallPlanetSpeed = 0.3f;
    public float bigPlanetSpeed = 0.1f;

    private void Update()
    {
        // Scroll the stars layer continuously
        ScrollStars();

        // Move the planets and reset them when they exit the screen
        ScrollPlanet(smallPlanet, smallPlanetSpeed);
        ScrollPlanet(bigPlanet, bigPlanetSpeed);
    }

    private void ScrollStars()
    {
        foreach (Transform tile in starsTiles)
        {
            // Move the stars tile
            tile.position += Vector3.left * starsSpeed * Time.deltaTime;

            // Check if the tile has moved off-screen and reset its position
            if (tile.position.x <= -GetLayerWidth(tile))
            {
                Transform rightmostTile = GetRightmostTile(starsTiles);
                tile.position = new Vector3(rightmostTile.position.x + GetLayerWidth(rightmostTile), tile.position.y, tile.position.z);
            }
        }
    }

    private void ScrollPlanet(Transform planet, float speed)
    {
        // Move the planet
        planet.position += Vector3.left * speed * Time.deltaTime;

        // If the planet exits the left edge of the screen, reset it
        float screenWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        if (planet.position.x <= -screenWidth / 2 - GetLayerWidth(planet) / 2)
        {
            planet.position = new Vector3(screenWidth / 2 + GetLayerWidth(planet) / 2, planet.position.y, planet.position.z);
        }
    }

    private float GetLayerWidth(Transform layer)
    {
        // Get the width of the tile or sprite
        SpriteRenderer spriteRenderer = layer.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            return spriteRenderer.bounds.size.x;
        }
        Debug.LogWarning($"Layer {layer.name} does not have a SpriteRenderer.");
        return 0f;
    }

    private Transform GetRightmostTile(Transform[] layerTiles)
    {
        // Find the tile farthest to the right
        Transform rightmost = layerTiles[0];
        foreach (Transform tile in layerTiles)
        {
            if (tile.position.x > rightmost.position.x)
            {
                rightmost = tile;
            }
        }
        return rightmost;
    }

}
