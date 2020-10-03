using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour
{
	public string ncase = "http://ncase.me/covid-19/";
	public string who = "http://www.who.int/";
	public string yt = "http://www.youtube.com/adityansah/";
	public string twtr = "http://twitter.com/aditya_n_sah";
	public string itch = "http://adisking1.itch.io/";

	public void OpenNcase()
	{
		#if !UNITY_EDITOR
					openWindow(ncase);
		#endif
	}
	public void OpenWHO()
	{
		#if !UNITY_EDITOR
					openWindow(who);
		#endif
	}

	public void OpenYoutube()
	{
		#if !UNITY_EDITOR
					openWindow(yt);
		#endif
	}
	public void OpenTwitter()
	{
		#if !UNITY_EDITOR
					openWindow(twtr);
		#endif
	}
	public void OpenItch()
	{
		#if !UNITY_EDITOR
					openWindow(itch);
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}