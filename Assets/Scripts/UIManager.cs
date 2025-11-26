using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
public Text healthText;
ShipController player;


void Start()
{
GameObject p = GameObject.FindGameObjectWithTag("Player");
if (p) player = p.GetComponent<ShipController>();
}


void Update()
{
if (player != null && healthText != null)
{
healthText.text = $"HP: {Mathf.CeilToInt(player.health)} / {Mathf.CeilToInt(player.maxHealth)}";
}
}
}