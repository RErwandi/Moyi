using System.ComponentModel;
using CarterGames.Assets.AudioManager;

public partial class SROptions {

	[Category("Forest")]
	public void NightMode()
	{
		ForestBehaviour.Instance.TurnOnNightSky();
	}
	
	[Category("Forest")]
	public void EnableStars1()
	{
		ForestBehaviour.Instance.EnableStars1();
	}
	
	[Category("Forest")]
	public void EnableStars2()
	{
		ForestBehaviour.Instance.EnableStars2();
	}
	
	[Category("Forest")]
	public void EnableStars3()
	{
		ForestBehaviour.Instance.EnableStars3();
	}
	
	[Category("Forest")]
	public void BiggerMoon()
	{
		ForestBehaviour.Instance.BiggerMoon();
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