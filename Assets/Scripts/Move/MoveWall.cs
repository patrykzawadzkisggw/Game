using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    private Transform target;
    Vector2 initialPosition;
    [SerializeField] private List<GameObject> trampolines;
    private List<Trampoline> trampolineComponents;
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed=0.5f;
    [Header("Sounds")]
    [SerializeField] private AudioClip moveSound;
    public int timeplaysound = 7;
    bool isplaying = false;
    void Start()
    {
        target = GetComponent<Transform>();
        initialPosition = target.position;

        trampolineComponents = new List<Trampoline>();
        foreach (var trampoline in trampolines)
        {
            trampolineComponents.Add(trampoline.GetComponent<Trampoline>());
        }
    }

    void Update()
    {
        bool anyTrampolineOpen = trampolineComponents.Any(t => t?.isOpen ?? false);

        if (anyTrampolineOpen)
        {
            target.position = Vector2.MoveTowards(target.position, new Vector2(initialPosition.x, initialPosition.y + movementDistance), Time.deltaTime * speed);
            if (!isplaying)
            {
                SoundManager.instance.PlaySoundRepeatedly(moveSound, timeplaysound);
                isplaying = true;
            }
        }
        else
        {
            target.position = Vector2.MoveTowards(target.position, initialPosition, Time.deltaTime * 1);
            isplaying = false;
        }
    }
}
