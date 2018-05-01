using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	[Command]
	public void CmdShoot(float attackPower, float damageBonus) {
		GameObject bulletCopy = (GameObject)Instantiate(GC.gc.bullet_small, transform);
		Transform _shootPoint = GetComponent<Player>().shootPoint.transform;
		bulletCopy.GetComponent<Rigidbody>().AddForce(_shootPoint.forward * attackPower * 70f);
		
		NetworkServer.Spawn(bulletCopy);
		bulletCopy.transform.parent = null;
		bulletCopy.transform.position = _shootPoint.transform.position;
		bulletCopy.GetComponent<Bullet>().shooter = this.gameObject;
	}

	[Command]
	public void CmdShoot42(float attackPower, float damageBonus) {
		GameObject bulletCopy = (GameObject)Instantiate(GC.gc.bullet_big, transform);
		Transform _shootPoint = GetComponent<Player>().shootPoint.transform;
		bulletCopy.GetComponent<Rigidbody>().AddForce(_shootPoint.forward * attackPower * 70f);
		
		NetworkServer.Spawn(bulletCopy);
		bulletCopy.transform.parent = null;
		bulletCopy.transform.position = _shootPoint.transform.position;
		bulletCopy.GetComponent<Bullet>().shooter = this.gameObject;
	}

	public void LocalShoot(float attackPower){
		GameObject _bulletLocalCopy = Instantiate(GC.gc.bullet_small_local, transform);
		Transform _shootPoint = GetComponent<Player>().shootPoint.transform;
		_bulletLocalCopy.GetComponent<Rigidbody>().AddForce(_shootPoint.forward * attackPower * 70f);
		_bulletLocalCopy.transform.position = _shootPoint.transform.position;
	}

	public void LocalShoot42(float attackPower){
		GameObject _bulletLocalCopy = Instantiate(GC.gc.bullet_big_local, transform);
		Transform _shootPoint = GetComponent<Player>().shootPoint.transform;
		_bulletLocalCopy.GetComponent<Rigidbody>().AddForce(_shootPoint.forward * attackPower * 70f);
		_bulletLocalCopy.transform.position = _shootPoint.transform.position;
	}
}
