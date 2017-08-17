using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour
{
    [SerializeField] string theURL;

    public void LinkToResourceURL()
    {
        Application.OpenURL(theURL);
    }
	
}
