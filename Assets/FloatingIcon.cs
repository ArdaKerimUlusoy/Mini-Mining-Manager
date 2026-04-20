using UnityEngine;

public class FloatingIcon : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float lifeTime = 1f;
    public Vector3 initialScale = new Vector3(0.03f, 0.03f, 0.5f);

    private float timer = 0f;
    private float waveFrequency = 10f; // Zigzag frekansý
    private float waveAmplitude = 0.1f; // Zigzag geniþliði
    private Vector3 moveDirection;

    private SpriteRenderer sr;

    void Start()
    {
        transform.position = startPosition;
        transform.localScale = initialScale;
        moveDirection = endPosition - startPosition;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifeTime;

        // Ana pozisyon hareketi (lineer)
        Vector3 straightMovement = Vector3.Lerp(startPosition, endPosition, t);

        // Zigzag hareketi (saða-sola hafif dalga)
        float offsetX = Mathf.Sin(t * waveFrequency) * waveAmplitude;
        Vector3 offset = new Vector3(offsetX, 0f, 0f);

        transform.position = straightMovement + offset;

        // Saydamlýk animasyonu (fade out)
        if (sr != null)
        {
            Color color = sr.color;
            color.a = Mathf.Lerp(1f, 0f, t);
            sr.color = color;
        }

        // Ölçek küçültme (isteðe baðlý)
        transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
