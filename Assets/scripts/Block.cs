using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip[] breakSounds;
    //cached reference
    Level level;
    GameSession status;

    private void Start()
    {
        level= FindObjectOfType<Level>();
        level.CountBreakableBlocks();

        status = FindObjectOfType<GameSession>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        destroyBlock();
    }

    private void destroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSounds[UnityEngine.Random.Range(0, breakSounds.Length)], Camera.main.transform.position);
        level.BlockDestroyed();
        status.addToScore();
        Destroy(gameObject);
    }
}
