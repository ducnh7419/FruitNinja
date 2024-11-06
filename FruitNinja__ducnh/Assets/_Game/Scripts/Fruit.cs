using System.Collections;
using System.Collections.Generic;
using GlobalEnum;
using UnityEngine;

public class Fruit : GameUnit
{
    public int ID;
    private EDirect eDirect;
    private bool isSliced;
    [SerializeField] private FruitJuice fruitJuicePrefab;
    [SerializeField] private Collider fruitCollider;
    [SerializeField] private GameObject wholeFruit;
    [SerializeField] private GameObject slicedFruitHorizontal;
    [SerializeField] private GameObject slicedFruitVertical;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ParticleSystem juiceEffect;
    private FruitJuice fruitJuice;

    private void Start()
    {
    }

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void Reset()
    {
        TF.up = Vector3.up;
        rb.velocity = Vector3.zero;
        if (eDirect == EDirect.Horizontal)
        {
            eDirect = EDirect.None;
            foreach (Transform child in slicedFruitHorizontal.transform)
            {
                child.localPosition = Vector3.zero;
            }
            slicedFruitHorizontal.transform.localPosition = Vector3.zero;
            slicedFruitHorizontal.SetActive(false);
        }
        else if (eDirect == EDirect.Vertical)
        {
            eDirect = EDirect.None;
            foreach (Transform child in slicedFruitVertical.transform)
            {
                child.localPosition = Vector3.zero;
            }
            slicedFruitVertical.transform.localPosition = Vector3.zero;
            slicedFruitVertical.SetActive(false);
        }
        rb.angularVelocity = Vector3.zero;
        wholeFruit.SetActive(true);
        fruitCollider.enabled = true;

    }

    public void OnDespawn()
    {
        Reset();
        if (fruitJuice != null)
        {
            fruitJuice.OnDespawn();
        }
        SimplePool.Despawn(this);

    }

    public void Slice(Vector3 direction, Vector3 pos, float force)
    {
        wholeFruit.SetActive(false);

        fruitCollider.enabled = false;
        // float angle=Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        // slicedFruit.transform.rotation=Quaternion.Euler(0f,angle,0f);
        eDirect = GetEDirect(direction);
        Rigidbody[] slices;
        Debug.Log(eDirect);
        if (eDirect == EDirect.Horizontal)
        {
            if (slicedFruitHorizontal != null)
            {
                slicedFruitHorizontal.SetActive(true);
                slices = slicedFruitHorizontal.GetComponentsInChildren<Rigidbody>();
            }
            else
            {
                slicedFruitVertical.SetActive(true);
                slices = slicedFruitVertical.GetComponentsInChildren<Rigidbody>();
                eDirect = EDirect.Vertical;
            }
        }
        else
        {
            if (slicedFruitVertical != null)
            {
                slicedFruitVertical.SetActive(true);
                slices = slicedFruitVertical.GetComponentsInChildren<Rigidbody>();
            }
            else
            {
                slicedFruitHorizontal.SetActive(true);
                slices = slicedFruitHorizontal.GetComponentsInChildren<Rigidbody>();
                eDirect = EDirect.Horizontal;
            }
        }

        for (int i = 0; i < slices.Length; i++)
        {
            slices[i].AddForceAtPosition(direction * force, pos, ForceMode.Impulse);
        }
        juiceEffect.Play();
        ShowFruitJuice();
    }

    public void DisableCollider()
    {
        fruitCollider.enabled = false;
    }

    public void EnableCollider()
    {
        fruitCollider.enabled = true;
    }

    private EDirect GetEDirect(Vector3 direction)
    {
        direction=direction.normalized;
        float verticalAngle = Vector2.Angle(direction, Vector2.up);
        float horizontalAngle = Vector2.Angle(direction, Vector2.right);
        if (verticalAngle < 45f)
        {
            return EDirect.Vertical;
        }
        if (horizontalAngle < 45f)
        {
            return EDirect.Horizontal;
        }
        return EDirect.None;
    }

    private void ShowFruitJuice()
    {
        if(fruitJuicePrefab==null) return;
        fruitJuice = SimplePool.Spawn<FruitJuice>(fruitJuicePrefab, TF.position, Quaternion.identity);
        fruitJuice.OnInit(fruitJuicePrefab.spriteRenderer);
    }

    public void MultipleSlice(Vector3 direction, Vector3 pos, float force){
        if(isSliced) return;
        
    }

}
