using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Przeszkoda : MonoBehaviour
{
    public Color MaxHp;
    public Color LowHP;
    public int ¯ycia;
    public bool IsRigidboody = false;

    public AudioClip Uderzenie;
    AudioSource audio;

    private Material materia³;
    private int MakymalnaIloœæ¿ycia = 4;
    private Rigidbody RB;

    void Start()
    {
        ¯ycia = Mathf.Clamp(¯ycia, 1, MakymalnaIloœæ¿ycia);
        materia³ = GetComponent<MeshRenderer>().material;

        materia³.color = Color.Lerp(LowHP, MaxHp, (float)(¯ycia - 1) / (float)(MakymalnaIloœæ¿ycia - 1));

        if (IsRigidboody)
        {
            RB = gameObject.AddComponent<Rigidbody>();
            RB.interpolation = RigidbodyInterpolation.Interpolate;
            RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            RB.mass = 0.55f;
            RB.isKinematic = true; // Domyœlnie wy³¹czamy fizykê
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Zmniejszamy ¿ycie
            ¯ycia--;
            if (¯ycia > 0)
            {
                SetColor();
            }
            else
            {
                Destroy(this.gameObject);
            }

            // Zmieniamy stan Rigidbody
            if (RB != null)
            {
                // Zmiana stanu isKinematic: w³¹czamy lub wy³¹czamy fizykê
                if (RB.isKinematic)
                {
                    RB.isKinematic = false; // W³¹czamy fizykê
                }
                else
                {
                    RB.isKinematic = true; // Wy³¹czamy fizykê
                }
            }

            // DŸwiêk uderzenia (jeœli jest przypisany)
            if (Uderzenie != null)
            {
                audio = GetComponent<AudioSource>();
                if (audio != null)
                {
                    audio.PlayOneShot(Uderzenie);
                }
            }
        }
    }

    void SetColor()
    {
        materia³.color = Color.Lerp(LowHP, MaxHp, (float)(¯ycia - 1) / (float)(MakymalnaIloœæ¿ycia - 1));
    }

    private void OnValidate()
    {
        materia³ = GetComponent<MeshRenderer>().sharedMaterial;
        materia³.color = Color.Lerp(LowHP, MaxHp, (float)(¯ycia - 1) / (float)(MakymalnaIloœæ¿ycia - 1));
    }
}
