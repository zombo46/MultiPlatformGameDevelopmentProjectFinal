using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerVitality : MonoBehaviour
{
    public float maxOxygen = 100;
    public float currentOxygen;
    public Slider oxygenBar;
    public Text oxygenText;
    private bool isDead = false;

    public int PlayerMaxHealth = 3;
    public int PlayerCurrentHealth;

    public UnityEvent playerRevived = new UnityEvent();

    public PlayerInventory playerInventory;
    
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // When true, oxygen does not tick down.
    private bool oxygenPaused = false;


    void Start()
    {
        currentOxygen = maxOxygen;
        PlayerCurrentHealth = PlayerMaxHealth;
        updateUI();

        playerInventory = transform.gameObject.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (isDead)
            return;

        if (!oxygenPaused)
        {
            currentOxygen -= Time.deltaTime;
            if (currentOxygen <= 0)
            {
                currentOxygen = 0;
                PlayerDeath();
            }
        }

        if (PlayerCurrentHealth <= 0)
        {
            PlayerCurrentHealth = 0;
            PlayerDeath();
        }

        updateUI();
    }

    public void ReduceHealth(float Amount)
    {
        if (isDead) {
            return;
        } else {
            int damage = Mathf.Max(1, Mathf.RoundToInt(Amount));
            PlayerCurrentHealth = Mathf.Clamp(PlayerCurrentHealth - damage, 0, PlayerMaxHealth);
            updateUI();
            if (PlayerCurrentHealth <= 0) {
                PlayerDeath();
            }
        }
    }

    public bool IsDead() => isDead;

    public void revive()
    {
        isDead = false;
        currentOxygen = maxOxygen;
        PlayerCurrentHealth = PlayerMaxHealth;
        updateUI();
        GetComponent<PlayerMovement>().enabled = false;
        transform.position = new Vector3(500f, 11f, 500f);
        GetComponent<PlayerMovement>().enabled = true;

        Debug.Log(transform.position);
        playerRevived.Invoke();
    }

    void updateUI()
    {
        if (oxygenBar != null)
        {
            if (oxygenBar.maxValue > 1f)
                oxygenBar.value = Mathf.Clamp(currentOxygen, oxygenBar.minValue, oxygenBar.maxValue);
            else
                oxygenBar.value = Mathf.Clamp01(currentOxygen / maxOxygen);
        }

        if (oxygenText != null)
            oxygenText.text = $"Oxygen level: {Mathf.Ceil(currentOxygen)}%";

        if (hearts != null && hearts.Length > 0 && fullHeart != null && emptyHeart != null) {
            int heartCount = hearts.Length;
            for (int i = 0; i < heartCount; i++) {
                if (i < PlayerCurrentHealth) {
                    hearts[i].sprite = fullHeart;
                } else {
                    hearts[i].sprite = emptyHeart;
                }
            }
        }
    }

    void PlayerDeath()
    {
        isDead = true;
        this.SendMessage("ShowDeath");
    }

    // Public to pause or unpause oxygen depletion in other scripts.
    public void SetOxygenPaused(bool paused)
    {
        oxygenPaused = paused;
    }

    public bool IsOxygenPaused()
    {
        return oxygenPaused;
    }
}
