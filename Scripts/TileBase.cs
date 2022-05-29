using UnityEngine;

public class TileBase : Tile
{
    // onlyOne = 0, rightOne = 1, leftOne = 2, both = 3
    public bool existLeft    = false;
    public bool existRight   = false;

    [SerializeField]
    private Sprite[] sprites;

    private void Update()
    {
        int whatType = existLeft == false && existRight == false ?
                        0 : existLeft && existRight ?
                        3 : existLeft ?
                        2 : 1 ;

        this.GetComponent<SpriteRenderer>().sprite = sprites[whatType] ;
    }

    public override void Collision(CollisionDirection direction)
    {
        if ( direction == CollisionDirection.Down )
        {
            movement2D.JumpTo();
        }
    }
}
