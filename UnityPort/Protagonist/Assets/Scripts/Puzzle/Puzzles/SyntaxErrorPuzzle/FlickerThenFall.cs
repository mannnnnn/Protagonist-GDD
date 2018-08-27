using System.Collections.Generic;
using UnityEngine;

public interface FlickerThenFallItem
{
    void SetAlpha(float alpha);
    void Fall(Vector2 velocity);
    Transform transform { get; }
}

/**
 * Causes some list of FlickerThenFallItems to flicker and then fall, just like the letters do in the syntax error puzzle
 * Note that this does not destroy the letters, so make sure that is done.
 */
public class FlickerThenFall : MonoBehaviour
{
    // flash the letter alpha as part of the completion animation
    float singleFlickerTimer = 0f;
    float timeBetweenFlickers = 0.05f;
    // time spent flickering before separating the letters
    float flickerTimer = 0f;
    protected float flickerDuration = 1f;
    // time spent watching the separated letters fall
    bool separated = false;
    float separatedTimer = 0f;
    float separatedDuration = 1f;

    public IEnumerable<FlickerThenFallItem> items;
    public void Initialize(IEnumerable<FlickerThenFallItem> items)
    {
        this.items = items;
    }

    void Update()
    {
        // flash transparency
        singleFlickerTimer += GameTime.deltaTime;
        if (singleFlickerTimer > timeBetweenFlickers)
        {
            singleFlickerTimer = 0f;
            float alpha = Random.Range(0f, 1f);
            foreach (var letter in items)
            {
                letter.SetAlpha(alpha);
            }
        }
        // separate letters
        if (!separated)
        {
            flickerTimer += GameTime.deltaTime;
            if (flickerTimer >= flickerDuration)
            {
                separated = true;
                // fall away from top center of screen
                Vector2 pos = ScreenResolution.MapViewToWorldPoint(new Vector2(0.5f, 1f));
                foreach (var letter in items)
                {
                    Vector2 velocity = ((Vector2)letter.transform.position - pos).normalized * 5f;
                    letter.Fall(velocity);
                }
            }
        }
        // then end the animation
        else
        {
            separatedTimer += GameTime.deltaTime;
            if (separatedTimer >= separatedDuration)
            {
                Destroy(this);
            }
        }
    }
}