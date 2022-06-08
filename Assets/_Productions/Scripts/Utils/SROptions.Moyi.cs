using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public partial class SROptions {

	[Category("Forest")]
	public void NightMode()
	{
		ForestBehaviour.Instance.TurnOnNightSky();
	}
	
}