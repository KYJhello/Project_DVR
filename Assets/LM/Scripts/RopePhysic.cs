using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM
{
    public class RopePhysic : MonoBehaviour
    {
        public bool increaseRope;
        public bool decreaseRope;

        public float gravMulty;

        public float fishinfHookArea = 0.5f;

        public LineRenderer lineRenderer;

        public int segmentCount = 15;
        public float segmentLength = 0.2f;
        public float ropeWidth = 0.01f;
        private int constraintLoop = 20;
        private float castRadius;
        private bool onGround;

        public Transform startPos;

        private List<Segment> segments = new List<Segment>();

        private void IncreaseRope()
        {
            if (increaseRope)
            {
                segmentCount++;
                constraintLoop++;
                segments.Add(new Segment(segments[segmentCount - 2].position - new Vector3(0, segmentLength)));
                increaseRope = false;
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
            }
        }

        private void Reset()
        {
            TryGetComponent(out lineRenderer);
        }

        private void Awake()
        {
            constraintLoop = segmentCount;
            castRadius = ropeWidth * 0.5f;
            Vector3 segmentPos = startPos.position;
            for (int i = 0; i < segmentCount; i++)
            {
                segments.Add(new Segment(segmentPos));
                segmentPos.y -= segmentLength;
            }
        }
        private void Update()
        {
            IncreaseRope();
            DecreaseRope();
        }

        private void FixedUpdate()
        {
            UpdateSegments();
            for (int i = 0; i < constraintLoop; i++)
            {
                ApplyConstraint();
            }

            DrawRope();
            EndSegmentFishingCheck();
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
                if (i == segments.Count - 1)
                {
                    if (onGround)
                        segments[i].position = Physics.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime * gravMulty;
                    else
                        segments[i].position += Physics.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime * gravMulty;
                }
                else
                    segments[i].position += Physics.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
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
        }

        private void EndSegmentFishingCheck()
        {
            RaycastHit[] hits = Physics.SphereCastAll(segments[segments.Count - 1].position, fishinfHookArea, Vector3.up, 0);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.layer == 0)
                    onGround = true;
            }
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