using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSqueakController : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip door;

    [Range(0, 1)]
    public float volume;
    [Range(0, 0.1f)]
    public float pitch;

    public float Speed;
    public float UpdateDelay;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(SpeedReckoner());
    }

    void Start()
    {
        sound.clip = door;
    }

    // Update is called once per frame
    void Update()
    {
        volume = (Speed * 20) / 100; // only get 20% of speed for volume 
        pitch = (Speed * 20) / 1000; // only get 2% of speed for pitch

        if (Speed >= 0.1f)
        {
            if (!sound.isPlaying)
            {
                sound.Play();
                Debug.Log("Play Sound");
            }
        }
        else
        {
            sound.Stop();
        }
        sound.volume = volume;
        sound.pitch = pitch + 0.9f;
    }
    private IEnumerator SpeedReckoner()
    {

        YieldInstruction timedWait = new WaitForSeconds(UpdateDelay);
        Vector3 lastPosition = transform.position;
        float lastTimestamp = Time.time;

        while (enabled)
        {
            yield return timedWait;

            var deltaPosition = (transform.position - lastPosition).magnitude;
            var deltaTime = Time.time - lastTimestamp;

            if (Mathf.Approximately(deltaPosition, 0f)) // Clean up "near-zero" displacement
                deltaPosition = 0f;

            Speed = deltaPosition / deltaTime;


            lastPosition = transform.position;
            lastTimestamp = Time.time;
        }
    }
}
