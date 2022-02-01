using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Chest : InteractiveObject
{
    [SerializeField] private Sprite _closedSprite;
    [SerializeField] private Sprite _openedSprite;
    [SerializeField] private int _gold;
    [SerializeField] private bool _isOpened;

    private SpriteRenderer _spriteRenderer;
    
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.GetComponent<Character>())
        {
            Interact(collider2D.gameObject);
        }
    }

    protected override void Interact(GameObject gameObject)
    {
        if (_isOpened == false)
        {
            gameObject.GetComponent<Character>().AddGold(_gold);
            _gold = 0;
            _isOpened = true;
            ChangeSpriteToOpened();
        }

        else
        {
            _isOpened = false;
            ChangeSpriteToClosed();
        }
    }

    private void OnValidate()
    {
        if (_gold < 0)
            _gold = 0;
    }

    private void ChangeSpriteToOpened()
    {
        _spriteRenderer.sprite = _openedSprite;
    }    
    
    private void ChangeSpriteToClosed()
    {
        _spriteRenderer.sprite = _closedSprite;
    }
}
