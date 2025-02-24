using UnityEngine;

public class InputHandling : MonoBehaviour
{
    private Camera cam;
    private LayerMask cardLayer;
    void Start()
    {
        cardLayer = LayerMask.GetMask("Card");
        cam = Camera.main;
        Managers.EventManager.Instance.OnClick += OnClick;
    }

    void OnClick(Vector2 pos)
    {
        var ray = cam.ScreenPointToRay(pos);
        if(Physics.Raycast(ray, out var hit, 100f,cardLayer))
        {
            if(hit.collider.TryGetComponent(out Card card))
            {
                Managers.EventManager.Instance.ONOnCardClick(card);
            }
        }
    }
}
