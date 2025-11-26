using UnityEngine;


public class GameManager : MonoBehaviour
{
public static GameManager Instance { get; private set; }


void Awake()
{
if (Instance == null) Instance = this; else Destroy(gameObject);
}


public void OnShipDestroyed(GameObject ship)
{
// Hook for scoring, spawning, or mission fail/win
}
}