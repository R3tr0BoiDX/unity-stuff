#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(SimpleAnimator))]
public class SimpleAnimatorEditor : Editor {
	private bool animate;
	private int i;
	private float lastEditorUpdate;
	private float localTime;
	private Sprite baseSprite;
	private SimpleAnimator anim;
	private SpriteRenderer rend;
	private Image image;

	private float resetDelay;
	private bool delay;

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		anim = (SimpleAnimator)target;
		if (anim.useGUIMode) {
			image = ((MonoBehaviour)this.target).gameObject.GetComponent<Image>();
		} else {
			rend = ((MonoBehaviour)this.target).gameObject.GetComponent<SpriteRenderer>();
		}

		Rect rect = EditorGUILayout.GetControlRect(false, 1);
		EditorGUI.DrawRect(rect, Color.grey);

		GUIContent content = new GUIContent("Reset Delay", "Time until the animation resets");
		resetDelay = EditorGUILayout.FloatField(content, resetDelay, EditorStyles.numberField);

		if (GUILayout.Button("Animate")) {
			if (anim.useGUIMode) {
				baseSprite = image.sprite;
			} else {
				baseSprite = rend.sprite;
			}
			animate = true;
		}
	}

	protected virtual void OnEditorUpdate() {
		float deltaTime = Time.realtimeSinceStartup - lastEditorUpdate;
		localTime += deltaTime;
		if (animate && localTime > anim.TimeBetweenFrames) {
			localTime = 0;
			SetSprite(anim.Frames[i]);
			i++;
			if (i >= anim.Frames.Length) {
				i = 0;
				delay = true;
				animate = false;
			}
		} else if (delay) {
			if (localTime > resetDelay) {
				SetSprite(baseSprite);
				delay = false;
			}
		}
		lastEditorUpdate = Time.realtimeSinceStartup;
	}

	private void SetSprite(Sprite _sprite) {
		if (anim.useGUIMode) {
			image.sprite = _sprite;
		} else {
			rend.sprite = _sprite;
		}
	}

	protected virtual void OnEnable() {
		EditorApplication.update += OnEditorUpdate;
	}

	protected virtual void OnDisable() {
		EditorApplication.update -= OnEditorUpdate;
	}
}

#endif
