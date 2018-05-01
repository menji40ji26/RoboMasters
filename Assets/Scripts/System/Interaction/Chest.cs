using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	public int ammo17;
	public int ammo42;
	public string type;

	


	public void TransferAmmo(Player _receiver){
		_receiver.ammo += ammo17;
		_receiver.ammo42 += ammo42;
		Destroy(this.gameObject);
	}

}
