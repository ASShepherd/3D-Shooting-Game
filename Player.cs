using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 4.0f;
    private CharacterController _charControl;
    private float _playerGrav = -9.81f;
    [SerializeField]
    private GameObject _muzzleFlash, _hitMarkerPrefab, _hitMarkerContainer, _weapon;
    [SerializeField]
    private AudioSource _weaponSound;
    [SerializeField]
    private int _currentAmmo, _maxAmmo = 150;
    private bool _isReloading = false;
    private UIManager _uiManager;
    private bool _hascoin, _weaponsEnabled;
    // Start is called before the first frame update
    void Start()
    {
        _currentAmmo = _maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("Player Script: UI Manager not found!");
        }
        _uiManager.UpdateAmmo(_currentAmmo);
        Cursor.lockState = CursorLockMode.Locked;
        _charControl = GetComponent<CharacterController>();
        if (_charControl == null)
        {
            Debug.Log("Player Script: Character Controller not attatched!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _currentAmmo  > 0 && _isReloading == false && _weaponsEnabled == true)
        {
            PlayerShoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _weaponSound.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.R) && _isReloading == false)
        {
            StartCoroutine(Reload());
        }
        PlayerMovement();
    }


    void PlayerMovement()
    {
            float HorizontalInput = Input.GetAxis("Horizontal");
            float VerticalInput = Input.GetAxis("Vertical");
            Vector3 playerDirection = new Vector3(HorizontalInput, 0, VerticalInput);
            Vector3 playerVelocity = playerDirection * _playerSpeed;
            playerVelocity.y += _playerGrav;
            playerVelocity = transform.transform.TransformDirection(playerVelocity);
            _charControl.Move(playerVelocity * Time.deltaTime);
    }

    void PlayerShoot()
    {
        _muzzleFlash.SetActive(true);
        _currentAmmo--;
        _uiManager.UpdateAmmo(_currentAmmo);
        if (_weaponSound.isPlaying == false)
        {
            _weaponSound.Play();
        }
        //cast ray from centre of main camera
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
            GameObject currentHit = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            currentHit.transform.SetParent(_hitMarkerContainer.transform);
            Destroy(currentHit, 2.0f);
            if (hitInfo.transform.name == "Wooden_Crate")
            {
                Destructible crate = hitInfo.transform.GetComponent<Destructible>();
                if (crate != null)
                {
                    crate.crateDestroy();
                }
            }
        }
        
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        _uiManager.Reloading();
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = 150;
        _uiManager.UpdateAmmo(_currentAmmo);
        _isReloading = false;
    }

    public void EnableWeapons()
    {
        _weapon.SetActive(true);
        _weaponsEnabled = true;
    }

    public void pickUpCoin()
    {
        _hascoin = true;
    }

    public void removeCoin()
    {
        _hascoin = false;
    }

    public bool PlayerCoin()
    {
        return _hascoin;
    }
}
