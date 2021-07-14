using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //Configuration params
    [SerializeField] AudioClip[] breakSounds;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    
    //cached reference
    Level level;
    GameSession status;

    //state variable
    [SerializeField] int timesHit;//only serialized for debug
    private void Start()
    {
        CountBreakableBlocks();

        status = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (CompareTag("Breakable"))
        {
            level.CountBlocks();
        }
        
    }

    private void showNextSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log(("Block sprite is missing from array" + gameObject.name));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            destroyBlock();
        }
        else
        {
            showNextSprite();
        }
    }

    private void destroyBlock()
    {
        if (gameObject.tag == "Breakable")
        {
            PlayBlockDestroySFX();
            level.BlockDestroyed();
            triggerParticleVFX();
            Destroy(gameObject);   
        }
        else if (gameObject.tag == "Unbreakable")
        {
            PlayBlockDestroySFX();
        }
        
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(breakSounds[UnityEngine.Random.Range(0, breakSounds.Length)], Camera.main.transform.position);
        status.addToScore();
    }

    private void triggerParticleVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
