using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMover : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform locationOne;
    public Transform locationTwo;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector2.Distance(locationOne.position, locationTwo.position);
    }

    // Follows the target position like with a spring
    void Update()
    {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers and pingpong the movement.
        transform.position = Vector2.Lerp(locationOne.position, locationTwo.position, Mathf.PingPong(fracJourney, 1));
    }
}
