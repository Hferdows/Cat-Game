using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private RectTransform bar;
    private Image barImage;
    public GameManager gameManager;

    public bool isDead; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bar = GetComponent<RectTransform>();
        barImage = GetComponent<Image>();
        Health.totalHealth = 1f;
    }

    public void Damage(float damage)
    {
        //make sure health doesnt go less than 0
        if((Health.totalHealth -= damage) > 0.1f)
        {
            Health.totalHealth -= damage;
        }
        //else player is dead, load game over scene 
        else
        {
            isDead = true;
            gameObject.SetActive(false);
            Health.totalHealth = 0f;
            gameManager.gameOver();
        }

        //when healthis below 30%, change the health bar to red 
        if(Health.totalHealth < 0.3f) 
        {
            barImage.color = Color.red;
        }

        SetSize(Health.totalHealth);
    }

    public void SetSize(float size)
    {
        //update the scale of the bar on the x axis when health decreases 
        bar.localScale = new Vector3(size, 1f);
    }
}
