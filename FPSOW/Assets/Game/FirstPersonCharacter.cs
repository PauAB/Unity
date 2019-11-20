using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCharacter : MonoBehaviour,IEntity
{
    [SerializeField]
    PlayerData data;
    Ability[] abilities;
    float reloadingTime;
    bool reloading;
    float actualBullets;
    float totalBullets;
    float shootingCooldown;
    bool shooting;
    float shootingTimer;
    Projectile projectile;

    const int numAbilities = 3;
    [SerializeField]
    public CharacterMove move;

    Reload reloadCmd;
    Shoot shootCmd;

    void setCharacter()
    {
        abilities = new Ability[numAbilities];
        for(int i = 0; i< numAbilities; i++)
        {
            abilities[i] = data.abilities[i].GetComponent<Ability>();
            abilities[i].InitAbility(this);
            abilities[i].EAwake();
        }
        reloadingTime = data.reloadingTime;
        reloading = false;
        actualBullets = data.totalBullets;
        totalBullets = data.totalBullets;
        shootingCooldown = data.shootingCooldown;
        shooting = false;
        shootingTimer = 0.0f;

       
        InputManager.SetInputs("useAbility_1", abilities[0]);
        InputManager.SetInputs("useAbility_2", abilities[1]);
        InputManager.SetInputs("useAbility_3", abilities[2]);
        InputManager.SetInputs("Reload", reloadCmd);
        //InputManager.SetInputs("Fire1", shootCmd);
    }

    public void EAwake()
    {
        reloadCmd = new Reload();
        shootCmd = new Shoot();

        setCharacter();
       
        reloadCmd.SetReload(reloadCR());
        shootCmd.SetShoot(shootCR());
    }

    public void EUpdate(float delta)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            actualBullets = 0;
            Debug.Log(actualBullets);
        }
    }

    IEnumerator shootCR()
    {
        while(true)
        {
            yield return null;
        }
    }

    IEnumerator reloadCR()
    {      
        while (true)
        {
            if (Input.GetButtonDown("Reload"))
            {
                reloading = true;

                yield return new WaitForSeconds(reloadingTime);

                actualBullets = totalBullets;

                Debug.Log(actualBullets);
            }

            reloading = false;

            Debug.Log("reloadCR");

            yield return null;
        }
    }

}
