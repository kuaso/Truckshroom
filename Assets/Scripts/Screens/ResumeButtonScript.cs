using UnityEngine;
public class ResumeButtonScript : MonoBehaviour
{
    public void DestroyMenu() => Destroy(transform.root.gameObject);
}