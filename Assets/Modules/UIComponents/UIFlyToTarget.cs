using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class UIFlyToTarget : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] private RectTransform _target;

        [Header("Fly")]
        [SerializeField] private float _duration = 0.7f;
        [SerializeField] private Ease _flyEase = Ease.InCubic;
        [SerializeField] private float _arcHeight = 150f;
        [SerializeField] private float _arcRandomX = 80f;

        [Header("Scale")]
        [SerializeField] private float _popScale = 1.3f;
        [SerializeField] private float _popDuration = 0.15f;
        [SerializeField] private float _endScale = 0.4f;

        [Header("Target feedback")]
        [SerializeField] private float _targetPunch = 0.15f;
        [SerializeField] private float _targetPunchDuration = 0.25f;
        [SerializeField] private float _holdOnTarget = 0.05f;


        private Sequence _sequence;

        public Tween Fly(RectTransform item)
        {
            if (item == null || _target == null || _sequence != null)
                return null;

            Transform origParent = item.parent;
            Vector3 origLocalPos = item.localPosition;
            Quaternion origLocalRot = item.localRotation;
            Vector3 baseScale = item.localScale;

            float canvasScale = item.lossyScale.x / Mathf.Max(item.localScale.x, 0.0001f);

            Vector3 start = item.position;
            Vector3 end = _target.TransformPoint(_target.rect.center);
            end.z = start.z;

            float offsetX = UnityEngine.Random.Range(-_arcRandomX, _arcRandomX);
            Vector3 arcOffset = new Vector3(offsetX, _arcHeight, 0f) * canvasScale;
            Vector3 control = (start + end) * 0.5f + arcOffset;

            _sequence = DOTween.Sequence();

            _sequence.Append(item.DOScale(baseScale * _popScale, _popDuration).SetEase(Ease.OutBack));

            _sequence.Append(DOVirtual.Float(0f, 1f, _duration,
                    t => item.position = Bezier(start, control, end, t))
                .SetEase(_flyEase));
            _sequence.Join(item.DOScale(baseScale * _endScale, _duration).SetEase(Ease.InQuad));

            _sequence.AppendCallback(() =>
            {
                if (_targetPunch > 0f)
                {
                    _target.DOKill(true);
                    _target.DOPunchScale(Vector3.one * _targetPunch, _targetPunchDuration, 6, 0.8f);
                }
            });

            _sequence.OnComplete(() =>
            {
                if (item != null && origParent != null)
                {
                    item.SetParent(origParent, false);
                    item.localPosition = origLocalPos;
                    item.localRotation = origLocalRot;
                    item.localScale = baseScale;
                }
            });

            _sequence.SetLink(item.gameObject);
            _sequence.OnKill(() => _sequence = null);
            return _sequence;
        }

        private static Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            float u = 1f - t;
            return u * u * a + 2f * u * t * b + t * t * c;
        }
    }
}