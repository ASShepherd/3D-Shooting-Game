using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoCount;
    [SerializeField]
    private GameObject _coinImage;
    public void UpdateAmmo(int AmmoCount)
    {
        _ammoCount.text = "Ammo : " + AmmoCount;
    }

    public void Reloading()
    {
        _ammoCount.text = "Reloading...";
    }
    public void CollectedCoin()
    {
        _coinImage.SetActive(true);
    }

    public void UsedCoin()
    {
        _coinImage.SetActive(false);
    }
}
