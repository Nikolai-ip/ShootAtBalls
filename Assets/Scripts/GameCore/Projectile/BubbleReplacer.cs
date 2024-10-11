using System;
using System.Collections;
using UnityEngine;

namespace GameCore.Projectile
{
    internal class BubbleReplacer:MonoBehaviour
    {
        private Transform _tr;
        [SerializeField] private float _replaceTime;
        private void Awake()
        {
            _tr = GetComponent<Transform>();
        }

        public void ReplaceBubble(Bubble replaceable)
        {
            var target = replaceable.GetComponent<Transform>().position;
            replaceable.gameObject.SetActive(false);
            StartCoroutine(Replace(target));
        }

        private IEnumerator Replace(Vector2 target)
        {
            float time = 0;
            Vector2 startPos = _tr.position;
            while (time < _replaceTime)
            {
                time += Time.deltaTime;
                Vector2 newPos = Vector2.Lerp(startPos, target, time / _replaceTime);
                _tr.position = newPos;
                yield return null;
            }
        }
    }
}