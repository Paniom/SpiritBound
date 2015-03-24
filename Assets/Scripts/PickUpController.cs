using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickUpController : MonoBehaviour
{

    GameObject player;
    string playerState;

    GameObject ScorePickup;
    GameObject GemPickup;
    GameObject CoinPickup;
    GameObject WolfPowerPickup;
    GameObject FoxPowerPickup;

    public bool foxCanSee;
    public bool wolfCanSee;
    public bool muskaloCanSee;

    public enum PowerUpType { Score, Gem, Coin, WolfPower, FoxPower };
    public PowerUpType ThisPowerUpType;

    AudioClip pickupSound;

    // Use this for initialization
    void Start()
    {
        pickupSound = Resources.Load<AudioClip>("Sounds/pickup");
        player = GameObject.FindGameObjectWithTag("Player");
        ScorePickup = transform.Find("ScorePickup").gameObject;
        GemPickup = transform.Find("GemPickup").gameObject;
        CoinPickup = transform.Find("CoinPickup").gameObject;
        WolfPowerPickup = transform.Find("WolfPowerPickup").gameObject;
        FoxPowerPickup = transform.Find("FoxPowerPickup").gameObject;
        switch (ThisPowerUpType)
        {
            case PowerUpType.Coin:
                {
                    tag = "Coin";
                    ScorePickup.SetActive(true);
                    GemPickup.SetActive(false);
                    CoinPickup.SetActive(false);
                    WolfPowerPickup.SetActive(false);
                    FoxPowerPickup.SetActive(false);

                    break;
                }
            case PowerUpType.Gem:
                {
                    tag = "Gem";
                    GemPickup.SetActive(true);
                    ScorePickup.SetActive(false);
                    CoinPickup.SetActive(false);
                    WolfPowerPickup.SetActive(false);
                    FoxPowerPickup.SetActive(false);
                    break;
                }
            case PowerUpType.Score:
                {
                    CoinPickup.SetActive(true);
                    GemPickup.SetActive(false);
                    ScorePickup.SetActive(false);
                    WolfPowerPickup.SetActive(false);
                    FoxPowerPickup.SetActive(false);
                    break;
                }
            case PowerUpType.FoxPower:
                {
                    WolfPowerPickup.SetActive(true);
                    GemPickup.SetActive(false);
                    CoinPickup.SetActive(false);
                    ScorePickup.SetActive(false);
                    FoxPowerPickup.SetActive(false);
                    break;
                }
            case PowerUpType.WolfPower:
                {
                    FoxPowerPickup.SetActive(true);
                    GemPickup.SetActive(false);
                    CoinPickup.SetActive(false);
                    WolfPowerPickup.SetActive(false);
                    ScorePickup.SetActive(false);
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != player.GetComponent<PlayerController>().stateMachine.getState())
        {
            playerState = player.GetComponent<PlayerController>().stateMachine.getState();

            switch (playerState)
            {
                case "Muskalo":
                    if (muskaloCanSee)
                    {
                        collider.enabled = true;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            if (rend.material.HasProperty("_Color"))
                            {
                                Color m = rend.material.color;
                                m.a = 1f;
                                rend.material.color = m;
                            }
                        }
                    }
                    else
                    {
                        collider.enabled = false;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            if (rend.material.HasProperty("_Color"))
                            {
                                Color m = rend.material.color;
                                m.a = 0.15f;
                                rend.material.color = m;
                            }
                        }
                    }
                    break;
                case "Fox":
                    if (foxCanSee)
                    {
                        collider.enabled = true;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            if (rend.material.HasProperty("_Color"))
                            {
                                Color m = rend.material.color;
                                m.a = 1f;
                                rend.material.color = m;
                            }
                        }
                    }
                    else
                    {
                        collider.enabled = false;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            if (rend.material.HasProperty("_Color"))
                            {
                                Color m = rend.material.color;
                                m.a = 0.15f;
                                rend.material.color = m;
                            }
                        }
                    }
                    break;
                case "Wolf":
                    if (wolfCanSee)
                    {
                        collider.enabled = true;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            if (rend.material.HasProperty("_Color"))
                            {
                                Color m = rend.material.color;
                                m.a = 1f;
                                rend.material.color = m;
                            }
                        }
                    }
                    else
                    {
                        collider.enabled = false;
                        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
                        {
                            if (rend.material.HasProperty("_Color"))
                            {
                                Color m = rend.material.color;
                                m.a = 0.15f;
                                rend.material.color = m;
                            }
                        }
                    }
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("PickUp", ThisPowerUpType.ToString(), SendMessageOptions.DontRequireReceiver);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);
        }
    }
}
