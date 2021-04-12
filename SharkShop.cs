using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    //check collision with player, if player has coin, then sell player gun and take coin
    private void OnTriggerStay(Collider otherObj)
    {
        if (otherObj.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player _player = otherObj.GetComponent<Player>();
                if (_player != null)
                {
                    if (_player.PlayerCoin() == true)
                    {
                        _player.removeCoin();
                        UIManager _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                        if (_uiManager != null)
                        {
                            _uiManager.UsedCoin();
                        }
                        GetComponent<AudioSource>().Play();
                        _player.EnableWeapons();
                    }
                    else
                    {
                        Debug.Log("The shark shakes his head, you have no coin silly!");
                    }
                }
            }
        }
    }
}
