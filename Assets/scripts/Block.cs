using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip[] breakSounds;
    //cached reference
    Level level;

    private void Start()
    {
        level= FindObjectOfType<Level>();
        level.CountBreakableBlocks();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        destroyBlock();
    }

    private void destroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSounds[UnityEngine.Random.Range(0, breakSounds.Length)], Camera.main.transform.position);
        level.BlockDestroyed();
        Destroy(gameObject);
    }
}
