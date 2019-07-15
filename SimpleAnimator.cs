using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAnimator : MonoBehaviour {
	[SerializeField] private bool GUIMode;
	[SerializeField] private bool startAtSpawn = true;
	[SerializeField] private bool loopAnimation;
	[SerializeField] private bool destroyAfterFinished;
	[SerializeField] private float destroyDelay;
	[SerializeField] private float timeBetweenFrames;
	[SerializeField] private Sprite[] frames;

	private int i;
	private bool play;
	private Coroutine routine;
	private SpriteRenderer rend;
	private Image image;

	public Sprite[] Frames {
		get => frames;
	}

	public float TimeBetweenFrames {
		get => timeBetweenFrames;
	}

	public bool useGUIMode {
		get => GUIMode;
		set => GUIMode = value;
	}

    private void Start() {
		if (GUIMode) {
			image = GetComponent<Image>();
		} else {
			rend = GetComponent<SpriteRenderer>();
		}

		if (startAtSpawn) {
			play = true;
			routine = StartCoroutine(Animate());
		}
	}

	public void Play() {
		if (!play) {
			play = true;
		}
		routine = StartCoroutine(Animate());
	}

	public void Stop() {
		StopCoroutine(routine);
        SetSprite(frames[0]);
    }

    public void Pause() {
		if (play) {
			play = false;
		}
	}

	private IEnumerator Animate() {
		if (play) {
			SetSprite(frames[i]);
			i++;
			yield return new WaitForSeconds(timeBetweenFrames);
			if (i < frames.Length) {
				routine = StartCoroutine(Animate());
			} else if (loopAnimation) {
				i = 0;
				routine = StartCoroutine(Animate());
			} else if (destroyAfterFinished) {
				Destroy(gameObject, destroyDelay);
			}
		}
	}

	private void SetSprite(Sprite _sprite) {
		if (GUIMode) {
			image.sprite = _sprite;
		} else {
			rend.sprite = _sprite;
		}
	}
}