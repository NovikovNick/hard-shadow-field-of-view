using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.U2D.Animation;

public class PlayerDamageResolver : MonoBehaviour
{
    [SerializeField] private SpriteResolver head;

    void Start()
    {
        Assert.IsNotNull(head);
    }

    public void OnHealthChanged(float amount)
    {
        if(amount < 75)
        {
            head.SetCategoryAndLabel("Head", "head75");
        }
        // head.SetCategoryAndLabel("Head", "head100");
    }
}
