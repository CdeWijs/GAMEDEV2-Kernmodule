using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour
{
    public GameObject projectilePrefab;
    public float range = 200f;

    [SerializeField]
    private LayerMask mask;
    private Transform cannonPosition;

    private void Start()
    {
        cannonPosition = transform.GetChild(0);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && isLocalPlayer)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!isServer)
        {
            CmdPlayerShot();
        }
        else
        {
            RpcPlayerShot();
        }
    }

    [Command]
    public void CmdPlayerShot()
    {
        GameObject projectile = Instantiate(projectilePrefab, cannonPosition.position, Quaternion.identity, cannonPosition);
        NetworkServer.Spawn(projectile);
        projectile.transform.position = new Vector3(cannonPosition.position.x, cannonPosition.position.y, cannonPosition.position.z) + cannonPosition.forward * 2;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = cannonPosition.forward * range;
        RpcSwitchTurns();
    }

    [ClientRpc]
    public void RpcSwitchTurns()
    {
        GameManager.SwitchTurns();
    }

    [ClientRpc]
    public void RpcPlayerShot()
    {
        GameObject projectile = Instantiate(projectilePrefab, cannonPosition.position, Quaternion.identity, cannonPosition);
        projectile.transform.position = new Vector3(cannonPosition.position.x, cannonPosition.position.y, cannonPosition.position.z) + cannonPosition.forward * 2;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = cannonPosition.forward * range;
        GameManager.SwitchTurns();
    }
}
