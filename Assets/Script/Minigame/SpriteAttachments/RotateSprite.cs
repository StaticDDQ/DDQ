using UnityEngine;

public class RotateSprite : SpriteAction {

    [SerializeField] private float speed;

    private void Update() {
        if(canDo)
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
