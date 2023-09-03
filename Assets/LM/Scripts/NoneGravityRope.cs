using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class NoneGravityRope : MonoBehaviour
    {
        public bool increaseRope;
        public bool decreaseRope;
        public bool isEnable;

        public float fishinfHookArea = 0.5f;

        public LineRenderer lineRenderer;

        public int segmentCount = 20;
        public float segmentLength = 0.05f;
        public float ropeWidth = 0.002f;
        private int constraintLoop = 20;
        public float ropeLength;

        public Transform startPos;
        public Transform endPos;

        private List<Segment> segments = new List<Segment>();

        public void RopeOn()
        {
            lineRenderer.enabled = true;
            isEnable = true;
        }
        public void RopeOff()
        {
            lineRenderer.enabled = false;
            isEnable = false;
        }

        private void IncreaseRope()
        {
            if (increaseRope)
            {
                segmentCount++;
                constraintLoop++;
                segments.Add(new Segment(segments[segmentCount - 2].position));
                increaseRope = false;
                ropeLength = segmentLength * segmentCount;
            }
        }
        private void DecreaseRope()
        {
            if (decreaseRope)
            {
                segmentCount--;
                constraintLoop--;
                segments.Remove(segments[segments.Count - 1]);
                decreaseRope = false;
                ropeLength = segmentLength * segmentCount;
            }
        }

        private void Reset()
        {
            TryGetComponent(out lineRenderer);
            segments.Clear();
            Awake();
        }

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            constraintLoop = segmentCount;
            Vector3 segmentPos = startPos.position;
            for (int i = 0; i < segmentCount; i++)
            {
                segments.Add(new Segment(segmentPos));
            }
            ropeLength = segmentLength * segmentCount;
            lineRenderer.enabled = false;
            isEnable = false;
        }
        private void Update()
        {
            if (!isEnable)
                return;
            IncreaseRope();
            DecreaseRope();
        }

        private void FixedUpdate()
        {
            if (!isEnable)
                return;
            UpdateSegments();
            for (int i = 0; i < constraintLoop; i++)
            {
                ApplyConstraint();
            }
            DrawRope();
        }

        private void DrawRope()
        {
            Debug.Log("inDraw");
            lineRenderer.startWidth = ropeWidth;
            lineRenderer.endWidth = ropeWidth;
            Vector3[] segmentPos = new Vector3[segments.Count];
            for (int i = 0; i < segmentPos.Length; i++)
            {
                segmentPos[i] = segments[i].position;

            }
            lineRenderer.positionCount = segmentPos.Length;
            lineRenderer.SetPositions(segmentPos);
        }

        private void UpdateSegments()
        {
            for (int i = 0; i < segments.Count; i++)
            {

                segments[i].velocity = segments[i].position - segments[i].prevPos;
                segments[i].prevPos = segments[i].position;

                segments[i].position += segments[i].velocity;
            }
        }

        private void ApplyConstraint()
        {
            segments[0].position = startPos.position;
            for (int i = 0; i < segments.Count - 1; i++)
            {
                float distance = (segments[i].position - segments[i + 1].position).magnitude;
                float different = segmentLength - distance;
                Vector3 dir = (segments[i + 1].position - segments[i].position).normalized;
                Vector3 movement = dir * different;

                if (i == 0)
                    segments[i + 1].position += movement;
                else
                {
                    segments[i].position -= movement * 0.5f;
                    segments[i + 1].position += movement * 0.5f;
                }
            }
            segments[segments.Count - 1].position = endPos.position;
        }

        public class Segment
        {
            public Vector3 prevPos;
            public Vector3 position;
            public Vector3 velocity;

            public Segment(Vector3 position)
            {
                prevPos = position;
                this.position = position;
                velocity = Vector3.zero;
            }
        }
    }
}