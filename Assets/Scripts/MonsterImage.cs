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
	private SpriteRenderer sr;

    void Start()
    {
        actor = GetComponent<Actor>();
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
		sr = GetComponent<SpriteRenderer> ();
    }

    void Update()
    {
        switch ((4 + actor.pos.direction - player.pos.direction) % 4)
        {
            case 0:
                sr.sprite = image1;
                break;
            case 1:
                sr.sprite = image2;
                break;
            case 2:
                sr.sprite = image3;
                break;
            case 3:
                sr.sprite = image4;
                break;
            default:
                break;
        }
    }
}
