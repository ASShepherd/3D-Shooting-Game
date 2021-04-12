using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private bool _playerInTrigger = false;
    private Player _player;
    [SerializeField]
    private AudioClip _coinSound;
    private UIManager _uiManager;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Coin Script: Player was not attatched");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("Coin Script: The UI manager was not found");
        }
    }
    private void OnTriggerStay(Collider otherObj)
    {
        if (otherObj.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                _player.pickUpCoin();
                AudioSource.PlayClipAtPoint(_coinSound, transform.position, 0.15f);
                _uiManager.CollectedCoin();
                Destroy(this.gameObject);
            }
        }
    }



}
