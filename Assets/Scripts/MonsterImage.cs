using UnityEngine;
using System.Collections;

public class MonsterImage : MonoBehaviour
{
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    private Actor actor;
    private Actor player;

    void Start()
    {
        actor = GetComponent<Actor>();
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
    }

    void Update()
    {
        switch ((4 + actor.pos.direction - player.pos.direction) % 4)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = image1;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = image2;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = image3;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = image4;
                break;
            default:
                break;
        }
    }
}
