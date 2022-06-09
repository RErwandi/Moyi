using System.ComponentModel;
using CarterGames.Assets.AudioManager;

public partial class SROptions {

	[Category("Forest")]
	public void NightMode()
	{
		ForestBehaviour.Instance.TurnOnNightSky();
	}

	[Category("Music")]
	public void ForestBGM()
	{
		NetworkMusicManager.Instance.Play("Forest BGM", 1);
	}
	
	[Category("Music")]
	public void MoyiStargazing()
	{
		NetworkMusicManager.Instance.Play("Apocalypse - Cigarettes After Sex", volume: 0.6f);
	}
	
	[Category("Music")]
	public void MoyiDancingOnStreet()
	{
		NetworkMusicManager.Instance.Play("Iron & Wine - Flightless Bird American Mouth ");
	}
}