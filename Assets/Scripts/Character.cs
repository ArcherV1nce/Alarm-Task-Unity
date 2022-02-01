using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _gold;

    public string Name;

    public void AddGold (int goldAmount)
    {
        _gold += goldAmount;
    }
}
