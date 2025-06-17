using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Przeszkoda : MonoBehaviour
{
    public Color MaxHp;
    public Color LowHP;
    public int �ycia;
    public bool IsRigidboody = false;

    public AudioClip Uderzenie;
    AudioSource audio;

    private Material materia�;
    private int MakymalnaIlo��ycia = 4;
    private Rigidbody RB;

    void Start()
    {
        �ycia = Mathf.Clamp(�ycia, 1, MakymalnaIlo��ycia);
        materia� = GetComponent<MeshRenderer>().material;

        materia�.color = Color.Lerp(LowHP, MaxHp, (float)(�ycia - 1) / (float)(MakymalnaIlo��ycia - 1));

        if (IsRigidboody)
        {
            RB = gameObject.AddComponent<Rigidbody>();
            RB.interpolation = RigidbodyInterpolation.Interpolate;
            RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            RB.mass = 0.55f;
            RB.isKinematic = true; // Domy�lnie wy��czamy fizyk�
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Zmniejszamy �ycie
            �ycia--;
            if (�ycia > 0)
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
                // Zmiana stanu isKinematic: w��czamy lub wy��czamy fizyk�
                if (RB.isKinematic)
                {
                    RB.isKinematic = false; // W��czamy fizyk�
                }
                else
                {
                    RB.isKinematic = true; // Wy��czamy fizyk�
                }
            }

            // D�wi�k uderzenia (je�li jest przypisany)
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
        materia�.color = Color.Lerp(LowHP, MaxHp, (float)(�ycia - 1) / (float)(MakymalnaIlo��ycia - 1));
    }

    private void OnValidate()
    {
        materia� = GetComponent<MeshRenderer>().sharedMaterial;
        materia�.color = Color.Lerp(LowHP, MaxHp, (float)(�ycia - 1) / (float)(MakymalnaIlo��ycia - 1));
    }
}
